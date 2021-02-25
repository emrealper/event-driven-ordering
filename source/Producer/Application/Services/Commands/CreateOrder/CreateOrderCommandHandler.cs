using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.AggregatesModel.OrderAggregate;
using MediatR;

namespace Application.Services.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
   
        private readonly IMediator _mediator;

        public CreateOrderCommandHandler(IMediator mediator)
        {
          
            _mediator = mediator;

        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var customer = new Customer(request.CustomerId, 
                request.CustomerName, 
                request.CustomerLastName, request.CustomerEmail);

            var restaurant = new Restaurant(request.RestaurantId,request.RestaurantName);

            var order = new Order(customer,restaurant);

            foreach (var item in request.OrderProducts)
            {

                order.AddOrderProducts(item.ProductId, 
                    item.ProductName, item.UnitCost, 
                    item.CurrencyType,item.Quantity);
            }




            //assume we create transaction and added new order to the database 
            //after payment completed
            //await... 
            //Integration event published to Kafka Service Bus for emailing for customer
            order.SetDummyGreatedAndAssignId();
            
            await _mediator.Publish(new OrderCreated
            {
                //dummy order id
                Id = order.Id,
                CustomerName = order.Customer.CustomerName,
                CustomerLastName = order.Customer.CustomerLastName,
                CustomerEmail = order.Customer.CustomerEmail




            }, cancellationToken);

            return Unit.Value;



        }
    }
}
