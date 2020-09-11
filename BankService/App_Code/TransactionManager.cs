using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BankOfBIT_XW.Models;
using Utility;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TransactionManager" in code, svc and config file together.
public class TransactionManager : ITransactionManager
{
    private BankOfBIT_XWContext db = new BankOfBIT_XWContext();
	public void DoWork()
	{
	}

    /// <summary>
    /// Represent a deposit web service 
    /// </summary>
    /// <param name="accountId">The account id of the deposit account</param>
    /// <param name="amount">Amount deposited</param>
    /// <param name="notes">transaction notes</param>
    /// <returns>new account balance</returns>
    public double? Deposit(int accountId, double amount, string notes)
    {
        try
        {
            BankAccount bankAccount = GetAccout(accountId);
            UpdateBalance(accountId, (int)TransactionTypeValues.Deposit, amount);
            TransactionRecord(accountId, (int)TransactionTypeValues.Deposit, amount, notes);
            return bankAccount.Balance;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Represent a Withdrawal web service 
    /// </summary>
    /// <param name="accountId">The account id of the withdrawal account</param>
    /// <param name="amount">Amount deposited</param>
    /// <param name="notes">Transaction notes</param>
    /// <returns>new account balance</returns>
    public double? Withdrawal(int accountId, double amount, string notes)
    {
        try
        {
            BankAccount bankAccount = GetAccout(accountId);
            UpdateBalance(accountId, (int)TransactionTypeValues.Withdrawal, amount);

            TransactionRecord(accountId, (int)TransactionTypeValues.Withdrawal, amount, notes);
            return bankAccount.Balance;
        }
        catch (Exception)
        {
            return 0;
        }
    }


    /// <summary>
    /// Represent a bill payment web service 
    /// </summary>
    /// <param name="accountId">The account id of the account that made bill payment</param>
    /// <param name="amount">Amount paid</param>
    /// <param name="notes">Transaction notes</param>
    /// <returns>new account balance</returns>
    public double? BillPayment(int accountId, double amount, string notes)
    {
        try
        {
            BankAccount bankAccount = GetAccout(accountId);
            UpdateBalance(accountId, (int)TransactionTypeValues.BillPayment, amount);

            TransactionRecord(accountId, (int)TransactionTypeValues.BillPayment, amount, notes);

            return bankAccount.Balance;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Represent transfer web service 
    /// </summary>
    /// <param name="accountId">The account id of the account that made transfer</param>
    /// <param name="amount">Amount trasfered</param>
    /// <param name="notes">Transaction notes</param>
    /// <returns>new account balance</returns>
    public double? Transfer(int fromAccount, int toAccount, double amount, string notes)
    {
        try
        {
            BankAccount bankAccountFrom = GetAccout(fromAccount);
            BankAccount bankAccountTo = GetAccout(toAccount);

                UpdateBalance(fromAccount, (int)TransactionTypeValues.Withdrawal, amount);
       
            

                TransactionRecord(fromAccount, (int)TransactionTypeValues.Transfer, amount, notes);

          
                UpdateBalance(toAccount, (int)TransactionTypeValues.Transfer, amount);




                TransactionRecord(toAccount, (int)TransactionTypeValues.TransferRecipient, amount, notes);
           
            return bankAccountFrom.Balance;
        }
        catch (Exception ex)
        {
            string Text = ex.Message;
            return null;
        }
    }

    /// <summary>
    /// Represent calculate interest web service 
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="notes"></param>
    /// <returns></returns>
    public double? CalculateInterest(int accountId, string notes)
    {
        BankAccount bankAccount = db.BankAccounts.Where(x => x.BankAccountId == accountId).SingleOrDefault();
        AccountState state = db.AccountStates.Where(x => x.AccountStateId == bankAccount.AccountStateId).SingleOrDefault();

        double rate = state.RateAdjustment(bankAccount);

        double interest = (rate * bankAccount.Balance) / 12;

        bankAccount.Balance += interest;
        db.SaveChanges();

        TransactionRecord(accountId, (int)TransactionTypeValues.CalculateInterest, interest, notes);
        return bankAccount.Balance;
    }

    /// <summary>
    /// Update the balance of account where transaction happens
    /// </summary>
    /// <param name="bankAccount">an object of bankaccount class</param>
    /// <param name="accountId">the account id</param>
    /// <param name="transactionTypeId">the transaction type id</param>
    /// <param name="amount">amount associated with the transaction</param>
    private void UpdateBalance(int accountId, int transactionTypeId, double amount)
    {
        BankAccount bankAccount = GetAccout(accountId);

        if (transactionTypeId == (int)TransactionTypeValues.Deposit || transactionTypeId == (int)TransactionTypeValues.Transfer)
        {           
            bankAccount.Balance += amount;
        }
        if (transactionTypeId == (int)TransactionTypeValues.Withdrawal || transactionTypeId == (int)TransactionTypeValues.BillPayment)
        {
            bankAccount.Balance -= amount;

        }
        db.SaveChanges();
        bankAccount.ChangeState();
        db.SaveChanges();
    }

    /// <summary>
    /// Record the transaction and to the database
    /// </summary>
    /// <param name="accountId">the account id</param>
    /// <param name="transactionTypeId">the id of the transaction type</param>
    /// <param name="amount">amount associated with the transaction</param>
    /// <param name="notes">note made with the transaction</param>
    private static void TransactionRecord(int accountId, int transactionTypeId, double amount, string notes)
    {
        BankOfBIT_XWContext db = new BankOfBIT_XWContext();
        if (transactionTypeId == (int)TransactionTypeValues.Deposit || transactionTypeId == (int)TransactionTypeValues.TransferRecipient)
        {
            Transaction transaction = new Transaction();
            transaction.BankAccountId = accountId;
            transaction.TransactionTypeId = transactionTypeId;
            transaction.Deposit = amount;
            transaction.Withdrawal = 0;
            transaction.DateCreated = DateTime.Today;
            transaction.Notes = notes;
            db.Transactions.Add(transaction);
        }

        if (transactionTypeId == (int)TransactionTypeValues.Withdrawal || transactionTypeId == (int)TransactionTypeValues.BillPayment || transactionTypeId == (int)TransactionTypeValues.Transfer)
        {
            Transaction transaction = new Transaction();
            transaction.BankAccountId = accountId;
            transaction.TransactionTypeId = transactionTypeId;
            transaction.Withdrawal = amount;
            transaction.Deposit = 0;
            transaction.DateCreated = DateTime.Today;
            transaction.Notes = notes;
            db.Transactions.Add(transaction);
        }

        if (transactionTypeId == (int)TransactionTypeValues.CalculateInterest)
        {
            if (amount > 0)
            {
                Transaction transaction = new Transaction();
                transaction.BankAccountId = accountId;
                transaction.TransactionTypeId = transactionTypeId;
                transaction.Deposit = amount;
                transaction.Withdrawal = 0;
                transaction.DateCreated = DateTime.Today;
                transaction.Notes = notes;
                db.Transactions.Add(transaction);
            }
            if (amount < 0)
            {
                Transaction transaction = new Transaction();
                transaction.BankAccountId = accountId;
                transaction.TransactionTypeId = transactionTypeId;
                transaction.Withdrawal = System.Math.Abs(amount);
                transaction.Deposit = 0;
                transaction.DateCreated = DateTime.Today;
                transaction.Notes = notes;
                db.Transactions.Add(transaction);
            }
        }
        db.SaveChanges();
    }

    /// <summary>
    /// Get specified bank account record.
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    private BankAccount GetAccout(int accountId)
    {
        BankAccount bankAccount;

        bankAccount = db.BankAccounts.Where(x => x.BankAccountId == accountId).SingleOrDefault();
        return bankAccount;
    }
}
