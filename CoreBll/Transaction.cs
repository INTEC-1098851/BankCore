using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBll
{

    public class Transaction
    {
        public int Id { get; set; }
        public Client Payer { get; set; }
        public int PayerId { get; set; }
        public string PayerAccount { get; set; }
        public string PayerIdentification { get; set; }
        public Client Payee { get; set; }
        public int PayeeId { get; set; }
        public string PayeeAccount { get; set; }
        public string PayeeIdentification { get; set; }
        public Bank Bank { get; set; }
        public int PayeeBankId { get; set; }
        public string PayeeBankName { get; set; }
        public TransactionType TransactionType { get; set; }
        public int? TransactionTypeId { get { return (int)TransactionType; } }
        public DateTime CreationDate { get; set; }
        public string CreationDateStr { get { return CreationDate.ToString("dd-MM-yyyy"); } }
        public string Number { get; set; }
        public string Concept { get; set; }

        public float Debit { get; set; }
        public float Credit { get; set; }
        public bool IsCredit { get { return Credit > 0; } }
        public CurrencyType CurrencyType { get; set; }
        public int CurrencyTypeId { get { return (int)CurrencyType; } }
        public float Balance { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string EffectiveDateStr { get { return EffectiveDate.ToString("dd-MM-yyyy"); } }
        public Status Status { get; set; }
        public int StatusId { get { return (int)Status; } }

    }
}
