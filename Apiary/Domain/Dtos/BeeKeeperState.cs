using ApiaryEngine.Abstractions;
using ApiaryEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Dtos
{
    public class BeeKeeperState : IActorState
    {
        public int BeeKeeperId { get; set; }
        public Point Position { get; set; }
        public int CollectedHoney { get; set; }
        public string State { get; set; }
        public ActorType ActorType { get; init; } = ActorType.BeeKeeper;
    }
}
