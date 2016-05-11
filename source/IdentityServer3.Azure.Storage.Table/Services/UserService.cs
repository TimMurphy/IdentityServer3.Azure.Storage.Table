using System;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using Microsoft.WindowsAzure.Storage.Table;

namespace IdentityServer3.Azure.Storage.Table.Services
{
    public class UserService : IUserService
    {
        private readonly CloudTable _table;

        public UserService(CloudTable table)
        {
            _table = table;
        }

        public Task PreAuthenticateAsync(PreAuthenticationContext context)
        {
            throw new NotImplementedException("todo");
        }

        public Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            throw new NotImplementedException("todo");
        }

        public Task AuthenticateExternalAsync(ExternalAuthenticationContext context)
        {
            throw new NotImplementedException("todo");
        }

        public Task PostAuthenticateAsync(PostAuthenticationContext context)
        {
            throw new NotImplementedException("todo");
        }

        public Task SignOutAsync(SignOutContext context)
        {
            throw new NotImplementedException("todo");
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            throw new NotImplementedException("todo");
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            throw new NotImplementedException("todo");
        }
    }
}