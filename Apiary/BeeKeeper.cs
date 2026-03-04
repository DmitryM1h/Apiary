using Apiary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apiary
{
    public class BeeKeeper : IStartable
    {
        public int _unitsOfHoney = 0;

        private const int _minutesToVisit = 1;

        public readonly Hive[] _hives;

        public BeeKeeper(Hive[] hives)
        {
            _hives = hives;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(_minutesToVisit);

                await VisitHives();
            }
        }

        private async Task VisitHives()
        {
            foreach (var hive in _hives)
            {
                var honeyToTake = hive.UnitsOfHoney / 10;

                _unitsOfHoney += await hive.DecreaseHoneyAsync(honeyToTake);
            }

        }
    }

}
