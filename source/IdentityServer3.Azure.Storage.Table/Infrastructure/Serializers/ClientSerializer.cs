using System;
using IdentityServer3.Core.Models;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using OpenMagic.Azure.Storage.Table;

namespace IdentityServer3.Azure.Storage.Table.Infrastructure.Serializers
{
    public class ClientSerializer : IDynamicTableEntitySerializer<Client>
    {
        public Client Deserialize(DynamicTableEntity row)
        {
            // todo: use columns
            return JsonConvert.DeserializeObject<Client>(row["Client"].StringValue);
        }

        public DynamicTableEntity Serialize(Client row)
        {
            throw new NotImplementedException();
        }
    }
}