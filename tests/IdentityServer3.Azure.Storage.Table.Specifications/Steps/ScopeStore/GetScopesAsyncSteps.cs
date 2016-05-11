using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using IdentityServer3.Azure.Storage.Table.Infrastructure.Serializers;
using IdentityServer3.Azure.Storage.Table.Specifications.Helpers;
using IdentityServer3.Azure.Storage.Table.Specifications.Helpers.Models;
using IdentityServer3.Azure.Storage.Table.Stores;
using IdentityServer3.Core.Models;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using OpenMagic.Azure.Storage.Table;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace IdentityServer3.Azure.Storage.Table.Specifications.Steps.ScopeStore
{
    [Binding]
    public class GetScopesAsyncSteps
    {
        private readonly Actual _actual;
        private readonly Given _given;

        public GetScopesAsyncSteps(Given given, Actual actual)
        {
            _given = given;
            _actual = actual;
        }

        [Given(@"scope table has scopes:")]
        public void GivenScopeTableHasScopes(TechTalk.SpecFlow.Table givenTable)
        {
            _given.Scopes = givenTable
                .CreateSet<GivenScope>()
                .Select(s => new Scope
                {
                    Name = s.Name,
                    ShowInDiscoveryDocument = s.ShowInDiscoveryDocument
                })
                .ToArray();

            var tableEntities = _given.Scopes.Select(scope =>
            {
                var entityProperties = new Dictionary<string, EntityProperty>
                {
                    {nameof(Scope.ShowInDiscoveryDocument), new EntityProperty(scope.ShowInDiscoveryDocument)},
                    {nameof(Scope), new EntityProperty(JsonConvert.SerializeObject(scope))}
                };

                return new DynamicTableEntity(scope.Name, "", null, entityProperties);
            });

            _given.ScopesTable = AzureTableProvider.GetTable<Scope>(true);
            _given.ScopesTable.InsertAsync(tableEntities).Wait();
        }

        [Given(@"publicOnly is '(.*)'")]
        public void GivenPublicOnlyIs(bool publicOnly)
        {
            _given.PublicOnly = publicOnly;
        }

        [When(@"ScopeStore\.GetScopesAsync\(\<publicOnly\>\) is called")]
        public void WhenScopeStore_GetScopesAsyncIsCalled()
        {
            var scopeStore = new Stores.ScopeStore(new Table<Scope>(AzureTableProvider.ConnectionString, _given.ScopesTable.Name, DynamicTableEntitySerializers.ScopeSerializer));

            _actual.Scopes = scopeStore.GetScopesAsync(_given.PublicOnly).Result.ToArray();
        }

        [Then(@"the result should scopes:")]
        public void ThenTheResultShouldScopes(TechTalk.SpecFlow.Table expectedTable)
        {
            var expectedScopeNames = expectedTable.CreateSet<GivenScope>().Select(s => s.Name);
            var expectedScopes = _given.Scopes.Where(s => expectedScopeNames.Contains(s.Name)).ToArray();

            expectedScopes.Length.Should().Be(expectedTable.RowCount);
            _actual.Scopes.ShouldAllBeEquivalentTo(expectedScopes);
        }
    }
}