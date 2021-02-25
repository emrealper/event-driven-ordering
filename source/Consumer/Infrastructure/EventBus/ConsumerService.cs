using Application.IEventBusService;
using Application.IntegrationEvents;
using Application.Interfaces;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.EventBus
{
    public class ConsumerService : IConsumerService
    {

       
        private string _topic;
        private string _retryTopic;
        private ConsumerConfig consumerConfig;
        private readonly IMediator _mediator;
        private AsyncRetryPolicy _retryPolicy;
        private readonly IProducerService _producureService;

        private readonly IDeserializeKafkaMessage<ReceivedOrderPaidEvent> _deserializeKafkaMessage;

        public ConsumerService(IConfiguration config, IMediator mediator, IProducerService producureService,
                IDeserializeKafkaMessage<ReceivedOrderPaidEvent> deserializeKafkaMessage)
        {


            var kafkaHost = config.GetSection("KafkaConfiguration:Host").Value;
            var kafkaPort = config.GetSection("KafkaConfiguration:Port").Value;
            var groupId = config.GetSection("KafkaConfiguration:GroupId").Value;


            _deserializeKafkaMessage = deserializeKafkaMessage;
            _producureService = producureService;

            _topic = config.GetSection("KafkaConfiguration:Topic").Value;
            _retryTopic = config.GetSection("KafkaConfiguration:RetryTopic").Value;

            consumerConfig = new ConsumerConfig
            {
                BootstrapServers = $"{kafkaHost}:{kafkaPort}",
                GroupId = groupId,
                
                 
            };
            _mediator = mediator;




            //defined retry policy in case of failing
            _retryPolicy = Policy
             .Handle<Exception>()
             .WaitAndRetryAsync(2, retryAttempt => {
                 var timeToWait = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                 Console.WriteLine($"Waiting {timeToWait.TotalSeconds} seconds");
                 return timeToWait;
             }
             );


        }

        public async Task SubscribeServiceBusAsync()
        {

            using (var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build())
            {
                consumer.Subscribe(this._topic);
                var cts = new CancellationTokenSource();
                

                try
                {
                    while (true)
                        try
                        {
                            var consumeResult = consumer.Consume(cts.Token);


                            //try to execute consumed message 
                            if (!await TryConsume(consumeResult, cts))
                            {

                                //if it fails send message to the dead letter queue/retry topic
                                await _producureService.SendEventBusAsync(consumeResult.Message.Value, _retryTopic);
                            }

                            Console.WriteLine(consumeResult.Message.Value);
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                }
                finally
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    consumer.Close();
                }
            }

            

        }



        private async Task<bool> TryConsume(ConsumeResult<Ignore, string> consumeResult, CancellationTokenSource token)
        {

            try
            {
                
                //Deserialize kafka message 
                var receivedEvent = _deserializeKafkaMessage.Deserialize(consumeResult.Message.Value);
                //send notification email using retry policy. After 2 attempt send it dead letter queue
                await _retryPolicy.ExecuteAsync<Unit>(async () => await _mediator.Send(receivedEvent));
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }









    }
}
