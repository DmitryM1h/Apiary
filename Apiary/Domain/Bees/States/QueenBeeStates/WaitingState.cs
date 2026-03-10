using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Bees;


namespace ApiaryEngine.Domain.States.QueenBeeStates;

public class WaitingState : IState
{
    public DateTime _nextTimeAct;
    public QueenBee Context { get; set; }


    public WaitingState(QueenBee context)
    {
        Context = context;
        _nextTimeAct = DateTime.Now.AddSeconds(QueenBee._secondsToTryProduce);
    }

    public bool IsCompleted { get; set; } = false;

    public void Act()
    {
        if(DateTime.Now >= _nextTimeAct)
        {
            IsCompleted = true;
        }
    }

    public IState NextState()
    {
        return new CollectingHoneyState(Context);
    }

}
