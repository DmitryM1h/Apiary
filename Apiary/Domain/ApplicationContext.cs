using ApiaryEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain
{
    public class ApplicationContext
    {
        public static AsyncLocal<IActor> Context { get; private set; } = new();

        public void SetActor(IActor actor)
        {
            Context.Value = actor;
        }
    }
}
