using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication18.DBContext;

namespace WebApplication18.Controllers
{
    public class LisitingsController : Controller
    {
        private JobTinderEntities db = new JobTinderEntities();

        // GET: Lisitings
        public ActionResult Index()
        {
            return View(db.CProfiles.ToList());
        }

        // GET: Lisitings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lisiting lisiting = db.Lisitings.Find(id);
            if (lisiting == null)
            {
                return HttpNotFound();
            }
            return View(lisiting);
        }

        // GET: Lisitings/Create
        /*public ActionResult Create()
        {
            return View();
        }

        // POST: Lisitings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Company_ID,Com_JobPosition,Com_CareerField,Com_Location")] Lisiting lisiting)
        {
            if (ModelState.IsValid)
            {
                db.Lisitings.Add(lisiting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lisiting);
        }*/

        // GET: Lisitings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lisiting lisiting = db.Lisitings.Find(id);
            if (lisiting == null)
            {
                return HttpNotFound();
            }
            return View(lisiting);
        }

        // POST: Lisitings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Company_ID,Com_JobPosition,Com_CareerField,Com_Location")] Lisiting lisiting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lisiting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lisiting);
        }

        // GET: Lisitings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lisiting lisiting = db.Lisitings.Find(id);
            if (lisiting == null)
            {
                return HttpNotFound();
            }
            return View(lisiting);
        }

        // POST: Lisitings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lisiting lisiting = db.Lisitings.Find(id);
            db.Lisitings.Remove(lisiting);
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
