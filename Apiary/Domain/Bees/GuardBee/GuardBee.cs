using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Bees.States.GuardBeeStates;
using ApiaryEngine.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Bees.GuardBeeStates
{
    public class GuardBee : Bee, IActor
    {
        public Hive Hive { get; init; }

        public GuardBee(Hive hive) : base(hive.HiveId)
        {
            Hive = hive;
            base.State = new GuardingHiveState();
        }

        public IActorState GetState()
        {
            return new BeeState
            {
                BeeId = this.BeeId,
                HiveId = this.HiveId,
                Position = this.Position,
                state = this.State.GetType().Name,
                ActorType = ActorType.GuardBee
            };
        }
    }
}
