using ApiaryEngine.abstractions;
using ApiaryEngine.Domain.Bees.QueenBee.States;
using ApiaryEngine.Helpers;
using ApiaryEngine.Interfaces;


namespace ApiaryEngine.Domain.Bees.QueenBee
{
    public class QueenBee : Bee, ITickable
    {
        public const int _amountOfHoneyToBornBee = 1000;

        IState state;


        public QueenBee(int hiveId)
        {
            BeeId = IdentityProvider.GetIdentity();

            HiveId = hiveId;

            state = new WaitingState(this);
        }


        public async Task Tick()
        {
            state.Act();

            if(state.IsCompleted)
            {
                state = state.NextState();
            }
        }
    }


   

    

}
