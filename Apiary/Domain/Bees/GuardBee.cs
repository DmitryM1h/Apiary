using ApiaryEngine.abstractions;
using ApiaryEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Bees
{
    public class GuardBee : Bee, ITickable
    {


        public IState State { get; private set; }

        public Hive Hive { get; init; }

        public GuardBee(Hive hive) 
        {
            Hive = hive;
        }
        public Task Tick()
        {
            throw new NotImplementedException();
        }
    }
}
