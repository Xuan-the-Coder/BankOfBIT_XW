using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using BankOfBIT_XW;
using BankOfBIT_XW.Models;
using Utility;

namespace WindowsBankingApplication
{
    class Batch
    {
        private string inputFileName;
        private string logFileName;
        private string logData;

        private BankOfBIT_XWContext db = new BankOfBIT_XWContext();

        public void ProcessTransmission(string institution, string key)
        {
            inputFileName = DateTime.Today.Year + "-" + DateTime.Today.DayOfYear.ToString("D3") + "-" + institution + ".xml";

            logFileName = "LOG " + DateTime.Today.Year + "-" + DateTime.Today.DayOfYear.ToString("D3") + "-" + institution + ".txt";

            string encrypted = inputFileName + ".encrypted";
            if (File.Exists(encrypted))
            {
                Encryption.Decrypt(encrypted, inputFileName, key);
               
            }
            try
            {
                if (File.Exists(inputFileName))
                {
                    processHeader(institution);
                    processDetails();
                    if (!File.Exists(encrypted))
                    {
                        logData += "\nThe encrypted file doesn't exist.";
                        Encryption.Encrypt(inputFileName, encrypted, key);
                    }
                }
                else
                {
                    throw new Exception(inputFileName + " Not Found,Logdate: " + DateTime.Today.ToString());
                }
            }
            catch (Exception e)
            {
                logData += e.Message + "\r\n";
            }
        }

        private void processHeader(string institution)
        {
            bool instFounded = false;
            int chkSum = 0;

            XDocument xDocument = XDocument.Load(inputFileName);
            XElement xElement = xDocument.Element("account_update");

            XAttribute dateAttribute = xElement.Attribute("date");
            string dateValue = dateAttribute.Value.ToString();
            string currentDate = DateTime.Today.ToString("yyyy-MM-dd");
            if (dateValue != currentDate)
            {
                throw new Exception("Attributes number incorrect");
            }

            XAttribute institutionAttribute = xElement.Attribute("institution");
            string instValue = institutionAttribute.Value.ToString();

            if (institution != instValue)
            {
                throw new Exception("Institution number not match filename");
            }

            IQueryable<Institution> instNums = from results in db.Institutions select results;
            foreach (Institution s in instNums)
            {
                if (instValue == s.InstitutionNumber.ToString())
                {
                    instFounded = true;
                    break;
                }
            }
            if (!instFounded)
            {
                throw new Exception("Attribute institution number incorrect");
            }



            if (xElement.Attributes().Count() < 3)
            {
                throw new Exception("Attributes number incorrect");
            }

            //The checksum attribute must match the sum of all account_no elements in the file
            IEnumerable<XElement> checksums = xDocument.Descendants().Where(x => x.Name == "account_no");
            string checksumAttr = xElement.Attribute("checksum").Value.ToString();

            foreach (XElement x in checksums)
            {
                chkSum += int.Parse(x.Value);
            }
            if (checksumAttr != chkSum.ToString())
            {
                throw new Exception("Attribute checksum incorrect");
            }
 
        }

        private void processDetails()
        {
            double t;
            XDocument xDocument = XDocument.Load(inputFileName);
            //XDocument xDocs = XDocument.Load(inputFileName);
            XElement xElement = xDocument.Element("account_update");
            string instNumber = xElement.Attribute("institution").Value.ToString();
            //string instNum = xElement.Attribute("institution").Value.ToString();
            IEnumerable<XElement> filterDoc = xDocument.Descendants().Where(obj => obj.Name == "transa11ction");

            IEnumerable<XElement> filterNode = filterDoc.Where(x => x.Nodes().Count()==5);
            //IEnumerable<XElement> node = filterDoc.Where(x => x.Nodes().Count() == 5);
            processErrors(filterDoc, filterNode, "Transaction validation error");

            IEnumerable<XElement> filterInst = filterNode.Where(x => x.Element("institution").Value == instNumber);
            processErrors(filterNode, filterInst, "institution validation error");


            IEnumerable<XElement> filterType = filterInst.Where(x => double.TryParse((x.Element("type").Value), out t));
            processErrors(filterInst, filterType, "type validation error");

            IEnumerable<XElement> filterAmount = filterType.Where(x => double.TryParse((x.Element("amount").Value), out t));
            processErrors(filterType, filterAmount, "amount validation error");

            IEnumerable<XElement> filterTypeValue = filterAmount.Where(x => int.Parse(x.Element("type").Value) == 2 || int.Parse(x.Element("type").Value) == 6);
            processErrors(filterAmount, filterTypeValue, "type value validation error");

            IEnumerable<XElement> filterAmountVal = filterTypeValue.Where(x => (int.Parse(x.Element("type").Value) == 6 && double.Parse(x.Element("amount").Value) == 0) ||
                                                           (int.Parse(x.Element("type").Value) == 2 && double.Parse(x.Element("amount").Value) > 0));
            processErrors(filterTypeValue, filterAmountVal, "type range validation error");

            IEnumerable<long> accList = from results in db.BankAccounts
                                        select results.AccountNumber;

            //LINQ-to-Object query based on two prior queries
            IEnumerable<XElement> filterAccNum = filterAmount.Where(obj => accList.Contains(long.Parse(obj.Element("account_no").Value)));
            processErrors(filterAmountVal, filterAccNum, "account number validation error");

            IEnumerable<XElement> AccNum = filterAmount.Where(x => accList.Contains(long.Parse(x.Element("account_no").Value)));

            //Call the ProcessTransactions method passing the error free result set
            processTransactions(filterAccNum);
        }

