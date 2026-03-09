using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Interfaces
{
    public interface IState
    {
        public void Act();
        public bool IsCompleted { get; set; }
        public IState NextState();
    }
}
