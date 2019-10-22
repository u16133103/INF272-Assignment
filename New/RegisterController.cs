using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication18.DBContext;
using WebApplication18.Models;

namespace WebApplication18.Controllers
{
    public class RegisterController : Controller
    {
        JobTinderEntities db = new JobTinderEntities();

        public ActionResult Homepage()
        {
            return View();
        }
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }
        #region Registration post method for data save 
        [HttpPost]
        public ActionResult Register(CRegister obj)
        {
            /*try
            {*/
                obj.EmailVerification = false;
                var IsExists = IsEmailExists(obj.Email);
                if (IsExists)
                {
                    ModelState.AddModelError("EmailExists", "Email Already Exists");
                    return View();
                }
                
                obj.Password = encryptPassword.textToEncrypt(obj.Password);
                obj.ActivationCode = Guid.NewGuid();
                db.CRegisters.Add(obj);
                db.SaveChanges();

                #region Send Email Verification Link
                SendEmailToUser(obj.Email, obj.ActivationCode.ToString());
                var Message = "Registration completed. Please check your email :" + obj.Password;
                ViewBag.Message = Message;
                #endregion
                return RedirectToAction("Registration", "Register");
            /*}
            catch {Exception e }
            return View();*/
        }

        public ActionResult Registration()
        {
            return View();
        }
        #endregion

        #region Check Email Exists or not in DB  
        public bool IsEmailExists(string eMail)
        {
            var IsCheck = db.CRegisters.Where(email => email.Email == eMail).FirstOrDefault();
            return IsCheck != null;
        }
        #endregion

        public void SendEmailToUser(string emailId, string activationCode)
        {
            var GenarateUserVerificationLink = "/Register/UserVerification/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, GenarateUserVerificationLink);

            var fromMail = new MailAddress("polaki.blantina@gmail.com", "JobTinder"); // set your email  
            var fromEmailpassword = "mapaseka"; // Set your password   
            var toEmail = new MailAddress(emailId);

            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(fromMail.Address, fromEmailpassword);

            var Message = new MailMessage(fromMail, toEmail);
            Message.Subject = "Registration Completed-Demo";
            Message.Body = "<br/> Your registration completed succesfully." +
                           "<br/> please click on the below link for account verification" +
                           "<br/><br/><a href=" + link + ">" + link + "</a>";
            Message.IsBodyHtml = true;
            smtp.Send(Message);
        }

        #region Verification from Email Account.  
        public ActionResult UserVerification(string id)
        {
            bool Status = false;

            db.Configuration.ValidateOnSaveEnabled = false; // Ignor to password confirmation   
            var IsVerify = db.CRegisters.Where(u => u.ActivationCode == new Guid(id)).FirstOrDefault();

            if (IsVerify != null)
            {
                
                IsVerify.EmailVerification = true;
                db.SaveChanges();
                ViewBag.Message = "Email Verification completed";
                Status = true;
            }
            else
            {
                ViewBag.Message = "Invalid Request";
                //ViewBag.Status = false;
                //Status = false;
            }
            ViewBag.Status = Status; 
            return View();
        }
        #endregion

        #region User Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin logUsr, string ReturnUrl="")
        {

            /* var _passWord = encryptPassword.textToEncrypt(logUsr.Password);
             bool Isvalid = db.CRegisters.Any(x => x.Email == logUsr.EmailId &&  x.EmailVerification == true &&
             x.Password == _passWord);
             if (Isvalid)
             {
                 int timeout = logUsr.Rememberme ? 60 : 5; // Timeout in minutes, 60 = 1 hour.  
                 var ticket = new FormsAuthenticationTicket(logUsr.EmailId, false, timeout);
                 string encrypted = FormsAuthentication.Encrypt(ticket);
                 var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                 cookie.Expires = System.DateTime.Now.AddMinutes(timeout);
                 cookie.HttpOnly = true;
                 Response.Cookies.Add(cookie);
                 return RedirectToAction("CompanyProfile", "UserDash");
             }
             else
             {
                 ModelState.AddModelError("", "Invalid Information... Please try again!");
             }
             return View();*/
            /* var pass = encryptPassword.textToEncrypt(logUsr.Password);
             var _admin = db.CRegisters.Where(s => s.Email == logUsr.EmailId);
             if (_admin.Any())
             {
                 if (_admin.Where(s => s.Password == pass).Any())
                 {

                     return Json(new { status = true, message = "Login Successfull!" });
                 }
                 else
                 {
                     return Json(new { status = false, message = "Invalid Password!" });
                 }
             }
             else
             {
                 return Json(new { status = false, message = "Invalid Email!" });
             }*/

            string message = "";
            using (JobTinderEntities dc = new JobTinderEntities())
            {
                var v = db.CRegisters.Where(a => a.Email == logUsr.EmailId && a.EmailVerification == true).FirstOrDefault();
                if (v != null)
                {
                    /*if (!v.EmailVerification)
                    {
                        ViewBag.Message = "Please verify your email first";
                        return View();
                    }*/
                    if (string.Compare(encryptPassword.textToEncrypt(logUsr.Password), v.Password) == 0)
                    {
                        int timeout = logUsr.Rememberme ? 60 : 5; // 60 = 1 hour.
                        var ticket = new FormsAuthenticationTicket(logUsr.EmailId, logUsr.Rememberme, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("JobSProfile", "JobSeekerProfile");
                        }
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(ForgetPassword pass)
        {
            var IsExists = IsEmailExists(pass.EmailId);
            if (!IsExists)
            {
                ModelState.AddModelError("EmailNotExists", "This email is not exists");
                return View();
            }
            var objUsr = db.CRegisters.Where(x => x.Email == pass.EmailId).FirstOrDefault();

            // Genrate OTP   
            string OTP = GeneratePassword();

            objUsr.ActivationCode = Guid.NewGuid();
            objUsr.OPT = OTP;
            db.Entry(objUsr).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            
            ForgetPasswordEmailToUser(objUsr.Email, objUsr.ActivationCode.ToString(), objUsr.OPT);
            return View();
        }

        public string GeneratePassword()
        {
            string OTPLength = "4";
            string OTP = string.Empty;

            string Chars = string.Empty;
            Chars = "1,2,3,4,5,6,7,8,9,0";

            char[] seplitChar = { ',' };
            string[] arr = Chars.Split(seplitChar);
            string NewOTP = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(OTPLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                NewOTP += temp;
                OTP = NewOTP;
            }
            return OTP;
        }

        public void ForgetPasswordEmailToUser(string emailId, string activationCode, string OTP)
        {
            var GenerateUserVerificationLink = "/Register/ChangePassword/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, GenerateUserVerificationLink);

            var fromMail = new MailAddress("polaki.blantina@gmail.com", "JobTinder"); // set your email  
            var fromEmailpassword = "mapaseka"; // Set your password   
            var toEmail = new MailAddress(emailId);

            //var smtp = new SmtpClient();
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(fromMail.Address, fromEmailpassword);

            var Message = new MailMessage(fromMail, toEmail);
            Message.Subject = "Password Reset-Demo";
            Message.Body = "<br/> Please click on the below link for password change" +
                            "<br/><br/><a href=" + link + ">" + link + "</a>" +
                            "<br/>OTP for password change : " + OTP;
                           
            Message.IsBodyHtml = true;
            smtp.Send(Message);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}