using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication18.DBContext;
//using Microsoft.Reprting.WebForms;

namespace WebApplication18.Controllers
{
    public class CProfilesController : Controller
    {
        private JobTinderEntities db = new JobTinderEntities();

        // GET: CProfiles
        public ActionResult Index(string searching)
        {

            return View(db.JSProfiles.Where(a=>a.FirstName.Contains(searching) || searching == null).ToList());
        }

       /* public ActionResult Reports(string searching)
        {
            LocalReport localreport = new LocalReport();

            return View();
        }*/

        // GET: CProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CProfile cProfile = db.CProfiles.Find(id);
            if (cProfile == null)
            {
                return HttpNotFound();
            }
            return View(cProfile);
        }

        // GET: CProfiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CompanyName,CompanyNumber,Email,Address,Description")] CProfile cProfile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.CProfiles.Add(cProfile);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException ex )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return View(cProfile);
        }*/

        // GET: CProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CProfile cProfile = db.CProfiles.Find(id);
            if (cProfile == null)
            {
                return HttpNotFound();
            }
            return View(cProfile);
        }

        // POST: CProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CompanyName,CompanyNumber,Email,Address,Description")] CProfile cProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cProfile);
        }

        // GET: CProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CProfile cProfile = db.CProfiles.Find(id);
            if (cProfile == null)
            {
                return HttpNotFound();
            }
            return View(cProfile);
        }

        // POST: CProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CProfile cProfile = db.CProfiles.Find(id);
            db.CProfiles.Remove(cProfile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
