using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer3.Azure.Storage.Table.Models
{
    /// <summary>
    ///     User model used by 
    ///     <see cref="IdentityServer3.Azure.Storage.Table.Services.User"/>
    /// </summary>
    /// <remarks>
    ///     Copied from <see cref="IdentityServer3.Core.Services.InMemory.InMemoryUser"/>
    /// </remarks>
    public class User
    {
        public User()
        {
            Enabled = true;
            Claims = new List<Claim>();
        }

        public string Subject { get; set; }
        public bool Enabled { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Provider { get; set; }
        public string ProviderId { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}