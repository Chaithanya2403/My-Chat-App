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
    public class ManageUsersController : Controller
    {
        private Sdo_ChatEntities db = new Sdo_ChatEntities();

        // GET: ManageUsers
        public ActionResult Index()
        {
            return View(db.ManageUsers.ToList());
        }

        // GET: ManageUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManageUser manageUser = db.ManageUsers.Find(id);
            if (manageUser == null)
            {
                return HttpNotFound();
            }
            return View(manageUser);
        }

        // GET: ManageUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Email,Password,ConfirmPassword,UserroleName,UserState,UserDistrict,UserTaluk,UserGramapanchayath,UserVillage,UserId,OperationUnit")] ManageUser manageUser)
        {
            if (ModelState.IsValid)
            {
                db.ManageUsers.Add(manageUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manageUser);
        }

        // GET: ManageUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManageUser manageUser = db.ManageUsers.Find(id);
            if (manageUser == null)
            {
                return HttpNotFound();
            }
            return View(manageUser);
        }

        // POST: ManageUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Email,Password,ConfirmPassword,UserroleName,UserState,UserDistrict,UserTaluk,UserGramapanchayath,UserVillage,UserId,OperationUnit")] ManageUser manageUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manageUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manageUser);
        }

        // GET: ManageUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManageUser manageUser = db.ManageUsers.Find(id);
            if (manageUser == null)
            {
                return HttpNotFound();
            }
            return View(manageUser);
        }

        // POST: ManageUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ManageUser manageUser = db.ManageUsers.Find(id);
            db.ManageUsers.Remove(manageUser);
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
