using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BussinessLayer;
using System.Data.Entity;

namespace SRICHIDACADEMY.Controllers
{

    public class HomeController : Controller
    {
        private Sdo_ChatEntities db = new Sdo_ChatEntities();

        UserManagement userManagement = new UserManagement();
        public ActionResult Index()
        {
            BussinessLayer.Sdo_ChatEntities db = new BussinessLayer.Sdo_ChatEntities();
            var users = db.UserProfiles.ToList();
            return View(users);
        }

        public ActionResult About()
        {
            ViewBag.Message = "About";

            return View();
        }

        public ActionResult Faculty()
        {
            ViewBag.Message = "Faculty";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Riskmethod()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Digitalmethod()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username , string password)
        {
            if(userManagement.IsValidUser(username , password))
            {
                var userRole = userManagement.GetUserRoleByUserName(username);
                var userId = userManagement.GetUserIdByUserName(username);
                Session["Username"] = username;
                Session["UserPassword"] = password;
                Session["Role"] = userRole;
                if(userRole == "Admin")
                {
                    return RedirectToAction("AdminHomePage","Home");
                }
                else if(userRole == "General")
                {
                     return RedirectToAction("Index","Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["ErrorMessgae"] = "Invalid UserName/Password";
                return RedirectToAction("ErrorDisplay","Home");
            }
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult AssignRole()
        {
            ViewBag.UserId = new SelectList(db.UserProfiles, "Id", "FullName");
            ViewBag.Roles = new SelectList(db.UserRoles, "Id", "RoleName");
            return View();
        }
        [HttpPost]
        public ActionResult AssignRole(FormCollection collection)
        {
            //string username = collection["UserId"];
            //string rolename = collection["Roles"];
            int userId = Convert.ToInt32(collection["UserId"]);
            int roleId = Convert.ToInt32(collection["Roles"]);
            UserProfile userProfile = new UserProfile();
            userProfile = db.UserProfiles.Where(c => c.Id == userId).Select(c => c).First();
            userProfile.Role = roleId;
            if (ModelState.IsValid)
            {
                db.Entry(userProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        
        // POST: /Manage/AddUser
        [HttpPost]
        public ActionResult AddUser(string name, string email, string phone, string message)
        {
            BussinessLayer.Sdo_ChatEntities db = new BussinessLayer.Sdo_ChatEntities();
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = new BussinessLayer.ManageUser()
            {
                Username = name,
                Email = email,
                Password = phone,
                OperationUnit = message
            };
            db.ManageUsers.Add(user);
            db.SaveChanges();
            ViewBag.Status = "Your Request has been received. We will get back you soon!";
            return View();
        }
        // GET: Courses
        public ActionResult Courses()
        {
            BussinessLayer.Sdo_ChatEntities db = new BussinessLayer.Sdo_ChatEntities();
            var courses = db.CourseDetails.ToList();
            return View(courses);
        }
        public ActionResult CourceOverview()
        {
            return View();
        }
        public ActionResult FAQs()
        {
            return View();
        }

        public ActionResult UserHomePage()
        {
            return View();
        }
        public ActionResult Chat()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.UserName = Session["UserName"];
            ViewBag.Password = Session["UserPassword"];
            //ViewBag.UserRole = Session["Role"];

            return View();
        }
        public ActionResult SuccessDisplay()
        {
            return View();
        }
        public ActionResult AdminHomePage()
        {
            return View();
        }
    }
}