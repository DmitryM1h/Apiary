using ApiaryEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Bees.QueenBee.States
{
    public class WaitingState : IState
    {
        public DateTime _nextTimeAct;
        public QueenBee Context { get; set; }


        public WaitingState(QueenBee context)
        {
            Context = context;
            _nextTimeAct = DateTime.Now.AddSeconds(QueenBee._secondsToTryProduce);
        }

        public bool IsCompleted { get; set; } = false;

        public void Act()
        {
            if(DateTime.Now >= _nextTimeAct)
            {
                IsCompleted = true;
            }
        }

        public IState NextState()
        {
            return new CollectingHoneyState(Context);
        }

    }
}
