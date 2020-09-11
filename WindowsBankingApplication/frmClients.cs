using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using BankOfBIT_XW;
using BankOfBIT_XW.Models;

//note:  this needed to be done because during development
//ef released v. 6
//ef6 needed to add this
//using System.Data.Entity.Core;

using System.IO.Ports;      //for rfid assignment

namespace WindowsBankingApplication
{

    
    public partial class frmClients : Form
    {
        ///given: client and bankaccount data will be retrieved
        ///in this form and passed throughout application
        ///these variables will be used to store the current
        ///client and selected bankaccount
        ConstructorData constructorData = new ConstructorData();
        private BankOfBIT_XWContext db = new BankOfBIT_XWContext();

        SerialPort myPort = new SerialPort();

        delegate void SetLeaveCallBack(string readData);

        public frmClients()
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
        public frmClients(ConstructorData constructorData)
        {
            InitializeComponent();

            //further code to be added
            //this.constructorData.client = constructorData.client;
            //this.constructorData.bankAccount = constructorData.bankAccount;
            //clientNumberMaskedTextBox.Text = this.constructorData.client.ClientNumber.ToString();
            //clientNumberMaskedTextBox_Leave(null, EventArgs.Empty);

            //clientBindingSource.DataSource = constructorData.client;
            //bankAccountBindingSource.DataSource = constructorData.bankAccount;

            clientNumberMaskedTextBox.Text = constructorData.client.ClientNumber.ToString();
            clientNumberMaskedTextBox_Leave(null, null);

            if (constructorData.bankAccount != null)
            {
                cbAcctNumber.Text = constructorData.bankAccount.AccountNumber.ToString();

            }
        }

        /// <summary>
        /// given: open history form passing data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            constructorData.bankAccount = (BankAccount)bankAccountBindingSource.Current;
            constructorData.client = (Client)clientBindingSource.Current;

            //instance of frmHistory passing constructor data
            frmHistory frmHistory = new frmHistory(constructorData);
            //open in frame
            frmHistory.MdiParent = this.MdiParent;
            //show form
            frmHistory.Show();
            this.Close();
        }

        /// <summary>
        /// given: open transaction form passing constructor data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkTransaction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //constructorData.bankAccount = (BankAccount)bankAccountBindingSource.Current;
            //constructorData.client = (Client)clientBindingSource.Current;

            constructorData.bankAccount = (BankAccount)bankAccountBindingSource.Current;
            constructorData.client = (Client)clientBindingSource.Current;

            //instance of frmTransaction passing constructor data
            frmTransaction frmTransaction = new frmTransaction(constructorData);
            //open in frame
            frmTransaction.MdiParent = this.MdiParent;
            //show form
            frmTransaction.Show();
            this.Close();
        }






        



       /// <summary>
       /// given
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void frmClients_Load(object sender, EventArgs e)
        {
            //keeps location of form static when opened and closed
            this.Location = new Point(0, 0);
        }

        private void clientNumberMaskedTextBox_Leave(object sender, EventArgs e)
        {
            int clientNumber = int.Parse(clientNumberMaskedTextBox.Text.Replace("-", ""));
            Client client = db.Clients.Where(x => x.ClientNumber == clientNumber).SingleOrDefault();
            clientBindingSource.DataSource = client;// db.Clients.Where(x => x.ClientNumber == clientNumber).ToList();
            try
            {
                if (client == null)
                {
                    lnkTransaction.Enabled = false;
                    lnkDetails.Enabled = false;
                    clientBindingSource.Clear();
                    bankAccountBindingSource.Clear();
                    MessageBox.Show("error");
                }
                else
                {
                    //BankAccount bankAccount = db.BankAccounts.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
                   IQueryable<BankAccount> bankAccount = db.BankAccounts.Where(x => x.ClientId == client.ClientId);//.ToList().AsQueryable();
                   constructorData.client = client;

                    if (!bankAccount.Any())
                    {
                        lnkTransaction.Enabled = false;
                        lnkDetails.Enabled = false;
                        bankAccountBindingSource.Clear();
                    }
                    else
                    {
                        
                        this.cbAcctNumber.DataSource = this.bankAccountBindingSource;
                        this.cbAcctNumber.DisplayMember = "AccountNumber";
                        this.cbAcctNumber.ValueMember = "AccountNumber";
                        bankAccountBindingSource.DataSource = bankAccount.ToList();
                        lnkTransaction.Enabled = true;
                        lnkDetails.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void bandAccountBindingSource_CurrentChanged(object sender, EventArgs e)
        {

            constructorData.bankAccount = (BankAccount)bankAccountBindingSource.Current;
            constructorData.client = (Client)clientBindingSource.Current;
        }

        private void SetLeave(string readData)
        {
            if (clientNumberMaskedTextBox.InvokeRequired)
            {
            }
        }

  }
}
