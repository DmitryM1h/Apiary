using ApiaryEngine.abstractions;
using ApiaryEngine.Domain.Bees.QueenBee.States;
using ApiaryEngine.Helpers;
using ApiaryEngine.Interfaces;


namespace ApiaryEngine.Domain.Bees.QueenBee
{
    public class QueenBee : Bee, ITickable
    {
        public const int _amountOfHoneyToBornBee = 1000;

        public const int _secondsToTryProduce = 10;


        public IState State { get; private set; }


        public QueenBee(int hiveId)
        {
            BeeId = IdentityProvider.GetIdentity();

            HiveId = hiveId;

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
