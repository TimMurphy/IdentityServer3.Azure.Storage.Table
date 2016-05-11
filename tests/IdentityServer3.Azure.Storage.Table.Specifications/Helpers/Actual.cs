using IdentityServer3.Core.Models;
using Microsoft.WindowsAzure.Storage.Table;

namespace IdentityServer3.Azure.Storage.Table.Specifications.Helpers
{
    public class Actual
    {
        public Scope[] Scopes { get; set; }
        public DynamicTableEntity DynamicTableEntity { get; set; }
        public Scope Scope { get; set; }
    }
}