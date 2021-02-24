using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application;
using Application.IEventBusService;
using Application.IntegrationEvents;
using Application.IntegrationEvents.Handlers;
using Application.Interfaces;
using Application.Notification;
using Infrastructure.EventBus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmailAndNotificationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {

                    services.AddMediatR(typeof(Program));
                    services.AddApplication();
                    services.AddInfrastructure();
                    services.AddHostedService<EventBusConsumerWorker>();

  


                });
    }
}
