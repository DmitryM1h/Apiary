using System;
using System.Collections.Generic;
using System.Text;

namespace Apiary
{
    public static class Locks
    {
        public static SemaphoreSlim HiveLocker = new(1,1);

        public static Lock BeeKeepersLock = new();
        public static Lock QuuenBeesLock = new();
    }
}
