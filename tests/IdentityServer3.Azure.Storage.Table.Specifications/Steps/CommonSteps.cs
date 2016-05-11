using TechTalk.SpecFlow;

namespace IdentityServer3.Azure.Storage.Table.Specifications.Steps
{
    [Binding]
    public class CommonSteps
    {
        [Given(@"todo")]
        public void GivenTodo()
        {
            ScenarioContext.Current.Pending();
        }
    }
}