using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain;
using ApiaryEngine.Helpers;

namespace ApiaryEngine.abstractions
{
    public interface IActorState {}
    public class BeeState : IActorState
    {
        public int BeeId { get; init; }
        public int HiveId { get; init; }
        public Point Position { get; init; }
        public string state { get; init; }

        public override string ToString()
        {
            return $"{BeeId} , {HiveId}, {Position}, {state}";
        }
    }

    public abstract class Bee
    {
        public int BeeId { get; init; }
        public int HiveId { get; init; }
        public Point Position { get; private set; }

        protected IState state;

        public Bee(int hiveId)
        {
            Position = Apiary.HivePositions[hiveId]; // как мокать теперь??
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
            state.Act();

            if (state.IsCompleted)
            {
                state = state.NextState();

                Console.WriteLine($"Пчелка {GetType().Name} (id = {BeeId}) перешла в состояние {state.GetType().Name}");
            }
        }
    }
}
