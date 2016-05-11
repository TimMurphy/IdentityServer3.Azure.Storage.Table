using System.Collections.Generic;
using IdentityServer3.Core.Models;
using Microsoft.WindowsAzure.Storage.Table;

namespace IdentityServer3.Azure.Storage.Table.Specifications.Helpers
{
    public class Given
    {
        public IEnumerable<Scope> Scopes { get; internal set; }
        public bool PublicOnly { get; set; }
        public CloudTable ScopesTable { get; set; }
        public Scope Scope { get; set; }
        public DynamicTableEntity DynamicTableEntity { get; set; }
    }
}