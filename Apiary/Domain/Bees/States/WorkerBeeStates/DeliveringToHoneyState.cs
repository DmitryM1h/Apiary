using ApiaryEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Bees.States.WorkerBeeStates
{
    public class DeliveringToHoneyState : IState
    {
        public bool IsCompleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        // Летит в свой улей, но может перепутать с каким то шансом
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
