using Application.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataHelpers
{
    public class DeserializeKafkaMessage<E> : IDeserializeKafkaMessage<E>
        where E : class, IRequest

    {

        public E Deserialize(string kafkaMessage)
        {



            return JsonConvert.DeserializeObject<E>(kafkaMessage);
        }


    }
}
