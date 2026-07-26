namespace ApiaryEngine.Domain
{
    public class Hive
    {
        public int HiveId { get; init; }

        private readonly Dictionary<int, int> HoneyByBee = new();
        public int Honey => HoneyByBee.Values.Sum();

        public readonly Dictionary<int, bool> BeesInHoney = new();

        public Hive(int hiveId)
        {
            HiveId = hiveId;
        }

        public bool TryTakeHoney(int amount, out int? honey)
        {
            honey = null;

            if (amount > Honey)
                return false;

            int collectedHoney = 0;
            int remainingToTake = amount;

            foreach (var beeId in HoneyByBee.Keys.ToList())
            {
                if (remainingToTake <= 0)
                    break;

                if (HoneyByBee.TryGetValue(beeId, out int currentValue) && currentValue > 0)
                {
                    int takeFromBee = Math.Min(currentValue, remainingToTake);
                    collectedHoney += takeFromBee;
                    remainingToTake -= takeFromBee;
                    HoneyByBee[beeId] = currentValue - takeFromBee;
                }
            }
            honey = collectedHoney;
            return true;
        }

        public void IncreaseHoney(int beeId, int amount)
        {
            if (HoneyByBee.ContainsKey(beeId))
                HoneyByBee[beeId] += amount;
            else
                HoneyByBee[beeId] = amount;

            Console.WriteLine($"Теперь в улье (ID= {HiveId}) {Honey} ед. меда!");
        }
    }
}