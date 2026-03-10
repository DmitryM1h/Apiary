using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Abstractions
{
    public interface IState
    {
        public void Act();
        public bool IsCompleted { get; set; }
        public IState NextState();
    }
}
