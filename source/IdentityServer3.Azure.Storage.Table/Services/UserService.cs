using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer3.Azure.Storage.Table.Models;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;

namespace IdentityServer3.Azure.Storage.Table.Services
{
    /// <summary>
    ///     Azure Storage Table persistence layer for <see cref="User"/>.
    /// </summary>
    /// <remarks>
    ///     Copied from <see cref="IdentityServer3.Core.Services..UserService"/>.
    /// </remarks>
    public class UserService : UserServiceBase
    {
        /// <summary>
        ///     This methods gets called for local authentication (whenever 
        ///     the user uses the username and password dialog).
        /// </summary>
        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            //var query =
            //    from u in _users
            //    where u.Username == context.UserName && u.Password == context.Password
            //    select u;

            //var user = query.SingleOrDefault();
            //if (user != null)
            //{
            //    context.AuthenticateResult = new AuthenticateResult(user.Subject, GetDisplayName(user));
            //}

            //return Task.FromResult(0);
            throw new NotImplementedException();
        }

        /// <summary>
        ///     This method gets called when the user uses an external identity 
        ///     provider to authenticate.
        /// </summary>
        public override Task AuthenticateExternalAsync(ExternalAuthenticationContext context)
        {
            //var query =
            //    from u in _users
            //    where
            //        u.Provider == context.ExternalIdentity.Provider &&
            //        u.ProviderId == context.ExternalIdentity.ProviderId
            //    select u;

            //var user = query.SingleOrDefault();
            //if (user == null)
            //{
            //    string displayName;

            //    var name = context.ExternalIdentity.Claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.Name);
            //    if (name == null)
            //    {
            //        displayName = context.ExternalIdentity.ProviderId;
            //    }
            //    else
            //    {
            //        displayName = name.Value;
            //    }

            //    user = new User
            //    {
            //        Subject = CryptoRandom.CreateUniqueId(),
            //        Provider = context.ExternalIdentity.Provider,
            //        ProviderId = context.ExternalIdentity.ProviderId,
            //        Username = displayName,
            //        Claims = context.ExternalIdentity.Claims
            //    };
            //    _users.Add(user);
            //}

            //context.AuthenticateResult = new AuthenticateResult(user.Subject, GetDisplayName(user), identityProvider: context.ExternalIdentity.Provider);

            //return Task.FromResult(0);
            throw new NotImplementedException();
        }

        /// <summary>
        ///     This method is called whenever claims about the user are requested 
        ///     (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        public override Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //var query =
            //    from u in _users
            //    where u.Subject == context.Subject.GetSubjectId()
            //    select u;
            //var user = query.Single();

            //var claims = new List<Claim>{
            //    new Claim(Constants.ClaimTypes.Subject, user.Subject),
            //};

            //claims.AddRange(user.Claims);
            //if (!context.AllClaimsRequested)
            //{
            //    claims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
            //}

            //context.IssuedClaims = claims;

            //return Task.FromResult(0);
            throw new NotImplementedException();
        }

        /// <summary>
        ///     This method gets called whenever identity server needs to determine 
        ///     if the user is valid or active (e.g. during token issuance or 
        ///     validation)
        /// </summary>
        /// <exception cref="System.ArgumentNullException">subject</exception>
        public override Task IsActiveAsync(IsActiveContext context)
        {
            //if (context.Subject == null) throw new ArgumentNullException("subject");

            //var query =
            //    from u in _users
            //    where
            //        u.Subject == context.Subject.GetSubjectId()
            //    select u;

            //var user = query.SingleOrDefault();

            //context.IsActive = (user != null) && user.Enabled;

            //return Task.FromResult(0);
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Retrieves the display name.
        /// </summary>
        protected virtual string GetDisplayName(User user)
        {
            //var nameClaim = user.Claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.Name);
            //if (nameClaim != null)
            //{
            //    return nameClaim.Value;
            //}

            //return user.Username;
            throw new NotImplementedException();
        }
    }
}