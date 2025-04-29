using ControlProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlProject.Controllers
{
    public class AdminLayoutController : Controller
    {
        // GET: AdminLayout
        ControlContext _context = new ControlContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminLayoutNavbar()
        {

            var userName = Session["currentUser"].ToString();

            if (string.IsNullOrEmpty(userName))
            {

                return PartialView("Index", "Login");
            }
            ViewBag.nameSurname = _context.Admins.Where(x => x.UserName == userName).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault();


            ViewBag.imageUrl = _context.Admins.Where(x => x.UserName == userName).Select(x => x.ImageUrl).FirstOrDefault();

            return PartialView();
        }
    }
}