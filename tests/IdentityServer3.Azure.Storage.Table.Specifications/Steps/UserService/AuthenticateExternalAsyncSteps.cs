using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Equivalency;
using IdentityServer3.Azure.Storage.Table.Models;
using IdentityServer3.Azure.Storage.Table.Specifications.Helpers;
using IdentityServer3.Azure.Storage.Table.Specifications.Helpers.Models;
using IdentityServer3.Azure.Storage.Table.Stores;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace IdentityServer3.Azure.Storage.Table.Specifications.Steps.UserService
{
    [Binding]
    public class AuthenticateExternalAsyncSteps
    {
        private readonly Given _given;

        public AuthenticateExternalAsyncSteps(Given given)
        {
            _given = given;
        }

        [Given(@"user table has users:")]
        public void GivenUserTableHasUsers(TechTalk.SpecFlow.Table givenTable)
        {
            _given.Users = givenTable
                .CreateSet<GivenUser>()
                .Select(givenUser => new User
                {
                    ExternalIdentity = new ExternalIdentity
                    {
                        Provider = givenUser.Provider,
                        ProviderId = givenUser.ProviderId
                    },
                    Subject = givenUser.Subject,
                    Username = givenUser.Username
                })
                .ToArray();

            _given.UserStore = A.Fake<IUserStore>();

            A.CallTo(() => _given.UserStore.FindByExternalIdentityAsync(A<ExternalIdentity>.Ignored))
                .ReturnsLazily((ExternalIdentity externalIdentity) => _given.Users.SingleOrDefault(u => u.ExternalIdentity.Provider == externalIdentity.Provider && u.ExternalIdentity.ProviderId == externalIdentity.ProviderId));
        }

        [Given(@"Provider is '(.*)'")]
        public void GivenProviderIs(string provider)
        {
            _given.Provider = provider;
        }

        [Given(@"ProviderId is '(.*)'")]
        public void GivenProviderIdIs(string providerId)
        {
            _given.ProviderId = providerId;
        }

        [Given(@"Subject is '(.*)'")]
        public void GivenSubjectIs(string subject)
        {
            _given.Subject = subject;
        }

        [Given(@"Username is '(.*)'")]
        public void GivenUsernameIs(string username)
        {
            _given.Username = username;
        }

        [When(@"UserService\.AuthenticateExternalAsync\(context\) is called")]
        public void WhenUserService_AuthenticateExternalAsyncContextIsCalled()
        {
            _given.ExternalAuthenticationContext = new ExternalAuthenticationContext
            {
                ExternalIdentity = new ExternalIdentity
                {
                    Provider = _given.Provider,
                    ProviderId = _given.ProviderId,
                    Claims = new List<Claim>
                    {
                        new Claim(Constants.ClaimTypes.Name, _given.Username)
                    }
                }
            };

            var userService = new Services.UserService(_given.UserStore);

            userService.AuthenticateExternalAsync(_given.ExternalAuthenticationContext).Wait();
        }

        [Then(@"context\.AuthenticateResult should be set with user details")]
        public void ThenContext_AuthenticateResultShouldBeSetWithUserDetails()
        {
            var actual = _given.ExternalAuthenticationContext.AuthenticateResult;
            var expected = new AuthenticateResult(_given.Subject, _given.Username, identityProvider: _given.Provider);

            actual.ShouldBeEquivalentTo(expected, AssertionConfiguration());
        }

        private static Func<EquivalencyAssertionOptions<AuthenticateResult>, EquivalencyAssertionOptions<AuthenticateResult>> AssertionConfiguration()
        {
            // User.Claims and User.Identities are excluded by they create a cyclic reference exception.
            return config => config
                .Excluding(authenticateResult => authenticateResult.User.Claims)
                .Excluding(authenticateResult => authenticateResult.User.Identities);
        }

        [Then(@"a new user should be added to user table")]
        public void ThenANewUserShouldBeAddedToUserTable()
        {
            A.CallTo(() => _given.UserStore.AddAsync(A<User>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}