using ApiaryEngine.abstractions;
using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Bees.WorkerBee.States;



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
        public int CollectedHoney { get; private set; }

        public WorkerBee(int hiveId) : base(hiveId)
        {
            base.state = new WaitingState(this);
        }

        public IActorState GetState()
        {
            return new WorkerBeeState
            {
                BeeId = this.BeeId,
                HiveId = this.HiveId,
                CollectedHoney = this.CollectedHoney,
                Position = this.Position,
                state = this.state.GetType().Name
                

            };
        }
    }




}
