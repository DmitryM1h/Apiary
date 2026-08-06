using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain;
using ApiaryEngine.Helpers;

namespace ApiaryEngine.Domain.Bees
{
    public abstract class Bee
    {
        public int BeeId { get; init; }
        public int HiveId { get; init; }
        public Point Position { get; private set; }

        protected IState State;

        public Bee(int hiveId)
        {
            Position = Apiary.HivePositions[hiveId];
            BeeId = IdentityProvider.GetIdentity();
            HiveId = hiveId;
        }

        public void UpdatePosition(Point position)
        {
            Position = position;

            Console.WriteLine($"Пчелка {GetType().Name} (id = {BeeId}) теперь в координате {Position}");
        }

        public void Tick()
        {
            State.Act();

            if (State.IsCompleted)
            {
                State = State.NextState();

                Console.WriteLine($"Пчелка {GetType().Name} (id = {BeeId}) перешла в состояние {State.GetType().Name}");
            }
        }
    }
}
