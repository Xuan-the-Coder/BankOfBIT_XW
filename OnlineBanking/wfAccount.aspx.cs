using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankOfBIT_XW.Models;

public partial class wfAccount : System.Web.UI.Page
{
    private BankOfBIT_XWContext db = new BankOfBIT_XWContext();
    protected void Page_Load(object sender, EventArgs e)
    {
            try
            {
                //Client object from Clients table
                Client client = (Client)Session["Client"];
                lblClientName.Text = client.FullName;

                //AccountNumber from BankAccount table
                lblNumber.Text = (string)Session["AccountNumber"];
                lblBalanceAmount.Text = (string)Session["Balance"];

                //Retrieve bankAcountId value from Session variable
                int bankAccountId = (int)Session["AccountId"];

               

                //Query Transactions table with bankAccountId 
                IQueryable<Transaction> transaction = from TransactionsResults
                            in db.Transactions
                            where TransactionsResults.BankAccountId == bankAccountId
                            select TransactionsResults;

                //Binds grid view with query result 
                gvAccount.DataSource = transaction.ToList();
                gvAccount.DataBind();
            }
            catch (Exception exception)
            {
                //output the error message when failing to get data from database, 
                lblExceptionMsg.Text = exception.Message; 
                lblExceptionMsg.Visible = true;
            }
    }

    protected void LinkClicked(object sender, EventArgs e)
    {
        Server.Transfer("wfTransaction.aspx");
    }
}