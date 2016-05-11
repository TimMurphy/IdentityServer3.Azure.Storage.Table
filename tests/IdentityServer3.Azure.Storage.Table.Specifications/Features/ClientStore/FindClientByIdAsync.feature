Feature: FindClientByIdAsync

Background: 
	Given client table has clients:
        | ClientId         | Enabled |
        | enabledClientId  | true    |
        | disabledClientId | false   |

Scenario: clientId is null
    Given clientId is null
    When ClientStore.FindClientByIdAsync(clientId)
    Then null should be returned

Scenario: clientId is empty
    Given clientId is empty
    When ClientStore.FindClientByIdAsync(clientId)
    Then null should be returned

Scenario: clientId exists and client is enabled
    Given clientId is 'enabledClientId'
    When ClientStore.FindClientByIdAsync(clientId)
    Then client should be returned

Scenario: clientId exists but client is disabled
    Given clientId is 'disabledClientId'
    When ClientStore.FindClientByIdAsync(clientId)
    Then null should be returned

Scenario: clientId does not exist
    Given clientId is 'nonExistantClientId'
    When ClientStore.FindClientByIdAsync(clientId)
    Then null should be returned
