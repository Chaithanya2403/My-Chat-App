using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BussinessLayer;

namespace SRICHIDACADEMY.Controllers
{
    public class FacultyDetailsController : Controller
    {
        private Entities db = new Entities();

        // GET: FacultyDetails
        public ActionResult Index()
        {
            return View(db.FacultyDetails.ToList());
        }

        // GET: FacultyDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacultyDetail facultyDetail = db.FacultyDetails.Find(id);
            if (facultyDetail == null)
            {
                return HttpNotFound();
            }
            return View(facultyDetail);
        }

        // GET: FacultyDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FacultyDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Designation,Experience,SpecelizedAreas,CompensationperHour,CreatedOn,CreatedBy,UpdatedOn")] FacultyDetail facultyDetail)
        {
            if (ModelState.IsValid)
            {
                db.FacultyDetails.Add(facultyDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(facultyDetail);
        }

        // GET: FacultyDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacultyDetail facultyDetail = db.FacultyDetails.Find(id);
            if (facultyDetail == null)
            {
                return HttpNotFound();
            }
            return View(facultyDetail);
        }

        // POST: FacultyDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Designation,Experience,SpecelizedAreas,CompensationperHour,CreatedOn,CreatedBy,UpdatedOn")] FacultyDetail facultyDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facultyDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(facultyDetail);
        }

        // GET: FacultyDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacultyDetail facultyDetail = db.FacultyDetails.Find(id);
            if (facultyDetail == null)
            {
                return HttpNotFound();
            }
            return View(facultyDetail);
        }

        // POST: FacultyDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FacultyDetail facultyDetail = db.FacultyDetails.Find(id);
            db.FacultyDetails.Remove(facultyDetail);
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
