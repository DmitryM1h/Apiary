using System;
using System.Collections.Generic;
using System.Text;

namespace Apiary.Exceptions
{
    public class OutOfHoneyException : Exception
    {
        public const string errorMessage = "No more honey";
        public OutOfHoneyException() : base(errorMessage) { }
    }
}
