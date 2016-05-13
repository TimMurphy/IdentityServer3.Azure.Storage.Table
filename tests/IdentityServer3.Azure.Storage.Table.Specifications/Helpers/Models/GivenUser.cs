namespace IdentityServer3.Azure.Storage.Table.Specifications.Helpers.Models
{
    internal class GivenUser
    {
        public string Subject { get; set; }
        public string Provider { get; set; }
        public string ProviderId { get; set; }
        public string UserName { get; set; }
    }
}