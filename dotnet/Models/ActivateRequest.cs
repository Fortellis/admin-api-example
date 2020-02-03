namespace AdminAPIImplementation.Models
{
    public class ActivateRequest
    {
        public EntityInfo entityInfo { get; set; }
        public SolutionInfo solutionInfo { get; set; }
        public UserInfo userInfo { get; set; }
        public ApiInfo apiInfo { get; set; }
        public string subscriptionId { get; set; }
        public string connectionId { get; set; }

    }

    public class ApiInfo
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class EntityInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string countryCode { get; set; }
    }

    public class SolutionInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string developer { get; set; }
        public string contactEmail { get; set; }
    }

    public class UserInfo
    {
        public string fortellisId { get; set; }
    }
}
