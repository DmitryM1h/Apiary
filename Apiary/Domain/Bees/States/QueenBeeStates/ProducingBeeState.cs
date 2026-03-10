using ApiaryEngine.Domain.Bees;
using ApiaryEngine.Abstractions;



namespace ApiaryEngine.Domain.States.QueenBeeStates;


public class ProducingBeeState : IState
{
    private const int _secondsToProduce = 10;

    public DateTime? _finishDate;
    public bool IsCompleted { get; set; } = false;

    private QueenBee _context;

    public ProducingBeeState(QueenBee context)
    {
        _context = context;

        _finishDate = DateTime.Now.AddSeconds(_secondsToProduce);
    }

    public void Act()
    {
        if(DateTime.Now >= _finishDate)
        {
            IsCompleted = true;
        }
    }

    public IState NextState()
    {
        return new WaitingState(_context);
    }
}
