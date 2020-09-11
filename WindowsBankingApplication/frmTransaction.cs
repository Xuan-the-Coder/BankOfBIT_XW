using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BankOfBIT_XW;
using BankOfBIT_XW.Models;
using Utility;
//note:  this needed to be done because during development
//ef released v. 6
//ef6 needed to add this
//using System.Data.Entity.Core;

namespace WindowsBankingApplication
{
    public partial class frmTransaction : Form
    {

        ///given:  client and bankaccount data will be retrieved
        ///in this form and passed throughout application
        ///this object will be used to store the current
        ///client and selected bankaccount
        ConstructorData constructorData;
        private BankOfBIT_XWContext db = new BankOfBIT_XWContext();


        public frmTransaction()
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
        public frmTransaction(ConstructorData constructorData)
        {
            InitializeComponent();
            this.constructorData = constructorData;
            //this.constructorData = new ConstructorData();
            //this.constructorData.client = constructorData.client;
            //this.constructorData.bankAccount = constructorData.bankAccount;

            clientNumberMaskedLabel.Text = this.constructorData.client.ClientNumber.ToString();
            clientBindingSource.DataSource = constructorData.client;
            //BankAccount bankAccount = db.BankAccounts.Where(x => x.ClientId == constructorData.client.ClientId).FirstOrDefault();
            bankAccountBindingSource.DataSource = constructorData.bankAccount;
        }

        /// <summary>
        /// given: this code will navigate back to frmClient with
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
        /// given:  further code required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmTransaction_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            lblAcctNumber.Mask = Utility.BusinessRules.AccountFormat(constructorData.bankAccount.description);
            try
            {
                IQueryable<TransactionType> transactionType = from results in db.TransactionTypes
                                                          where results.Description != "Transfer (Recipient)" 
                                                          where results.Description != "Interest"
                                                          select results;
                //this.transactionTypeBindingSource.DataSource = transactionType.ToList();
                cbTransType.DataSource = transactionType.ToList();
                cbTransType.DisplayMember = "Description";
                cbTransType.ValueMember = "Description";
                cboAccountPayee.Visible = false;
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Method to deal with combobox selected index change event. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbTransType_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            //if bill payment is selected, bind again.
            if (cbTransType.SelectedValue.ToString() == "Bill Payment")
            {
                cboAccountPayee.Visible = true;
                lblAcctPayee.Visible = true;
                lblAcctPayee.Text = "Payee:";
                clearBinding();
                payeeBind();
            }

            //If Transfer is selected, bind payee with bank accounts 
            if (cbTransType.SelectedValue.ToString() == TransactionTypeValues.Transfer.ToString())
            {
                cboAccountPayee.Visible = true;
                lblAcctPayee.Visible = true;
                lblAcctPayee.Text = "To Account:";
                clearBinding();

                long clientId = this.constructorData.client.ClientId;
                long accNumber = this.constructorData.bankAccount.AccountNumber;
                IQueryable<BankAccount> bankAccount = from results in db.BankAccounts
                                                      where results.ClientId == clientId
                                                      where results.AccountNumber != accNumber
                                                      select results;

                if (bankAccount.Any())
                {
                    //bankAccountBindingSource.DataSource = bankAccount.ToList();
                    //cboAccountPayee.DataSource = bankAccountBindingSource;
                    cboAccountPayee.DataSource = bankAccount.ToList();
                    cboAccountPayee.DisplayMember = "AccountNumber";
                    cboAccountPayee.ValueMember = "BankAccountId";
                }
                else
                {
                    cboAccountPayee.Visible =false;
                    lblNoAccounts.Visible = true;
                }
            }

            if (cbTransType.SelectedValue.ToString() == TransactionTypeValues.Withdrawal.ToString() || cbTransType.SelectedValue.ToString() == TransactionTypeValues.Deposit.ToString())
            {
                clearBinding();
                cboAccountPayee.Visible = false;
                lblAcctPayee.Visible = false;
            }

        }

        /// <summary>
        /// Method to deal with link process click event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ServiceReference1.TransactionManagerClient transactionManager = new ServiceReference1.TransactionManagerClient();
            decimal valueInput;
            if (!decimal.TryParse(txtAmount.Text, out valueInput))
            {
                MessageBox.Show("Amount cannot contain letters or special characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                long accNumber = this.constructorData.bankAccount.AccountNumber;
                double amount = double.Parse(txtAmount.Text);
                BankAccount bankAccount = db.BankAccounts.Where(x => x.AccountNumber == accNumber).SingleOrDefault();
                int acctId = bankAccount.BankAccountId;
                if (cbTransType.SelectedValue.ToString() == "Deposit")
                {
                    double? returnValue = transactionManager.Deposit(acctId, amount, "Deposit");
                    confirmTransaction(returnValue, "Deposit");
                }
                else
                {
                    if (amount > this.constructorData.bankAccount.Balance)
                    {
                        MessageBox.Show("Not sufficient balance.");
                    }
                    else
                    {

                        //When billpayment is selected, call billpayment mehtod of the web service. 
                        if (cbTransType.SelectedValue.ToString() == "Bill Payment")
                        {


                            double? returnValue = transactionManager.BillPayment(acctId, amount, "Bill Payment to " + cboAccountPayee.Text);
                            confirmTransaction(returnValue, "Bill Payment");

                        }
                        //When transfer is selected, call transfer mehtod of the web service. 
                        if (cbTransType.SelectedValue.ToString() == "Transfer")
                        {

                            double? returnValue = transactionManager.Transfer(acctId, int.Parse(cboAccountPayee.SelectedValue.ToString()), amount, "Transfer");
                            confirmTransaction(returnValue, "Transfer");
                        }
                        if (cbTransType.SelectedValue.ToString() == "Withdrawal")
                        {
                            double? returnValue = transactionManager.Withdrawal(acctId, amount, "Withdrawal");
                            confirmTransaction(returnValue, "Withdrawal");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Confirm transfer and update balance displayed on web page. 
        /// </summary>
        /// <param name="retVal"></param>
        /// <param name="strTransferType"></param>
        protected void confirmTransaction(double? retVal, String strTransferType)
        {
            if (retVal != null)
            {
                lblBalance.Text = string.Format("{0:C}", retVal);
                this.constructorData.bankAccount.Balance = (double)retVal;
                txtAmount.Text = null;
            }
            else
            {
                string exception = "Failed to process " + strTransferType + ".";
                throw new System.Exception(exception);
            }
        }

        /// <summary>
        /// Bind the payee dropdownlist with data queired from database. 
        /// </summary>
        protected void payeeBind()
        {
            IQueryable<Payee> payee = from results in db.Payees
                                      select results;
    
            cboAccountPayee.DataSource = payee.ToList();
            cboAccountPayee.DisplayMember = "Description";
            cboAccountPayee.ValueMember = "Description";
        }
        /// <summary>
        /// Clead data binding for drop down list
        /// </summary>
        protected void clearBinding()
        {
            cboAccountPayee.DataSource = null;
            cboAccountPayee.DisplayMember = null;
            cboAccountPayee.ValueMember = null;
        }
    }
}
