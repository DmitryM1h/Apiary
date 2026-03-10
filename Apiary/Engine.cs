using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain;
using ApiaryEngine.Domain.Bees;

namespace ApiaryEngine
{
    public class Engine
    {
        TaskCompletionSource src;

        CancellationTokenSource cts;


        List<ITickable> actors = new();

        public Engine()
        {
            src = new TaskCompletionSource();
            cts = new CancellationTokenSource();
            cts.Token.Register(() => src.SetResult());
        }

        public async Task Run()
        {

            List<Hive> hives = new();

            for (int i = 0; i < 5; i++)
            {
                var hive = new Hive(i);

                var queenBee = new QueenBee(hive);

                List<WorkerBee> workers = new();

                var guardBee = new GuardBee(hive);

                for (int j = 0; j < 3; j++)
                {
                    var bee = new WorkerBee(i);
                    actors.Add(bee);
                }

                hives.Add(hive);

                actors.Add(queenBee);
                actors.Add(guardBee)
            }

            var hivesArr = hives.ToArray();

            Apiary.SetHives(hivesArr);

            var beeKeeper = new BeeKeeper(hivesArr);

            actors.Add(beeKeeper);

            while(true)
            {
                foreach (var liver in actors)
                {
                    await liver.Tick();

                    await Task.Delay(100);
                }
            }

        }
    }
}
