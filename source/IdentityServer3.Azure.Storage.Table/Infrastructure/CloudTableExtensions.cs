using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace IdentityServer3.AzureTableStorage.Infrastructure
{
    internal static class CloudTableExtensions
    {
        internal static async Task<IEnumerable<TTableEntity>> ExecuteQueryAsync<TTableEntity>(this CloudTable table, TableQuery<TTableEntity> tableQuery) where TTableEntity : ITableEntity, new()
        {
            var tableEntities = new List<TTableEntity>();

            TableContinuationToken continuationToken = null;
            do
            {
                // Retrieve a segment (up to 1,000 entities).
                var tableQueryResult = await table.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);

                // Assign the new continuation token to tell the service where to
                // continue on the next iteration (or null if it has reached the end).
                continuationToken = tableQueryResult.ContinuationToken;

                tableEntities.AddRange(tableQueryResult.Results);

                // Loop until a null continuation token is received, indicating the end of the table.
            } while (continuationToken != null);

            return tableEntities;
        }

        internal static Task InsertAsync(this CloudTable table, ITableEntity tableEntity)
        {
            return table.ExecuteAsync(TableOperation.Insert(tableEntity));
        }

        internal static async Task InsertAsync(this CloudTable table, IEnumerable<ITableEntity> tableEntities)
        {
            foreach (var tableEntity in tableEntities)
            {
                await table.InsertAsync(tableEntity);
            }
        }
    }
}