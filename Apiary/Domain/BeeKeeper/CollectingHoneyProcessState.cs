using ApiaryEngine.Abstractions;

namespace ApiaryEngine.Domain.BeeKeeper.States
{
    public class CollectingHoneyProcessState : IState
    {
        public bool IsCompleted { get; set; } = false;
        private readonly BeeKeeper _context;
        private readonly Hive _hive;
        private IEnumerator<int> _collectingProcess;

        public CollectingHoneyProcessState(Hive hive)
        {
            _context = (BeeKeeper)ApplicationContext.Context.Value!;
            _hive = hive;
            _collectingProcess = CollectHoney();
        }

        public void Act()
        {
            if (!_collectingProcess.MoveNext())
            {
                IsCompleted = true;
                _collectingProcess.Dispose();
                return;
            }

            var honeyCollected = _collectingProcess.Current;
            if (honeyCollected > 0)
            {
                _context.CollectHoneyFromHive(honeyCollected);
            }
        }

        private IEnumerator<int> CollectHoney()
        {
            while (_hive.Honey > 0)
            {
                int collected = 0;

                if (_hive.Honey >= 10)
                {
                    if (_hive.TryTakeHoney(10, out int? honey))
                    {
                        collected = honey ?? 0;
                    }
                }
                else
                {
                    if (_hive.TryTakeHoney(1, out int? honey))
                    {
                        collected = honey ?? 0;
                    }
                }

                if (collected > 0)
                {
                    yield return collected;
                }
                else
                {
                    break; // Если не удалось собрать - выходим
                }
            }
        }

        public IState NextState()
        {
            return new ReturnToBaseState(_context);
        }
    }
}