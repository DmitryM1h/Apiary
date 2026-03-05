using ApiaryEngine.abstractions;
using ApiaryEngine.Exceptions;
using ApiaryEngine.Helpers;
using ApiaryEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.Bees
{
    public class GuardBee : Bee, IStartable
    {
        private readonly Hive _hive;
        public GuardBee(Hive hive)
        {
            BeeId = IdentityProvider.GetIdentity();
            HiveId = hive.HiveId;
            _hive = hive;

            Task.Run(async () =>
            {
                try
                {
                    await StartAsync(CancellationToken.None);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
