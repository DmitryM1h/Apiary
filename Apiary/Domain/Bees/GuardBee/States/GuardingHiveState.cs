using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Bees.GuardBeeStates;
using ApiaryEngine.Helpers;

namespace ApiaryEngine.Domain.Bees.States.GuardBeeStates
{
    public class GuardingHiveState : IState
    {
        public bool IsCompleted { get; set; } = false;

        private Lazy<GuardBee> context = new(() => (GuardBee)ApplicationContext.Context.Value!);
        //private Lazy<Point> hivePosition = new(() => Apiary.HivePositions[context.Value.Hive.HiveId]);


        private IEnumerator<(double X, double Y)> _guardBeePath;
        public GuardingHiveState()
        {
            //this.context = (GuardBee)ApplicationContext.Context.Value!;
            //hivePosition = Apiary.HivePositions[context.Hive.HiveId];
            _guardBeePath = GuardBeeRoute();
        }

        public void Act()
        {
            if (!_guardBeePath.MoveNext())
            {
                _guardBeePath = GuardBeeRoute();
                _guardBeePath.MoveNext();
            }

            var newPosition = _guardBeePath.Current;

            var noiseX = (new Random().NextDouble() - 0.5) * 1.5;
            var noiseY = (new Random().NextDouble() - 0.5) * 1.5;

            context.Value.UpdatePosition(new Point(
                newPosition.X + noiseX,
                newPosition.Y + noiseY
            ));


        }

        public IState NextState()
        {
            return this;
        }

        private IEnumerator<(double X, double Y)> GuardBeeRoute()
        {
            var hivePosition = Apiary.HivePositions[context.Value.Hive.HiveId];

            var routetoradius = RouteToRadius(context.Value.Position);

            while (routetoradius.MoveNext())
            {
                yield return routetoradius.Current;
            }

            var routeAroundHive = RouteAroundHive(hivePosition);

            while (routeAroundHive.MoveNext())
            {
                yield return routeAroundHive.Current;
            }
        }

        private static IEnumerator<(double X, double Y)> RouteAroundHive(Point hivePosition)
        {
            const int radius = 5;
            var points = new List<(double X, double Y)>();

            for (double angle = 0; angle < 2 * Math.PI; angle += 0.1)
            {
                double x = hivePosition.X + radius * Math.Cos(angle);
                double y = hivePosition.Y + radius * Math.Sin(angle);
                points.Add((x, y));
            }

            while (true)
            {
                foreach (var point in points)
                {
                    yield return point;
                }
            }
        }

        private IEnumerator<(double X, double Y)> RouteToRadius(Point initialPosition)
        {
            var hivePosition = Apiary.HivePositions[context.Value.Hive.HiveId];

            var firstPoint = GetFirstPointOnCircle(hivePosition);

            double x = initialPosition.X;
            double y = initialPosition.Y;

            double dx = firstPoint.X - x;
            double dy = firstPoint.Y - y;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance < 0.1)
            {
                yield return firstPoint;
                yield break;
            }

            int steps = (int)(distance * 2);
            for (int i = 1; i <= steps; i++)
            {
                double t = (double)i / steps;
                yield return (
                    x + dx * t,
                    y + dy * t
                );
            }

            yield return firstPoint;
        }

        private (double X, double Y) GetFirstPointOnCircle(Point hivePosition)
        {
            const int radius = 5;
            return (hivePosition.X + radius, hivePosition.Y);
        }
    }
}