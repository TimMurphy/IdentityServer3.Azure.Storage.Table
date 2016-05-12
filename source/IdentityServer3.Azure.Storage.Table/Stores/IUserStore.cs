using System.Threading.Tasks;
using IdentityServer3.Azure.Storage.Table.Models;
using IdentityServer3.Core.Models;

namespace IdentityServer3.Azure.Storage.Table.Stores
{
    public interface IUserStore
    {
        Task AddAsync(User user);
        Task<User> FindByCredentialsAsync(string userName, string password);
        Task<User> FindByExternalIdentityAsync(ExternalIdentity externalIdentity);
        Task<User> FindBySubjectAsync(string subject);
        Task<User> GetBySubject(string subject);
    }
}