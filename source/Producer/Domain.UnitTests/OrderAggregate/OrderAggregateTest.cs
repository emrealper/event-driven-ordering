using Domain.AggregatesModel.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Domain.UnitTests.OrderAggregate
{
    public class OrderAggregateTest
    {


        [Fact]
        public void CREATE_ORDER_ITEM_SUCCESS()
        {
            //Arrange    
            var customerId = 322332;
            var customerName = "Emre";
            var customerLastName = "Alper";
            var customerEmail = "emrealper@gmail.com";
            var restaurantId = 66789;
            var restaurantName = "Quick China";
         
            var customer = new Customer(customerId,customerName,customerLastName,customerEmail);
            var restaurant = new Restaurant(restaurantId,restaurantName);
        

            //Act 
            var fakeOrderItem = new Order(customer,restaurant);

            //Assert
            Assert.NotNull(fakeOrderItem);
        }



        [Fact]
        public void SET_DUMMY_ID_TO_ORDER_SUCCESS()
        {
            //Arrange    
            var customerId = 322332;
            var customerName = "Emre";
            var customerLastName = "Alper";
            var customerEmail = "emrealper@gmail.com";
            var restaurantId = 66789;
            var restaurantName = "Quick China";

            var customer = new Customer(customerId, customerName, customerLastName, customerEmail);
            var restaurant = new Restaurant(restaurantId, restaurantName);


            //Act 
            var fakeOrderItem = new Order(customer, restaurant);
            fakeOrderItem.SetDummyGreatedAndAssignId();

            //Assert
            Assert.True(fakeOrderItem.Id.GetType() == typeof(Guid));
        }
    }
}
