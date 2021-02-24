using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.IEventBusService;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmailAndNotificationService
{
    public class EventBusConsumerWorker : BackgroundService
    {
        private readonly ILogger<EventBusConsumerWorker> _logger;
        private readonly IConsumerService _consumerService;

        public EventBusConsumerWorker(ILogger<EventBusConsumerWorker> logger,IConsumerService consumerService)
        {
            _logger = logger;
            _consumerService = consumerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

            await _consumerService.SubscribeServiceBusAsync();



            }
        }
    }
}
