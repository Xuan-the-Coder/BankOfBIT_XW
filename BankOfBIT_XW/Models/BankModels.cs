using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Data;
using Utility;

namespace BankOfBIT_XW.Models
{

    public class BankModels : Controller
    {
        //
        // GET: /BankModels/

        public ActionResult Index()
        {
            return View();
        }

    }

    /// <summary>
    /// Represent an account state
    /// </summary>
    public abstract class AccountState
    {
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name="Account\nState")]
        public int AccountStateId { get; set; }

        protected static BankOfBIT_XWContext db = new BankOfBIT_XWContext();

        [Display(Name="Lower\nLimit")]
        [DisplayFormat(DataFormatString="{0:C}")]
        public double LowerLimit { get; set; }

        [Display(Name = "Upper\nLimit")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double UpperLimit { get; set; }

        [Required]
        [Display(Name="Interest\nRate")]
        [DisplayFormat(DataFormatString="{0:P}")]
        public double Rate { get; set; }

        [Display(Name = "Account\nState")]
        public string Description 
        { 
            get
            {
                return this.GetType().Name.Replace("State","");
            }
        }

        /// <summary>
        /// Adjust the interest rate 
        /// </summary>
        /// <param name="bankaccount">an instance of bank account</param>
        /// <returns></returns>
        public virtual double RateAdjustment(BankAccount bankAccount)
        {
            return 0;
        }

        /// <summary>
        /// Check if the account state has changed
        /// </summary>
        /// <param name="bankAccount">an instance of bank account</param>
        public virtual void StateChangeCheck(BankAccount bankAccount)
        {
        }

    }

    /// <summary>
    /// Represent a bank account
    /// </summary>
    public abstract class BankAccount
    {
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BankAccountId { get; set; }

        [Display(Name="Account\nNumber")]
        public long AccountNumber { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [Required]
        [ForeignKey("AccountState")]
        public int AccountStateId { get; set; }

        [Required]
        [Display(Name="Current\nBalance")]
        [DisplayFormat(DataFormatString="{0:C}",ApplyFormatInEditMode=true)]
        public double Balance { get; set; }

        [Required]
        [Display(Name="Openning\nBalance")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public double OpeningBalance { get; set; }

        [Required]
        [DisplayFormat(DataFormatString="{0:d}", ApplyFormatInEditMode=true)]
        [Display(Name="Date\nCreated")]
        public DateTime DateCreated { get; set; }

        [Display(Name="Account\nNotes")]
        public string Notes { get; set; }

        [Display(Name="Account\nType")]
        public string description { 
            get
            {
                //return this.GetType().Name.Replace("Account","");
                return Display.DisplayHeading(this.GetType().Name, this.GetType().Name.IndexOf("Account"));
            }
        }

        /// <summary>
        /// Set the next account number 
        /// </summary>
        public abstract void SetNextAccountNumber();

        /// <summary>
        /// Change the account state
        /// </summary>
        public void ChangeState()
        {
            BankOfBIT_XWContext db = new BankOfBIT_XWContext();
            AccountState currentState;
            int oldStateId;
            do
            {
                currentState = db.AccountStates.Where(x => x.AccountStateId == this.AccountStateId).SingleOrDefault();
                oldStateId = this.AccountStateId;
                currentState.StateChangeCheck(this);
            } while (this.AccountStateId != oldStateId);
        }

        public virtual AccountState AccountState { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<Transaction> Transaction { get; set; }
    }

    /// <summary>
    /// Represent a client of the bank 
    /// </summary>
    public class Client
    {
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }

        [Display(Name="Client")]
        public long ClientNumber { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        [Display(Name = "First\name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        [Display(Name = "Last\name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        [Display(Name = "Street\nAddress")]
        public string Address { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        public string City { get; set; }

        [Required]
        [StringLength(2)]
        [RegularExpression("AB|BC|MB|N[BLTSU]|ON|PE|QC|SK|YT", ErrorMessage = "valid Canadian province code is not entered")]
        public string Province { get; set; }

        [Required]
        [Display(Name = "Postal\nCode")]
        [StringLength(7,MinimumLength=7)]
        [RegularExpression("(?!.*[DFIOQU])[A-VXY][0-9][A-Z] [0-9][A-Z][0-9]", ErrorMessage = "format does not match")]
        public string PostalCode { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name="Date\nCreated")]
        public DateTime DateCreated{ get; set; }

        [Display(Name="Clients\nNotes")]
        public string Notes { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return String.Format("{0}{1}{2}", FirstName," ", LastName);
            }
        }

        [Display(Name = "Address")]
        public string FullAddress
        {
            get
            {
                return String.Format("{0}{1}{2}{3}", Address, City, Province, PostalCode);
            }
        }

        /// <summary>
        /// Set the next account number 
        /// </summary>
        public void SetNextClientNumber()
        {
            long? number = StoredProcedures.NextNumber("NextClientNumbers");
            if (number != null)
            {
                ClientNumber = (long)StoredProcedures.NextNumber("NextClientNumbers");
            }
        }

        public virtual ICollection<BankAccount> BankAccount{ get; set; }
    }

    /// <summary>
    /// Represent the bronze account state 
    /// </summary>
    public class BronzeState : AccountState
    {

        private static BronzeState bronzeState;
        private const double LOWER_LIMIT = 0;
        private const double UPPER_LIMIT = 5000;
        private const double RATE = 0.01;

        /// <summary>
        /// Constuctor for bronze state
        /// </summary>
        private BronzeState()
        {
            this.LowerLimit = LOWER_LIMIT;
            this.UpperLimit = UPPER_LIMIT;
            this.Rate = RATE;
        }

        /// <summary>
        /// Get the instance of BronzeState class
        /// </summary>
        /// <returns></returns>
        public static BronzeState GetInstance()
        {
            if(bronzeState == null)
            {
                bronzeState = db.BronzeStates.SingleOrDefault();
                if(bronzeState == null)
                {
                    bronzeState = new BronzeState();
                    db.BronzeStates.Add(bronzeState);
                    db.SaveChanges();
                }
            }
            return bronzeState;
        }

        /// <summary>
        /// Adjust the interest rate 
        /// </summary>
        /// <param name="bankAccount">An instance of bank account</param>
        /// <returns></returns>
        public override double RateAdjustment(BankAccount bankAccount)
        {
            double rate = this.Rate;
            if (bankAccount.Balance <= 0)
            {
                rate = 0.055;
            }
            return rate;
        }

        /// <summary>
        /// Chenck if the account state has changed
        /// </summary>
        /// <param name="bankAccount">An instance of bank account</param>
        public override void StateChangeCheck(BankAccount bankAccount)
        {
            if (bankAccount.Balance > UPPER_LIMIT)
            {
                bankAccount.AccountStateId += 1;
            }
        }

    }

    /// <summary>
    /// Represent the Gold account state 
    /// </summary>
    public class GoldState : AccountState
    {
        private static GoldState goldState;
        private const double LOWER_LIMIT = 10000;
        private const double UPPER_LIMIT = 20000;
        private const double RATE = 0.02;

        /// <summary>
        /// Constuct an instance of GoldState Class
        /// </summary>
        private GoldState()
        {
            this.LowerLimit = LOWER_LIMIT;
            this.UpperLimit = UPPER_LIMIT;
            this.Rate = RATE;
        }

        /// <summary>
        /// Get the instance of Goldstate class
        /// </summary>
        /// <returns></returns>
        public static GoldState GetInstance()
        {
            if (goldState == null)
            {
                goldState = db.GoldStates.SingleOrDefault();
                if (goldState == null)
                {
                    db.GoldStates.Add(goldState);
                    db.SaveChanges();
                }
            }
            return goldState;
        }

        /// <summary>
        /// Adjust the interest rate 
        /// </summary>
        /// <param name="bankAccount">An instance of bank account</param>
        /// <returns></returns>
        public override double RateAdjustment(BankAccount bankAccount)
        {
            double rate = this.Rate;

            if((bankAccount.DateCreated.Year + 10)<DateTime.Today.Year)
            {
                rate = this.Rate + 0.01;
            }
            return rate;
        }

        /// <summary>
        /// Chenck if the account state has changed
        /// </summary>
        /// <param name="bankAccount">An instance of bank account</param>
        public override void StateChangeCheck(BankAccount bankAccount)
        {
            if (bankAccount.Balance < LOWER_LIMIT)
            {
                bankAccount.AccountStateId -= 1;
            }
            if (bankAccount.Balance > UPPER_LIMIT)
            {
                bankAccount.AccountStateId += 1;
            }
        }

    }
    
    /// <summary>
    /// Represent a silver state
    /// </summary>
    public class SilverState : AccountState
    {
        private static SilverState silverState;
        private const double LOWER_LIMIT = 5000;
        private const double UPPER_LIMIT = 10000;
        private const double RATE = 0.0125;

        /// <summary>
        /// Constuct an instance of SilverState Class
        /// </summary>
        private SilverState()
        {
            this.LowerLimit = LOWER_LIMIT;
            this.UpperLimit = UPPER_LIMIT;
            this.Rate = RATE;
        }

        /// <summary>
        /// Get the instance of SilverState class
        /// </summary>
        /// <returns></returns>
        public static SilverState GetInstance()
        {
            if (silverState == null)
            {
                silverState = db.SilverStates.SingleOrDefault();
                if (silverState == null)
                {
                    db.SilverStates.Add(silverState);
                    db.SaveChanges();
                }
            }
            return silverState;
        }

        /// <summary>
        /// Adjust the interest rate 
        /// </summary>
        /// <param name="bankAccount">An instance of bank account</param>
        /// <returns></returns>
        public override double RateAdjustment(BankAccount bankAccount)
        {
            double rate = this.Rate;
            return rate;
        }

        /// <summary>
        /// Chenck if the account state has changed
        /// </summary>
        /// <param name="bankAccount">An instance of bank account</param>
        public override void StateChangeCheck(BankAccount bankAccount)
        {
            if (bankAccount.Balance < LOWER_LIMIT)
            {
                bankAccount.AccountStateId -= 1;
            }
            if (bankAccount.Balance > UPPER_LIMIT)
            {
                bankAccount.AccountStateId += 1;
            }
        }
    }

    /// <summary>
    /// Represent a platinum state 
    /// </summary>
    public class PlatinumState : AccountState
    {
        public static PlatinumState platinumState;
        private const double LOWER_LIMIT = 20000;
        private const double UPPER_LIMIT = 0;
        private const double RATE = 0.0250;

        /// <summary>
        /// Constuct an instance of PlatinumState Class
        /// </summary>
        public PlatinumState()
        {
            this.LowerLimit = LOWER_LIMIT;
            this.UpperLimit = UPPER_LIMIT;
            this.Rate = RATE;
        }

        /// <summary>
        /// Get the instance of Platinum class
        /// </summary>
        /// <returns></returns>
        public static PlatinumState GetInstance()
        {
            if (platinumState == null)
            {
                platinumState = db.PlatinumStates.SingleOrDefault();
                if (platinumState == null)
                {
                    db.PlatinumStates.Add(platinumState);
                    db.SaveChanges();
                }
            }
            return platinumState;
        }

        /// <summary>
        /// Adjust the interest rate 
        /// </summary>
        /// <param name="bankAccount">An instance of bank account</param>
        /// <returns></returns>
        public override double RateAdjustment(BankAccount bankAccount)
        {
            double rate = this.Rate;

            if ((bankAccount.DateCreated.Year + 10) < DateTime.Today.Year)
            {
                rate += 0.01;
            }
            if (bankAccount.Balance > 2 * LOWER_LIMIT)
            {
                rate += 0.005;
            }
            return rate;
        }

        /// <summary>
        /// Chenck if the account state has changed
        /// </summary>
        /// <param name="bankAccount">An instance of bank account</param>
        public override void StateChangeCheck(BankAccount bankAccount)
        {
            if (bankAccount.Balance < LOWER_LIMIT)
            {
                bankAccount.AccountStateId -= 1;
            }
        }
    }

    /// <summary>
    /// Represent a saving account
    /// </summary>
    public class SavingAccount : BankAccount
    {
        [Required]
        [Display(Name="Service\nCharges")]
        [DisplayFormat(DataFormatString="{0:C}")]
        public double SavingServiceCharges { get; set; }

        /// <summary>
        /// Set the next account number 
        /// </summary>
        public override void SetNextAccountNumber()
        {
            long? number = StoredProcedures.NextNumber("NextSavingAccounts");
            if (number != null)
            {
                AccountNumber = (long)number;
            }
        }
    }

    /// <summary>
    /// Represent an investment account
    /// </summary>
    public class InvestmentAccount : BankAccount
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:P}")]
        [Display(Name="Interest\nRate")]
        public double InterestRate { get; set; }

        /// <summary>
        /// Set the next account number 
        /// </summary>
        public override void SetNextAccountNumber()
        {
            long? number = StoredProcedures.NextNumber("NextInvestmentAccounts");
            if (number != null)
            {
                this.AccountNumber = (long)number;
            }
        }
    }

    /// <summary>
    /// Represent a mortgage account
    /// </summary>
    public class MortgageAccount : BankAccount
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:P}")]
        [Display(Name="Interest\nRate")]
        public double MortgageRate { get; set; }

        [Required]
        public int Amortization { get; set; }

        /// <summary>
        /// Set the next account number 
        /// </summary>
        public override void SetNextAccountNumber()
        {
            long? number = StoredProcedures.NextNumber("NextMortgageAccounts");
            if (number != null)
            {
                AccountNumber = (long)number;
            }
        }
    }

    /// <summary>
    /// Represent a chequing account 
    /// </summary>
    public class ChequingAccount : BankAccount
    {
        [Required]
        [Display(Name="Service\nCharges")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double ChequingServiceCharges { get; set; }


        /// <summary>
        /// Set the next account number 
        /// </summary>
        public override void SetNextAccountNumber()
        {
            long? number = StoredProcedures.NextNumber("NextChequingAccounts");
            if (number != null)
            {
                AccountNumber = (long)number;
            }
        }
    }

    /// <summary>
    /// Represent the next available client number.
    /// </summary>
    public class NextClientNumber
    {
        protected static BankOfBIT_XWContext db = new BankOfBIT_XWContext();

        private const int NEXT_AVAILABLE_NUMBER = 20000000;

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextClientNumberId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextClientNumber nextClientNumber;

        private NextClientNumber()
        {
            this.NextAvailableNumber = NEXT_AVAILABLE_NUMBER;
        }

        /// <summary>
        /// Get an instance of next client number 
        /// </summary>
        /// <returns>nextClientNumber</returns>
        public static NextClientNumber GetInstance()
        {
            if (nextClientNumber == null)
            {
                nextClientNumber = db.NextClientNumbers.SingleOrDefault();
                if (nextClientNumber == null)
                {
                    nextClientNumber = new NextClientNumber();
                    db.NextClientNumbers.Add(nextClientNumber);
                    db.SaveChanges();
                }
            }
            return nextClientNumber;
        }
    }

    /// <summary>
    /// Represent the next available saving account
    /// </summary>
    public class NextSavingAccount
    {
        protected static BankOfBIT_XWContext db = new BankOfBIT_XWContext();
        private const int NEXT_AVAILABLE_NUMBER = 20000;

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextSavingAccountId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextSavingAccount nextSavingAccount;

        private NextSavingAccount()
        {
            this.NextAvailableNumber = NEXT_AVAILABLE_NUMBER;
        }

        /// <summary>
        /// Get an instance of next saving account 
        /// </summary>
        /// <returns>nextSavingAccount</returns>
        public static NextSavingAccount GetInstance()
        {
            if (nextSavingAccount == null)
            {
                nextSavingAccount = db.NextSavingAccounts.SingleOrDefault();
                if (nextSavingAccount == null)
                {
                    nextSavingAccount = new NextSavingAccount();
                    db.NextSavingAccounts.Add(nextSavingAccount);
                    db.SaveChanges();
                }
            }
            return nextSavingAccount;
        }
    }

    /// <summary>
    /// Represent the next available mortgage account
    /// </summary>
    public class NextMortgageAccount
    {
        protected static BankOfBIT_XWContext db = new BankOfBIT_XWContext();
        private const int NEXT_AVAILABLE_NUMBER = 200000;

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextMortgageAccountId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextMortgageAccount nextMortgageAccount;

        private NextMortgageAccount()
        {
            this.NextAvailableNumber = NEXT_AVAILABLE_NUMBER;
        }

        /// <summary>
        /// Get an instance of next mortgage account
        /// </summary>
        /// <returns>nextMortgageAccount</returns>
        public static NextMortgageAccount GetInstance()
        {
            if (nextMortgageAccount == null)
            {
                nextMortgageAccount = db.NextMortgageAccounts.SingleOrDefault();
                if (nextMortgageAccount == null)
                {
                    nextMortgageAccount = new NextMortgageAccount();
                    db.NextMortgageAccounts.Add(nextMortgageAccount);
                    db.SaveChanges();
                }

            }
            return nextMortgageAccount;
        }
    }

    /// <summary>
    /// Represent the next available investment account
    /// </summary>
    public class NextInvestmentAccount
    {
        protected static BankOfBIT_XWContext db = new BankOfBIT_XWContext();
        private const int NEXT_AVAILABLE_NUMBER = 2000000;

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextInvestmentAccountId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextInvestmentAccount nextInvestmentAccount;

        private NextInvestmentAccount()
        {
            this.NextAvailableNumber = NEXT_AVAILABLE_NUMBER;
        }

        /// <summary>
        /// Get an instance of next investment account
        /// </summary>
        /// <returns>nextInvestmentAccount</returns>
        public static NextInvestmentAccount GetInstance()
        {
            if (nextInvestmentAccount == null)
            {
                nextInvestmentAccount = db.NextInvestmentAccounts.SingleOrDefault();
                if (nextInvestmentAccount == null)
                {
                    nextInvestmentAccount = new NextInvestmentAccount();
                    db.NextInvestmentAccounts.Add(nextInvestmentAccount);
                    db.SaveChanges();
                }
            }
            return nextInvestmentAccount;
        }
    }

    /// <summary>
    /// Represent the next available chequing account
    /// </summary>
    public class NextChequingAccount
    {
        protected static BankOfBIT_XWContext db = new BankOfBIT_XWContext();

        private const int NEXT_AVAILABLE_NUMBER = 20000000;

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextChequingAccountId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextChequingAccount nextChequingAccount;

        private NextChequingAccount()
        {
            this.NextAvailableNumber = NEXT_AVAILABLE_NUMBER;
        }

        /// <summary>
        /// Get an  instance of next chequing account
        /// </summary>
        /// <returns>nextChequingAccount</returns>
        public static NextChequingAccount GetInstance()
        {
            if (nextChequingAccount == null)
            {
                nextChequingAccount = db.NextChequingAccounts.SingleOrDefault();
                if (nextChequingAccount == null)
                {
                    nextChequingAccount = new NextChequingAccount();
                    db.NextChequingAccounts.Add(nextChequingAccount);
                    db.SaveChanges();
                }

            }
            return nextChequingAccount;
        }
    }

    /// <summary>
    /// Represent the next available transaction number
    /// </summary>
    public class NextTransactionNumber
    {
        protected static BankOfBIT_XWContext db = new BankOfBIT_XWContext();

        private const int NEXT_AVAILABLE_NUMBER = 700;

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextTransactionNumberId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextTransactionNumber nextTransactionNumber;

        private NextTransactionNumber()
        {
            this.NextAvailableNumber = NEXT_AVAILABLE_NUMBER;
        }

        /// <summary>
        /// Get an instance of next transaction number 
        /// </summary>
        /// <returns>nextTransactionNumber</returns>

        public static NextTransactionNumber GetInstance()
        {
            if (nextTransactionNumber == null)
            {
                nextTransactionNumber = db.NextTransactionNumbers.SingleOrDefault();
                if (nextTransactionNumber == null)
                {
                    nextTransactionNumber = new NextTransactionNumber();
                    db.NextTransactionNumbers.Add(nextTransactionNumber);
                    db.SaveChanges();
                }
            }
            return nextTransactionNumber;
        }
    }

    /// <summary>
    /// Represent a payee
    /// </summary>
    public class Payee
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PayeeId { get; set; }

        [Display(Name = "Payee")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Represent a institution
    /// </summary>
    public class Institution
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int InstitutionId { get; set; }

        [Display(Name = "Institution\nNumber")]
        public int InstitutionNumber { get; set; }

        [Display(Name = "Institution")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Represent a transaction type
    /// </summary>
    public class TransactionType
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TransactionTypeId { get; set; }

        [Display(Name = "Transaction\nType")]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public double ServiceCharges { get; set; }
    }

    /// <summary>
    /// Represent a transaction 
    /// </summary>
    public class Transaction
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [Display(Name = "Transaction\nNumber")]
        public long TransactionNumber { get; set; }

        [Required]
        [ForeignKey("BankAccount")]
        public int BankAccountId { get; set; }

        [Required]
        [ForeignKey("TransactionType")]
        public int TransactionTypeId { get; set; }

        [Display(Name = "Deposit")]
        public double Deposit { get; set; }

        [Display(Name = "Withdrawal")]
        public double Withdrawal { get; set; }

        [Required]
        [Display(Name = "Date\nCreated")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Account\nNote")]
        public string Notes { get; set; }

        public virtual BankAccount BankAccount { get; set; }

        public virtual TransactionType TransactionType { get; set; }

        /// <summary>
        /// Set the next transaction number 
        /// </summary>
        public void SetNextTransactionNumber()
        {
            long? number = StoredProcedures.NextNumber("NextTransactionNumbers");
            if (number != null)
            {
                TransactionNumber = (long)number;
            }
        }
    }

    /// <summary>
    /// Represent a RFID tag
    /// </summary>
    public class RFIDTag
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RFIDTagId { get; set; }

        public long CardNumber { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
    }

    /// <summary>
    /// Represent a store procedure 
    /// </summary>
    public static class StoredProcedures
    {
        
        public static long? NextNumber(string tableName)
        {
            try
            {
                //establish a database connection
                SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=BankofBIT_XWContext;Integrated Security=True");
                long? returnValue = 0;
                //Declare a stored procedure for database operaton
                SqlCommand storedProcedure = new SqlCommand("next_number", connection);

                //Set the command type to the storedprocedure 
                storedProcedure.CommandType = CommandType.StoredProcedure;

                //Decalre the output to map to the database
                storedProcedure.Parameters.AddWithValue("@TableName", tableName);
                SqlParameter outputParameter = new SqlParameter("@NewVal", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };

               //add the mapped parameter to the storedprocedure
                storedProcedure.Parameters.Add(outputParameter);
                connection.Open();

                //execute the query
                storedProcedure.ExecuteNonQuery();
                connection.Close();

                //Set the return value to the output parameter
                returnValue = (long?)outputParameter.Value;
                return returnValue;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
