using ApiaryEngine.Domain;
using ApiaryEngine.Domain.Bees.QueenBee;
using ApiaryEngine.Domain.Bees.WorkerBee;

namespace ApiaryEngine
{
    public class Engine
    {
        TaskCompletionSource src = new();

        CancellationTokenSource cts = new();

        public Engine()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => src.SetResult());
        }

        public Task Run()
        {

            List<Hive> hives = new();

            for (int i = 0; i < 5; i++)
            {
                var hive = new Hive(i);

                var queenBee = new QueenBee(hive);

                List<WorkerBee> workers = new();

                for (int j = 0; j < 3; j++)
                {
                    var bee = new WorkerBee(i);
                }

                hives.Add(hive);
            }

            var hivesArr = hives.ToArray();

            Apiary.SetHives(hivesArr);

            var beeKeeper = new BeeKeeper(hivesArr);


            return src.Task;
        }
    }
}
