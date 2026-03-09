using ApiaryEngine.Exceptions;
using ApiaryEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Bees.QueenBee.States
{
    internal class CollectingHoneyState : IState
    {
        public int CollectedHoney { get; set; } = 0;
        public bool IsCompleted { get; set; }

        public QueenBee Context { get; init; }  
        public CollectingHoneyState(QueenBee context)
        {
            Context = context;
        }

        public void Act()
        {
            var hive = Apiary.FindHive(Context.HiveId);

            if (hive is null)
                throw new LostBeeException();

            var honey = hive.TryTakeHoney(QueenBee._amountOfHoneyToBornBee);

            if (honey == -1)
            {
                IsCompleted = true;
                return;
            }

        }

        public IState NextState()
        {
            if (CollectedHoney > 0)
                return new ProducingBeeState(Context);
            else
                return new WaitingState(Context);
        }
    }
}
