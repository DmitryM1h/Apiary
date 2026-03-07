using ApiaryEngine.abstractions;
using ApiaryEngine.Helpers;
using ApiaryEngine.Interfaces;


namespace ApiaryEngine.Domain.Bees.QueenBee
{
    public class QueenBee : Bee, ITickable
    {
        private const int _amountOfHoneyToBornBee = 1000;

        QueenContext _queenState;

        private readonly Hive _hive;

        public QueenBee(Hive hive)
        {
            _hive = hive;
            BeeId = IdentityProvider.GetIdentity();
            HiveId = hive.HiveId;
            _queenState = new();
        }


        public async ValueTask CreateBee()
        {
            if (_queenState!.producingBeeState!.CollectedHoney == 0)
            {

                var honey = _hive.TryTakeHoney(_amountOfHoneyToBornBee); // взять ресурс

                if (honey == -1)
                {
                    _queenState.SwitchState();
                    return;
                }
                else
                    _queenState.UpdateCollectedHoney(honey);

                Console.WriteLine($"Королева (ID= {BeeId}), (HiveID= {HiveId}) взяла мед {_amountOfHoneyToBornBee}!");

            }

            if (_queenState.producingBeeState.Finished == false)
            {
                _queenState.InProcessOfProducing();
            }

            if (_queenState!.producingBeeState!.IsFinished())
            {
                var workerBee = new WorkerBee(HiveId);

                Console.WriteLine($"Королева (ID= {BeeId}) родила пчелку!");

                _queenState.producingBeeState.FinishProducing();

                _queenState.SwitchState();
            }
        }

    


        public async Task Tick()
        {
            if (_queenState.CanStartProducing)
            {
                await CreateBee();

                _queenState.SwitchState();
            }

        }
    }


   

    

}
