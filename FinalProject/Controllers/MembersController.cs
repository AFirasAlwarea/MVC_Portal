using FinalProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class MembersController : Controller
    {
        public static List<string> positions = new List<string> { "Attack", "Middle Field", "Defence", "Goal Keepr" };

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Members
        public ActionResult Index()
        {
            var model = from x in db.Users
                        orderby x.Fullname.ToUpper()
                        select x;

            return View(model.ToList());
        }

        //public static List<ApplicationUser> _filtered = new List<ApplicationUser>();

        //[HttpGet]
        //public ActionResult filter()
        //{
        //var model = from x in _filtered
        //            orderby x.Fullname
        //            select x;
        //    return View("Index", model);
        //}

        //[HttpPost]
        //public ActionResult filter(string search)
        //{
        //    _filtered = new List<ApplicationUser>();
        //    search = search.ToLower();
        //    var onePlayer = db.Users.ToList();
        //    foreach (var item in onePlayer)
        //    {
        //        //if (item.Fullname.ToUpper().Contains(search))
        //        //{
        //    var user = db.Users.FirstOrDefault(u => u.UserName == search);
        //            _filtered.Add(item);
        //        //}
        //    }
        //    return RedirectToAction("filter", _filtered);
        //}

        public ActionResult Edit(string id)
        {

            ViewBag.Roles = db.Roles;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser USer = db.Users.Find(id);
            if (USer == null)
            {
                return HttpNotFound();
            }
            return View(USer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fullname,Address,Birthdate,Position,Email")] ApplicationUser Users, string Roles)
        {
            var store = new RoleStore<IdentityRole>(db);
            var userstor = new UserStore<ApplicationUser>(db);
            var roleManager = new RoleManager<IdentityRole>(store);
            var UserManager = new UserManager<ApplicationUser>(userstor);

            if (ModelState.IsValid)
            {
                ApplicationUser Temp = db.Users.Find(Users.Id);
                Temp.Fullname = Users.Fullname;
                Temp.Birthdate = Users.Birthdate;
                Temp.Address = Users.Address;
                Temp.Position = Users.Position;
                Temp.Email = Users.Email;
                Temp.Roles.Clear();
                UserManager.AddToRole(Temp.Id, Roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Users);
        }



        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser post = db.Users.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser post = db.Users.Find(id);
            post.Messages.Clear();
            post.Posts.Clear();
            post.Roles.Clear();
            db.Users.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {

            ViewBag.Roles = db.Roles;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser USer = db.Users.Find(id);
            if (USer == null)
            {
                return HttpNotFound();
            }
            return View(USer);
        }

        public ActionResult Create()
        {

            ViewBag.Roles = db.Roles;
            ViewBag.positions = positions;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Fullname, string Email, string Password, string Address, string Position, DateTime Birthdate, string Role)
        {
            var store = new RoleStore<IdentityRole>(db);
            var userstor = new UserStore<ApplicationUser>(db);
            var roleManager = new RoleManager<IdentityRole>(store);
            var UserManager = new UserManager<ApplicationUser>(userstor);

            //ApplicationDbContext context = new ApplicationDbContext();
            ApplicationUser NewUser = new ApplicationUser();
            PasswordHasher hash = new PasswordHasher();
            var hashedPassword = hash.HashPassword(Password);

            // It didn't work because I used the context which is a new instance of ApplicatonDbContext.
            // I could do it with db instead.
            //context.Users.AddOrUpdate(U => U.Email,       
            //    new ApplicationUser {                     
            //        Fullname = Fullname,                   
            //        Email = Email,                        
            //        PasswordHash = Password,              
            //        Address = Address,
            //        Position = Position,
            //        Birthdate = Birthdate,
            //        UserName = Email                    
            //    });

            //NewUser = db.Users.Find(Fullname);
            NewUser.Position = Position;
            NewUser.Address = Address;
            NewUser.Birthdate = Birthdate;
            NewUser.Email = Email;
            NewUser.Fullname = Fullname;
            NewUser.UserName = Email;
            //NewUser.PasswordHash = hashedPassword;
            //db.Users.AddOrUpdate(U => U.Email , NewUser);




            var available = UserManager.FindByEmail(NewUser.Email);
            if (available == null)
            {
                // It is better to use this way for the Password to be hashed and salted.
                UserManager.Create(NewUser, Password);
                db.SaveChanges();
                UserManager.AddToRole(NewUser.Id, Role);
            }
            return RedirectToAction("Index");
        }
    }
}