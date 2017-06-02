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
        public ActionResult Index()
        {
            Session["login"] = null;
            return View();
        }

        public ActionResult Teacher()
        {
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

        public ActionResult Student()
        {
            if (Session["login"] == null)
            {
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

        [HttpPost]
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