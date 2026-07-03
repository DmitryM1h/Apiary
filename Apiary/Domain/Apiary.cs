using System.Linq.Expressions;

namespace ApiaryEngine.Domain
{
    public readonly record struct Point(int X, int Y);
    public record class Flower(int Id, int HoneyAmount = 100);
    public static class Apiary
    {
        private static int Length = 100;
        private static int Width = 100;

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
            { 0, new Flower(0, 100) },
            { 1, new Flower(1, 100) },
            { 2, new Flower(2, 100) },
            { 3, new Flower(3, 100) },
            { 4, new Flower(4, 100) },
            { 5, new Flower(5, 100) },
            { 6, new Flower(6, 100) },
            { 7, new Flower(7, 100) },
            { 8, new Flower(8, 100) },
            { 9, new Flower(9, 100) },
            { 10, new Flower(10, 100) },
            { 11, new Flower(11, 100) },
            { 12, new Flower(12, 100) },
            { 13, new Flower(13, 100) },
            { 14, new Flower(14, 100) },
            { 15, new Flower(15, 100) },
            { 16, new Flower(16, 100) },
            { 17, new Flower(17, 100) },
            { 18, new Flower(18, 100) },
            { 19, new Flower(19, 100) }
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
    }
}