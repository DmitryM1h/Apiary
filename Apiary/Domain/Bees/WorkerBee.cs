using ApiaryEngine.abstractions;
using ApiaryEngine.Abstractions;
using ApiaryEngine.Exceptions;
using ApiaryEngine.Helpers;

namespace ApiaryEngine.Domain.Bees
{
    public class WorkerBee : Bee, ITickable
    {
        public int CollectedHoney { get; private set; }

        IState state;
        public WorkerBee(int hiveId)
        {
            BeeId = IdentityProvider.GetIdentity();
            HiveId = hiveId;

        }

        Task ITickable.Tick()
        {
            throw new NotImplementedException();
        }
    }

}
