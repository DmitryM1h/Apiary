using ApiaryEngine.abstractions;
using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Bees.WorkerBee.States;
using ApiaryEngine.Domain.Shared;



namespace ApiaryEngine.Domain.Bees.WorkerBee
{
    public class WorkerBeeState : BeeState
    {
        public int CollectedHoney { get; init; }

        public override string ToString()
        {
            return base.ToString() + $" honey: {CollectedHoney}";
        }

    }
    public class WorkerBee : Bee, IActor
    {
        public int CollectedNectar { get; private set; }

        public const int NectarCapacity = 30;
        public int producedHoney;
        public WorkerBee(int hiveId) : base(hiveId)
        {
            base.State = new WaitingState<VisitingFlowersState>();
        }

        public void AddNectar(int nectarAmount)
        {
            CollectedNectar += nectarAmount;
        }

        public void ProduceHoney()
        {
            CollectedNectar-= 1;
            producedHoney += 1;
        }

        public int GetHoney()
        {
            var honeyToReturn = producedHoney;
            producedHoney = 0;
            return honeyToReturn;
        }

        public IActorState GetState()
        {
            return new WorkerBeeState
            {
                BeeId = this.BeeId,
                HiveId = this.HiveId,
                CollectedHoney = this.CollectedNectar,
                Position = this.Position,
                state = this.State.GetType().Name,
                ActorType = ActorType.WorkerBee
            };
        }
    }




}
