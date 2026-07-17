using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Bees.GuardBeeStates;



namespace ApiaryEngine.Domain.Bees.States.GuardBeeStates
{
    public class GuardingHiveState : IState
    {
        public bool IsCompleted { get; set; } = false;

        private GuardBee context;

        IEnumerator<(double X, double Y)> _guardBeePath;
        private Point hivePosition;
        public GuardingHiveState(GuardBee context)
        {
            this.context = context;
            hivePosition = Apiary.HivePositions[context.Hive.HiveId];
            _guardBeePath = GuardBeeRoute();
        }

        public void Act()
        {
            // TODO проверить что чужая пчела в радиусе не своего улья

            if (!_guardBeePath.MoveNext())
            {
                //IsCompleted = true;
                _guardBeePath.Dispose();
                return;
            }
            var newPosition = _guardBeePath.Current;

            context.UpdatePosition(new Point((int)newPosition.X, (int)newPosition.Y));
        }

        public IState NextState()
        {
            throw new NotImplementedException();
        }



        private IEnumerator<(double X, double Y)>? _routeAroundHive;
        public IEnumerator<(double X, double Y)> GuardBeeRoute()
        {
            _routeAroundHive = RouteAroundHive(hivePosition);
            var routetoradius = RouteToRadius(context.Position);
            
            while(routetoradius.MoveNext())
            {
                yield return routetoradius.Current;
            }

            var routeAroundHive = RouteAroundHive(hivePosition);

            while(routeAroundHive.MoveNext())
            {
                yield return routeAroundHive.Current;
            }
        }
        private static IEnumerator<(double X, double Y)> RouteAroundHive(Point hivePosition)
        {
            const int radius = 5;
            const double step = 0.1;
            int stepNumber = 10;

            var xCoords = Enumerable.Range(hivePosition.X - radius, 2 * radius + 1)
                         .SelectMany(t =>
                         {
                             List<double> seqs = [];
                             for (int i = 0; i <= stepNumber; i++)
                             {
                                 seqs.Add(t + i * step);
                             }
                             return seqs;
                         });

            var yCoords = Enumerable.Range(hivePosition.Y - radius, 2 * radius + 1)
                                           .SelectMany(t =>
                                           {
                                               List<double> seqs = [];
                                               for (int i = 0; i <= stepNumber; i++)
                                               {
                                                   seqs.Add(t + i * step);
                                               }
                                               return seqs;
                                           });

            const double epsilon = 2;

            var circleCoords = from x in xCoords
                               from y in yCoords
                               where Math.Abs((x - hivePosition.X) * (x - hivePosition.X) +
                               (y - hivePosition.Y) * (y - hivePosition.Y) - radius * radius) < epsilon
                               select (x, y);

            var deb = circleCoords.ToList();
            var d1 = xCoords.ToList();
            var d2 = yCoords.ToList();

            var route = circleCoords.GetEnumerator();

            while(true)
            {
                if(route.MoveNext())
                {
                    yield return route.Current;
                }
                else
                {
                    route.Reset();
                }
            }
        }

        private IEnumerator<(double X, double Y)> RouteToRadius(Point initialPosition)
        {
            _routeAroundHive!.MoveNext();

            var destinationPosition = _routeAroundHive.Current;

            (double X, double Y) currentPosition = (initialPosition.X, initialPosition.Y);

            int stepX = initialPosition.X < destinationPosition.X ? 1 : -1;
            int stepY = initialPosition.Y < destinationPosition.Y ? 1 : -1;

            while (currentPosition != destinationPosition)
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
                currentPosition = (x, y);
                yield return currentPosition;
            }

        }
    }
}
