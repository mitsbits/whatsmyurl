using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhatsMyUrl.Dal.Model
{
    public class SessionGroupMessage
    {
        public SessionGroupMessage(string sender, string body, params string[] recipients) : this()
        {
            Sender = sender;
            Body = body;
            Recipients = recipients;
        }
        protected SessionGroupMessage()
        {
            CreatedOn = DateTime.UtcNow;
        }

        [Key]
        public DateTime CreatedOn { get; set; }
      
        public string InternalRecipients { get; set; }
        public string[] Recipients
        {
            get
            {
                return InternalRecipients.Split(';');
            }
            set
            {
                InternalRecipients = String.Join(";", value);
            }
        }
        public string Sender { get; set; }
        public string Body { get; set; }
    }
}