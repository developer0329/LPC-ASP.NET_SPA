using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LPC.Models
{
    public class Utils
    {
        public string getSQLDateString(DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}