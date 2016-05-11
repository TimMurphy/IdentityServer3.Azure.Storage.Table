using System.Linq;
using System.Threading.Tasks;
using Anotar.LibLog;
using EmptyStringGuard;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using NullGuard;
using OpenMagic.Azure.Storage.Table;

namespace IdentityServer3.Azure.Storage.Table.Stores
{
    public class ClientStore : IClientStore
    {
        private readonly ITable<Client> _table;

        public ClientStore(ITable<Client> table)
        {
            _table = table;
        }

        [return: AllowNull]
        public async Task<Client> FindClientByIdAsync([AllowNull, AllowEmpty] string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                return null;
            }

            var clients = await _table.FindByPartitionKeyAsync(clientId);
            var client = clients.SingleOrDefault();

            if (client == null)
            {
                LogTo.Warn($"Cannot find {nameof(clientId)} '{clientId}'.");
                return null;
            }

            if (client.Enabled)
            {
                return client;
            }

            LogTo.Warn($"{nameof(clientId)} '{clientId}' is disabled.");
            return null;
        }
    }
}