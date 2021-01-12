using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoreBll;
namespace BanCore.Services
{
    public class TransactionService
    {

        private static CoreDbEntities db = new CoreDbEntities();
        public string Insert(Transaction transaction)
        {
            if (ValidationHandler.ValidateTransaction(transaction))
            {
                try
                {
                    db.Database.BeginTransaction();
                    db.InsertOrUpdateTransaction(transaction.Id, transaction.PayerId, transaction.PayerAccount, 
                        transaction.PayerIdentification, transaction.PayeeId,transaction.PayeeAccount,
                        transaction.PayeeIdentification,transaction.PayeeBankId,transaction.TransactionTypeId,
                        transaction.Number,transaction.Concept,transaction.Debit,transaction.Credit,
                        transaction.CurrencyTypeId,transaction.Balance,transaction.ReferenceNumber,
                        transaction.EffectiveDate,(int)Status.Active, null);
                    db.Database.CurrentTransaction.Commit();
                    return "Transaccion Creada";
                }
                catch (Exception e)
                {
                    db.Database.CurrentTransaction.Rollback();
                }
            }
            return "La Transaccion no puede ser realizada, favor verifique los campos requeridos";
        }
        public string Update(Transaction transaction)
        {
            if (ValidationHandler.ValidateTransaction(transaction))
            {
                try
                {
                    db.Database.BeginTransaction();
                    db.InsertOrUpdateTransaction(transaction.Id, transaction.PayerId, transaction.PayerAccount,
                     transaction.PayerIdentification, transaction.PayeeId, transaction.PayeeAccount,
                     transaction.PayeeIdentification, transaction.PayeeBankId, transaction.TransactionTypeId,
                     transaction.Number, transaction.Concept, transaction.Debit, transaction.Credit,
                     transaction.CurrencyTypeId, transaction.Balance, transaction.ReferenceNumber,
                     transaction.EffectiveDate, (int)Status.Active, null);
                    db.Database.CurrentTransaction.Commit();
                    return "Transaccion Modificada";
                }
                catch (Exception e)
                {
                    db.Database.CurrentTransaction.Rollback();
                }
            }
            return "La transaccion no puede ser modificada, favor verifique los campos requeridos";
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
            List<Transaction> transactions = new List<Transaction>();
            var rows = db.gettransactions(null, null, null,null,null,null,null,null,null,null,null,null,null);
            if (rows.Count() > 0)
            {
                foreach (var r in rows)
                {
                    transactions.Add(new Transaction
                    {
                        Balance = (float)r.Balance,
                        PayeeBankId = (int)r.PayeeBankId,
                        Concept = r.Concept,
                        CreationDate = (DateTime)r.CreationDate,
                        Credit = (float)r.Credit,
                        CurrencyType = (CurrencyType)r.CurrencyTypeId,
                        Debit = (float)r.Debit,
                        EffectiveDate = (DateTime)r.EffectiveDate,
                        Id = (int)r.Id,
                        Number = r.Number,
                        PayeeAccount = r.PayeeAccount,
                        PayeeId = (int)r.PayeeId,
                        PayeeIdentification = r.PayeeIdentification,
                        PayerAccount = r.PayerAccount,
                        PayerId = (int)r.PayerId,
                        PayerIdentification = r.PayerIdentification,
                        ReferenceNumber = r.ReferenceNumber,
                        Status = (Status)r.StatusId,
                        TransactionType = (TransactionTypeEnum)r.TransactionTypeId
                    });
                }
            }
            return transactions;
        }
        public Transaction GetById(int id)
        {
            Transaction transaction = new Transaction();
            var rows = db.gettransactions(id, null, null, null, null, null, null, null, null, null, null, null, null);
            if (rows.Count() == 1)
            {
                var r = rows.FirstOrDefault();
                transaction = new Transaction
                {
                    Balance = (float)r.Balance,
                    PayeeBankId = (int)r.PayeeBankId,
                    Concept = r.Concept,
                    CreationDate = (DateTime)r.CreationDate,
                    Credit = (float)r.Credit,
                    CurrencyType = (CurrencyType)r.CurrencyTypeId,
                    Debit = (float)r.Debit,
                    EffectiveDate = (DateTime)r.EffectiveDate,
                    Id = (int)r.Id,
                    Number = r.Number,
                    PayeeAccount = r.PayeeAccount,
                    PayeeId = (int)r.PayeeId,
                    PayeeIdentification = r.PayeeIdentification,
                    PayerAccount = r.PayerAccount,
                    PayerId = (int)r.PayerId,
                    PayerIdentification = r.PayerIdentification,
                    ReferenceNumber = r.ReferenceNumber,
                    Status = (Status)r.StatusId,
                    TransactionType = (TransactionTypeEnum)r.TransactionTypeId
                };
            }
            return transaction;
        }
        public List<Transaction> GetByPayerId(int payerId)
        {
            List<Transaction> transactions = new List<Transaction>();
            var rows = db.gettransactions(null, payerId, null, null, null, null, null, null, null, null, null, null, null);
            if (rows.Count() > 0)
            {
                foreach (var r in rows)
                {
                    transactions.Add(new Transaction
                    {
                        Balance = (float)r.Balance,
                        PayeeBankId = (int)r.PayeeBankId,
                        Concept = r.Concept,
                        CreationDate = (DateTime)r.CreationDate,
                        Credit = (float)r.Credit,
                        CurrencyType = (CurrencyType)r.CurrencyTypeId,
                        Debit = (float)r.Debit,
                        EffectiveDate = (DateTime)r.EffectiveDate,
                        Id = (int)r.Id,
                        Number = r.Number,
                        PayeeAccount = r.PayeeAccount,
                        PayeeId = (int)r.PayeeId,
                        PayeeIdentification = r.PayeeIdentification,
                        PayerAccount = r.PayerAccount,
                        PayerId = (int)r.PayerId,
                        PayerIdentification = r.PayerIdentification,
                        ReferenceNumber = r.ReferenceNumber,
                        Status = (Status)r.StatusId,
                        TransactionType = (TransactionTypeEnum)r.TransactionTypeId
                    });
                }
            }
            return transactions;
        }
        public List<Transaction> GetByPayeeId(int payeeId)
        {
            List<Transaction> transactions = new List<Transaction>();
            var rows = db.gettransactions(null, null, null, null, payeeId, null, null, null, null, null, null, null, null);
            if (rows.Count() > 0)
            {
                foreach (var r in rows)
                {
                    transactions.Add(new Transaction
                    {
                        Balance = (float)r.Balance,
                        PayeeBankId = (int)r.PayeeBankId,
                        Concept = r.Concept,
                        CreationDate = (DateTime)r.CreationDate,
                        Credit = (float)r.Credit,
                        CurrencyType = (CurrencyType)r.CurrencyTypeId,
                        Debit = (float)r.Debit,
                        EffectiveDate = (DateTime)r.EffectiveDate,
                        Id = (int)r.Id,
                        Number = r.Number,
                        PayeeAccount = r.PayeeAccount,
                        PayeeId = (int)r.PayeeId,
                        PayeeIdentification = r.PayeeIdentification,
                        PayerAccount = r.PayerAccount,
                        PayerId = (int)r.PayerId,
                        PayerIdentification = r.PayerIdentification,
                        ReferenceNumber = r.ReferenceNumber,
                        Status = (Status)r.StatusId,
                        TransactionType = (TransactionTypeEnum)r.TransactionTypeId
                    });
                }
            }
            return transactions;
        }
        public Transaction GetByNumber(string number)
        {
            Transaction transaction = new Transaction();
            var rows = db.gettransactions(null, null, null, null, null, null, null, number, null, null, null, null, null);
            if (rows.Count() == 1)
            {
                var r = rows.FirstOrDefault();
                transaction = new Transaction
                {
                    Balance = (float)r.Balance,
                    PayeeBankId = (int)r.PayeeBankId,
                    Concept = r.Concept,
                    CreationDate = (DateTime)r.CreationDate,
                    Credit = (float)r.Credit,
                    CurrencyType = (CurrencyType)r.CurrencyTypeId,
                    Debit = (float)r.Debit,
                    EffectiveDate = (DateTime)r.EffectiveDate,
                    Id = (int)r.Id,
                    Number = r.Number,
                    PayeeAccount = r.PayeeAccount,
                    PayeeId = (int)r.PayeeId,
                    PayeeIdentification = r.PayeeIdentification,
                    PayerAccount = r.PayerAccount,
                    PayerId = (int)r.PayerId,
                    PayerIdentification = r.PayerIdentification,
                    ReferenceNumber = r.ReferenceNumber,
                    Status = (Status)r.StatusId,
                    TransactionType = (TransactionTypeEnum)r.TransactionTypeId
                };
            }
            return transaction;
        }
        public Transaction GetByReference(string reference)
        {
            Transaction transaction = new Transaction();
            var rows = db.gettransactions(null, null, null, null, null, null, null, null, reference, null, null, null, null);
            if (rows.Count() == 1)
            {
                var r = rows.FirstOrDefault();
                transaction = new Transaction
                {
                    Balance = (float)r.Balance,
                    PayeeBankId = (int)r.PayeeBankId,
                    Concept = r.Concept,
                    CreationDate = (DateTime)r.CreationDate,
                    Credit = (float)r.Credit,
                    CurrencyType = (CurrencyType)r.CurrencyTypeId,
                    Debit = (float)r.Debit,
                    EffectiveDate = (DateTime)r.EffectiveDate,
                    Id = (int)r.Id,
                    Number = r.Number,
                    PayeeAccount = r.PayeeAccount,
                    PayeeId = (int)r.PayeeId,
                    PayeeIdentification = r.PayeeIdentification,
                    PayerAccount = r.PayerAccount,
                    PayerId = (int)r.PayerId,
                    PayerIdentification = r.PayerIdentification,
                    ReferenceNumber = r.ReferenceNumber,
                    Status = (Status)r.StatusId,
                    TransactionType = (TransactionTypeEnum)r.TransactionTypeId
                };
            }
            return transaction;
        }
        public List<Transaction> FilterBy(int? payerId,string payerAccount=null,string payerIdentication=null,
            int? payeeId=null,string payeeAccount=null,string payeeIdentification=null,DateTime? creationDateFrom=null,
            DateTime? creationDateTo=null,DateTime? effectiveDateFrom=null,DateTime? effectiveDateTo=null)
        {
            List<Transaction> transactions = new List<Transaction>();
            var rows = db.gettransactions(null, payerId, payerAccount, payerIdentication, payeeId, payeeAccount, payeeIdentification,
                null, null, creationDateFrom, creationDateTo, effectiveDateFrom, effectiveDateTo);
            if (rows.Count() > 0)
            {
                foreach (var r in rows)
                {
                    transactions.Add(new Transaction
                    {
                        Balance = (float)r.Balance,
                        PayeeBankId = (int)r.PayeeBankId,
                        Concept = r.Concept,
                        CreationDate = (DateTime)r.CreationDate,
                        Credit = (float)r.Credit,
                        CurrencyType = (CurrencyType)r.CurrencyTypeId,
                        Debit = (float)r.Debit,
                        EffectiveDate = (DateTime)r.EffectiveDate,
                        Id = (int)r.Id,
                        Number = r.Number,
                        PayeeAccount = r.PayeeAccount,
                        PayeeId = (int)r.PayeeId,
                        PayeeIdentification = r.PayeeIdentification,
                        PayerAccount = r.PayerAccount,
                        PayerId = (int)r.PayerId,
                        PayerIdentification = r.PayerIdentification,
                        ReferenceNumber = r.ReferenceNumber,
                        Status = (Status)r.StatusId,
                        TransactionType = (TransactionTypeEnum)r.TransactionTypeId
                    });
                }
            }
            return transactions;
        }
    }
}