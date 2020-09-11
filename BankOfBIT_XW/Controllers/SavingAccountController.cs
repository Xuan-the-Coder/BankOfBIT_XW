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
    public class SavingAccountController : Controller
    {
        private BankOfBIT_XWContext db = new BankOfBIT_XWContext();

        //
        // GET: /SavingAccount/

        public ActionResult Index()
        {
            var bankaccounts = db.SavingAccounts.Include(s => s.AccountState).Include(s => s.Client);
            return View(bankaccounts.ToList());
        }

        //
        // GET: /SavingAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            SavingAccount savingaccount = (SavingAccount)db.BankAccounts.Find(id);
            if (savingaccount == null)
            {
                return HttpNotFound();
            }
            return View(savingaccount);
        }

        //
        // GET: /SavingAccount/Create

        public ActionResult Create()
        {
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description");
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");
            return View();
        }

        //
        // POST: /SavingAccount/Create

        [HttpPost]
        public ActionResult Create(SavingAccount savingaccount)
        {
            savingaccount.SetNextAccountNumber();
            if (ModelState.IsValid)
            {
                db.BankAccounts.Add(savingaccount);
                db.SaveChanges();
                savingaccount.ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AcccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", savingaccount.AccountStateId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", savingaccount.ClientId);
            return View(savingaccount);
        }

        //
        // GET: /SavingAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SavingAccount savingaccount = (SavingAccount)db.BankAccounts.Find(id);
            if (savingaccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", savingaccount.AccountStateId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", savingaccount.ClientId);
            return View(savingaccount);
        }

        //
        // POST: /SavingAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(SavingAccount savingaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(savingaccount).State = EntityState.Modified;
                db.SaveChanges();
                savingaccount.ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AcccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", savingaccount.AccountStateId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", savingaccount.ClientId);
            return View(savingaccount);
        }

        //
        // GET: /SavingAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SavingAccount savingaccount = (SavingAccount)db.BankAccounts.Find(id);
            if (savingaccount == null)
            {
                return HttpNotFound();
            }
            return View(savingaccount);
        }

        //
        // POST: /SavingAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SavingAccount savingaccount = (SavingAccount)db.BankAccounts.Find(id);
            db.BankAccounts.Remove(savingaccount);
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