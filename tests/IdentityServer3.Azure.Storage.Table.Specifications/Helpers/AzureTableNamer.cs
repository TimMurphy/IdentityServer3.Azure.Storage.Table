namespace IdentityServer3.Azure.Storage.Table.Specifications.Helpers
{
    internal static class AzureTableNamer
    {
        internal static string GetTableName<TEntity>()
        {
            return $"is3{typeof(TEntity).Name}s";
        }
    }
}