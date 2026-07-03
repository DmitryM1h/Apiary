using ApiaryEngine.abstractions;
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
