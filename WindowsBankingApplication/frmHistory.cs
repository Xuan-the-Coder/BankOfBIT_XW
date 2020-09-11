using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using BankOfBIT_XW;
using BankOfBIT_XW.Models;

//note:  this needed to be done because during development
//ef released v. 6
//ef6 needed to add this
//using System.Data.Entity.Core;

namespace WindowsBankingApplication
{
    public partial class frmHistory : Form
    {

        ///given:  client and bankaccount data will be retrieved
        ///in this form and passed throughout application
        ///this object will be used to store the current
        ///client and selected bankaccount
        ConstructorData constructorData;
        private BankOfBIT_XWContext db = new BankOfBIT_XWContext();

        public frmHistory()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given:  This constructor will be used when returning to frmClient
        /// from another form.  This constructor will pass back
        /// specific information about the client and bank account
        /// based on activites taking place in another form
        /// </summary>
        /// <param name="client">specific client instance</param>
        /// <param name="account">specific bank account instance</param>
        public frmHistory(ConstructorData constructorData)
        {
            InitializeComponent();
            this.constructorData = constructorData;

            clientNumberMaskedLabel.Text = this.constructorData.client.ClientNumber.ToString();
            lblFullName.Text = this.constructorData.client.FullName; 
            accountNumberMaskedLabel.Text = this.constructorData.bankAccount.AccountNumber.ToString();
            lblBalance.Text = this.constructorData.bankAccount.Balance.ToString("C");

        }

        /// <summary>
        /// given:  this code will navigate back to frmClient with
        /// the specific client and account data that launched
        /// this form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //return to client with the data selected for this form
            frmClients frmClients = new frmClients(constructorData);
            frmClients.MdiParent = this.MdiParent;
            frmClients.Show();
            this.Close();
        }

        /// <summary>
        /// given - further code required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHistory_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            accountNumberMaskedLabel.Mask = Utility.BusinessRules.AccountFormat(constructorData.bankAccount.description);

            int accountNumber = int.Parse(accountNumberMaskedLabel.Text.Replace("-", ""));
            BankAccount bankAccount = db.BankAccounts.Where(x => x.AccountNumber == accountNumber).SingleOrDefault();
            //var query = from Results in db.Transactions
            //            join TypeResults in db.TransactionTypes
            //            on Results.TransactionTypeId equals TypeResults.TransactionTypeId
            //            where Results.BankAccountId == bankAccount.BankAccountId
            //            select new
            //            {
            //                Date = Results.DateCreated,
            //                TransactionType = TypeResults.Description,
            //                AmountIn = Results.Deposit,
            //                AmountOut = Results.Withdrawal,
            //                Detail = Results.Notes
            //            };
            var query = from Results in db.Transactions
                        join TypeResults in db.TransactionTypes
                        on Results.TransactionTypeId equals TypeResults.TransactionTypeId
                        where Results.BankAccountId == bankAccount.BankAccountId
                        select new
                        {
                            Date = Results.DateCreated,
                            TransactionType = TypeResults.Description,
                            AmountIn = Results.Deposit,
                            AmountOut = Results.Withdrawal,
                            Detail = Results.Notes
                        };
            gvTransaction.DataSource = query.ToList();

            this.gvTransaction.Columns[0].DefaultCellStyle.Format = "yyyy/MM/dd";
            this.gvTransaction.Columns[2].DefaultCellStyle.Format = "c";
            this.gvTransaction.Columns[3].DefaultCellStyle.Format = "c";
            //IQueryable<Transaction> transaction = db.Transactions.Where(x => x.BankAccountId == bankAccount.BankAccountId);

        }
    }
}
