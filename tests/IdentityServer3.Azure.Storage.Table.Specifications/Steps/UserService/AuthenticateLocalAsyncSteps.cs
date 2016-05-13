using System;
using FluentAssertions;
using FluentAssertions.Equivalency;
using IdentityServer3.Azure.Storage.Table.Specifications.Helpers;
using IdentityServer3.Core.Models;
using TechTalk.SpecFlow;

namespace IdentityServer3.Azure.Storage.Table.Specifications.Steps.UserService
{
    [Binding]
    public class AuthenticateLocalAsyncSteps
    {
        private readonly Given _given;

        public AuthenticateLocalAsyncSteps(Given given)
        {
            _given = given;
        }

        [Given(@"Password is '(.*)'")]
        public void GivenPasswordIs(string password)
        {
            _given.Password = password;
        }

        [When(@"UserService\.AuthenticateLocalAsync\(context\) is called")]
        public void WhenUserService_AuthenticateLocalAsyncContextIsCalled()
        {
            _given.LocalAuthenticationContext = new LocalAuthenticationContext
            {
                UserName = _given.UserName,
                Password = _given.Password
            };

            var userService = new Services.UserService(_given.UserStore);

            userService.AuthenticateLocalAsync(_given.LocalAuthenticationContext).Wait();
        }

        [Then(@"LocalAuthenticationContext\.AuthenticateResult should be set with user details")]
        public void ThenLocalAuthenticationContext_AuthenticateResultShouldBeSetWithUserDetails()
        {
            var actual = _given.LocalAuthenticationContext.AuthenticateResult;
            var expected = new AuthenticateResult(_given.Subject, _given.UserName);

            actual.ShouldBeEquivalentTo(expected, AssertionConfiguration());
        }

        private static Func<EquivalencyAssertionOptions<AuthenticateResult>, EquivalencyAssertionOptions<AuthenticateResult>> AssertionConfiguration()
        {
            // User.Claims and User.Identities are excluded by they create a cyclic reference exception.
            return config => config
                .Excluding(authenticateResult => authenticateResult.User.Claims)
                .Excluding(authenticateResult => authenticateResult.User.Identities);
        }

        [Then(@"LocalAuthenticationContext\.AuthenticateResult should be null")]
        public void ThenLocalAuthenticationContext_AuthenticateResultShouldBeNull()
        {
            _given.LocalAuthenticationContext.AuthenticateResult.Should().BeNull();
        }
    }
}