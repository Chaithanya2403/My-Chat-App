using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BussinessLayer;
using System.IO;
using System.Data.Entity.Infrastructure;

namespace SRICHIDACADEMY.Controllers
{
    public class UserProfilesController : Controller
    {
        private Sdo_ChatEntities db = new Sdo_ChatEntities();


        DataProvider dataProvider = new DataProvider();

        // GET: UserProfiles
        //public ActionResult Index()
        //{
        //    //int userId = Convert.ToInt32(Session["UserId"]);
        //    var userProfiles = db.UserProfiles.Include(u => u.UserRole);
        //    return View(userProfiles.ToList());
        //}
        public ActionResult UserList()
        {
            var userlist = dataProvider.GetUsers().AsEnumerable();
            ViewBag.Users = userlist;
            return View();
        }

        // GET: UserProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // GET: UserProfiles/Create
        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(db.UserRoles, "Id", "RoleName");
            return View();
        }

        // POST: UserProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserProfile userProfile)
        {
            //HttpPostedFileBase usrImg = Request.Files["userImage"];
            //if (usrImg != null)
            //{
            //    string pic = System.IO.Path.GetFileName(usrImg.FileName);
            //    string path = System.IO.Path.Combine(Server.MapPath("../images/Profile"), pic);
            //    usrImg.SaveAs(path);

            //    userProfile.UserImage = string.Concat("../images/Profile/", pic);

            //    using (MemoryStream ba = new MemoryStream())
            //    {
            //        usrImg.InputStream.CopyTo(ba);
            //        byte[] array = ba.GetBuffer();
            //    }
            //}

               
            db.UserProfiles.Add(userProfile);
            db.SaveChanges();
            //ViewBag.status = "Your Registration is Compleated Successfully go to Home Page";
            ViewBag.Role = new SelectList(db.UserRoles, "Id", "RoleName", userProfile.Role);
            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role = new SelectList(db.UserRoles, "Id", "RoleName", userProfile.Role);
            return View(userProfile);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,FullName,Email,Password,ConfirmPassword,UserImage,Mobile,Role")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Role = new SelectList(db.UserRoles, "Id", "RoleName", userProfile.Role);
            return View(userProfile);
        }

        // GET: UserProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // POST: UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile userProfile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userProfile);
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
