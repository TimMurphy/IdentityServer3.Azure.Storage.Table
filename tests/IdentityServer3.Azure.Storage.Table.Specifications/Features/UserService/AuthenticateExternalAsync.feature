Feature: AuthenticateExternalAsync

Background: 
	Given user table has users:
        | Subject                              | Provider         | ProviderId       | Username          |
        | 661ac23d-45f0-463b-a1d0-760544209131 | Dummy Provider 1 | DummyProviderId1 | Dummy User Name 1 |

Scenario: Existing external user
    Given Subject is '661ac23d-45f0-463b-a1d0-760544209131'
	And Provider is 'Dummy Provider 1'
    And ProviderId is 'DummyProviderId1'
    And UserName is 'Dummy User Name 1'
    When UserService.AuthenticateExternalAsync(context) is called
    Then context.AuthenticateResult should be set with user details

Scenario: New external user
    Given Subject is 'Dummy Subject Does Not Exist'
	And Provider is 'Dummy Provider Does Not Exist'
    And ProviderId is 'DummyProviderIdDoesNotExist'
    And UserName is 'Dummy User Name Does Not Exist'
    When UserService.AuthenticateExternalAsync(context) is called
    Then a new user should be added to user table
    And context.AuthenticateResult should be set with user details

