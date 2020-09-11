using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankOfBIT_XW.Models;

namespace BankOfBIT_XW.Controllers
{
    public class NexChequingAccountController : Controller
    {
        private BankOfBIT_XWContext db = new BankOfBIT_XWContext();

        //
        // GET: /NexChequingAccount/

        public ActionResult Index()
        {
            return View(NextChequingAccount.GetInstance());
        }

        //
        // GET: /NexChequingAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            NextChequingAccount nextchequingaccount = db.NextChequingAccounts.Find(id);
            if (nextchequingaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextchequingaccount);
        }

        //
        // GET: /NexChequingAccount/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /NexChequingAccount/Create

        [HttpPost]
        public ActionResult Create(NextChequingAccount nextchequingaccount)
        {
            if (ModelState.IsValid)
            {
                db.NextChequingAccounts.Add(nextchequingaccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nextchequingaccount);
        }

        //
        // GET: /NexChequingAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            NextChequingAccount nextchequingaccount = db.NextChequingAccounts.Find(id);
            if (nextchequingaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextchequingaccount);
        }

        //
        // POST: /NexChequingAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(NextChequingAccount nextchequingaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextchequingaccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nextchequingaccount);
        }

        //
        // GET: /NexChequingAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            NextChequingAccount nextchequingaccount = db.NextChequingAccounts.Find(id);
            if (nextchequingaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextchequingaccount);
        }

        //
        // POST: /NexChequingAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            NextChequingAccount nextchequingaccount = db.NextChequingAccounts.Find(id);
            db.NextChequingAccounts.Remove(nextchequingaccount);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}