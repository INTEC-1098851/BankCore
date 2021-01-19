using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBll
{

    public class Transaction
    {
        public int Id { get; set; }
        public Client Payer { get; set; }
        public int? PayerId { get; set; }
        public string PayerAccount { get; set; }
        public string PayerIdentification { get; set; }
        public Client Payee { get; set; }
        public int? PayeeId { get; set; }
        public ProductType PayeeProductType{ get; set; }
        public int PayeeProductTypeId { get { return (int)PayeeProductType; } }
        public string PayeeProductNumber { get; set; }
        public string PayeeIdentification { get; set; }
        public Bank Bank { get; set; }
        public int? PayeeBankId { get; set; }
        public string PayeeBankName { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public int? TransactionTypeId { get { return (int)TransactionType; } }
        public DateTime CreationDate { get; set; }
        public string CreationDateStr { get { return CreationDate.ToString("dd-MM-yyyy"); } }
        public string Number { get; set; }
        public string Concept { get; set; }
        public float Amount { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public int CurrencyTypeId { get { return (int)CurrencyType; } }
        public string ReferenceNumber { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string EffectiveDateStr { get { return EffectiveDate.HasValue ? EffectiveDate.Value.ToString("dd-MM-yyyy"):""; } }
        public Status Status { get; set; }
        public int StatusId { get { return (int)Status; } }
        public float? PayerBalance { get; set; }
        public float? PayeeBalance { get; set; }
    }
}
