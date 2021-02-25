using Application.IntegrationEvents;
using Infrastructure.DataHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Infrastructure.UnitTest.DataTests
{
   public class DeserializeKafkaMessageTests
    {


        [Theory]
        [InlineData("{\r\n  \"OrderId\": \"3892d379-6549-4ba9-9419-ecb1c00b5009\",\r\n  \"CustomerName\": \"Emre\",\r\n  \"CustomerLastName\": \"Alper\",\r\n  \"CustomerEmail\": \"emrealper@gmail.com\"\r\n}")]
        public void Should_Correctly_Deserialize_Kafka_Message_To_Received_Order_Paid_Event(string kafkaMessage)
        {

            var deserializedKafkaMessage = new DeserializeKafkaMessage<ReceivedOrderPaidEvent>();

            var expected = new ReceivedOrderPaidEvent
            {
                OrderId = new Guid("3892d379-6549-4ba9-9419-ecb1c00b5009"),
                CustomerName = "Emre",
                CustomerLastName = "Alper",
                CustomerEmail = "emrealper@gmail.com",
               
            };

            var actual = deserializedKafkaMessage.Deserialize(kafkaMessage);


            var expectedStr = JsonConvert.SerializeObject(expected);
            var actualStr = JsonConvert.SerializeObject(actual);
            Assert.Equal(expectedStr, actualStr);
        }

    }
}
