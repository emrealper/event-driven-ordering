using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AggregatesModel.OrderAggregate
{
    public class OrderProduct
    {
        public decimal UnitCost { get; set; }

        public CurrencyType CurrencyType { get; set; }

        public long ProductId { get;  set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }




        protected OrderProduct() { }

        public OrderProduct(long productId, string productName, decimal unitCost, CurrencyType currencyType, int quantity)
        {


            ProductId = productId;

            ProductName = productName;

            UnitCost = unitCost;

            CurrencyType = currencyType;

            Quantity = quantity;

     

        }


    }
}
