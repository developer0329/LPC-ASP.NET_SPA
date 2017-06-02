using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LPC.Models
{
    /**
     * View Model For Login Form
    **/
    public class LoginModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    /**
     * Data Model For Login User
    **/
    public class LoginUser
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string type { get; set; }
    }
}