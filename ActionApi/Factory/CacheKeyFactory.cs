namespace ActionApi.Factory
{
    public static class CacheKeyFactory
    {
        public static string GeneratePersonnelKey(int userId)
        {
            return $"personnel_cache_{userId}";
        }
    }
}
