using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Bees.QueenBee
{
    public class QueenContext
    {
        public QueenBeeState State { get; private set; }

        public WaitingState? waitingState;

        public ProducingBeeState? producingBeeState;

        public bool CanStartProducing => waitingState!._nextTimeAct >= DateTime.Now;

        public QueenContext()
        {
            waitingState = new();
        }

        public void SwitchState()
        {
            if (State == QueenBeeState.Waiting)
            {
                State = QueenBeeState.ProducingBee;

                producingBeeState = new();

            }
            else
            {
                State = QueenBeeState.Waiting;
                waitingState!.Update();

            }
        }

        public void UpdateCollectedHoney(int honey)
        {
            producingBeeState!.CollectedHoney = honey;
        }

        public void InProcessOfProducing()
        {
            producingBeeState!.StartProducing();
        }

        public class WaitingState
        {
            public DateTime _nextTimeAct;

            public WaitingState()
            {
                _nextTimeAct = DateTime.Now.AddSeconds(15);
            }

            public void Update()
            {
                _nextTimeAct = DateTime.Now.AddSeconds(15);
            }

        }

        public class ProducingBeeState
        {
            public DateTime? processFinishDate;

            public bool Finished { get; set; } = false;
            public int CollectedHoney { get; set; } = 0;

            public void SetCollectedHoney(int honey)
            {
                CollectedHoney = honey;
            }

            public void StartProducing()
            {                
                processFinishDate = DateTime.Now.AddSeconds(15);
            }

            public void FinishProducing()
            {
                Finished = true;
            }
            
            public bool IsFinished() => DateTime.Now >= processFinishDate;
        }
        public enum QueenBeeState
        {
            Waiting,
            ProducingBee
        }
    }



    //public struct Tick
    //{
    //    public readonly DateTime timeStamp = DateTime.Now;
    //    public readonly DateTime endOfTick = DateTime.Now.AddMilliseconds(10);

    //    public Tick()
    //    {

    //    }
    //}
}
