using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloMVC.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            if(Session["UserID"] == null)
            {
                return RedirectToAction("../Login/Index");
            }
            UserService userS = new UserService();
            UsersVM usersVM = userS.GetUsers();
            return View(usersVM);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserFM user)
        {
            //if user valid create user
            UserService users = new UserService();
            users.CreateUser(user);
            return RedirectToAction("Index");
            //else return to create with errors
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            UserService users = new UserService();
            UserFM userFM = users.GetUserFM(ID);
            return View(userFM);
        }
        [HttpPost]
        public ActionResult Edit(UserFM userFM)
        {
            //if user valid edit user
            UserService users = new UserService();
            users.UpdateUser(userFM);
            return RedirectToAction("Index");
            //else return edit with errors
        }
        [HttpGet]
        public ActionResult ChangePassword(int ID)
        {
            UserService users = new UserService();
            return View(users.GetPasswordFM(ID));
        }
        [HttpPost]
        public ActionResult ChangePassword(PasswordFM passwordFM)
        {
            //if user valid edit pass
            UserService users = new UserService();
            if(users.VerifyPassword(passwordFM) && passwordFM.NewPassword == passwordFM.VerifyPassword && passwordFM.NewPassword.Length > 7)
            {
                users.UpdatePassword(passwordFM);
            }
            return RedirectToAction("Index");
            //else return edit with errors
        }
        public ActionResult ResetPassword(int ID)
        {
            UserService users = new UserService();
            users.ResetPassword(ID);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int ID)
        {
            UserService users = new UserService();
            users.DeleteUser(ID);
            return RedirectToAction("Index");
        }
	}
}