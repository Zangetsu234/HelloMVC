﻿using BLL;
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
        public void Delete(int ID)
        {

        }
	}
}