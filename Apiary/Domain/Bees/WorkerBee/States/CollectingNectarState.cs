using ApiaryEngine.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Bees.WorkerBee.States
{
    public class CollectingNectarState : IState
    {
        public WorkerBee Context { get; set; }
        Flower flower;
        IEnumerator<int> CollectingHoneyProcess;

        public CollectingNectarState(WorkerBee context, Flower flower)
        {
            Context = context;
            this.flower = flower;
            CollectingHoneyProcess = CollectHoney();

        }

        public bool IsCompleted { get; set; } = false;

        public void Act()
        {
            if(!CollectingHoneyProcess.MoveNext())
            {
                IsCompleted = true;
                CollectingHoneyProcess.Dispose();
                return;
            }

            var collectedHoney = CollectingHoneyProcess.Current;
            Context.AddNectar(collectedHoney);
        }

        public IEnumerator<int> CollectHoney()
        {
            while (flower.NectarAmount > 0)
            {
                if (flower.NectarAmount >= 5)
                {
                    yield return flower.GetHoney(5);
                }
                else
                {
                    yield return flower.GetHoney(1);
                }
            }
        }

        public IState NextState()
        {
            if (Context.CollectedNectar >= WorkerBee.NectarCapacity) // пока так оставлю
            {
                return new DeliveringToHoneyState(Context);
            }
            else
            {
                return new VisitingFlowersState(); // TODO исключить цветок который уже посещали.
            }
        }
    }
}
