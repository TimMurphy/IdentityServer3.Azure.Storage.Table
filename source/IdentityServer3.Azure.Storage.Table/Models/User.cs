using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace IdentityServer3.Azure.Storage.Table.Models
{
    public class User
    {
        public string Subject { get; private set; }
        public string Username { get; private set; }
        public string Provider { get; private set; }
        public string ProviderId { get; private set; }
        public bool Enabled { get; private set; }
        public IEnumerable<Claim> Claims { get; private set; }

        public string GetDisplayName()
        {
            var nameClaim = Claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.Name);

            return nameClaim == null ? Username : nameClaim.Value;
        }

        public static User FromExternalIdentity(ExternalIdentity externalIdentity)
        {
            //string displayName;

            //var name = context.ExternalIdentity.Claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.Name);
            //if (name == null)
            //{
            //    displayName = context.ExternalIdentity.ProviderId;
            //}
            //else
            //{
            //    displayName = name.Value;
            //}

            //user = new User
            //{
            //    Subject = CryptoRandom.CreateUniqueId(),
            //    Provider = context.ExternalIdentity.Provider,
            //    ProviderId = context.ExternalIdentity.ProviderId,
            //    Username = displayName,
            //    Claims = context.ExternalIdentity.Claims
            //};
            throw new NotImplementedException();
        }
    }
}