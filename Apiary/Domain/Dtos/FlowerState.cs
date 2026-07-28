using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Bees;
using ApiaryEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Dtos
{
    public class FlowerState : IActorState
    {
        public int FlowerId { get; set; }
        public Point Position { get; set; }
        public int NectarAmount { get; set; }
        public ActorType ActorType { get; init; } = ActorType.Flower;
    }
}
