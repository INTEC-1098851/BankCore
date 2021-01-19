using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBll
{
    public static class ValidationHandler
    {
        public static bool ValidateAccount(Account acc)
        {
            if (acc.OwnerId == 0 && (!acc.AccountTypeId.HasValue) || acc.AccountManagerId == 0)
            {
                return false;
            }
            return true;
        }
        public static bool ValidateCard(Card card)
        {
            if (string.IsNullOrEmpty(card.Number) || card.OwnerId == 0 || card.Limit == 0 ||
                card.CutOffDate == null || card.PaymentLimitDate == null)
            {
                return false;
            }
            return true;
        }
        public static bool ValidateClient(Client client)
        {
            if (string.IsNullOrEmpty(client.Name) || string.IsNullOrEmpty(client.LastName) || (!client.IdentificationTypeId.HasValue) ||
                string.IsNullOrEmpty(client.Identification) || string.IsNullOrEmpty(client.Telephone) ||
                string.IsNullOrEmpty(client.Address) || (!client.GenderId.HasValue))
            {
                return false;
            }
            return true;
        }

        public static string ValidateTransaction(Transaction trans)
        {
            if(trans.TransactionType == TransactionTypeEnum.Deposit)
            {
                if (trans.PayeeId == 0 || trans.PayeeProductTypeId == 0 || 
                    string.IsNullOrEmpty(trans.PayeeProductNumber) || trans.Amount == 0)
                {
                    return "Para las transacciones de depósito son necesarios los campos: " +
                        "[IdBeneficiario,IdTipoProducto,NumeroProducto,Monto]";
                }
            }
            else if(trans.TransactionType == TransactionTypeEnum.Retire)
            {
                if (trans.PayerId == 0 || string.IsNullOrEmpty(trans.PayerAccount) ||
                  string.IsNullOrEmpty(trans.PayerIdentification) || trans.Amount == 0)
                {
                    return "Para las transacciones de retiro son necesarios los campos: " +
                        "[IdPropietarioCuenta,IdentificacionPropietario,NumeroProducto,Monto]";
                }
            }
            else if(trans.TransactionType == TransactionTypeEnum.BankTransfer)
            {
                if (trans.PayerId == 0 || string.IsNullOrEmpty(trans.PayerAccount) || 
                    string.IsNullOrEmpty(trans.PayerIdentification) || trans.PayeeId == 0 || 
                    trans.PayeeProductTypeId == 0 || string.IsNullOrEmpty(trans.PayeeProductNumber) || 
                    string.IsNullOrEmpty(trans.PayeeIdentification) || (trans.Amount == 0))
                {
                    return "Para las transacciones interbancarias son necesarios los campos " +
                   "[IdPropietarioCuenta,CuentaPropietario,IdentificacionPropietario," +
                   "IdBeneficiario,TipoProductoBeneficiario,NumeroProductoBeneficiario," +
                   "IdentificacionBeneficiario,Monto]";
                }
            }
            else if(trans.TransactionType == TransactionTypeEnum.NationalBankTransfer)
            {
                if (trans.PayerId == 0 || string.IsNullOrEmpty(trans.PayerAccount) ||
                    string.IsNullOrEmpty(trans.PayerIdentification) || trans.PayeeId == 0 ||
                    trans.PayeeProductTypeId == 0 || string.IsNullOrEmpty(trans.PayeeProductNumber) ||
                    string.IsNullOrEmpty(trans.PayeeIdentification) || (trans.Amount == 0) ||
                    trans.PayeeBankId == 0)
                {
                    return "Para las transacciones bancarias nacionales son necesarios los campos " +
                   "[IdPropietarioCuenta,CuentaPropietario,IdentificacionPropietario," +
                   "IdBeneficiario,TipoProductoBeneficiario,NumeroProductoBeneficiario," +
                   "IdentificacionBeneficiario,IdBancoBeneficiario,Monto]";
                }
            }
            return "Ok";
        }
        public static bool ValidateTransactionType(TransactionType type)
        {
            if (string.IsNullOrEmpty(type.Name))
            {
                return false;
            }
            return true;
        }
        public static bool ValidateUser(User user)
        {
            if (user.ClientId == 0 || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }
            return true;
        }
    
        public static bool ValidateEmployeeUser(EmployeeUser user)
        {
            if (user.EmployeeId == 0 || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }
            return true;
        }
    }
}
