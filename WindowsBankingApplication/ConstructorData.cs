using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankOfBIT_XW;
using BankOfBIT_XW.Models;
using Utility;

namespace WindowsBankingApplication
{
    /// <summary>
    /// given:TO BE MODIFIED
    /// this class is used to capture data to be passed
    /// among the windows forms
    /// </summary>
    public class ConstructorData
    {
        public Client client { get; set; }

        public BankAccount bankAccount{get;set;}
     }

    
}
