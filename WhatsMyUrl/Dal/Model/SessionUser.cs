using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhatsMyUrl.Dal.Model
{
    public class SessionUser
    {
        [StringLength(24)]
        [Key]
        public string SessionId { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
    }
}