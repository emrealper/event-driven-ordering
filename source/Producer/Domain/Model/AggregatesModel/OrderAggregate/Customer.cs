using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AggregatesModel.OrderAggregate
{
    public class Customer : ValueObject
    {
        public long CustomerId { get; private set; }
        public String CustomerName { get; private set; }
        public String CustomerLastName { get; private set; }
        public String CustomerEmail { get; private set; }
    

        public Customer() { }

        public Customer(long id,string name, string lastName, string email)
        {
            CustomerId = id;
            CustomerName = name;
            CustomerLastName = lastName;
            CustomerEmail = email;
 
        }

    

    }
}
