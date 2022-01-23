using FlamingoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flamingo_Airways.Models;

namespace Flamingo_Airways.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginView(Passenger p)
        {
            //ViewBag.username = p.FName;
            LoginDAL loginDAL = new LoginDAL();
            if(loginDAL.AuthenticateUser(p))
            {
                TempData["useremail"] = p.Email;
               
                //Response.Write("Login Success");
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.errormsg = "Either Email or Password is incorrect. Try again";
                Response.Write("Either Email or Password is incorrect. Try again");
                return View();
            }
        }
        public ActionResult RegisterView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterView(LoginModel lm)
        {
            LoginDAL loginDAL = new LoginDAL();
            if(loginDAL.EmailExist(lm.Email))
            {
                ViewBag.erroremail = "Email Already Registered";
                return View();
            }


            Passenger p = new Passenger();
            TempData["username"] =lm.FName;
            p.FName = lm.FName;
            p.LName = lm.LName;
            p.Email = lm.Email;
            p.Pwd = lm.Pwd;
            p.PhoneNo = lm.PhoneNo;
            p.Age = lm.Age;
            loginDAL.Registration(p);
            return View("AccountCreated");
        }
        public ActionResult ForgetPass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPass(LoginModel m)
        {

            LoginDAL logindal = new LoginDAL();
            if (!logindal.EmailExist(m.Email))
            {
                ViewBag.femail = "Email Id not Registered";
                return View();
            }
            Passenger p = new Passenger();
            p.Email = m.Email;
            p.Pwd = m.Pwd;
            logindal.ChangePassword(p);
            ViewBag.msg = "Password Changed Successfully!!";
            return View();
        }




    }
}