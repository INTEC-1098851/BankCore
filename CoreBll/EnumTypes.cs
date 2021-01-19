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
        Current=1,
        Other=2
    }
    [Flags]
    public enum CurrencyType
    {
        DOP=0,
        US=1,
        EUR=2
    }
    public enum Status
    {
        Inactive = 0,
        Active =1,
    }
    public enum TransactionTypeEnum
    {
        Deposit = 0,
        Retire=1,
        BankTransfer = 2,
        NationalBankTransfer=3
    }
    public enum Role
    {
        Administrator = 0,
        Crud= 1,
        View = 2
    }
    public enum ProductType
    {
        Account=0,
        Card=1,
        Loan=2
    }
}
