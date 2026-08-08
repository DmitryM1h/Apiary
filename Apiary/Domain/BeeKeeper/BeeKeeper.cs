using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.BeeKeeper.States;
using ApiaryEngine.Domain.Bees;
using ApiaryEngine.Domain.Shared;
using ApiaryEngine.Helpers;

namespace ApiaryEngine.Domain.BeeKeeper
{

    public class BeeKeeper : IActor
    {
        public int BeeKeeperId { get; private set; }
        public Point Position { get; private set; }
        public int CollectedHoney { get; private set; } = 0;

        private IState _currentState;
        private readonly Hive[] _hives;
        private int _currentHiveIndex = 0;

        public BeeKeeper(Hive[] hives)
        {
            _hives = hives;
            BeeKeeperId = 1;

            Position = new Point(40, 0);
            _currentState = new WaitingState<CollectingHoneyState>();
        }

        public void Tick()
        {
            if (_currentState.IsCompleted)
            {
                _currentState = _currentState.NextState();
            }

            _currentState.Act();
        }

        public IActorState GetState()
        {
            return new BeeKeeperState
            {
                BeeKeeperId = this.BeeKeeperId,
                Position = this.Position,
                CollectedHoney = this.CollectedHoney,
                State = _currentState.GetType().Name
            };
        }

        public void UpdatePosition(Point newPosition)
        {
            Position = newPosition;
        }

        public void CollectHoneyFromHive(int honeyAmount)
        {
            CollectedHoney += honeyAmount;
            Console.WriteLine($"Пасечник  собрал {honeyAmount} меда. Всего: {CollectedHoney}");
        }

        public Hive GetNextHive()
        {
            if (_hives.Length == 0)
                throw new Exception("No initialized hives");

            _currentHiveIndex = (_currentHiveIndex + 1) % _hives.Length;
            return _hives[_currentHiveIndex];
        }
    }
}