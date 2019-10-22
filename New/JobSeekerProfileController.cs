using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication18.DBContext;
using WebApplication18.Models;


namespace WebApplication18.Controllers
{
    public class JobSeekerProfileController : Controller
    {
        // GET: JobSeekerProfile
        [HttpGet]
        public ActionResult JobSProfile()
        {
            return View();
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult JobSProfile(JSProfile C, HttpPostedFileBase file)
        {
            if (file == null)
            {
                ModelState.AddModelError("CustomError", "Please select CV");
                return View();
            }

            if (!(file.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" ||
                file.ContentType == "application/pdf"))
            {
                ModelState.AddModelError("CustomError", "Only .docx and .pdf file allowed");
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    file.SaveAs(Path.Combine(Server.MapPath("~/UploadedCV"), fileName));
                    using (JobTinderEntities dc = new JobTinderEntities())
                    {
                        C.ImagePath = fileName;
                        dc.JSProfiles.Add(C);
                        dc.SaveChanges();
                    }
                    ModelState.Clear();
                    C = null;
                    ViewBag.Message = "Successfully Done";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error! Please try again";
                    return View();
                }
            }

           /* string filename = Path.GetFileNameWithoutExtension(file.ImagePath.FileName);
            string fileExt = Path.GetExtension(file.ImagePath.FileName);
            filename = DateTime.Now.ToString("yyyyMMdd") + " " + filename.Trim()  + fileExt;
            // string uploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
            string uploadPath = ConfigurationManager.AppSettings["UserImagePath"];
            HttpPostedFileBase temp = new HttpPostedFileWrapper(uploadPath);
            file.ImagePath = uploadPath + filename;
            file.ImagePath.SaveAs(file.ImagePath)*/
            return View();
        }
    }
}