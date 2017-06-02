using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LPC.Models
{
    /**
     * Model For Comment Submit Form of Teacher and Student View
    **/
    public class CommentModel
    {
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [Display(Name = "id")]
        public int id { get; set; }

        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [Display(Name = "post_id")]
        public int post_id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "content")]
        public string content { get; set; }

        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [Display(Name = "author")]
        public int author { get; set; }

        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [Display(Name = "date")]
        public DateTime date { get; set; }
    }

    /**
     * Model For Information of a Post or a Comment in Teacher and Student View
    **/
    public class CommentViewItemModel
    {
        public string comment_content { get; set; }
        public string comment_author { get; set; }
        public string comment_date { get; set; }
    }

    /**
     * Model For Post & Comment List in Teacher and Student View
    **/
    public class CommentViewModel
    {
        public int post_id { get; set; }
        public string post_title { get; set; }
        public string post_content { get; set; }
        public string post_author { get; set; }
        public string post_date { get; set; }
        public List<CommentViewItemModel> comments { get; set; }
    }
}