using System.Collections.Concurrent;

namespace ApiaryEngine.Domain
{
    public class Hive
    {
        public int HiveId { get; init; }

        private readonly ConcurrentDictionary<int, int> HoneyByBee = new();
        public int Honey => HoneyByBee.Values.ToList().Sum();

        public Hive(int hiveId)
        {
            HiveId = hiveId;
        }


        public int TryTakeHoney(int amount)
        {
            if (amount > Honey)
                return -1;

            int collectedHoney = 0;
            int remainingToTake = amount;

            foreach (var beeId in HoneyByBee.Keys.ToList())
            {
                if (remainingToTake <= 0)
                    break;

                HoneyByBee.AddOrUpdate(beeId,
                    key => 0,
                    (key, oldValue) =>
                    {
                        if (oldValue > 0 && remainingToTake > 0)
                        {
                            int takeFromBee = Math.Min(oldValue, remainingToTake);
                            collectedHoney += takeFromBee;
                            remainingToTake -= takeFromBee;
                            return oldValue - takeFromBee;
                        }
                        return oldValue;
                    });
            }

            return collectedHoney;
        }

        public void IncreaseHoney(int beeId, int amount)
        {

            HoneyByBee.AddOrUpdate(beeId,
                amount,
                (key, currentValue) => { return currentValue + amount; });


            Console.WriteLine($"Теперь в улье (ID= {HiveId}) {Honey} ед. меда!");

        }
    }
}
