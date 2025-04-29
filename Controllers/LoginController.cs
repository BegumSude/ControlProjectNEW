using ControlProject.Context;
using ControlProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ControlProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        ControlContext _context = new ControlContext();
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return View();
        }
        [HttpPost]
        public ActionResult Index( Admin model)
        {
            var admin = _context.Admins.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);
            if (admin == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı!");
                return View(model);
            }
            FormsAuthentication.SetAuthCookie(admin.UserName, false);
            Session["currentUser"] = admin.UserName;
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}