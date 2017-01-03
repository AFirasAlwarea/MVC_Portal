using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class RecievedMessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RecievedMessages
        public ActionResult Index()
        {
            return View(db.RecievedMessages.ToList());
        }

        // GET: RecievedMessages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecievedMessages recievedMessages = db.RecievedMessages.Find(id);
            if (recievedMessages == null)
            {
                return HttpNotFound();
            }
            return View(recievedMessages);
        }

        // GET: RecievedMessages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecievedMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Body,timesent,Read,ReceptionId,SenderId")] RecievedMessages recievedMessages)
        {
            if (ModelState.IsValid)
            {
                db.RecievedMessages.Add(recievedMessages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recievedMessages);
        }

        // GET: RecievedMessages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecievedMessages recievedMessages = db.RecievedMessages.Find(id);
            if (recievedMessages == null)
            {
                return HttpNotFound();
            }
            return View(recievedMessages);
        }

        // POST: RecievedMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Body,timesent,Read,ReceptionId,SenderId")] RecievedMessages recievedMessages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recievedMessages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recievedMessages);
        }

        // GET: RecievedMessages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecievedMessages recievedMessages = db.RecievedMessages.Find(id);
            if (recievedMessages == null)
            {
                return HttpNotFound();
            }
            return View(recievedMessages);
        }

        // POST: RecievedMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecievedMessages recievedMessages = db.RecievedMessages.Find(id);
            db.RecievedMessages.Remove(recievedMessages);
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
