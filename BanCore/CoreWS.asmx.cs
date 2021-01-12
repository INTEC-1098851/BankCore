using BanCore.Services;
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
        private static readonly AccountService _accService = new AccountService();
        #region Account
        [WebMethod]
        public string InsertAccount(Account acc)
            =>_accService.Insert(acc);
        [WebMethod]
        public string UpdateAccount(Account acc)
            => _accService.Update(acc);
        [WebMethod]
        public string DeleteAccount(int id)
            => _accService.Delete(id);
        [WebMethod]
        public List<Account> GetAllAccounts()
            => _accService.GetAll();
        [WebMethod]
        public Account GetAccountById(int id)
            => _accService.GetById(id);
        [WebMethod]
        public Account GetAccountByNumber(string number)
            => _accService.GetByNumber(number);
        [WebMethod]
        public List<Account> GetAccountByClient(int clientId)
            => _accService.GetByClient(clientId);
        [WebMethod]
        public List<Account> GetAccountsByCurrencyType(int currencyTypeId)
            => _accService.GetByCurrencyType(currencyTypeId);
        #endregion
        #region Transaction
        [WebMethod]
        public string InsertOrUpdateTransaction(Transaction tran)
        {
            //tran.Payer = _clientService.GetOne()
            if (ValidationHandler.ValidateClient(tran.Payer))
            {
                if (ValidationHandler.ValidateTransaction(tran))
                {
                    db.InsertOrUpdateTransaction(tran.Id, tran.PayerId, tran.PayerAccount, tran.PayerIdentification,
                        tran.PayeeId, tran.PayeeAccount, tran.PayeeIdentification, tran.PayeeBankId, tran.TransactionTypeId,
                        tran.CreationDate, tran.Number, tran.Concept, tran.Debit, tran.Credit, tran.CurrencyTypeId, tran.Balance,
                        tran.ReferenceNumber, tran.EffectiveDate, tran.StatusId, null);
                    return "Transacción completada";
                }
            }
            return "Transacciónn fallida";
        }
        #endregion

    }
}
