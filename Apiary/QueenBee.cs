using Apiary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apiary
{
    public class QueenBee : IStartable
    {
        private const int _amountOfHoneyToBornBee = 100;

        private List<Bee> _beesInHoney;

        private Func<int, Task<int>> SpendHoneyFromHive;
        public QueenBee(List<Bee> beesInHoney, Func<int, Task<int>> spendHoneyFromHive)
        {
            _beesInHoney = beesInHoney;

         
            SpendHoneyFromHive = spendHoneyFromHive;
        }

        Random _rnd = new();
        private async Task TryProduceNewBees()
        {
            int numOfBees = _rnd.Next(1, 10);

            for (int i = 0; i < numOfBees; i++)
            {
                await Task.Delay(2000);

                await SpendHoneyFromHive(_amountOfHoneyToBornBee);

                _beesInHoney.Add(new Bee());

            }
                
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {

            while(!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);

                await TryProduceNewBees();

            }
        }
    }
}
