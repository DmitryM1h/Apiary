using ApiaryEngine.Abstractions;
using ApiaryEngine.Helpers;



namespace ApiaryEngine.Domain.Bees.WorkerBee.States
{
    public class VisitingFlowersState : IState
    {
        public bool IsCompleted { get; set; }

        WorkerBee BeeContext;

        Flower? flower;

        IEnumerator<Point> _routeToFlower;
        public VisitingFlowersState()
        {
            BeeContext = (WorkerBee)ApplicationContext.Context.Value!;

            _routeToFlower = RouteToRandomFlower(BeeContext.Position);
        }

        public void Act()
        {
            if(!_routeToFlower.MoveNext())
            {
                IsCompleted = true;
                _routeToFlower.Dispose();
                return;
            }
            var newPosition = _routeToFlower.Current;

            BeeContext.UpdatePosition(newPosition);
        }

        public IState NextState()
        {
            return new CollectingNectarState(flower!);
        }

        public Flower FindClosestFlower()
        {
            var currentPostion = BeeContext.Position;

            int closestFlowerId = 0;
            double closestDist = double.MaxValue;

            foreach(var (flowerId, position) in Apiary.FlowerPositions)
            {
                var diffX = (position.X - currentPostion.X);
                var diffY = (position.Y - currentPostion.Y);
                var dist = Math.Sqrt(diffX * diffX + diffY * diffY);
                if(dist < closestDist)
                {
                    closestDist = dist;
                    closestFlowerId = flowerId;
                }
            }

            return Apiary.FindFlower(closestFlowerId)!;
        }

        private IEnumerator<Point> RouteToRandomFlower(Point initialPosition)
        {
            var (flower, destinationPosition) = GetRandomFlower();

            Console.WriteLine($"Пчелка {BeeContext.GetType().Name} {BeeContext.BeeId} двигается к цветку {flower.Id} {destinationPosition}");

            var currentPosition = initialPosition;

            double stepX = initialPosition.X < destinationPosition.X ? 1 : -1;
            double stepY = initialPosition.Y < destinationPosition.Y ? 1 : -1;

            while(!currentPosition.Equals(destinationPosition))
            {
                double x = currentPosition.X + stepX;
                double y = currentPosition.Y + stepY;
                if ((y > destinationPosition.Y && stepY == 1) || (y < destinationPosition.Y && stepY == -1))
                {
                    y = destinationPosition.Y;
                    stepY = 0;
                }
                if ((x > destinationPosition.X && stepX == 1) || (x < destinationPosition.X && stepX == -1))
                {
                    x = destinationPosition.X;
                    stepX = 0;
                }
                currentPosition = new Point(x, y);
                yield return currentPosition;
            }

            this.flower = flower;
        }

        public static (Flower flower, Point position) GetRandomFlower()
        {
           var keys =  Apiary.Flowers.Keys;

           var randomFlowerId = Random.Shared.Next(0, keys.Count() - 1);

           return (Apiary.FindFlower(randomFlowerId)!, Apiary.FlowerPositions[randomFlowerId]);
                
        }
    }
}
