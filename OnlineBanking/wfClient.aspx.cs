using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankOfBIT_XW.Models;

public partial class wfClient : System.Web.UI.Page
{
    private BankOfBIT_XWContext db = new BankOfBIT_XWContext();
       
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            //Get user id from the login page 
            int clientName = int.Parse(Page.User.Identity.Name);

            //Query the database using the user id retried from the login page 
            //Client client = (from result
            //                 in db.Clients
            //                 where result.ClientNumber == clientName
            //                 select result).SingleOrDefault();

            Client client = db.Clients.Where(x => x.ClientNumber == clientName).SingleOrDefault();

            //Bind the lael with value of client's full name 
            lblClientName.Text = client.FullName;
            lblClientName.Visible = true;

            //Store the client object into a session variable 
            Session["Client"] = client;

            //Query the database for corresonding user's bank account 
            //IQueryable<BankAccount> bankAccount = from results
            //                                      in db.BankAccounts
            //                                      where results.ClientId == client.ClientId
            //                                      select results;
            IQueryable<BankAccount> bankAccount = from results
                                                  in db.BankAccounts
                                                  where results.ClientId == client.ClientId
                                                  select results;

            //Bind the retrieved bank account value to the grid view control 
            gvClient.DataSource = bankAccount.ToList();
            gvClient.DataBind();
            lblExceptionMsg.Text = "To display Error/Exception Message";
        }
        catch (Exception exception)
        {
            lblExceptionMsg.Text = exception.Message;
            lblExceptionMsg.Visible = true;
        }

    }

    protected void gvClient_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Stored values polulated to the grid view into session varaibles.
            //Session["AccountNumber"] = gvClient.SelectedRow.Cells[1].Text;
            //Session["Balance"] = gvClient.SelectedRow.Cells[3].Text;

            Session["AccountNumber"] = gvClient.SelectedRow.Cells[1].Text;
            Session["Balance"] = gvClient.SelectedRow.Cells[3].Text;

            //long accountNumber = long.Parse(Session["AccountNumber"].ToString());
            long accountNumber = long.Parse(Session["AccountNumber"].ToString());
            BankAccount bankAccount = db.BankAccounts.Where(x => x.AccountNumber == accountNumber).SingleOrDefault();
            Session["AccountId"] = bankAccount.BankAccountId;

            //Direct user to wfaccount web form 
            Server.Transfer("wfAccount.aspx");          
        }
        catch (Exception ex)
        {
            //Handle the exception message
            lblExceptionMsg.Text = ex.Message;
            lblExceptionMsg.Visible = true;
        }
    }
}