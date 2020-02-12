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
    public class DomainMastersController : Controller
    {
        private Entities db = new Entities();

        // GET: DomainMasters
        public ActionResult Index()
        {
            return View(db.DomainMasters.ToList());
        }

        // GET: DomainMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DomainMaster domainMaster = db.DomainMasters.Find(id);
            if (domainMaster == null)
            {
                return HttpNotFound();
            }
            return View(domainMaster);
        }

        // GET: DomainMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DomainMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Domain,CategoryId,CreatedOn,UpdatedOn")] DomainMaster domainMaster)
        {
            if (ModelState.IsValid)
            {
                db.DomainMasters.Add(domainMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(domainMaster);
        }

        // GET: DomainMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DomainMaster domainMaster = db.DomainMasters.Find(id);
            if (domainMaster == null)
            {
                return HttpNotFound();
            }
            return View(domainMaster);
        }

        // POST: DomainMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Domain,CategoryId,CreatedOn,UpdatedOn")] DomainMaster domainMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(domainMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(domainMaster);
        }

        // GET: DomainMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DomainMaster domainMaster = db.DomainMasters.Find(id);
            if (domainMaster == null)
            {
                return HttpNotFound();
            }
            return View(domainMaster);
        }

        // POST: DomainMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DomainMaster domainMaster = db.DomainMasters.Find(id);
            db.DomainMasters.Remove(domainMaster);
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
