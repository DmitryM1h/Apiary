using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Shared;
using ApiaryEngine.Exceptions;



namespace ApiaryEngine.Domain.Bees.QueenBee.States
{
    public class CollectingHoneyState : IState
    {
        public int CollectedHoney { get; set; } = 0;
        public bool IsCompleted { get; set; }

        public QueenBee Context { get; init; }  
        public CollectingHoneyState()
        {
            Context = (QueenBee)ApplicationContext.Context.Value!;
        }

        public void Act()
        {
            var hive = Context._hive ?? throw new LostBeeException();

            if (!hive.TryTakeHoney(QueenBee._amountOfHoneyToBornBee, out var honey))
            {
                IsCompleted = true;
                return;
            }

            CollectedHoney = honey!.Value;
            IsCompleted = true;

        }

        public IState NextState()
        {
            if (CollectedHoney > 0)
                return new ProducingBeeState();
            else
                return new WaitingState<CollectingHoneyState>();
        }
    }
}
