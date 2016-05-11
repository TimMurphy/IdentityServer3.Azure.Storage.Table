using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using NullGuard;
using OpenMagic.Azure.Storage.Table;
using OpenMagic.Extensions.Collections.Generic;

namespace IdentityServer3.Azure.Storage.Table.Stores
{
    /// <summary>
    ///     Retrieve <see cref="Scope" /> information from a Azure Storage Table.
    /// </summary>
    /// <seealso cref="IdentityServer3.Core.Services.IScopeStore" />
    public class ScopeStore : IScopeStore
    {
        private readonly ITable<Scope> _table;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScopeStore" /> class.
        /// </summary>
        /// <param name="table">
        ///     The Azure Storage Table that stores <see cref="Scope">Scopes</see>.
        /// </param>
        public ScopeStore(ITable<Scope> table)
        {
            _table = table;
        }

        /// <summary>
        ///     Gets all scopes.
        /// </summary>
        /// <param name="scopeNames">
        ///     List of scopes to return if they exist.
        /// </param>
        /// <returns>
        ///     List of scopes in the store that exist in <paramref name="scopeNames" />.
        /// </returns>
        public Task<IEnumerable<Scope>> FindScopesAsync([AllowNull] IEnumerable<string> scopeNames)
        {
            return FindScopesAsync(scopeNames?.ToArray() ?? new string[] {});
        }

        /// <summary>
        ///     Gets all defined scopes.
        /// </summary>
        /// <param name="publicOnly">
        ///     if set to <c>true</c> only public scopes
        ///     (<see cref="Scope.ShowInDiscoveryDocument" />) are returned.
        /// </param>
        /// <returns>
        ///     List of all defined scopes.
        /// </returns>
        public async Task<IEnumerable<Scope>> GetScopesAsync(bool publicOnly = true)
        {
            var scopes = await _table.GetAllAsync();

            if (publicOnly)
            {
                // todo: Improve ITable<TEntity>.GetAllAsync() to accept where clause
                scopes = scopes.Where(s => s.ShowInDiscoveryDocument);
            }

            return scopes;
        }

        private async Task<IEnumerable<Scope>> FindScopesAsync(string[] scopeNames)
        {
            // todo: Improve ITable<TEntity>.GetAllAsync() to accept where clause
            var scopes = await GetScopesAsync( /*publicOnly*/ false);

            return scopeNames.IsNullOrEmpty() ?
                scopes :
                scopes.Where(scope => scopeNames.Contains(scope.Name));
        }
    }
}