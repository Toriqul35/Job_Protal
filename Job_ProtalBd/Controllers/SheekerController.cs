﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Job_ProtalBd.Models;
using System.IO;
using System.Net.Mail;
using System.Web.Security;
using System.Net;

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

             
                // SendVerificationLinkEmail(Registration.E_Mail,Registration.ActivationCode.ToString());
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
            ViewBag.Status = Status;
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
            return RedirectToAction("Index", "Home");
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
        public ActionResult Setting()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UploadResume()
        {
            return View();
        }
     
        [HttpPost]
        public ActionResult UploadResume(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                string fil = Guid.NewGuid() +Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath("~/UploadResume"), fil));
                
            }
            return View("file uploaded successfully");
        }


        [NonAction]
        public bool IsEmailExist(string E_Mail)
        {
            using (SheekerEntities1 dc = new SheekerEntities1())
            {
                Registration v = dc.Registrations.Where(a => a.E_Mail == E_Mail).FirstOrDefault();
                return v != null;
            }
            
        }
       /*[NonAction]
        public void SendVerificationLinkEmail(string E_Mail, string ActivationCode)
        {
            var scheme = Request.Url.Scheme;
            var host = Request.Url.Host;
            var port = Request.Url.Port;

            string url = scheme + "://" + host;

            var verifyUrl = "//" + ActivationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("jobportalbd@gmail.com", "Dotnet Awesome");
            var toEmail = new MailAddress(E_Mail);
            var fromEmailPassword = "********"; // Replace with actual password
            string subject = "Your account is successfully created!";

            string body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                " successfully created. Please click on the below link to verify your account" +
                " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }*/

    }
}