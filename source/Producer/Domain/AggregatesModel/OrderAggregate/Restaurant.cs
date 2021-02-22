using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AggregatesModel.OrderAggregate
{
    public class Restaurant : ValueObject
    {
        public long RestaurantId { get; private set; }
        public String RestaurantName { get; private set; }
       
    

        public Restaurant() { }

        public Restaurant(long id,string name)
        {
            RestaurantId = id;
            RestaurantName = name;
        
 
        }

    

    }
}
