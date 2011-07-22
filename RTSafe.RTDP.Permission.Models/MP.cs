using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace RTSafe.RTDP.Permission.Models
{
    [Table("MP")]
    public class MP
    {
        
        public int ID { get; set; }


        public string Name { get; set; }

        [NotMapped]
        public string Description { get; set; }
    }
}
