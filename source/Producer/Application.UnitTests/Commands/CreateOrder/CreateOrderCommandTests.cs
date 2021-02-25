using Application.Services.Commands.CreateOrder;
using Domain.AggregatesModel.OrderAggregate;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using static Application.Services.Commands.CreateOrder.CreateOrderCommand;

namespace Application.UnitTests.Commands.CreateOrder
{
  public  class CreateOrderCommandTests
    {


        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseOrderCreatedNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateOrderCommandHandler(mediatorMock.Object);
            var customerName = "Emre";
            var customerLastName = "Alper";
            var customerEmail = "emrealper@gmail.com";

            var newOrderEventData = new CreateOrderCommand
            {
                CustomerId = 322332,
                CustomerName = customerName,
                CustomerLastName = customerLastName,
                CustomerEmail = customerEmail,
                DeliveryAddress = "Oosterdoksstraat 80, 1011 DK Amsterdam, Netherlands",
                RestaurantId = 667789,
                RestaurantName = "Quick China",
                OrderNote= "please don't ring the doorbell baby is sleeping",
                PaymentMethodType=Domain.Enums.PaymentMethodType.CreditCard,
             


            };
            newOrderEventData.OrderProducts = new List<CreateOrderCommand.OrderProductDto>();

            newOrderEventData.OrderProducts.Add(new OrderProductDto { ProductId = 784567, ProductName = "Philadelphia Roll Menu (16 Pieces)", Quantity = 1, UnitCost = 17.5M, 
                CurrencyType = Domain.Enums.CurrencyType.Euro });
            newOrderEventData.OrderProducts.Add(new OrderProductDto { ProductId = 784589, ProductName = "California Roll Half Menu(8 Pieces)", Quantity = 1, UnitCost = 8.25M, 
                CurrencyType = Domain.Enums.CurrencyType.Euro });




            // Act
            var result = sut.Handle(newOrderEventData, CancellationToken.None);


            // Assert
            mediatorMock.Verify(m => m.Publish(It.Is<OrderCreated>(cc => 
            cc.CustomerName == customerName && 
            cc.CustomerLastName==customerLastName && 
            cc.CustomerEmail == customerEmail),
                It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
