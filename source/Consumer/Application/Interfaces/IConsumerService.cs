using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IEventBusService
{
    public interface IConsumerService
    {
        Task SubscribeServiceBusAsync();
    }
}
