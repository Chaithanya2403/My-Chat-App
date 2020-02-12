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
    public class UserCourseDetailsController : Controller
    {
        private Entities db = new Entities();

        // GET: UserCourseDetails
        public ActionResult Index()
        {
            return View(db.UserCourseDetails.ToList());
        }

        // GET: UserCourseDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCourseDetail userCourseDetail = db.UserCourseDetails.Find(id);
            if (userCourseDetail == null)
            {
                return HttpNotFound();
            }
            return View(userCourseDetail);
        }

        // GET: UserCourseDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserCourseDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,CategoryId,DomainId,CourseId,CreatedOn,UpdatedOn")] UserCourseDetail userCourseDetail)
        {
            if (ModelState.IsValid)
            {
                db.UserCourseDetails.Add(userCourseDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userCourseDetail);
        }

        // GET: UserCourseDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCourseDetail userCourseDetail = db.UserCourseDetails.Find(id);
            if (userCourseDetail == null)
            {
                return HttpNotFound();
            }
            return View(userCourseDetail);
        }

        // POST: UserCourseDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,CategoryId,DomainId,CourseId,CreatedOn,UpdatedOn")] UserCourseDetail userCourseDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userCourseDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userCourseDetail);
        }

        // GET: UserCourseDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCourseDetail userCourseDetail = db.UserCourseDetails.Find(id);
            if (userCourseDetail == null)
            {
                return HttpNotFound();
            }
            return View(userCourseDetail);
        }

        // POST: UserCourseDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserCourseDetail userCourseDetail = db.UserCourseDetails.Find(id);
            db.UserCourseDetails.Remove(userCourseDetail);
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
