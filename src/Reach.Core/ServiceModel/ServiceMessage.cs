namespace Reach.ServiceModel
{
    public class ServiceMessage
    {
        public MessageSeverityLevel Severity { get; set; }

        public short Code { get; set; }

        public string Content { get; set; }
    }
}
