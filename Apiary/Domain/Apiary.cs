using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Bees;

namespace ApiaryEngine.Domain
{
    public class FlowerState : IActorState
    {
        public int FlowerId { get; set; }
        public Point Position { get; set; }
        public int NectarAmount { get; set; }
        public ActorType ActorType { get; init; } = ActorType.Flower;
    }
    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }


    }
    public record class Flower(int Id, Point position) : IActor
    {
        private volatile int _nectarAmount = 100;
        public int NectarAmount => _nectarAmount;
        public Point Position { get; set; } = position;

        private CancellationTokenSource cts = new();
        public int GetHoney(int honeyAmount)
        {
            if (honeyAmount > NectarAmount)
                throw new ArgumentException("Not enough honey");
            _nectarAmount -= honeyAmount;

            if (NectarAmount == 0 && _isRefreshing == false)
            {
                Refresh();
            }
            if (NectarAmount > 0 && _isRefreshing == true)
            {
                cts.Cancel();
                cts.Dispose();
                cts = new();
            }
            return honeyAmount;
        }
        private volatile bool _isRefreshing = false;
        private void Refresh()
        {
            _isRefreshing = true;
            cts.CancelAfter(TimeSpan.FromSeconds(15));
            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(5000, cts.Token); // начнем регенерацию через 5 секунд
                    while (!cts.Token.IsCancellationRequested && _nectarAmount < 100)
                    {
                        await Task.Delay(1000);
                        _nectarAmount += 10;
                    }
                    Console.WriteLine($"Flower has been refreshed (id = {Id})");
                }
                catch(OperationCanceledException) {}
                catch (Exception ex)
                {
                    Console.WriteLine("Critical Exception while refreshing flower");
                }
                finally
                {
                    _isRefreshing = false;
                    cts?.Dispose();
                    cts = new();
                }
            });
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }

        public IActorState GetState()
        {
            return new FlowerState() { FlowerId = Id, Position = Position, NectarAmount = _nectarAmount };
        }
    }
    public static class Apiary
    {

        private static Dictionary<int, Point> _hivePositions = new Dictionary<int, Point>
        {
            { 1, new Point(15, 15) },
            { 2, new Point(25, 20) },
            { 3, new Point(35, 30) },
            { 4, new Point(45, 25) },
            { 5, new Point(55, 35) },
            { 6, new Point(65, 20) },
            { 7, new Point(75, 30) },
            { 8, new Point(85, 15) },
            { 9, new Point(20, 50) },
            { 10, new Point(40, 55) }
        };

        private static Dictionary<int, Point> _flowerPositions = new Dictionary<int, Point>
        {
            { 0, new Point(10, 10) },
            { 1, new Point(30, 15) },
            { 2, new Point(50, 10) },
            { 3, new Point(70, 15) },
            { 4, new Point(90, 10) },
            { 5, new Point(15, 40) },
            { 6, new Point(35, 45) },
            { 7, new Point(55, 40) },
            { 8, new Point(75, 45) },
            { 9, new Point(95, 40) },
            { 10, new Point(10, 60) },
            { 11, new Point(30, 65) },
            { 12, new Point(50, 60) },
            { 13, new Point(70, 65) },
            { 14, new Point(90, 60) },
            { 15, new Point(15, 80) },
            { 16, new Point(35, 85) },
            { 17, new Point(55, 80) },
            { 18, new Point(75, 85) },
            { 19, new Point(95, 80) }
        };

        private static Dictionary<int, Flower> _flowers = new Dictionary<int, Flower>
{
    { 0, new Flower(0, _flowerPositions[0]) },
    { 1, new Flower(1, _flowerPositions[1]) },
    { 2, new Flower(2, _flowerPositions[2]) },
    { 3, new Flower(3, _flowerPositions[3]) },
    { 4, new Flower(4, _flowerPositions[4]) },
    { 5, new Flower(5, _flowerPositions[5]) },
    { 6, new Flower(6, _flowerPositions[6]) },
    { 7, new Flower(7, _flowerPositions[7]) },
    { 8, new Flower(8, _flowerPositions[8]) },
    { 9, new Flower(9, _flowerPositions[9]) },
    { 10, new Flower(10, _flowerPositions[10]) },
    { 11, new Flower(11, _flowerPositions[11]) },
    { 12, new Flower(12, _flowerPositions[12]) },
    { 13, new Flower(13, _flowerPositions[13]) },
    { 14, new Flower(14, _flowerPositions[14]) },
    { 15, new Flower(15, _flowerPositions[15]) },
    { 16, new Flower(16, _flowerPositions[16]) },
    { 17, new Flower(17, _flowerPositions[17]) },
    { 18, new Flower(18, _flowerPositions[18]) },
    { 19, new Flower(19, _flowerPositions[19]) }
};
        public static IReadOnlyDictionary<int, Point> HivePositions => _hivePositions.AsReadOnly();
        public static IReadOnlyDictionary<int, Point> FlowerPositions => _flowerPositions.AsReadOnly();
        public static IReadOnlyDictionary<int, Flower> Flowers => _flowers.AsReadOnly();

        private static Hive[] _hives = null!;

        public static void SetHives(Hive[] hives)
        {
            _hives = hives;
        }

        public static Hive? FindHive(int hiveId)
        {
            return _hives.Where(t => t.HiveId == hiveId).FirstOrDefault();
        }

        public static Flower? FindFlower(int flowerId)
        {
            return _flowers.TryGetValue(flowerId, out var flower) ? flower : null;
        }

        public static IEnumerable<Flower> GetAllFlowers()
        {
            return _flowers.Values;
        }
    }
}