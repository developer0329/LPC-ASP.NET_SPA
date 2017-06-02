using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LPC.Models
{
    public class Utils
    {
        public string getSQLDateString(DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string Hash(string value)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value)));
        }
    }
}