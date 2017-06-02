using LPC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LPC.Controllers
{
    public class HomeController : Controller
    {
        /*
         * This is a Controller for Login Page 
         * 
        **/
        public ActionResult Index()
        {
            Session["login"] = null;
            return View();
        }
	    /*
         * This is a Controller for Teacher View
         * 
        **/
        public ActionResult Teacher()
        {
	        // Check Login Status with session vaiable
            if (Session["login"] == null)
            {
                return new RedirectResult("/Home/Index");
            }
            else
            {
                ViewBag.login = Session["login"];

                if(ViewBag.login.type == "S")
                {
                    return new RedirectResult("/Home/Student");
                }
                Comment comm = new Comment();
                List<CommentViewModel> comments = comm.getAllComments();
                ViewBag.Stats = comments;

                Log log = new Log();
                foreach(CommentViewModel cmm in comments)
                {
                    ViewBag.seenList = string.Join(",", log.getAllSeenList(cmm.post_id));
                }
                
                return View();
            }
        }
        /**
         * This is a Controller for Announcement Post View
         * 
        **/
        public ActionResult Post()
        {
            // Check Login Status with session vaiable
            if (Session["login"] == null)
            {
                return new RedirectResult("/Home/Index");
            }
            else
            {
                ViewBag.login = Session["login"];

                if (ViewBag.login.type == "S")
                {
                    return new RedirectResult("/Home/Student");
                }

                return View();
            }
        }


        /*
         * This is a Controller for Student Page 
         * 
        **/
        public ActionResult Student()
        {
		    // Check Login Status with session vaiable
            if (Session["login"] == null)
            {
		        //If Login is not, redirect Login page
                return new RedirectResult("/Home/Index");
            }
            else
            {
                ViewBag.login = Session["login"];

                if (ViewBag.login.type == "T")
                {
                    return new RedirectResult("/Home/Teacher");
                }

                Comment comm = new Comment();
                List<CommentViewModel> comments = comm.getAllComments();
                ViewBag.Stats = comments;
                ViewBag.login = Session["login"];
                return View();
            }
        }
	
	    /*
         * This is API for Login. When Clicked Login Button, this API will be called.
         * 
        **/
        [HttpPost]
        [RequireHttps]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Login login = new Login();

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            LoginUser result = login.loginUser(model);
            if(result != null)
            {
		        // Success Login
                // Session Variable for security.
                Session["login"] = result;
                if (result.type.Equals("T"))
                {
                    return new RedirectResult("/Home/Teacher");
                }
                else
                {
                    return new RedirectResult("/Home/Student");
                }
            }
            else
            {
                Session["login"] = null;
                return View(model); ;
            }

        }


        /*
         * This is API for New Announcement Post.
         * 
        **/
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePostByTeacher(PostModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Post post = new Post();

            int result = post.CreatePost(model);
            return new RedirectResult("/Home/Teacher");
        }
        /*
         * This is API for New Comment. When Clicked Comment Button on Student view, this API will be called.
         * 
        **/
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult InsertCommentByStudent(CommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Comment cmm = new Comment();

            int result = cmm.insertComment(model);
            return new RedirectResult("/Home/Student");
        }

	    /*
         * This is API for New Comment. When Clicked Comment Button on Teacher view, this API will be called.
         * 
        **/
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult InsertCommentByTeacher(CommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Comment cmm = new Comment();

            int result = cmm.insertComment(model);
            return new RedirectResult("/Home/Teacher");
        }
    }
}