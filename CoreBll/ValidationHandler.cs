using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBll
{
    public static class ValidationHandler
    {
        public static bool ValidateAccount(Account acc)
        {
            if (string.IsNullOrEmpty(acc.Number) || acc.OwnerId == 0 && (!acc.AccountTypeId.HasValue) || acc.AccountManagerId == 0)
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

        public static bool ValidateTransaction(Transaction trans)
        {
            if (trans.PayerId == 0 || string.IsNullOrEmpty(trans.PayerAccount) || string.IsNullOrEmpty(trans.PayerIdentification) ||
                trans.PayeeId == 0 || string.IsNullOrEmpty(trans.PayeeAccount) || string.IsNullOrEmpty(trans.PayeeIdentification) ||
                (!trans.TransactionTypeId.HasValue) || (trans.Debit == 0 || trans.Credit == 0))
            {
                return false;
            }
            return true;
        }
        public static bool ValidateTransactionType(TransactionType type)
        {
            if (type.Id == 0 || string.IsNullOrEmpty(type.Name))
            {
                return false;
            }
            return true;
        }
        public static bool ValidateUser(User user)
        {
            if (user.Id == 0 || user.ClientId == 0 || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }
            return true;
        }
    
        public static bool ValidateEmployeeUser(EmployeeUser user)
        {
            if (user.Id == 0 || user.EmployeeId == 0 || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }
            return true;
        }
    }
}
