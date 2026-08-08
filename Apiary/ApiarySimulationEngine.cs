using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain;
using ApiaryEngine.Domain.BeeKeeper;
using ApiaryEngine.Domain.Bees.GuardBeeStates;
using ApiaryEngine.Domain.Bees.QueenBee;
using ApiaryEngine.Domain.Bees.WorkerBee;
using System.Threading.Channels;

namespace ApiaryEngine
{
    public class ApiarySimulationEngine
    {
        private readonly CancellationToken _cts;

        private readonly List<IActor> actors = [];

        private readonly Channel<IActorState> _statesBus = Channel.CreateBounded<IActorState>(1000);
        public ChannelReader<IActorState> GetChannelReader() => _statesBus.Reader;

        private ApplicationContext _applicationContext = new();

        public ApiarySimulationEngine(CancellationToken cts = default)
        {
            _cts = cts;
        }

        public async Task Run()
        {
            List<Hive> hives = [];

            for (int i = 1; i < 5; i++) // пока 5. больше 10 нельзя
            {
                var hive = new Hive(i);

                for (int j = 0; j < 3; j++)
                {
                    var bee = new WorkerBee(i);
                    actors.Add(bee);
                }

                hives.Add(hive);

                var queenBee = new QueenBee(hive);
                actors.Add(queenBee);

                var guardBee = new GuardBee(hive);
                actors.Add(guardBee);
            }

            var hivesArr = hives.ToArray();

            Apiary.SetHives(hivesArr);

            var beeKeeper = new BeeKeeper(hivesArr);

            actors.Add(beeKeeper);


            for (int i = 0; i < actors.Count; i++)
            {
                _ = RunActor(actors[i]);
            }

            var _timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));

            while (await _timer.WaitForNextTickAsync(_cts))
            {
                var flowersStates = Apiary.Flowers.Values.Select(t => t.GetState());

                foreach (var flowerState in flowersStates)
                    await _statesBus.Writer.WriteAsync(flowerState);

                var actorsEvents = ActorsEvents.ReadEvents();

                await HandleEvents(actorsEvents);
            }

        }

        public Task RunActor(IActor actor)
        {
            return Task.Run(async () =>
            {
                _applicationContext.SetActor(actor);

                var _timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));

                while (await _timer.WaitForNextTickAsync(_cts))
                {
                    actor.Tick();

                    var actorState = actor.GetState();

                    await _statesBus.Writer.WriteAsync(actorState);

                }
            }, _cts);
        }

        public async Task HandleEvents(IEnumerable<IEvent> events)
        {
            foreach(var @event in events)
            {
                switch(@event)
                {
                    case BeeWasBornEvent ev:
                        var actor = new WorkerBee(ev.HiveId);
                        actors.Add(actor);
                        _ = RunActor(actor);
                            
                        break;

                }
            }
        }
    }
}
