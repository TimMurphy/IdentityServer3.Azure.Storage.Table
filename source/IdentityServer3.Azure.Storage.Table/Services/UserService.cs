using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer3.Azure.Storage.Table.Models;
using IdentityServer3.Azure.Storage.Table.Stores;
using IdentityServer3.Core;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.Default;

namespace IdentityServer3.Azure.Storage.Table.Services
{
    /// <summary>
    ///     Azure Storage Table persistence layer for <see cref="User" />.
    /// </summary>
    /// <remarks>
    ///     Copied from <see cref="UserService" />.
    /// </remarks>
    public class UserService : UserServiceBase
    {
        private readonly IUserStore _userStore;

        public UserService(IUserStore userStore)
        {
            _userStore = userStore;
        }

        /// <summary>
        ///     This method gets called when the user uses an external identity
        ///     provider to authenticate.
        /// </summary>
        public override async Task AuthenticateExternalAsync(ExternalAuthenticationContext context)
        {
            var user = await _userStore.FindByExternalIdentityAsync(context.ExternalIdentity);

            if (user == null)
            {
                user = User.FromExternalIdentity(context.ExternalIdentity);

                // We are authenticating, not authorizing, the user so its ok to add anyone to the store
                await _userStore.AddAsync(user);
            }

            context.AuthenticateResult = new AuthenticateResult(user.Subject, user.GetDisplayName(), identityProvider: context.ExternalIdentity.Provider);
        }

        /// <summary>
        ///     This methods gets called for local authentication (whenever
        ///     the user uses the userName and password dialog).
        /// </summary>
        public override async Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var user = await _userStore.FindByCredentialsAsync(context.UserName, context.Password);

            if (user != null)
            {
                context.AuthenticateResult = new AuthenticateResult(user.Subject, user.GetDisplayName());
            }
        }

        /// <summary>
        ///     This method is called whenever claims about the user are requested
        ///     (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userStore.GetBySubject(context.Subject.GetSubjectId());

            var claims = new List<Claim>
            {
                new Claim(Constants.ClaimTypes.Subject, user.Subject)
            };

            claims.AddRange(user.Claims);

            if (!context.AllClaimsRequested)
            {
                claims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
            }

            context.IssuedClaims = claims;
        }

        /// <summary>
        ///     This method gets called whenever identity server needs to determine
        ///     if the user is valid or active (e.g. during token issuance or
        ///     validation)
        /// </summary>
        /// <exception cref="ArgumentNullException">content.Subject</exception>
        public override async Task IsActiveAsync(IsActiveContext context)
        {
            if (context.Subject == null)
            {
                throw new ArgumentNullException($"{nameof(context)}.{nameof(IsActiveContext.Subject)}");
            }

            var user = await _userStore.FindBySubjectAsync(context.Subject.GetSubjectId());

            context.IsActive = user?.Enabled ?? false;
        }
    }
}