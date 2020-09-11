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
    public class NextSavingAccountController : Controller
    {
        private BankOfBIT_XWContext db = new BankOfBIT_XWContext();

        //
        // GET: /NextSavingAccount/

        public ActionResult Index()
        {
            return View(NextSavingAccount.GetInstance());
        }

        //
        // GET: /NextSavingAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            NextSavingAccount nextsavingaccount = db.NextSavingAccounts.Find(id);
            if (nextsavingaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextsavingaccount);
        }

        //
        // GET: /NextSavingAccount/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /NextSavingAccount/Create

        [HttpPost]
        public ActionResult Create(NextSavingAccount nextsavingaccount)
        {
            if (ModelState.IsValid)
            {
                db.NextSavingAccounts.Add(nextsavingaccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nextsavingaccount);
        }

        //
        // GET: /NextSavingAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            NextSavingAccount nextsavingaccount = db.NextSavingAccounts.Find(id);
            if (nextsavingaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextsavingaccount);
        }

        //
        // POST: /NextSavingAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(NextSavingAccount nextsavingaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextsavingaccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nextsavingaccount);
        }

        //
        // GET: /NextSavingAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            NextSavingAccount nextsavingaccount = db.NextSavingAccounts.Find(id);
            if (nextsavingaccount == null)
            {
                return HttpNotFound();
            }
            return View(nextsavingaccount);
        }

        //
        // POST: /NextSavingAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            NextSavingAccount nextsavingaccount = db.NextSavingAccounts.Find(id);
            db.NextSavingAccounts.Remove(nextsavingaccount);
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