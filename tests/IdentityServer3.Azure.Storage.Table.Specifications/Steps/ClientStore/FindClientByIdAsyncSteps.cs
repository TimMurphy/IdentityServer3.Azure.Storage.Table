using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using IdentityServer3.Azure.Storage.Table.Specifications.Helpers;
using IdentityServer3.Azure.Storage.Table.Specifications.Helpers.Models;
using IdentityServer3.Core.Models;
using Microsoft.WindowsAzure.Storage.Table;
using OpenMagic.Azure.Storage.Table;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace IdentityServer3.Azure.Storage.Table.Specifications.Steps.ClientStore
{
    [Binding]
    public class FindClientByIdAsyncSteps
    {
        private readonly Actual _actual;
        private readonly Given _given;

        public FindClientByIdAsyncSteps(Given given, Actual actual)
        {
            _given = given;
            _actual = actual;
        }

        [Given(@"client table has clients:")]
        public void GivenClientTableHasClients(TechTalk.SpecFlow.Table givenTable)
        {
            _given.Clients = givenTable
                .CreateSet<GivenClient>()
                .Select(s => new Client
                {
                    ClientId = s.ClientId,
                    Enabled = s.Enabled
                })
                .ToArray();

            var tableEntities = _given.Clients.Select(client =>
            {
                var entityProperties = new Dictionary<string, EntityProperty>
                {
                    {nameof(Client.Enabled), EntityProperty.GeneratePropertyForBool(client.Enabled)}
                };

                return new DynamicTableEntity(client.ClientId, "", null, entityProperties);
            });

            _given.ClientsTable = AzureTableProvider.GetTable<Client>(true);
            _given.ClientsTable.InsertAsync(tableEntities).Wait();
        }

        [Given(@"clientId is null")]
        public void GivenClientIdIsNull()
        {
            GivenClientIdIs(null);
        }

        [Given(@"clientId is empty")]
        public void GivenClientIdIsEmpty()
        {
            GivenClientIdIs("");
        }

        [Given(@"clientId is '(.*)'")]
        public void GivenClientIdIs(string clientId)
        {
            _given.ClientId = clientId;
        }

        [When(@"ClientStore\.FindClientByIdAsync\(clientId\)")]
        public void WhenClientStore_FindClientByIdAsyncClientId()
        {
            _actual.Client = _given.ClientStore.FindClientByIdAsync(_given.ClientId).Result;
        }

        [Then(@"null should be returned")]
        public void ThenNullShouldBeReturned()
        {
            _actual.Client.Should().BeNull();
        }

        [Then(@"client should be returned")]
        public void ThenClientShouldBeReturned()
        {
            _actual.Client.ClientId.Should().Be(_given.ClientId);
        }
    }
}