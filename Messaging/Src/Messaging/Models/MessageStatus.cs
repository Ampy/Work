using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RTSafe.RTDP.Messaging.Models
{
    [Table("MessageStatus")]
    public class MessageStatus
    {
        [Key]
        public System.Guid MessageStatusId { get; set; }
        public System.Guid MessageId { get; set; }
        public int CategoryId { get; set; }
        public Nullable<System.DateTime> SendTime { get; set; }
        public string Status { get; set; }
        public string StatusContent { get; set; }
        public virtual Message Message { get; set; }
        public virtual Category Category { get; set; }
    }
}
