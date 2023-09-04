namespace AirlinesWeb.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string StatusResult { get; set; }
        public string RequestUrl { get;set; }
    }
}