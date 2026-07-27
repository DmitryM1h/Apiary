using ApiaryEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain
{
    public class ApplicationContext
    {
        public static IActor CurrentActor { get; private set; }

        public void SwitchActor(IActor actor)
        {
            CurrentActor = actor;
        }
    }
}
