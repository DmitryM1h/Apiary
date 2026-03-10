using ApiaryEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Bees.States.WorkerBeeStates
{
    public class VisitingFlowersState : IState
    {
        public bool IsCompleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
