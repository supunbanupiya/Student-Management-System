using Std.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Std.Controllers
{
    public class LoginController : Controller
    {
        lindaEntities db = new lindaEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(user objck)
        {
            if(ModelState.IsValid)
            {
                using (lindaEntities db = new lindaEntities())
                {
                    var obj = db.users.Where(x => x.username.Equals(objck.username) && x.password.Equals(objck.password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.id.ToString();
                        Session["UserName"] = obj.username.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "username or password incorrect");
                    }
                }
            }
           
            return View(objck);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}