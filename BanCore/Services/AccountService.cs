using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CoreBll;
using System.Configuration;

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
                    db.InsertOrUpdateAccount(acc.Id, acc.Alias, acc.Number, acc.OwnerId, acc.AccountTypeId, null, null,
                        acc.AccountManagerId, acc.CurrencyTypeId, (int)Status.Active, acc.Balance, null);
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
                    db.InsertOrUpdateAccount(acc.Id, acc.Alias, acc.Number, acc.OwnerId, acc.AccountTypeId, null, null,
                    acc.AccountManagerId, acc.CurrencyTypeId, (int)Status.Active, acc.Balance, null);
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
            List<Account> accounts = new List<Account>();
            var rows = db.getaccounts(null, null, null, null);
            if (rows.Count() > 0)
            {
                foreach (var r in rows)
                {
                    accounts.Add(new Account
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
                        LastTransaction = (DateTime)r.LastTransaction,
                        LastUpdate = (DateTime)r.LastUpdate,
                        Number = r.Number,
                        OwnerId = (int)r.OwnerId,
                        Status = (Status)r.StatusId
                        //Transit = 
                        //YesterdayBalance = ()r.Yes
                    });
                }
            }
            return accounts;
        }
        public Account GetById(int id)
        {
            Account account = new Account();
            var rows = db.getaccounts(id, null, null, null);
            if (rows.Count() == 1)
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
                    LastTransaction = (DateTime)r.LastTransaction,
                    LastUpdate = (DateTime)r.LastUpdate,
                    Number = r.Number,
                    OwnerId = (int)r.OwnerId,
                    Status = (Status)r.StatusId
                    //Transit = 
                    //YesterdayBalance = ()r.Yes
                };
            }
            return account;
        }
        public Account GetByNumber(string number)
        {
            Account account = new Account();
            var rows = db.getaccounts(null, number, null, null);
            if (rows.Count() == 1)
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
                    LastTransaction = (DateTime)r.LastTransaction,
                    LastUpdate = (DateTime)r.LastUpdate,
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
            List<Account> accounts = new List<Account>();
            var rows = db.getaccounts(null, null, clientId, null);
            if (rows.Count() > 0)
            {
                foreach (var r in rows)
                {
                    accounts.Add(new Account
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
                        LastTransaction = (DateTime)r.LastTransaction,
                        LastUpdate = (DateTime)r.LastUpdate,
                        Number = r.Number,
                        OwnerId = (int)r.OwnerId,
                        Status = (Status)r.StatusId
                        //Transit = 
                        //YesterdayBalance = ()r.Yes
                    });
                }
            }
            return accounts;
        }
        public List<Account> GetByCurrencyType(int currencyTypeId)
        {
            List<Account> accounts = new List<Account>();
            var rows = db.getaccounts(null, null, null, currencyTypeId);
            if (rows.Count() > 0)
            {
                foreach (var r in rows)
                {
                    accounts.Add(new Account
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
                        LastTransaction = (DateTime)r.LastTransaction,
                        LastUpdate = (DateTime)r.LastUpdate,
                        Number = r.Number,
                        OwnerId = (int)r.OwnerId,
                        Status = (Status)r.StatusId
                        //Transit = 
                        //YesterdayBalance = ()r.Yes
                    });
                }
            }
            return accounts;
        }
    }
}