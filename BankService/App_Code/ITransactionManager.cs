﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITransactionManager" in both code and config file together.
[ServiceContract]
public interface ITransactionManager
{
	[OperationContract]
	void DoWork();

    [OperationContract]
     double? Deposit(int accountId, double amount, string notes);

    [OperationContract]
     double? Withdrawal(int accountId, double amount, string notes);

    [OperationContract]
     double? BillPayment(int accountId, double amount, string notes);

    [OperationContract]
     double? Transfer(int fromAccount, int toAccount, double amount, string note);

    [OperationContract]
     double? CalculateInterest(int accountId, string notes);
}
