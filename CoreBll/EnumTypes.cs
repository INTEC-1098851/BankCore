using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBll
{
    public enum Gender
    {
        Male=0,
        Female=1
    }

    public enum IdentificationType
    {
        Cedula=0,
        Passport=1
    }

    public enum AccountType
    {
        Saving=0,
        Current=1
    }
    [Flags]
    public enum CurrencyType
    {
        DOP=0,
        US=1
    }
    public enum Status
    {
        Active=1,
        Inactive
    }
    public enum TransactionTypeEnum
    {
        Deposit = 1,
        BankTransfer = 2
    }
}
