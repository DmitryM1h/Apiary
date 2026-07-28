using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Bees.QueenBee.States;
using ApiaryEngine.Domain.Dtos;
using ApiaryEngine.Domain.Shared;


namespace ApiaryEngine.Domain.Bees.QueenBee
{
    public class QueenBee : Bee, IActor
    {
        public const int _amountOfHoneyToBornBee = 50;

        public const int _secondsToTryProduce = 10;
        public Hive _hive { get; set; }

        public QueenBee(Hive hive) : base(hive.HiveId)
        {
           
            _hive = hive;

            base.State = new WaitingState<CollectingHoneyState>();
        }

        public IActorState GetState()
        {
            return new BeeState 
            {
                BeeId = this.BeeId,
                HiveId = this.HiveId,
                Position = this.Position,
                state = this.State.GetType().Name,
                ActorType = ActorType.QueenBee,
            };
        }
    }


   

    

}
