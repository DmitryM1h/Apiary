using System;
using System.Collections.Generic;
using System.Text;

namespace Apiary
{
  
    public class Apiary
    {
        private BeeKeeper _beeKeeper;
        private Hive[] _hives;

        public Apiary()
        {
            _hives = new Hive[5];
            _beeKeeper = new BeeKeeper(_hives);
        }

    }

    
 
  
    
}
