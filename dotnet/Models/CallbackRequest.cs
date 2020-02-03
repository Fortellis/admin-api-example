namespace AdminAPIImplementation.Models
{
    public class CallbackRequest
    {
        static public string ACCEPTED = "accepted";
        static public string REJECTED = "rejected";
        public string status { get; set; }

        public CallbackRequest(string status)
        {
            this.status = status;
        }
    }
}
