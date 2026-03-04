using Apiary.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apiary
{
    public class Hive
    {
        public int UnitsOfHoney { get; private set; }

        private QueenBee _queenBee;

        private List<Bee> _bees = [];

        public Hive()
        {
            Func<int, Task<int>> DecreasyHoneyAction = (int amount) => DecreaseHoneyAsync(amount); 

            _queenBee = new(_bees, DecreaseHoneyAsync);
        }

        public async Task<int> DecreaseHoneyAsync(int amount)
        {
            if (amount > UnitsOfHoney)
                throw new OutOfHoneyException();

            //await Locks.HiveLocker.WaitAsync();

            Locks.HiveLock.Enter()


            UnitsOfHoney -= amount;

            return amount;

        }

        //private async Task<int> IncreaseHoneyAsync(int amount) 
        //{
        //    await Locks.HiveLocker.WaitAsync();

        //    UnitsOfHoney += amount;

        //    return amount;

        //}

    }
}
