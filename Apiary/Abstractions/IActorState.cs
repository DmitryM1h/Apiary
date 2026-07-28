using ApiaryEngine.Domain.Bees;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Abstractions
{
    public interface IActorState
    {
        public ActorType ActorType { get; init; }
    }
}