        /// <summary>
        /// Process transaction 
        /// </summary>
        /// <param name="transactionRecords">represent a collection of transaction records fro0m xml fileoo</param>
        private void processTransactions(IEnumerable<XElement> transactionRecords)
        {

            ServiceReference1.TransactionManagerClient transactionManager = new ServiceReference1.TransactionManagerClient();

            double? balance = null;
            foreach (XElement xele in transactionRecords)
            {
                int xAccountNo = int.Parse(xele.Element("account_no").Value);

                //BankAccount bankAccount = (from result in db.BankAccounts
                //           where result.AccountNumber == xAccountNo
                //           select result).SingleOrDefault();
                BankAccount bankAccount = db.BankAccounts.Where(x => x.AccountNumber == xAccountNo).SingleOrDefault();

                int xAccountId = bankAccount.BankAccountId;

                string xNotes = xele.Element("notes").Value;

                if (int.Parse(xele.Element("type").Value) == 2)
                {
                    double xAmount = double.Parse(xele.Element("amount").Value);
                    balance = transactionManager.Withdrawal(xAccountId, xAmount, xNotes);
                    if (balance != null)
                    {
                        logData += "Transaction Completed Successfully: Withdrawal " + xAmount.ToString("C") + " from account " + xAccountNo.ToString() + "\r\n";
                    }
                    else
                    {
                        logData += "Transaction Withdrawal Unsuccessful: " + "\r\n";
                    }
                }
                else if (int.Parse(xele.Element("type").Value) == 6)
                {
                    balance = transactionManager.CalculateInterest(xAccountId, xNotes);
                    logData += "Calculate Interest successfully: " + "\r\n";
                }
            }
        }

        /// <summary>
        /// Method of process all errors found within the current file being processed
        /// </summary>
        /// <param name="beforeQuery"></param>
        /// <param name="afterQuery"></param>
        /// <param name="message"></param>
        private void processErrors(IEnumerable<XElement> beforeQuery, IEnumerable<XElement> afterQuery, string message)
        {
            IEnumerable<XElement> errs = beforeQuery.Except(afterQuery);

            foreach (XElement e in errs)
            {
                logData += ("----------ERROR----------\r\n");
                logData += ("File : " + inputFileName + "\r\n");
                if (e.Element("institution") != null)
                {
                    logData += "Institution: " + e.Element("institution").ToString() + "\r\n";
                }
                if (e.Element("account_no") != null)
                {
                    logData += ("Account Number:" + e.Element("account_no").ToString() + "\r\n");
                }

                if (e.Element("type") != null)
                {
                    logData += ("Transaction Type:" + e.Element("type").ToString() + "\r\n");
                }
                if (e.Element("amount") != null)
                {
                    logData += ("Amount:" + e.Element("amount").ToString() + "\r\n");
                }
                if (e.Element("notes") != null)
                {
                    logData += ("Notes:" + e.Element("notes").ToString() + "\r\n");
                }
                logData += ("Nodes:" + e.Nodes().Count().ToString() + "\r\n");
                logData += (message + "\r\n");
                logData += ("-------------------------\r\n");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string WriteLogData()
        {
            string str;

            if (File.Exists("COMPLETE-" + inputFileName) == true)
            {
                File.Delete("COMPLETE-" + inputFileName);
            }
            if (File.Exists(inputFileName) == true)
            {
                File.Move(inputFileName, "COMPLETE-" + inputFileName);
            }

            if (logData != "")
            {
                str = logData;
            }
            else
            {
                str = "Transaction process finshed.\r\n";
            }
            StreamWriter swWrite = new StreamWriter(logFileName, true);
            swWrite.WriteLine(str);
            swWrite.Close();

            logData = "";
            logFileName = "";
            return str;
        }
    }
}

