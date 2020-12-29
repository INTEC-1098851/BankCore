using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBll
{
    public class Account
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Number { get; set; }
        public Client Owner { get; set; }
        public int OwnerId { get; set; }
        public AccountType AccountType { get; set; }
        public int AccountTypeId { get { return (int)AccountType; } }
        public DateTime CreationDate { get; set; }
        public string CreationDateStr { get { return CreationDate.ToString("dd-MM-yyyy"); } }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateStr { get { return LastUpdate.ToString("dd-MM-yyyy"); } }
        public DateTime LastTransaction { get; set; }
        public string LastTransactionStr { get { return LastTransaction.ToString("dd-MM-yyyy"); } }
        public int AccountManagerId { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public int CurrencyTypeId { get { return (int)CurrencyType; } }
        public Status Status { get; set; }
        public int StatusId { get { return (int)Status; } }
        public float Balance { get; set; }
        public float Transit { get; set; }
        public float AuthorizationAmount { get; set; }
        public float Available { get; set; }
        public float YesterdayBalance { get; set; }
        
    }
}
