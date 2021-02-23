using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.IntegrationEvents.Events
{
    public class OrderPaidIntegrationEvent
    {

        public Guid OrderId { get; set; }


        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }



        public string CustomerEmail { get; set; }





        public OrderPaidIntegrationEvent(Guid orderId, string customerName, string customerLastName,
            string customerEmail
            )
        {

            OrderId = orderId;
            CustomerName = customerName;
            CustomerLastName = customerLastName;
            CustomerEmail = customerEmail;


        }
    }
}
