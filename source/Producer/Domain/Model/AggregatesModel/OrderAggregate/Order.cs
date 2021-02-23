using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AggregatesModel.OrderAggregate
{
   public class Order : IAggregateRoot
    {


        //to use for dummy Id
        public Guid Id { get; private set; }

        // Customer is a Value Object
        public Customer Customer { get; private set; }

        public Restaurant Restaurant { get; private set; }


        public string OrderNote { get; set; }

        public PaymentMethodType PaymentMethodType { get; set; }

        private readonly List<OrderProduct> _orderProducts;
        public IReadOnlyCollection<OrderProduct> OrderProducts => _orderProducts;


        protected Order()
        {
            _orderProducts = new List<OrderProduct>();

        }

        public Order(Customer customer,Restaurant restaurant) : this()
        {
            Customer = customer;
            Restaurant = restaurant;
        }

        public void AddOrderProducts(long productId, string productName, decimal unitCost, CurrencyType currencyType, int quantity)
        {


            var orderProduct = new OrderProduct(productId, productName, unitCost, currencyType, quantity);
            _orderProducts.Add(orderProduct);

        }

        public void SetDummyGreatedAndAssignId()
        {
            Id = Guid.NewGuid();
        }




    }
}
