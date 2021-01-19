using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBll
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public Transaction Transaction { get; set; }
        public int TransactionId { get; set; }
        public float LastAmount { get; set; }
        public float NewAmount { get; set; }

    }
}
