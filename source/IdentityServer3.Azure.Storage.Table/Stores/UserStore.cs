using System;
using System.Threading.Tasks;
using IdentityServer3.Azure.Storage.Table.Models;
using IdentityServer3.Core.Models;

namespace IdentityServer3.Azure.Storage.Table.Stores
{
    public class UserStore : IUserStore
    {
        public Task AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByCredentialsAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByExternalIdentityAsync(ExternalIdentity externalIdentity)
        {
            //var query =
            //    from u in _users
            //    where
            //        u.Provider == context.ExternalIdentity.Provider &&
            //        u.ProviderId == context.ExternalIdentity.ProviderId
            //    select u;
            throw new NotImplementedException();
        }

        public Task<User> FindBySubjectAsync(string subject)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetBySubject(string subject)
        {
            throw new NotImplementedException();
        }
    }
}