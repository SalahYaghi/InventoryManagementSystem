namespace Contract.Common.Constants
{
    public static class CacheKeys
    {
        public static string ForEntityById(string group, string entity, string queryName, object id)
            => $"{group}:{entity}:{queryName}:{id}";

        public static string ForEntityPaged(string group, string entity, string queryName, int pageNumber, int pageSize)
            => $"{group}:{entity}:{queryName}:{pageNumber}:{pageSize}";

        public static string ForEntityList(string group, string entity, string queryName)
         => $"{group}:{entity}:{queryName}";

        public static string ForEntityList(string group, string entity, string queryName, params object?[] discriminators)
        {
            if (discriminators is null || discriminators.Length == 0)
                return ForEntityList(group, entity, queryName);

            return $"{group}:{entity}:{queryName}:{string.Join(':', discriminators.Select(d => d?.ToString() ?? "null"))}";
        }
    }
}
