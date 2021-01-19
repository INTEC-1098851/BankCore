using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBll
{
 
    public class Card
    {
        public int Id { get; set; }
        public Client Owner { get; set; }
        public int OwnerId { get; set; }
        public string Number { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationDateStr { get { return CreationDate.ToString("dd-MM-yyyy"); } }
        public DateTime? CutOffDate { get; set; }
        public string CutOffDateStr { get { return CutOffDate.HasValue ?  CutOffDate.Value.ToString("dd-MM-yyyy"):""; } }
        public DateTime? PaymentLimitDate { get; set; }
        public string PaymentLimitDateStr { get { return PaymentLimitDate.HasValue ? PaymentLimitDate.Value.ToString("dd-MM-yyyy"):""; } }
        public Status Status { get; set; }
        public int StatusId { get { return (int)Status; } }
        public float Limit { get; set; }
        public CurrencyType CurrencyTypes { get; set; }
        public int CurrencyTypesId { get { return (int)CurrencyTypes; } }
        public float Available { get; set; }
        public float? Balance { get; set; }
        public float CutOffBalance { get; set; }
        public float MinimumPayment { get; set; }
        public float ExpiredAmount { get; set; }
        public float ExpiredBill { get; set; }
        public float LastBalance { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public string LastPaymentDateStr { get { return LastPaymentDate.HasValue ? LastPaymentDate.Value.ToString("dd-MM-yyyy"):""; } }
        public float LastPayment { get; set; }
    }

}
