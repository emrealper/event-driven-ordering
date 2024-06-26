﻿using Application.Interfaces;
using Application.Notification;
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
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }

    }
}
