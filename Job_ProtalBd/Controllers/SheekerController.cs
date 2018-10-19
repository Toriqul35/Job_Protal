using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Job_ProtalBd.Models;
using System.Net.Mail;
using System.Net;
using System.Web.Security;

namespace Job_ProtalBd.Controllers
{
    public class SheekerController : Controller
    {
        // GET: Registration1
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult Registration([Bind(Exclude = "IsEmailVerried,ActivationCode")]Registration Registration)
        {
            bool Status = false;
            String message = "";
            
            // Mode validation
            if (ModelState.IsValid)
            {
                //email is alredey code
                var isExist = IsEmailExist(Registration.E_Mail);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email Already Exist");
                    return View(Registration);
                }
                //Genarete active code
              Registration.ActivationCode = Guid.NewGuid();

                //password hasshin
                Registration.Password = Crypto.Hash(Registration.Password);
                Registration.ConfrimPassword = Crypto.Hash(Registration.ConfrimPassword);

                Registration.IsEmailVerried = false;

                using (SheekerEntities1 dc = new SheekerEntities1())
                {
                    dc.Registrations.Add(Registration);
                    dc.SaveChanges();

                    message = "Registration Sucessfully done.Account activication link" +
                              "has been send your email id:"+Registration.E_Mail;

                    Status = true;
                }
            }
            else
            {
                message = "Invalid Request";
            }
            //send user
            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(Registration);
          }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using(SheekerEntities1 dc =new SheekerEntities1())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;
                var v = dc.Registrations.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if(v != null)
                {
                    v.IsEmailVerried = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }

            }
            ViewBag.Status =Status;
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login, string ReturnUrl="")
        {
            string message = "";
            using(SheekerEntities1 dc = new SheekerEntities1())
            {
            var v=dc.Registrations.Where(a=>a.E_Mail==login.E_Mail).FirstOrDefault();
                if (v !=null)
                {
                    if (string.Compare(Crypto.Hash(login.Password),v.Password)==0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(login.E_Mail, login.RememberMe,timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookise = new HttpCookie(FormsAuthentication.FormsCookieName,encrypted);

                        cookise.Expires = DateTime.Now.AddMinutes(timeout);
                        cookise.HttpOnly = true;
                        Response.Cookies.Add(cookise);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);

                        }
                        else
                        {
                            return RedirectToAction("Indo", "Home");
                        }
                    }
                    else
                    {
                        message = "InValid  provided";
                    }
                }
                else
                {
                    message = "InValid  provided";
                }
            }
             ViewBag.Message = message;
             return  View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Registration1");
        }

        public ActionResult list()
        {
            var Registrations = new List<Registration>();
                using (SheekerEntities1 dc =new SheekerEntities1())
            {
                Registrations = dc.Registrations.ToList();

            }
            return View(Registrations);
        }

        [NonAction]
        public bool IsEmailExist(string E_Mail)
        {
            using (SheekerEntities1 dc = new SheekerEntities1())
            {
                var v = dc.Registrations.Where(a => a.E_Mail == E_Mail).FirstOrDefault();
                return v != null;
            }
        }
        
    }
}