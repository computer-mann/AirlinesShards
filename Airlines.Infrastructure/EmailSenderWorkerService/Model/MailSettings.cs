using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmailSenderWorkerService.Model
{
    public class MailSettings
    {
        [Required(AllowEmptyStrings = false)]
        public string Server { get; set; }
        [Required(AllowEmptyStrings =false)]
        public int Port { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string SenderName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string SenderEmail { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
