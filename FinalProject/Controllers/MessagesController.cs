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
using Microsoft.AspNet.Identity.Owin;
using System.Web.UI;

namespace FinalProject.Controllers
{
    public class MessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Messages
        public ActionResult Index()
        {
            var store = new RoleStore<IdentityRole>(db);
            var userstor = new UserStore<ApplicationUser>(db);
            var roleManager = new RoleManager<IdentityRole>(store);
            var UserManager = new UserManager<ApplicationUser>(userstor);
            ApplicationUser user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            foreach (var item in user.RecievMessages)
            {
                if(item.Read == false)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('New Message')", true);
                    Response.Write("<script>alert('New Message ')</script>");
                }
            }
            return View(user);
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }


        public ActionResult DetailsReciev(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecievedMessages message = db.RecievedMessages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            ViewBag.Member = db.Users;
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Id, string Body)
        {
            var store = new RoleStore<IdentityRole>(db);
            var userstor = new UserStore<ApplicationUser>(db);
            var roleManager = new RoleManager<IdentityRole>(store);
            var UserManager = new UserManager<ApplicationUser>(userstor);

            ApplicationUser v = db.Users.Where(p => p.Id == Id).FirstOrDefault();
            Message g = new Message();
            RecievedMessages MesRec = new RecievedMessages();
            ApplicationUser user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            g.Body = Body;
            g.ReceptionId = v.Fullname;
            g.SenderId = user.Fullname;
            g.timesent = DateTime.Now;
            user.Messages.Add(g);

            MesRec.Body = Body;
            MesRec.ReceptionId = v.Fullname;
            MesRec.SenderId = user.Fullname;
            MesRec.timesent = g.timesent;
            v.RecievMessages.Add(MesRec);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Body,timesent,Read,ReceptionId,SenderId")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }


        // GET: Messages/Edit/5
        public ActionResult EditReciev(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecievedMessages message = db.RecievedMessages.Find(id);
            message.Read = true;
            db.SaveChanges();
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReciev([Bind(Include = "Id,Body,timesent,Read,ReceptionId,SenderId")] RecievedMessages message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }


        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult DeleteReciev(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecievedMessages message = db.RecievedMessages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("DeleteReciev")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed2(int id)
        {
            RecievedMessages message = db.RecievedMessages.Find(id);
            db.RecievedMessages.Remove(message);
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
