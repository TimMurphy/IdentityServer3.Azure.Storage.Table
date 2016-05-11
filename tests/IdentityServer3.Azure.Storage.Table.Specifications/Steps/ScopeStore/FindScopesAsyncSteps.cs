using System.Linq;
using FluentAssertions;
using IdentityServer3.Azure.Storage.Table.Specifications.Helpers;
using TechTalk.SpecFlow;

namespace IdentityServer3.Azure.Storage.Table.Specifications.Steps.ScopeStore
{
    [Binding]
    public class FindScopesAsyncSteps
    {
        private readonly Actual _actual;
        private readonly Given _given;

        public FindScopesAsyncSteps(Given given, Actual actual)
        {
            _given = given;
            _actual = actual;
        }

        [Given(@"scopeNames is null")]
        public void GivenScopeNamesIsNull()
        {
            _given.ScopeNames = null;
        }

        [Given(@"scopeNames is empty")]
        public void GivenScopeNamesIsEmpty()
        {
            _given.ScopeNames = new string[] {};
        }

        [Given(@"scopeNames is:")]
        public void GivenScopeNamesIs(TechTalk.SpecFlow.Table table)
        {
            _given.ScopeNames = table.Rows.Select(r => r["scopeName"]).ToArray();
        }

        [When(@"ScopeStore\.FindScopesAsync\(scopeNames\) is called")]
        public void WhenScopeStore_FindScopesAsyncScopeNamesIsCalled()
        {
            _actual.Scopes = _given.ScopeStore.FindScopesAsync(_given.ScopeNames).Result.ToArray();
        }

        [Then(@"all scopes should be returned")]
        public void ThenAllScopesShouldBeReturned()
        {
            _actual.Scopes.ShouldAllBeEquivalentTo(_given.Scopes);
        }

        [Then(@"these scopes should be returned:")]
        public void ThenTheseScopesShouldBeReturned(TechTalk.SpecFlow.Table table)
        {
            var actual = _actual.Scopes.Select(s => s.Name);
            var expected = table.Rows.Select(r => r["scopeName"]);

            actual.ShouldAllBeEquivalentTo(expected);
        }
    }
}