namespace ApiaryEngine.Helpers
{
    public static class IdentityProvider
    {
        private static int beeCounter = 0;

        public static int GetIdentity()
        {
            return Interlocked.Increment(ref beeCounter);
        }
    }
}
