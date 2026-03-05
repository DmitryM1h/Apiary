namespace ApiaryEngine.Exceptions
{
    public class OutOfHoneyException : Exception
    {
        public const string errorMessage = "No more honey";
        public OutOfHoneyException() : base(errorMessage) { }
    }
}
