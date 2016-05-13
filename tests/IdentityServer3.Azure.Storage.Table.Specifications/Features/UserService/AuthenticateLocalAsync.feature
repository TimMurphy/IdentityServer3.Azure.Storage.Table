Feature: AuthenticateLocalAsync

Background: 
	Given user table has users:
        | Subject                              | Provider         | ProviderId       | UserName          | Password         |
        | 661ac23d-45f0-463b-a1d0-760544209131 | Dummy Provider 1 | DummyProviderId1 | Dummy User Name 1 | Dummy Password 1 |

Scenario: UserName and password match
    Given UserName is 'Dummy User Name 1'
    And Password is 'Dummy Password 1'
    And Subject is '661ac23d-45f0-463b-a1d0-760544209131'
    When UserService.AuthenticateLocalAsync(context) is called
    Then LocalAuthenticationContext.AuthenticateResult should be set with user details

Scenario: UserName exists but password is incorrect
    Given UserName is 'Dummy User Name 1'
    And Password is 'incorrect'
    And Subject is '661ac23d-45f0-463b-a1d0-760544209131'
    When UserService.AuthenticateLocalAsync(context) is called
    Then LocalAuthenticationContext.AuthenticateResult should be null

Scenario: UserName does exist
    Given UserName is 'userName does not exist'
    And Password is 'incorrect'
    And Subject is '661ac23d-45f0-463b-a1d0-760544209131'
    When UserService.AuthenticateLocalAsync(context) is called
    Then LocalAuthenticationContext.AuthenticateResult should be null
