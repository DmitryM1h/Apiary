using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Abstractions
{
    public interface ITickable
    {
        public Task Tick();
       
    }
}
