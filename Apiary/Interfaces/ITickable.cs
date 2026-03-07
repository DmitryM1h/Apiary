using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Interfaces
{
    public interface ITickable
    {
        public IAsyncEnumerable<int> Tick();
       
    }
}
