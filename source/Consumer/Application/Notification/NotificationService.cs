using Application.Interfaces;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notification
{
    public class NotificationService : INotificationService
    {
        public Task SendEmailAsync(MessageDto message)
        {
          
            return Task.CompletedTask;
        }
    }
}
