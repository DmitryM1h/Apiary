using ApiaryEngine.abstractions;
using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Bees.QueenBee.States;
using ApiaryEngine.Domain.Bees.WorkerBee;
using ApiaryEngine.Helpers;


namespace ApiaryEngine.Domain.Bees.QueenBee
{
    public class QueenBee : Bee, IActor
    {
        public const int _amountOfHoneyToBornBee = 1000;

        public const int _secondsToTryProduce = 10;
        public Hive _hive { get; set; }

        public QueenBee(Hive hive) : base(hive.HiveId)
        {
           
            _hive = hive;

            base.state = new WaitingState(this);
        }

        public IActorState GetState()
        {
            throw new NotImplementedException();
        }
    }


   

    

}
