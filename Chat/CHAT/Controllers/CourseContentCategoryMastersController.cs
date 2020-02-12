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
    public class CourseContentCategoryMastersController : Controller
    {
        private Entities db = new Entities();

        // GET: CourseContentCategoryMasters
        public ActionResult Index()
        {
            return View(db.CourseContentCategoryMasters.ToList());
        }

        // GET: CourseContentCategoryMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseContentCategoryMaster courseContentCategoryMaster = db.CourseContentCategoryMasters.Find(id);
            if (courseContentCategoryMaster == null)
            {
                return HttpNotFound();
            }
            return View(courseContentCategoryMaster);
        }

        // GET: CourseContentCategoryMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseContentCategoryMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourseContentCategory")] CourseContentCategoryMaster courseContentCategoryMaster)
        {
            if (ModelState.IsValid)
            {
                db.CourseContentCategoryMasters.Add(courseContentCategoryMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseContentCategoryMaster);
        }

        // GET: CourseContentCategoryMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseContentCategoryMaster courseContentCategoryMaster = db.CourseContentCategoryMasters.Find(id);
            if (courseContentCategoryMaster == null)
            {
                return HttpNotFound();
            }
            return View(courseContentCategoryMaster);
        }

        // POST: CourseContentCategoryMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseContentCategory")] CourseContentCategoryMaster courseContentCategoryMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseContentCategoryMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseContentCategoryMaster);
        }

        // GET: CourseContentCategoryMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseContentCategoryMaster courseContentCategoryMaster = db.CourseContentCategoryMasters.Find(id);
            if (courseContentCategoryMaster == null)
            {
                return HttpNotFound();
            }
            return View(courseContentCategoryMaster);
        }

        // POST: CourseContentCategoryMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseContentCategoryMaster courseContentCategoryMaster = db.CourseContentCategoryMasters.Find(id);
            db.CourseContentCategoryMasters.Remove(courseContentCategoryMaster);
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
