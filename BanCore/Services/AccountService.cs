using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using CoreBll;

namespace BanCore.Services
{
    public class AccountService
    {
        private static CoreDbEntities db = new CoreDbEntities();
        public string Insert(Account acc)
        {
            if (ValidationHandler.ValidateAccount(acc))
            {
                try
                {
                    db.Database.BeginTransaction();
                    ObjectParameter newId = new ObjectParameter("NewId", typeof(int));
                    while (string.IsNullOrEmpty(acc.Number))
                    {
                        var accountNumber = RandomNumberGenerator.GenerateRandomBankAccount();
                        if (GetByNumber(accountNumber) == null)
                            acc.Number = accountNumber;
                    }
                    db.InsertOrUpdateAccount3(acc.Id, acc.Alias, acc.Number, acc.OwnerId, acc.AccountTypeId,
                        acc.AccountManagerId, acc.CurrencyTypeId, (int)Status.Active, acc.Balance, newId);
                    db.Database.CurrentTransaction.Commit();
                    return "Cuenta Creada";
                }
                catch (Exception e)
                {
                    db.Database.CurrentTransaction.Rollback();
                }
            }
            return "La cuenta no puede ser registrada, favor verifique los campos requeridos";
        }
        public string Update(Account acc)
        {
            if (ValidationHandler.ValidateAccount(acc))
            {
                try
                {
                    db.Database.BeginTransaction();
                    ObjectParameter newId = new ObjectParameter("NewId", typeof(int));
                    db.InsertOrUpdateAccount3(acc.Id, acc.Alias, acc.Number, acc.OwnerId, acc.AccountTypeId,
                        acc.AccountManagerId, acc.CurrencyTypeId, acc.StatusId, acc.Balance, newId);
                    db.Database.CurrentTransaction.Commit();
                    return "Cuenta Modificada";
                }
                catch (Exception e)
                {
                    db.Database.CurrentTransaction.Rollback();
                }
            }
            return "La cuenta no puede ser modificada, favor verifique los campos requeridos";
        }
        public string Delete(int id)
        {
            var account = GetById(id);
            if (account.Id == 0)
                return "Esta cuenta no existe";
            account.Status = Status.Inactive;
            Update(account);
            
            return "Cuenta eliminada.";
        }
        public List<Account> GetAll()
        {
            List<Account> accounts = null;
            var rows = db.getaccounts2(null, null, null, null).ToList();
            if (rows.Any())
            {
                accounts = new List<Account>();
                foreach (var r in rows)
                {
                    accounts.Add(new Account
                    {
                        AccountManagerId = (int)r.AccountManagerId,
                        AccountType = (AccountType)r.AccountTypeId,
                        Alias = r.Alias,
                        Balance = (float)r.Balance,
                        CreationDate = (DateTime)r.CreationDate,
                        CurrencyType = (CurrencyType)r.CurrencyTypeId,
                        Id = (int)r.Id,
                        LastTransaction = (DateTime?)r.LastTransaction,
                        LastUpdate = (DateTime?)r.LastUpdate,
                        Number = r.Number,
                        OwnerId = (int)r.OwnerId,
                        Status = (Status)r.StatusId,
                        YesterdayBalance = (float?)r.YesterdayBalance
                    });
                }
            }
            return accounts;
        }
        public Account GetById(int id)
        {
            Account account = null;
            var rows = db.getaccounts2(id, null, null, null).ToList();
            if (rows.Count() > 0)
            {
                var r = rows.FirstOrDefault();
                account = new Account
                {
                    AccountManagerId = (int)r.AccountManagerId,
                    AccountType = (AccountType)r.AccountTypeId,
                    Alias = r.Alias,
                    Balance = (float)r.Balance,
                    CreationDate = (DateTime)r.CreationDate,
                    CurrencyType = (CurrencyType)r.CurrencyTypeId,
                    Id = (int)r.Id,
                    LastTransaction = (DateTime?)r.LastTransaction,
                    LastUpdate = (DateTime?)r.LastUpdate,
                    Number = r.Number,
                    OwnerId = (int)r.OwnerId,
                    Status = (Status)r.StatusId,
                    YesterdayBalance = (float?)r.YesterdayBalance
                };
            }
            return account;
        }
        public Account GetByNumber(string number)
        {
            Account account = null;
            var rows = db.getaccounts2(null, number, null, null).ToList();
            if (rows.Count() > 0)
            {
                var r = rows.FirstOrDefault();
                account = new Account
                {
                    AccountManagerId = (int)r.AccountManagerId,
                    AccountType = (AccountType)r.AccountTypeId,
                    Alias = r.Alias,
                    //AuthorizationAmount =
                    ////Available = r.Availabl,
                    Balance = (float)r.Balance,
                    CreationDate = (DateTime)r.CreationDate,
                    CurrencyType = (CurrencyType)r.CurrencyTypeId,
                    Id = (int)r.Id,
                    LastTransaction = (DateTime?)r.LastTransaction,
                    LastUpdate = (DateTime?)r.LastUpdate,
                    Number = r.Number,
                    OwnerId = (int)r.OwnerId,
                    Status = (Status)r.StatusId
                    //Transit = 
                    //YesterdayBalance = ()r.Yes
                };
            }
            return account;
        }
        public List<Account> GetByClient(int clientId)
        {
            List<Account> accounts = null;
            var rows = db.getaccounts2(null, null, clientId, null).ToList();
            if (rows.Any())
            {
                accounts = new List<Account>();
                foreach (var r in rows)
                {
                    accounts.Add(new Account
                    {
                        AccountManagerId = (int)r.AccountManagerId,
                        AccountType = (AccountType)r.AccountTypeId,
                        Alias = r.Alias,
                        Balance = (float)r.Balance,
                        CreationDate = (DateTime)r.CreationDate,
                        CurrencyType = (CurrencyType)r.CurrencyTypeId,
                        Id = (int)r.Id,
                        LastTransaction = (DateTime?)r.LastTransaction,
                        LastUpdate = (DateTime?)r.LastUpdate,
                        Number = r.Number,
                        OwnerId = (int)r.OwnerId,
                        Status = (Status)r.StatusId,
                        YesterdayBalance = (float?)r.YesterdayBalance
                    });
                }
            }
            return accounts;
        }
        public List<Account> GetByCurrencyType(int currencyTypeId)
        {
            List<Account> accounts = null;
            var rows = db.getaccounts2(null, null, null, currencyTypeId).ToList();
            if (rows.Any())
            {
                accounts = new List<Account>();
                foreach (var r in rows)
                {
                    accounts.Add(new Account
                    {
                        AccountManagerId = (int)r.AccountManagerId,
                        AccountType = (AccountType)r.AccountTypeId,
                        Alias = r.Alias,
                        Balance = (float)r.Balance,
                        CreationDate = (DateTime)r.CreationDate,
                        CurrencyType = (CurrencyType)r.CurrencyTypeId,
                        Id = (int)r.Id,
                        LastTransaction = (DateTime?)r.LastTransaction,
                        LastUpdate = (DateTime?)r.LastUpdate,
                        Number = r.Number,
                        OwnerId = (int)r.OwnerId,
                        Status = (Status)r.StatusId,
                        YesterdayBalance = (float?)r.YesterdayBalance
                    });
                }
            }
            return accounts;
        }
    }
}