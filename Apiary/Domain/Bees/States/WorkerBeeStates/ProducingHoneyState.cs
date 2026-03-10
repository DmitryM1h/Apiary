using ApiaryEngine.Domain.States.WorkerBeeStates;
using ApiaryEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Bees.States.WorkerBeeStates;

public class ProducingHoneyState : IState
{

    private const int _secondsToProduce = 10;

    public DateTime? _finishDate;
    public bool IsCompleted { get; set; } = false;

    WorkerBee _context;

    public ProducingHoneyState(WorkerBee context)
    {
        _context = context;

        _finishDate = DateTime.Now.AddSeconds(_secondsToProduce);
    }

    public void Act()
    {
        if (DateTime.Now >= _finishDate)
        {
            IsCompleted = true;
        }
    }

    public IState NextState()
    {
        return new WaitingState(_context);
    }
}
