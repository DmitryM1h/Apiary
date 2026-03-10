using ApiaryEngine.Domain.Bees;
using ApiaryEngine.Domain.Bees.States.WorkerBeeStates;
using ApiaryEngine.Interfaces;


namespace ApiaryEngine.Domain.States.WorkerBeeStates;

public class WaitingState : IState
{
    public DateTime _nextTimeAct;
    public WorkerBee Context { get; set; }


    public WaitingState(WorkerBee context)
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
        return new ProducingHoneyState(Context);
    }

}
