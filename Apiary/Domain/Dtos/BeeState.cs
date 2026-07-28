using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Bees;
using ApiaryEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Dtos
{
    public class BeeState : IActorState
    {
        public int BeeId { get; init; }
        public int HiveId { get; init; }
        public Point Position { get; init; }
        public string state { get; init; }

        public ActorType ActorType { get; init; }

        public override string ToString()
        {
            return $"{BeeId} , {HiveId}, {Position}, {state}";
        }
    }
}
