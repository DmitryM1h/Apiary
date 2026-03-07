using ApiaryEngine.abstractions;
using ApiaryEngine.Helpers;
using ApiaryEngine.Interfaces;


namespace ApiaryEngine.Domain.Bees
{
    public class QueenBee : Bee, IStartable
    {
        private const int _amountOfHoneyToBornBee = 1000;

        private readonly Hive _hive;

        Random _rnd = new();

        public QueenBee(Hive hive)
        {
            _hive = hive;
            BeeId = IdentityProvider.GetIdentity();
            HiveId = hive.HiveId;

            Task.Run(async () =>
            {
                try
                {
                    await StartAsync(CancellationToken.None);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        private async Task TryProduceNewBees()
        {
            int desiredNumOfBees = _rnd.Next(1, 10);

            for (int i = 0; i < desiredNumOfBees; i++)
            {
                if (_hive.Honey < _amountOfHoneyToBornBee)
                    break;

                await CreateBee();
            }

        }

        public async ValueTask CreateBee()
        {
            var honey = _hive.TryTakeHoney(_amountOfHoneyToBornBee); // взять ресурс

            Console.WriteLine($"Королева (ID= {BeeId}), (HiveID= {HiveId}) взяла мед {_amountOfHoneyToBornBee}!");

            if (honey == -1)
                return;

            await Task.Delay(TimeSpan.FromSeconds(10)); // процесс занимает время

            var workerBee = new WorkerBee(HiveId);

            Console.WriteLine($"Королева (ID= {BeeId}) родила пчелку!");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(15), cancellationToken);

                await TryProduceNewBees();

            }
        }
    }
}
