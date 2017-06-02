using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LPC.Models
{
    /**
     * Data Model For Seen Log
    **/
    public class LogModel
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int post_id { get; set; }
        public DateTime seen_time { get; set; }
    }
}