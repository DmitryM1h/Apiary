using ApiaryEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Bees.States.GuardBeeStates
{
    public class InterceptingWrongBeesState : IState
    {
        public bool IsCompleted { get; set; } = false;

        private GuardBee context;

        public InterceptingWrongBeesState(GuardBee context)
        {
            this.context = context; 
        }

        public void Act()
        {
            throw new NotImplementedException();
        }

        public IState NextState()
        {
            throw new NotImplementedException();
        }
    }
}
