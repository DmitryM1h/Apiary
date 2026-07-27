using ApiaryEngine.Abstractions;


namespace ApiaryEngine.Domain.Bees.WorkerBee.States;

public class WaitingState : IState
{
    public DateTime _nextTimeAct;
    public WorkerBee Context { get; set; }


    public WaitingState()
    {
        Context = (WorkerBee)ApplicationContext.CurrentActor;

        _nextTimeAct = DateTime.Now.AddSeconds(10);
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
        return new VisitingFlowersState();
    }

}
