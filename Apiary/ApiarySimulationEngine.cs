using ApiaryEngine.abstractions;
using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain;
using ApiaryEngine.Domain.Bees.WorkerBee;
using System.Threading.Channels;

namespace ApiaryEngine
{
    public class ApiarySimulationEngine
    {
        private readonly CancellationToken _cts;

        private readonly List<IActor> actors = [];

        private readonly Channel<IActorState> _statesBus = Channel.CreateBounded<IActorState>(1000);

        public ChannelReader<IActorState> _stateReader => _statesBus.Reader;

        public ApiarySimulationEngine(CancellationToken cts = default)
        {
            _cts = cts;
        }

        public async Task Run()
        {
            List<Hive> hives = new();

            for (int i = 1; i < 5; i++) // пока 5. больше 10 нельзя
            {
                var hive = new Hive(i);

                //var queenBee = new QueenBee(hive);

                List<WorkerBee> workers = new();

               // var guardBee = new GuardBee(hive);

                for (int j = 0; j < 3; j++)
                {
                    var bee = new WorkerBee(i);
                    actors.Add(bee);
                }

                hives.Add(hive);

               // actors.Add(queenBee);
                //actors.Add(guardBee);
            }

            var hivesArr = hives.ToArray();

            Apiary.SetHives(hivesArr);

            // var beeKeeper = new BeeKeeper(hivesArr);

            // actors.Add(beeKeeper);

            var _timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));

            while(await _timer.WaitForNextTickAsync(_cts))
            {
                for(int i = 0; i < actors.Count; i++)
                {
                    actors[i].Tick();

                    var actorState = actors[i].GetState();

                    await _statesBus.Writer.WriteAsync(actorState);
                }
            }

        }
    }
}
