using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BankOfBIT_XW.Models;

namespace BankOfBIT_XW.Controllers
{
    public class BankAcccountController : ApiController
    {
        private BankOfBIT_XWContext db = new BankOfBIT_XWContext();

        // GET api/BankAcccount
        public IEnumerable<BankAccount> GetBankAccounts()
        {
            var bankaccounts = db.BankAccounts.Include(b => b.AccountState).Include(b => b.Client);
            return bankaccounts.AsEnumerable();
        }

        // GET api/BankAcccount/5
        public BankAccount GetBankAccount(int id)
        {
            BankAccount bankaccount = db.BankAccounts.Find(id);
            if (bankaccount == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return bankaccount;
        }

        // PUT api/BankAcccount/5
        public HttpResponseMessage PutBankAccount(int id, BankAccount bankaccount)
        {
            if (ModelState.IsValid && id == bankaccount.BankAccountId)
            {
                db.Entry(bankaccount).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/BankAcccount
        public HttpResponseMessage PostBankAccount(BankAccount bankaccount)
        {
            if (ModelState.IsValid)
            {
                db.BankAccounts.Add(bankaccount);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, bankaccount);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = bankaccount.BankAccountId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/BankAcccount/5
        public HttpResponseMessage DeleteBankAccount(int id)
        {
            BankAccount bankaccount = db.BankAccounts.Find(id);
            if (bankaccount == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.BankAccounts.Remove(bankaccount);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, bankaccount);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}