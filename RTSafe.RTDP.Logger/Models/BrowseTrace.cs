using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace RTSafe.RTDP.Logger.Models
{
    [Table("BrowseTrace")]
    public partial class BrowseTrace
    {
        [Key]
        public int ID { get; set; }
        public string SessionID { get; set; }
        public Guid UserID { get; set; }
        public string IpAddress { get; set; }
        public DateTime EnterTime { get; set; }
        public string PreviousUrl { get; set; }
        public string NowUrl { get; set; }

    }
}
