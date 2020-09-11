using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankOfBIT_XW.Models;
using Utility;

public partial class wfTransaction : System.Web.UI.Page
{
    private BankOfBIT_XWContext db = new BankOfBIT_XWContext();
    
    /// <summary>
    /// Triger when page is loaded.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //Check if postback happened.
        if (!IsPostBack)
        {
            lblAccNumber1.Text = (string)Session["AccountNumber"];
            lblBalance1.Text = (string)Session["Balance"];
            txtAmount.Style.Add("Text-Align", "Right");
            try
            {
                //Query database for transaction type info and bind them to the drop down list. 
                IQueryable<TransactionType> transaction = from results in db.TransactionTypes
                                                          where results.TransactionTypeId > 2
                                                          where results.TransactionTypeId < 5
                                                          select results;
                ddlTransType.DataSource = transaction.ToList();
                ddlTransType.DataTextField = "Description";
                ddlTransType.DataValueField = "TransactionTypeId";

                ddlTransType.DataBind();

                payeeBind();
            }
            catch (Exception ex)
            {
                lblExceptionMsg.Text = ex.Message;
                lblExceptionMsg.Visible = true;
            }

        }

    }

    /// <summary>
    /// Triggered when selected index changes. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTransType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if bill payment is selected, bind again.
        if (int.Parse(ddlTransType.SelectedValue) == (int)TransactionTypeValues.BillPayment)
        {
            clearBinding();
            payeeBind();
        }

        //If Transfer is selected, bind payee with bank accounts 
        else if (int.Parse(ddlTransType.SelectedValue) == (int)TransactionTypeValues.Transfer)
        {
            Client client = (Client)Session["Client"];
            long accountNumber = long.Parse(Session["AccountNumber"].ToString());
            clearBinding();
            IQueryable<BankAccount> bankAccount = from results in db.BankAccounts
                                                  where results.ClientId == client.ClientId
                                                  where results.AccountNumber != accountNumber
                                                  select results;
            ddlPayee.DataSource = bankAccount.ToList();
            ddlPayee.DataTextField = "AccountNumber";
            ddlPayee.DataValueField = "BankAccountId";
        }
        this.DataBind();
    }

    /// <summary>
    /// Triggered when submit button is clicked. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Create an instance of web service. 
        ServiceReference1.TransactionManagerClient transactionManager = new ServiceReference1.TransactionManagerClient();
        try
        {
            long accountNumber = long.Parse(Session["accountNumber"].ToString());
            BankAccount account = (from result
                                       in db.BankAccounts
                                   where result.AccountNumber == accountNumber
                                   select result).SingleOrDefault();

            //Check if the account have enough balance.
            if (double.Parse(txtAmount.Text) > account.Balance)
            {
                throw new System.Exception("Not sufficient balance.");
            }
            else
            {
                //When billpayment is selected, call billpayment mehtod of the web service. 
                if (int.Parse(ddlTransType.SelectedValue) == (int)TransactionTypeValues.BillPayment)
                {

                    double? returnValue = transactionManager.BillPayment((int)Session["AccountId"], double.Parse(txtAmount.Text), "Bill Payment to" + ddlPayee.SelectedItem.Text);
                    confirmTransfer(returnValue, "Bill Payment");

                }
                //When transfer is selected, call transfer mehtod of the web service. 
                if (int.Parse(ddlTransType.SelectedValue) == (int)TransactionTypeValues.Transfer)
                {

                    double? returnValue = transactionManager.Transfer((int)Session["AccountId"], int.Parse(ddlPayee.Text), double.Parse(txtAmount.Text), "Transfer between" +lblAccNumber1 .Text +" and " + ddlPayee.SelectedItem.Text);
                    confirmTransfer(returnValue, "Transfer");
                }
            }
        }
        catch (Exception ex)
        {
            lblExceptionMsg.Text = ex.Message;
            lblExceptionMsg.Visible = true;
        }
    }

    /// <summary>
    /// Clead data binding for drop down list
    /// </summary>
    protected void clearBinding()
    {
        ddlPayee.DataSource = null;
        ddlPayee.DataTextField = null;
        ddlPayee.DataValueField = null;
    }

    /// <summary>
    /// Bind the payee dropdownlist with data queired from database. 
    /// </summary>
    protected void payeeBind()
    {
        IQueryable<Payee> payee = from results in db.Payees
                                  select results;
        ddlPayee.DataSource = payee.ToList();
        ddlPayee.DataTextField = "Description";
        ddlPayee.DataValueField = "PayeeId";

        ddlPayee.DataBind();
    }

    /// <summary>
    /// Confirm transfer and update balance displayed on web page. 
    /// </summary>
    /// <param name="retVal"></param>
    /// <param name="strTransferType"></param>
    protected void confirmTransfer(double? retVal, String strTransferType)
    {
        if (retVal != null)
        {
            lblBalance1.Text = string.Format("{0:C}", retVal);
            Session["Balance"] = lblBalance1.Text;
            //Set amount input to null
            txtAmount.Text = null;
        }
        else
        {
            string exception = "Failed to process " + strTransferType + ".";
            throw new System.Exception(exception);
        }
    }
}