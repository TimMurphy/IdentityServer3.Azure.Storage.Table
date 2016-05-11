using System;
using System.Collections.Generic;
using IdentityServer3.Core.Models;
using Microsoft.WindowsAzure.Storage.Table;

namespace IdentityServer3.AzureTableStorage.Infrastructure.Serializers
{
    internal static class ClientConvert
    {
        internal static Client FromTableRow(string partitionkey, string rowkey, DateTimeOffset timestamp, IDictionary<string, EntityProperty> properties, string etag)
        {
            throw new NotImplementedException();
        }
    }
}