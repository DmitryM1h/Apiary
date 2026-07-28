using ApiaryEngine.Domain;
using ApiaryEngine.Domain.Bees;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Abstractions
{
    public interface IActor
    {
        public void Tick();

        public IActorState GetState();
       
    }
}
