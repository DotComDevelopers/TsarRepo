using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;


namespace TSAR.Models
{
    public class MailModel
    {
        public string To { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
    }
}

