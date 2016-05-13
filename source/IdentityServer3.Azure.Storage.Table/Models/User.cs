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
        public User()
        {
            Claims = new List<Claim>();
            Enabled = true;
        }

        public string Subject { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public ExternalIdentity ExternalIdentity { get; set; }

        public string GetDisplayName()
        {
            // todo: specification
            var nameClaim = Claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.Name);

            return nameClaim == null ? UserName : nameClaim.Value;
        }

        public static User FromExternalIdentity(ExternalIdentity externalIdentity)
        {
            // todo: specification
            var name = externalIdentity.Claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.Name);
            var displayName = name == null ? externalIdentity.ProviderId : name.Value;

            return new User
            {
                Subject = Guid.NewGuid().ToString(),
                ExternalIdentity = externalIdentity,
                UserName = displayName,
                Claims = externalIdentity.Claims
            };
        }
    }
}