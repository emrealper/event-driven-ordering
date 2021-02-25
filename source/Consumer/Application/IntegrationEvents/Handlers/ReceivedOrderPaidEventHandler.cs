using Application.Interfaces;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.IntegrationEvents.Handlers
{
    public class ReceivedOrderPaidEventHandler : IRequestHandler<ReceivedOrderPaidEvent>
    {

        private readonly INotificationService _notification;

        public ReceivedOrderPaidEventHandler(INotificationService notification)
        {

            _notification = notification;

        }



        public async Task<Unit> Handle(ReceivedOrderPaidEvent request, CancellationToken cancellationToken)
        {

            await _notification.SendEmailAsync(new MessageDto());

            return Unit.Value;


        }

    }
}
