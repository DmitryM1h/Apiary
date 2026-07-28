using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Dtos
{
    public class WorkerBeeState : BeeState
    {
        public int CollectedHoney { get; init; }

        public override string ToString()
        {
            return base.ToString() + $" honey: {CollectedHoney}";
        }

    }

}
