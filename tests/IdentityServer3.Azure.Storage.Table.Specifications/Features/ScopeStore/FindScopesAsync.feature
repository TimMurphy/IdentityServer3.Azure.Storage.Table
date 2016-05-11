Feature: FindScopesAsync

Background: 
	Given scope table has scopes:
        | Name  | ShowInDiscoveryDocument |
        | read  | true                    |
        | write | true                    |
        | abc   | false                   |

Scenario: scopeNames is null
	Given scopeNames is null
    When ScopeStore.FindScopesAsync(scopeNames) is called
    Then all scopes should be returned

Scenario: scopeNames is empty
	Given scopeNames is empty
    When ScopeStore.FindScopesAsync(scopeNames) is called
    Then all scopes should be returned

Scenario: scopeNames contains scopes in ScopeStore
	Given scopeNames is:
        | scopeName |
        | read      |
        | abc       |
    When ScopeStore.FindScopesAsync(scopeNames) is called
    Then these scopes should be returned:
        | scopeName |
        | read      |
        | abc       |

Scenario: scopeNames contains scopes not in ScopeStore
	Given scopeNames is:
        | scopeName   |
        | read        |
        | abc         |
        | nonexistant |
    When ScopeStore.FindScopesAsync(scopeNames) is called
    Then these scopes should be returned:
        | scopeName |
        | read      |
        | abc       |
