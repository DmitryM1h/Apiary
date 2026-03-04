using Apiary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apiary
{
    public class Bee : IStartable
    {

        Random _rnd = new Random();
        private int _secondsToAct => _rnd.Next(1, 20);

        public int CollectedUnitsOfHoney { get; private set; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(_secondsToAct);

                ProduceHoney();
            }
        }

        private void ProduceHoney()
        {
            CollectedUnitsOfHoney += _rnd.Next(10, 100);
        }
    }

}
