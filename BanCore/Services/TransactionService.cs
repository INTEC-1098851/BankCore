using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using CoreBll;
namespace BanCore.Services
{
    public class TransactionService
    {

        private static CoreDbEntities db = new CoreDbEntities();
        private static readonly AccountService _accService = new AccountService();
        private static readonly CardService _cardService = new CardService();
        public string Insert(Transaction transaction)
        {
            var validationHandlerResponse = ValidationHandler.ValidateTransaction(transaction);
            if (validationHandlerResponse.ToLower() == "ok")
            {
                try
                {
                    db.Database.BeginTransaction();
                    Account payerAccount=null, payeeAccount= null;
                    Card payeeCard = null;
                    payerAccount = _accService.GetByNumber(transaction.PayerAccount);
                    payerAccount.Balance -= transaction.Amount;
                    if (transaction.PayeeProductType == ProductType.Account)
                    {
                        payeeAccount = _accService.GetByNumber(transaction.PayeeProductNumber);
                        payeeAccount.Balance += transaction.Amount;
                    }else if(transaction.PayeeProductType == ProductType.Card)
                    {
                        payeeCard = _cardService.GetByNumber(transaction.PayeeProductNumber);
                        payeeCard.Balance += transaction.Amount;
                    }
                    while (string.IsNullOrEmpty(transaction.Number))
                    {
                        var transactionNumber = RandomNumberGenerator.GenerateRandomTransactionNumber();
                        if (GetByNumber(transactionNumber) == null)
                            transaction.Number = transactionNumber;
                    }
                    while (string.IsNullOrEmpty(transaction.ReferenceNumber))
                    {
                        var reference = RandomNumberGenerator.GenerateRandomTransactionReference();
                        if (GetByReference(reference) == null)
                            transaction.ReferenceNumber = reference;
                    }
                    ObjectParameter newId = new ObjectParameter("NewId", typeof(int));
                    db.InsertOrUpdateTransaction2(transaction.Id, transaction.PayerId, transaction.PayerAccount,
                               transaction.PayerIdentification, transaction.PayeeId, transaction.PayeeProductTypeId,
                               transaction.PayeeProductNumber, transaction.PayeeIdentification,
                               transaction.PayeeBankId, transaction.TransactionTypeId, transaction.Number,
                               transaction.Concept, transaction.Amount, transaction.CurrencyTypeId,
                               transaction.ReferenceNumber, transaction.EffectiveDate, payerAccount.Balance,
                               (transaction.PayeeProductType == ProductType.Account) ? payeeAccount.Balance: payeeCard.Balance,
                               (int)Status.Active, newId);
                    _accService.Update(payerAccount);
                    if (transaction.PayeeProductType == ProductType.Account) 
                        _accService.Update(payeeAccount);
                    else 
                        _cardService.Update(payeeCard);
                    db.Database.CurrentTransaction.Commit();
                    return "Transaccion Creada";
                }
                catch (Exception e)
                {
                    db.Database.CurrentTransaction.Rollback();
                }
            }
            return validationHandlerResponse;
        }
        public string Update(Transaction transaction)
        {
            var validationHandlerResponse = ValidationHandler.ValidateTransaction(transaction);
            if (validationHandlerResponse.ToLower() == "ok")
            {
                try
                {
                    db.Database.BeginTransaction();
                    Account payerAccount = null, payeeAccount = null;
                    Card payeeCard = null;
                    payerAccount = _accService.GetByNumber(transaction.PayerAccount);
                    float oldAmount = GetById(transaction.Id).Amount;
                    payerAccount = _accService.GetByNumber(transaction.PayeeProductNumber);
                    payerAccount.Balance += oldAmount - transaction.Amount;
                    if (transaction.PayeeProductType == ProductType.Account)
                    {
                        payeeAccount = _accService.GetByNumber(transaction.PayeeProductNumber);
                        payeeAccount.Balance -= oldAmount + transaction.Amount;
                    }
                    else if (transaction.PayeeProductType == ProductType.Card)
                    {
                        payeeCard = _cardService.GetByNumber(transaction.PayeeProductNumber);
                        payeeCard.Balance -= oldAmount + transaction.Amount;
                    }
                    ObjectParameter newId = new ObjectParameter("NewId", typeof(int));
                    db.InsertOrUpdateTransaction2(transaction.Id, transaction.PayerId, transaction.PayerAccount,
                        transaction.PayerIdentification, transaction.PayeeId,transaction.PayeeProductTypeId ,
                        transaction.PayeeProductNumber,transaction.PayeeIdentification, 
                        transaction.PayeeBankId, transaction.TransactionTypeId,transaction.Number, 
                        transaction.Concept, transaction.Amount, transaction.CurrencyTypeId,
                        transaction.ReferenceNumber, transaction.EffectiveDate, payerAccount.Balance,
                        (transaction.PayeeProductType == ProductType.Account) ? payeeAccount.Balance : payeeCard.Balance,
                        transaction.StatusId, newId);
                    _accService.Update(payerAccount);
                    if (transaction.PayeeProductType == ProductType.Account)
                        _accService.Update(payeeAccount);
                    else
                        _cardService.Update(payeeCard);
                    db.Database.CurrentTransaction.Commit();
                    return "Transaccion Modificada";
                }
                catch (Exception e)
                {
                    db.Database.CurrentTransaction.Rollback();
                }
            }
            return validationHandlerResponse;
        }
        public string Delete(int id)
        {
            var transaction = GetById(id);
            if (transaction.Id == 0)
                return "Esta tarjeta no existe";
            transaction.Status = Status.Inactive;
            Update(transaction);

            return "Tarjeta Desactivada.";
        }
        public List<Transaction> GetAll()
        {
            List<Transaction> transactions = null;
            var rows = db.gettransactions2(null,null, null, null,null,null,null,null,null,null,null,null,null,null).ToList();
            if (rows.Any())
            {
                 transactions = new List<Transaction>();
                foreach (var r in rows)
                {
                    transactions.Add(new Transaction
                    {
                        PayeeBankId = (int)r.PayeeBankId,
                        Concept = r.Concept,
                        CreationDate = (DateTime)r.CreationDate,
                        CurrencyType = (CurrencyType)r.CurrencyTypeId,
                        Amount = (float)r.Amount,
                        EffectiveDate = (DateTime?)r.EffectiveDate,
                        Id = (int)r.Id,
                        Number = r.Number,
                        PayeeProductNumber = r.PayeeProductNumber,
                        PayeeProductType = (ProductType)r.PayeeProductTypeId,
                        PayeeId = (int)r.PayeeId,
                        PayeeIdentification = r.PayeeIdentification,
                        PayerAccount = r.PayerAccount,
                        PayerId = (int)r.PayerId,
                        PayerIdentification = r.PayerIdentification,
                        ReferenceNumber = r.ReferenceNumber,
                        Status = (Status)r.StatusId,
                        TransactionType = (TransactionTypeEnum)r.TransactionTypeId, 
                        PayerBalance = (float?)r.PayerBalance,
                        PayeeBalance = (float?)r.PayeeBalance
                    });
                }
            }
            return transactions;
        }
        public Transaction GetById(int id)
        {
            Transaction transaction = null;
            var rows = db.gettransactions2(id, null, null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            if (rows.Count() > 0)
            {
                var r = rows.FirstOrDefault();
                transaction = new Transaction
                {
                    PayeeBankId = (int)r.PayeeBankId,
                    Concept = r.Concept,
                    CreationDate = (DateTime)r.CreationDate,
                    CurrencyType = (CurrencyType)r.CurrencyTypeId,
                    Amount = (float)r.Amount,
                    EffectiveDate = (DateTime?)r.EffectiveDate,
                    Id = (int)r.Id,
                    Number = r.Number,
                    PayeeProductNumber = r.PayeeProductNumber,
                    PayeeProductType = (ProductType)r.PayeeProductTypeId,
                    PayeeId = (int)r.PayeeId,
                    PayeeIdentification = r.PayeeIdentification,
                    PayerAccount = r.PayerAccount,
                    PayerId = (int)r.PayerId,
                    PayerIdentification = r.PayerIdentification,
                    ReferenceNumber = r.ReferenceNumber,
                    Status = (Status)r.StatusId,
                    TransactionType = (TransactionTypeEnum)r.TransactionTypeId,
                    PayerBalance = (float?)r.PayerBalance,
                    PayeeBalance = (float?)r.PayeeBalance
                };
            }
            return transaction;
        }
        public List<Transaction> GetByPayerId(int payerId)
        {
            List<Transaction> transactions = null;
            var rows = db.gettransactions2(null, payerId,null, null, null, null, null, null, null, null, null, null, null, null).ToList();
            if (rows.Any())
            {
                transactions = new List<Transaction>();
                foreach (var r in rows)
                {
                    transactions.Add(new Transaction
                    {
                        PayeeBankId = (int)r.PayeeBankId,
                        Concept = r.Concept,
                        CreationDate = (DateTime)r.CreationDate,
                        CurrencyType = (CurrencyType)r.CurrencyTypeId,
                        Amount = (float)r.Amount,
                        EffectiveDate = (DateTime?)r.EffectiveDate,
                        Id = (int)r.Id,
                        Number = r.Number,
                        PayeeProductNumber = r.PayeeProductNumber,
                        PayeeProductType = (ProductType)r.PayeeProductTypeId,
                        PayeeId = (int)r.PayeeId,
                        PayeeIdentification = r.PayeeIdentification,
                        PayerAccount = r.PayerAccount,
                        PayerId = (int)r.PayerId,
                        PayerIdentification = r.PayerIdentification,
                        ReferenceNumber = r.ReferenceNumber,
                        Status = (Status)r.StatusId,
                        TransactionType = (TransactionTypeEnum)r.TransactionTypeId,
                        PayerBalance = (float?)r.PayerBalance,
                        PayeeBalance = (float?)r.PayeeBalance
                    });
                }
            }
            return transactions;
        }
        public List<Transaction> GetByPayeeId(int payeeId)
        {
            List<Transaction> transactions = null;
            var rows = db.gettransactions2(null, null, null, null, payeeId,null, null, null, null, null, null, null, null, null).ToList();
            if (rows.Any())
            {
                transactions = new List<Transaction>();
                foreach (var r in rows)
                {
                    transactions.Add(new Transaction
                    {
                        PayeeBankId = (int)r.PayeeBankId,
                        Concept = r.Concept,
                        CreationDate = (DateTime)r.CreationDate,
                        CurrencyType = (CurrencyType)r.CurrencyTypeId,
                        Amount = (float)r.Amount,
                        EffectiveDate = (DateTime?)r.EffectiveDate,
                        Id = (int)r.Id,
                        Number = r.Number,
                        PayeeProductNumber = r.PayeeProductNumber,
                        PayeeProductType = (ProductType)r.PayeeProductTypeId,
                        PayeeId = (int)r.PayeeId,
                        PayeeIdentification = r.PayeeIdentification,
                        PayerAccount = r.PayerAccount,
                        PayerId = (int)r.PayerId,
                        PayerIdentification = r.PayerIdentification,
                        ReferenceNumber = r.ReferenceNumber,
                        Status = (Status)r.StatusId,
                        TransactionType = (TransactionTypeEnum)r.TransactionTypeId,
                        PayerBalance = (float?)r.PayerBalance,
                        PayeeBalance = (float?)r.PayeeBalance
                    });
                }
            }
            return transactions;
        }
        public Transaction GetByNumber(string number)
        {
            Transaction transaction = null;
            var rows = db.gettransactions2(null, null, null, null,null, null, null, null, number, null, null, null, null, null).ToList();
            if (rows.Count() > 0)
            {
                var r = rows.FirstOrDefault();
                transaction = new Transaction
                {
                    PayeeBankId = (int)r.PayeeBankId,
                    Concept = r.Concept,
                    CreationDate = (DateTime)r.CreationDate,
                    CurrencyType = (CurrencyType)r.CurrencyTypeId,
                    Amount = (float)r.Amount,
                    EffectiveDate = (DateTime?)r.EffectiveDate,
                    Id = (int)r.Id,
                    Number = r.Number,
                    PayeeProductNumber = r.PayeeProductNumber,
                    PayeeProductType = (ProductType)r.PayeeProductTypeId,
                    PayeeId = (int)r.PayeeId,
                    PayeeIdentification = r.PayeeIdentification,
                    PayerAccount = r.PayerAccount,
                    PayerId = (int)r.PayerId,
                    PayerIdentification = r.PayerIdentification,
                    ReferenceNumber = r.ReferenceNumber,
                    Status = (Status)r.StatusId,
                    TransactionType = (TransactionTypeEnum)r.TransactionTypeId,
                    PayerBalance = (float?)r.PayerBalance,
                    PayeeBalance = (float?)r.PayeeBalance
                };
            }
            return transaction;
        }
        public Transaction GetByReference(string reference)
        {
            Transaction transaction = null;
            var rows = db.gettransactions2(null, null, null, null, null,null, null, null, null, reference, null, null, null, null).ToList();
            if (rows.Count() > 0)
            {
                var r = rows.FirstOrDefault();
                transaction = new Transaction
                {
                    PayeeBankId = (int)r.PayeeBankId,
                    Concept = r.Concept,
                    CreationDate = (DateTime)r.CreationDate,
                    CurrencyType = (CurrencyType)r.CurrencyTypeId,
                    Amount = (float)r.Amount,
                    EffectiveDate = (DateTime?)r.EffectiveDate,
                    Id = (int)r.Id,
                    Number = r.Number,
                    PayeeProductNumber = r.PayeeProductNumber,
                    PayeeProductType = (ProductType)r.PayeeProductTypeId,
                    PayeeId = (int)r.PayeeId,
                    PayeeIdentification = r.PayeeIdentification,
                    PayerAccount = r.PayerAccount,
                    PayerId = (int)r.PayerId,
                    PayerIdentification = r.PayerIdentification,
                    ReferenceNumber = r.ReferenceNumber,
                    Status = (Status)r.StatusId,
                    TransactionType = (TransactionTypeEnum)r.TransactionTypeId,
                    PayerBalance = (float?)r.PayerBalance,
                    PayeeBalance = (float?)r.PayeeBalance
                };
            }
            return transaction;
        }
        public List<Transaction> FilterBy(int? payerId,string payerAccount=null,string payerIdentication=null,
            int? payeeId=null,int? payeeProductTypeId=null,string payeeProductNumber=null,string payeeIdentification=null,DateTime? creationDateFrom=null,
            DateTime? creationDateTo=null,DateTime? effectiveDateFrom=null,DateTime? effectiveDateTo=null)
        {
            List<Transaction> transactions = null;
            var rows = db.gettransactions2(null, payerId, payerAccount, payerIdentication, payeeId,payeeProductTypeId, payeeProductNumber, payeeIdentification,
                null, null, creationDateFrom, creationDateTo, effectiveDateFrom, effectiveDateTo).ToList();
            if (rows.Any())
            {
                transactions = new List<Transaction>();
                foreach (var r in rows)
                {
                    transactions.Add(new Transaction
                    {
                        PayeeBankId = (int)r.PayeeBankId,
                        Concept = r.Concept,
                        CreationDate = (DateTime)r.CreationDate,
                        CurrencyType = (CurrencyType)r.CurrencyTypeId,
                        Amount = (float)r.Amount,
                        EffectiveDate = (DateTime?)r.EffectiveDate,
                        Id = (int)r.Id,
                        Number = r.Number,
                        PayeeProductNumber = r.PayeeProductNumber,
                        PayeeProductType = (ProductType)r.PayeeProductTypeId,
                        PayeeId = (int)r.PayeeId,
                        PayeeIdentification = r.PayeeIdentification,
                        PayerAccount = r.PayerAccount,
                        PayerId = (int)r.PayerId,
                        PayerIdentification = r.PayerIdentification,
                        ReferenceNumber = r.ReferenceNumber,
                        Status = (Status)r.StatusId,
                        TransactionType = (TransactionTypeEnum)r.TransactionTypeId,
                        PayerBalance = (float?)r.PayerBalance,
                        PayeeBalance = (float?)r.PayeeBalance
                    });
                }
            }
            return transactions;
        }
    }
}