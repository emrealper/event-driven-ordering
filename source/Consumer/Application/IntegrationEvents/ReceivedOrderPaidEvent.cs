using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IntegrationEvents
{
    public class ReceivedOrderPaidEvent : IRequest
    {

        public Guid OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }

    }
}
