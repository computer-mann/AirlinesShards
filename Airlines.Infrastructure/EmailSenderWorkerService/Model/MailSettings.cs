using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmailSenderWorkerService.Model
{
    public class MailSettings
    {
        [JsonPropertyName("Server")]
        public string Server { get; set; }

        [JsonPropertyName("Port")]
        public int Port { get; set; }

        [JsonPropertyName("SenderName")]
        public string SenderName { get; set; }

        [JsonPropertyName("SenderEmail")]
        public string SenderEmail { get; set; }

        [JsonPropertyName("UserName")]
        public string UserName { get; set; }

        [JsonPropertyName("Password")]
        public string Password { get; set; }
    }
}
