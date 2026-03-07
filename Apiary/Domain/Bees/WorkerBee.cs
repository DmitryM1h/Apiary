using ApiaryEngine.abstractions;
using ApiaryEngine.Exceptions;
using ApiaryEngine.Helpers;
using ApiaryEngine.Interfaces;

namespace ApiaryEngine.Domain.Bees
{
    public class WorkerBee : Bee, IStartable
    {

        Random _rnd = new Random();
        private int _secondsToAct => _rnd.Next(10, 20);

        public int CollectedHoney { get; private set; }


        public WorkerBee(int hiveId)
        {
            BeeId = IdentityProvider.GetIdentity();
            HiveId = hiveId;


            // todo переделать на 1 поток, который обслуживает все сущности, а не создавать каждой свой
            Task.Run(async () =>
            {
                try
                {
                    await StartAsync(CancellationToken.None);
                }
                catch (LostBeeException ex)
                {
                    Console.WriteLine($"Пчелка с (ID= {BeeId}) потеряла улей :c ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }



        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(_secondsToAct));

                ProduceHoney();

                DeliverToHive();
            }
        }

        public void DeliverToHive()
        {
            Hive? hive = Apiary.FindHive(HiveId);

            if (hive is null)
                throw new LostBeeException();

            hive.IncreaseHoney(BeeId, CollectedHoney);

            CollectedHoney = 0;
        }

        private void ProduceHoney()
        {
            CollectedHoney += _rnd.Next(100, 900);

            Console.WriteLine($"Пчелка (ID= {BeeId}) (HiveID= {HiveId}) собрала {CollectedHoney} меда!");

        }
    }

}
