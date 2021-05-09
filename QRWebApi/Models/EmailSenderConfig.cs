using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace QRWebApi.Models
{
    public partial class EmailSenderConfig
    {
        public string EmailUser { get; set; }
        public string EmailPassword { get; set; }
        public string MailHost { get; set; }
        public string MailFrom { get; set; }
        public int Port { get; set; }
    }
}
