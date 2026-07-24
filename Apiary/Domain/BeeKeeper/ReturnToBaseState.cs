using ApiaryEngine.Abstractions;

namespace ApiaryEngine.Domain.BeeKeeper.States
{
    public class ReturnToBaseState : IState
    {
        public bool IsCompleted { get; set; } = false;
        private readonly BeeKeeper _context;
        private IEnumerator<Point> _routeToBase;
        private readonly Point _basePosition = new Point(40, 0);

        public ReturnToBaseState(BeeKeeper context)
        {
            _context = context;
            _routeToBase = RouteToBase(_context.Position, _basePosition);
        }

        public void Act()
        {
            if (_routeToBase != null)
            {
                if (_routeToBase.MoveNext())
                {
                    var newPosition = _routeToBase.Current;
                    _context.UpdatePosition(newPosition);
                    return;
                }
                else
                {
                    _routeToBase.Dispose();
                    _routeToBase = null;
                }
            }

            if (_routeToBase == null)
            {
                IsCompleted = true;
            }
        }

        private IEnumerator<Point> RouteToBase(Point startPosition, Point destinationPosition)
        {
            var currentPosition = startPosition;

            double stepX = startPosition.X < destinationPosition.X ? 1 :
                           startPosition.X > destinationPosition.X ? -1 : 0;
            double stepY = startPosition.Y < destinationPosition.Y ? 1 :
                           startPosition.Y > destinationPosition.Y ? -1 : 0;

            while (Math.Abs(currentPosition.X - destinationPosition.X) > 0.5 ||
                   Math.Abs(currentPosition.Y - destinationPosition.Y) > 0.5)
            {
                double x = currentPosition.X;
                double y = currentPosition.Y;

                if (Math.Abs(x - destinationPosition.X) > 0.5)
                    x += stepX;
                else
                    x = destinationPosition.X;

                if (Math.Abs(y - destinationPosition.Y) > 0.5)
                    y += stepY;
                else
                    y = destinationPosition.Y;

                currentPosition = new Point(x, y);
                yield return currentPosition;
            }

            yield return destinationPosition;
        }

        public IState NextState()
        {
            return new WaitingState(_context);
        }
    }
}