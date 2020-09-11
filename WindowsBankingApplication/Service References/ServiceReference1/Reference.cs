﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.36366
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsBankingApplication.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.ITransactionManager")]
    public interface ITransactionManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionManager/DoWork", ReplyAction="http://tempuri.org/ITransactionManager/DoWorkResponse")]
        void DoWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionManager/Deposit", ReplyAction="http://tempuri.org/ITransactionManager/DepositResponse")]
        System.Nullable<double> Deposit(int accountId, double amount, string notes);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionManager/Withdrawal", ReplyAction="http://tempuri.org/ITransactionManager/WithdrawalResponse")]
        System.Nullable<double> Withdrawal(int accountId, double amount, string notes);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionManager/BillPayment", ReplyAction="http://tempuri.org/ITransactionManager/BillPaymentResponse")]
        System.Nullable<double> BillPayment(int accountId, double amount, string notes);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionManager/Transfer", ReplyAction="http://tempuri.org/ITransactionManager/TransferResponse")]
        System.Nullable<double> Transfer(int fromAccount, int toAccount, double amount, string note);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionManager/CalculateInterest", ReplyAction="http://tempuri.org/ITransactionManager/CalculateInterestResponse")]
        System.Nullable<double> CalculateInterest(int accountId, string notes);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITransactionManagerChannel : WindowsBankingApplication.ServiceReference1.ITransactionManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TransactionManagerClient : System.ServiceModel.ClientBase<WindowsBankingApplication.ServiceReference1.ITransactionManager>, WindowsBankingApplication.ServiceReference1.ITransactionManager {
        
        public TransactionManagerClient() {
        }
        
        public TransactionManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TransactionManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransactionManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransactionManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DoWork() {
            base.Channel.DoWork();
        }
        
        public System.Nullable<double> Deposit(int accountId, double amount, string notes) {
            return base.Channel.Deposit(accountId, amount, notes);
        }
        
        public System.Nullable<double> Withdrawal(int accountId, double amount, string notes) {
            return base.Channel.Withdrawal(accountId, amount, notes);
        }
        
        public System.Nullable<double> BillPayment(int accountId, double amount, string notes) {
            return base.Channel.BillPayment(accountId, amount, notes);
        }
        
        public System.Nullable<double> Transfer(int fromAccount, int toAccount, double amount, string note) {
            return base.Channel.Transfer(fromAccount, toAccount, amount, note);
        }
        
        public System.Nullable<double> CalculateInterest(int accountId, string notes) {
            return base.Channel.CalculateInterest(accountId, notes);
        }
    }
}
