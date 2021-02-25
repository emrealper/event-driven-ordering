using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IDeserializeKafkaMessage<E>
        where E : class, IRequest

    {

        E Deserialize(string line);

      



    }



}
