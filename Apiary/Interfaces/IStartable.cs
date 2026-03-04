using System;
using System.Collections.Generic;
using System.Text;

namespace Apiary.Interfaces
{
    public interface IStartable
    {
        Task StartAsync(CancellationToken cancellationToken);
    }
}
