using ApiaryEngine.Abstractions;

namespace ApiaryEngine.Domain.BeeKeeper.States
{
    public class WaitingState : IState
    {
        public bool IsCompleted { get; set; } = false;
        private readonly BeeKeeper _context;
        private DateTime _waitUntil;

        public WaitingState(BeeKeeper context)
        {
            _context = context;
            _waitUntil = DateTime.Now.AddSeconds(20);
        }

        public void Act()
        {
            if (DateTime.Now >= _waitUntil)
            {
                IsCompleted = true;
            }
        }

        public IState NextState()
        {

            return new CollectingHoneyState();

        }
    }
}