using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Bees.QueenBee;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Shared
{
    public class WaitingState<V> : IState where V: IState, new()
    {
        public DateTime _nextTimeAct;
        public IActor Context { get; set; }


        public WaitingState()
        {
            Context = ApplicationContext.CurrentActor;
            _nextTimeAct = DateTime.Now.AddSeconds(15);
        }

        public bool IsCompleted { get; set; } = false;

        public void Act()
        {
            if (DateTime.Now >= _nextTimeAct)
            {
                IsCompleted = true;
            }
        }

        public IState NextState()
        {
            return new V();
        }

    }
}
