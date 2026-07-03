//using ApiaryEngine.Abstractions;

//namespace ApiaryEngine.Domain
//{
//    public class BeeKeeper : ITickable
//    {
//        public int _collectedHoney = 0;

//        private const int _secondsToVisit = 30;

//        public readonly Hive[] _hives;

//        public BeeKeeper(Hive[] hives)
//        {
//            _hives = hives;

//            Task.Run(async () =>
//            {
//                try
//                {
//                    await StartAsync(CancellationToken.None);
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine(ex.Message);
//                }
//            });
//        }

//        public void Tick()
//        {
//            return;
//        }

//        public async Task StartAsync(CancellationToken cancellationToken)
//        {
//            while (!cancellationToken.IsCancellationRequested)
//            {
//                await Task.Delay(TimeSpan.FromSeconds(_secondsToVisit));

//                VisitHives();
//            }
//        }

//        private void VisitHives()
//        {
//            foreach (var hive in _hives)
//            {
//                var honeyToTake = hive.Honey / 10;

//                var honey = hive.TryTakeHoney(honeyToTake);

//                if (honey == -1)
//                    break;

//                _collectedHoney += honey;

//                Console.WriteLine($"Я собрал {honey} ед. Мёда из улья с Id {hive.HiveId}");
//            }

//        }
//    }

//}
