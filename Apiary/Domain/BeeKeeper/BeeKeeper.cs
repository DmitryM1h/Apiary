using ApiaryEngine.abstractions;
using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.BeeKeeper.States;

namespace ApiaryEngine.Domain.BeeKeeper
{
    public class BeeKeeperState : IActorState
    {
        public int BeeKeeperId { get; set; }
        public Point Position { get; set; }
        public int CollectedHoney { get; set; }
        public string State { get; set; } = "CollectingHoney";
        public ActorType ActorType { get; init; } = ActorType.BeeKeeper;
    }
    public class BeeKeeper : IActor
    {
        public int BeeKeeperId { get; private set; }
        public Point Position { get; private set; }
        public int CollectedHoney { get; private set; } = 0;

        private IState _currentState;
        private readonly Hive[] _hives;
        private int _currentHiveIndex = 0;
        private int _honeyToCollect = 0;


        public BeeKeeper(Hive[] hives)
        {
            _hives = hives;
            BeeKeeperId = 1;

            Position = new Point(40, 0);
            _currentState = new WaitingState(this);


            //if (_hives.Length > 0)
            //{
            //    var firstHivePosition = Apiary.HivePositions[_hives[0].HiveId];
            //    Position = firstHivePosition;
            //    _currentState = new CollectingHoneyState(this, _hives[0]);
            //}
            //else
            //{
            //    Position = new Point(0, 0);
            //    _currentState = new WaitingState(this);
            //}
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
            Console.WriteLine($"🧑‍🌾 Пасечник {BeeKeeperId} собрал {honeyAmount} меда. Всего: {CollectedHoney}");
        }

        public Hive GetNextHive()
        {
            if (_hives.Length == 0)
                return null;

            _currentHiveIndex = (_currentHiveIndex + 1) % _hives.Length;
            return _hives[_currentHiveIndex];
        }

        public Hive GetCurrentHive()
        {
            return _hives[_currentHiveIndex];
        }
    }
}