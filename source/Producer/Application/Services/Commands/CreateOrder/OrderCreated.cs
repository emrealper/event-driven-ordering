
using Application.Interfaces;
using Application.Services.IntegrationEvents.Events;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.Commands.CreateOrder
{
    public class OrderCreated : INotification
    {


        public Guid Id { get; set; }
        public string CustomerName { get; set; }



        public string CustomerLastName { get; set; }



        public string CustomerEmail { get; set; }



        public class OrderCreatedHandler : INotificationHandler<OrderCreated>
        {
           
            private readonly IEventBusService _eventBus;


            public OrderCreatedHandler(IEventBusService eventBus)
            {
                
                _eventBus = eventBus;
            }

            public async Task Handle(OrderCreated notification, CancellationToken cancellationToken)
            {
             

                var eventMessage = new OrderPaidIntegrationEvent(notification.Id,
                    notification.CustomerName, notification.CustomerLastName, 
                    notification.CustomerEmail);

                string data = JsonConvert.SerializeObject(eventMessage, Formatting.Indented);
                await _eventBus.SendEventBusAsync(data,"orderPaid");



            }
        }


    }
}
