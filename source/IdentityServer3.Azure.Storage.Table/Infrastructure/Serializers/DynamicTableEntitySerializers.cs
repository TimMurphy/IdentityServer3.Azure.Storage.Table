using System.Diagnostics.CodeAnalysis;
using IdentityServer3.Core.Models;
using OpenMagic.Azure.Storage.Table;

namespace IdentityServer3.Azure.Storage.Table.Infrastructure.Serializers
{
    [SuppressMessage("ReSharper", "ArgumentsStyleAnonymousFunction")]
    [SuppressMessage("ReSharper", "ArgumentsStyleOther")]
    public static class DynamicTableEntitySerializers
    {
        public static DynamicTableEntitySerializer<Scope> ScopeSerializer => new DynamicTableEntitySerializer<Scope>(
            entityFactory: (partitionKey, rowKey) => new Scope {Name = partitionKey},
            partitionKeyFactory: scope => scope.Name,
            rowKeyFactory: scope => string.Empty,
            ignoreProperties: new[] {nameof(Scope.Name)});
    }
}