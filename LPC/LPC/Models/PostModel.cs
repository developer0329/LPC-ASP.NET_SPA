using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LPC.Models
{
    /**
     * Data Model For Post
    **/
    public class PostModel
    {
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [Display(Name = "id")]
        public int id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public string title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Content")]
        public string content { get; set; }

        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [Display(Name = "post_id")]
        public int author { get; set; }

        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [Display(Name = "date")]
        public DateTime date { get; set; }
    }
}