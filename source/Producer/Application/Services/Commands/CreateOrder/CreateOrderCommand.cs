using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Domain.Enums;
using MediatR;

namespace Application.Services.Commands.CreateOrder
{
   public class CreateOrderCommand : IRequest<Guid>
    {

        [DataMember]
        public long CustomerId { get; set; }


        [DataMember]
        public string CustomerName { get; set; }


        [DataMember]
        public string CustomerLastName { get; set; }


        [DataMember]
        public string CustomerEmail { get; set; }

        [DataMember]
        public string DeliveryAddress { get; set; }

        [DataMember]
        public long RestaurantId { get; set; }


        [DataMember]
        public string RestaurantName { get; set; }


        [DataMember]
        public string OrderNote { get; set; }

        [DataMember]
        public PaymentMethodType PaymentMethodType { get; set; }





        [DataMember]
        public List<OrderProductDto> OrderProducts { get; set; }



        public class OrderProductDto
        {
            public decimal UnitCost { get; set; }

            public CurrencyType CurrencyType { get; set; }

            public long ProductId { get; set; }

            public string ProductName { get; set; }

            public int Quantity { get; set; }

        }


    }
}
