using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.BeeKeeper.States;
using ApiaryEngine.Helpers;

namespace ApiaryEngine.Domain.BeeKeeper;
public class CollectingHoneyState : IState
{
    public bool IsCompleted { get; set; } = false;
    private readonly BeeKeeper _context;
    private readonly Hive _hive;
    private IEnumerator<Point> _routeToHive;

    public CollectingHoneyState()
    {
        _context = (BeeKeeper)ApplicationContext.Context.Value!;
        _hive = _context.GetNextHive();
        _routeToHive = RouteToHive(_context.Position, Apiary.HivePositions[_hive.HiveId]);
    }

    public void Act()
    {
        if (_routeToHive != null)
        {
            if (_routeToHive.MoveNext())
            {
                var newPosition = _routeToHive.Current;
                _context.UpdatePosition(newPosition);
                return;
            }
            else
            {
                _routeToHive.Dispose();
                _routeToHive = null;
            }
        }

        if (_routeToHive == null)
        {
            IsCompleted = true;
        }
    }

    private IEnumerator<Point> RouteToHive(Point startPosition, Point destinationPosition)
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
        return new CollectingHoneyProcessState(_hive);
    }
}