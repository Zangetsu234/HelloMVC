using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace HelloMVC.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            if(Session["UserID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Login(LoginFM credientials)
        {
            LoginVM user = new LoginService().Login(credientials);
            if(user != null)
            {
                Session["UserID"] = user.ID;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Username or Password incorrect.";
                return View("Index");
            }
        }
        public ActionResult Logout()
        {
            Session["UserID"] = null;
            return RedirectToAction("Index", "Home");
        }
	}
}