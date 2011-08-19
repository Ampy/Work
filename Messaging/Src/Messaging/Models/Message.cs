using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace RTSafe.RTDP.Messaging.Models
{
    [Table("Message")]
    public class Message
    {
        public Message()
        {
            Categories = new List<Category>();
        }

        [Key]
        public System.Guid MessageId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string FormattedBody { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public Nullable<System.DateTime> CreateTime { get; set; }

        public Nullable<int> Priority { get; set; }

        public string Severity { get; set; }

        //public virtual ICollection<MessageStatus> MessageStatus { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
