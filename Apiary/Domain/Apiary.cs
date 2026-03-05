namespace ApiaryEngine.Domain
{

    public static class Apiary
    {

        private static Hive[] _hives = null!;

        public static void SetHives(Hive[] hives)
        {
            _hives = hives;
        }

        public static Hive? FindHive(int hiveId)
        {
            return _hives.Where(t => t.HiveId == hiveId).FirstOrDefault();
        }

    }





}
