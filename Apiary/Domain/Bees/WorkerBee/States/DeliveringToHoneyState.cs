using ApiaryEngine.Abstractions;
using ApiaryEngine.Helpers;


namespace ApiaryEngine.Domain.Bees.WorkerBee.States
{
    public class DeliveringToHoneyState : IState
    {
        public bool IsCompleted { get; set; }

        WorkerBee _context;

        IEnumerator<Point> _routeToHome;


        public DeliveringToHoneyState(WorkerBee context)
        {
            _context = context;
            _routeToHome = RouteToHome(_context.Position);
        }
        // TODO Летит в свой улей, но может перепутать с каким то шансом
        public void Act()
        {
            if (!_routeToHome.MoveNext())
            {
                IsCompleted = true;
                _routeToHome.Dispose();
                return;
            }
            var newPosition = _routeToHome.Current;

            _context.UpdatePosition(newPosition);
        }

        private IEnumerator<Point> RouteToHome(Point initialPosition)
        {
            var destinationPosition = Apiary.HivePositions[_context.HiveId];

            var currentPosition = initialPosition;

            double stepX = initialPosition.X < destinationPosition.X ? 1 : -1;
            double stepY = initialPosition.Y < destinationPosition.Y ? 1 : -1;

            while (!currentPosition.Equals(destinationPosition))
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
        }


        public IState NextState()
        {
            return new ProducingHoneyState(_context);
        }
    }
}
