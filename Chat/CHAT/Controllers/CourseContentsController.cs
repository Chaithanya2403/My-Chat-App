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
    public class CourseContentsController : Controller
    {
        private Entities db = new Entities();

        // GET: CourseContents
        public ActionResult Index()
        {
            return View(db.CourseContents.ToList());
        }

        // GET: CourseContents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseContent courseContent = db.CourseContents.Find(id);
            if (courseContent == null)
            {
                return HttpNotFound();
            }
            return View(courseContent);
        }

        // GET: CourseContents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseContents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,DomainId,CourseId,ContentCategory,Details,FilePath,UpdatedOn")] CourseContent courseContent)
        {
            if (ModelState.IsValid)
            {
                db.CourseContents.Add(courseContent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseContent);
        }

        // GET: CourseContents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseContent courseContent = db.CourseContents.Find(id);
            if (courseContent == null)
            {
                return HttpNotFound();
            }
            return View(courseContent);
        }

        // POST: CourseContents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,DomainId,CourseId,ContentCategory,Details,FilePath,UpdatedOn")] CourseContent courseContent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseContent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseContent);
        }

        // GET: CourseContents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseContent courseContent = db.CourseContents.Find(id);
            if (courseContent == null)
            {
                return HttpNotFound();
            }
            return View(courseContent);
        }

        // POST: CourseContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseContent courseContent = db.CourseContents.Find(id);
            db.CourseContents.Remove(courseContent);
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
