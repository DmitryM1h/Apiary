using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Shared;



namespace ApiaryEngine.Domain.Bees.WorkerBee.States;

public class ProducingHoneyState : IState
{

    public bool IsCompleted { get; set; } = false;

    WorkerBee _context;

    public ProducingHoneyState()
    {
        _context = (WorkerBee)ApplicationContext.Context.Value!;
    }

    public void Act()
    {
        if(_context.CollectedNectar == 0)
        {
            var hive = Apiary.FindHive(_context.HiveId);
            hive!.IncreaseHoney(_context.BeeId, _context.GetHoney());
            IsCompleted = true;
            return;
        }

        _context.ProduceHoney();
    }
 
    public IState NextState()
    {
        return new WaitingState<VisitingFlowersState>();
    }
}
