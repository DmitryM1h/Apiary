using ApiaryEngine.abstractions;
using ApiaryEngine.Domain.States.QueenBeeStates;
using ApiaryEngine.Helpers;
using ApiaryEngine.Interfaces;


namespace ApiaryEngine.Domain.Bees
{
    public class QueenBee : Bee, ITickable
    {
        public const int _amountOfHoneyToBornBee = 1000;

        public const int _secondsToTryProduce = 10;
        public Hive _hive { get; set; }


        public IState State { get; private set; }


        public QueenBee(Hive hive)
        {
            BeeId = IdentityProvider.GetIdentity();

            HiveId = hive.HiveId;
            
            _hive = hive;

            State = new WaitingState(this);
        }


        public async Task Tick()
        {
            State.Act();

            if(State.IsCompleted)
            {
                State = State.NextState();
            }
        }
    }


   

    

}
