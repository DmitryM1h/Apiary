using ApiaryEngine.abstractions;
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

        public ChannelReader<IActorState> _stateReader => _statesBus.Reader;

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

                var queenBee = new QueenBee(hive);

                List<WorkerBee> workers = [];

                var guardBee = new GuardBee(hive);

                for (int j = 0; j < 3; j++)
                {
                    var bee = new WorkerBee(i);
                    actors.Add(bee);
                }

                hives.Add(hive);

                actors.Add(queenBee);
                actors.Add(guardBee);
            }

            var hivesArr = hives.ToArray();

            Apiary.SetHives(hivesArr);

            var beeKeeper = new BeeKeeper(hivesArr);

            actors.Add(beeKeeper);

            
            var _timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));

            while(await _timer.WaitForNextTickAsync(_cts))
            {
                var flowersStates = Apiary.Flowers.Values.Select(t => t.GetState());

                foreach (var flowerState in flowersStates)
                    await _statesBus.Writer.WriteAsync(flowerState);


                for (int i = 0; i < actors.Count; i++)
                {
                    actors[i].Tick();

                    var actorsEvents = ActorsEvents.ReadEvents();

                    var actorState = actors[i].GetState();

                    await HandleEvents(actorsEvents);

                    await _statesBus.Writer.WriteAsync(actorState);

                }
            }

        }

        public async Task HandleEvents(IEnumerable<IEvent> events)
        {
            foreach(var @event in events)
            {
                switch(@event)
                {
                    case BeeWasBornEvent ev:
                        actors.Add(new WorkerBee(ev.HiveId));
                        break;

                    //case FlowerRefreshedEvent ev:
                    //    await _statesBus.Writer.WriteAsync(ev);
                    //    break;

                }
            }
        }
    }
}
