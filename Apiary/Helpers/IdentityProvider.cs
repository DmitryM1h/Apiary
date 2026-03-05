namespace ApiaryEngine.Helpers
{
    public static class IdentityProvider
    {
        private static int beeCounter = 0;

        public static int GetIdentity()
        {
            Console.WriteLine($"Кол-во пчелок на данный момент {beeCounter + 1}");

            return Interlocked.Increment(ref beeCounter);
        }
    }
}
