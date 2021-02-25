using Application.IEventBusService;
using Application.IntegrationEvents;
using Application.Interfaces;
using Infrastructure.DataHelpers;
using Infrastructure.EventBus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {


            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IConsumerService, ConsumerService>();
            services.AddSingleton<IProducerService, ProducerService>();
            services.AddTransient<IDeserializeKafkaMessage<ReceivedOrderPaidEvent>>(s => new DeserializeKafkaMessage<ReceivedOrderPaidEvent>());
            return services;
        }

    }
}
