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
    public class SilverStateController : Controller
    {
        private BankOfBIT_XWContext db = new BankOfBIT_XWContext();

        //
        // GET: /SilverState/

        public ActionResult Index()
        {
            return View(SilverState.GetInstance());
        }

        //
        // GET: /SilverState/Details/5

        public ActionResult Details(int id = 0)
        {
            SilverState silverstate = (SilverState)db.AccountStates.Find(id);
            if (silverstate == null)
            {
                return HttpNotFound();
            }
            return View(silverstate);
        }

        //
        // GET: /SilverState/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SilverState/Create

        [HttpPost]
        public ActionResult Create(SilverState silverstate)
        {
            if (ModelState.IsValid)
            {
                db.AccountStates.Add(silverstate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(silverstate);
        }

        //
        // GET: /SilverState/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SilverState silverstate = (SilverState)db.AccountStates.Find(id);
            if (silverstate == null)
            {
                return HttpNotFound();
            }
            return View(silverstate);
        }

        //
        // POST: /SilverState/Edit/5

        [HttpPost]
        public ActionResult Edit(SilverState silverstate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(silverstate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(silverstate);
        }

        //
        // GET: /SilverState/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SilverState silverstate = (SilverState)db.AccountStates.Find(id);
            if (silverstate == null)
            {
                return HttpNotFound();
            }
            return View(silverstate);
        }

        //
        // POST: /SilverState/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SilverState silverstate = (SilverState)db.AccountStates.Find(id);
            db.AccountStates.Remove(silverstate);
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