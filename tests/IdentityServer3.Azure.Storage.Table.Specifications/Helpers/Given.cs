using System.Collections.Generic;
using IdentityServer3.Azure.Storage.Table.Infrastructure;
using IdentityServer3.Azure.Storage.Table.Stores;
using IdentityServer3.Core.Models;
using Microsoft.WindowsAzure.Storage.Table;
using OpenMagic.Azure.Storage.Table;

namespace IdentityServer3.Azure.Storage.Table.Specifications.Helpers
{
    public class Given
    {
        public IEnumerable<Scope> Scopes { get; internal set; }
        public bool PublicOnly { get; set; }
        public CloudTable ScopesTable { get; set; }
        public Scope Scope { get; set; }
        public DynamicTableEntity DynamicTableEntity { get; set; }
        public string[] ScopeNames { get; set; }
        public ScopeStore ScopeStore => new ScopeStore(new Table<Scope>(AzureTableProvider.ConnectionString, ScopesTable.Name, DynamicTableEntitySerializers.ScopeSerializer));
        public ClientStore ClientStore => new ClientStore(new Table<Client>(AzureTableProvider.ConnectionString, ClientsTable.Name, DynamicTableEntitySerializers.ClientSerializer));
        public IEnumerable<Client> Clients { get; set; }
        public CloudTable ClientsTable { get; set; }
        public string ClientId { get; set; }
    }
}