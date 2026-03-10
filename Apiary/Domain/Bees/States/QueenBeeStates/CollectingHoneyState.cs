using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Bees;
using ApiaryEngine.Exceptions;



namespace ApiaryEngine.Domain.States.QueenBeeStates
{
    public class CollectingHoneyState : IState
    {
        public int CollectedHoney { get; set; } = 0;
        public bool IsCompleted { get; set; }

        public QueenBee Context { get; init; }  
        public CollectingHoneyState(QueenBee context)
        {
            Context = context;
        }

        public void Act()
        {
            var hive = Context._hive;

            if (hive is null)
                throw new LostBeeException();

            var honey = hive.TryTakeHoney(QueenBee._amountOfHoneyToBornBee);

            if (honey == -1)
            {
                IsCompleted = true;
                return;
            }

        }

        public IState NextState()
        {
            if (CollectedHoney > 0)
                return new ProducingBeeState(Context);
            else
                return new WaitingState(Context);
        }
    }
}
