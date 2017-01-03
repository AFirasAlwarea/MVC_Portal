using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;


namespace FinalProject.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize(Roles = ("Admin,Player"))]
        public ActionResult Create()
        {

            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Admin,Player"))]
        public ActionResult Create([Bind(Include = "Id,Subject,Body")] Post post)
        {

            var store = new RoleStore<IdentityRole>(db);
            var userstor = new UserStore<ApplicationUser>(db);
            var roleManager = new RoleManager<IdentityRole>(store);
            var UserManager = new UserManager<ApplicationUser>(userstor);

            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                post.Datecreated = DateTime.Now;
                db.Posts.Add(post);
                user.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("partiaView",post.CommentsList);
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string Comment, int Id)
        {

            var store = new RoleStore<IdentityRole>(db);
            var userstor = new UserStore<ApplicationUser>(db);
            var roleManager = new RoleManager<IdentityRole>(store);
            var UserManager = new UserManager<ApplicationUser>(userstor);

            ApplicationUser user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            Comment com = new Comment();
            Post currentPost = db.Posts.Where(p => p.Id == Id).FirstOrDefault();
            
                com.Body = Comment;
                com.DateComment = DateTime.Now;
                com.WriterId = user.Fullname;
                currentPost.CommentsList.Add(com);
                db.SaveChanges();


            return PartialView("partialView", currentPost.CommentsList);
            

        }

        public ActionResult JSONExample(int? commen)
        {

            return PartialView("JSONExample");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteComment(int? editpers)
        {

            Comment com = db.Comments.Where(p => p.Id == (int)editpers).FirstOrDefault();
            db.Comments.Remove(com);
            db.SaveChanges();

            return PartialView();
        }


        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            post.CommentsList.Clear();
            db.Posts.Remove(post);
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
