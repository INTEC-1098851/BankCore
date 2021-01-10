using CoreBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Services;

namespace BanCore
{
    /// <summary>
    /// Summary description for CoreWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CoreWS : System.Web.Services.WebService
    {
        CoreDbEntities db = new CoreDbEntities();
        #region Account
        [WebMethod]
        public string InsertAccount(Account acc)
        {
            if (ValidationHandler.ValidateAccount(acc))
            {
                db.InsertOrUpdateAccount(acc.Id, acc.Alias, acc.Number, acc.OwnerId, acc.AccountTypeId, null, null,
                    acc.AccountManagerId, acc.CurrencyTypeId, (int)Status.Active, acc.Balance, null);
                return "Cuenta Creada";
            }
            return "La cuenta no puede ser registrada, favor verifique los campos requeridos";
        }
        [WebMethod]
        public string UpdateAccount(Account acc)
        {
            if (ValidationHandler.ValidateAccount(acc))
            {
                db.InsertOrUpdateAccount(acc.Id, acc.Alias, acc.Number, acc.OwnerId, acc.AccountTypeId, null, null,
                    acc.AccountManagerId, acc.CurrencyTypeId, (int)Status.Active, acc.Balance, null);
                return "Cuenta Modificada";
            }
            return "La cuenta no puede ser modificada, favor verifique los campos requeridos";
        }
        [WebMethod]
        public string DeleteAccount(int id)
        {
            var account = GetAccountById(id);
            if (account.Id == 0)
                return "Esta cuenta no existe";
            account.Status = Status.Inactive;
            UpdateAccount(account);
            return "Cuenta eliminada.";
        }
        [WebMethod]
        public List<Account> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();
            var rows = db.getaccounts(null, null, null, null);
            if(rows.Count() > 0)
            {
                foreach(var r in rows)
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
        public Account GetAccountById(int id)
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
        [WebMethod]
        public Account GetAccountByNumber(string number)
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
        [WebMethod]
        public List<Account> GetAccountByClient(int clientId)
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
        [WebMethod]
        public List<Account> GetAccountsByCurrencyType(int currencyTypeId)
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
        #endregion


    }
}
