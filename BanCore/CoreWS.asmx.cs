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
        private static readonly CardService _cardService = new CardService();
        private static readonly ClientService _clientService = new ClientService();
        private static readonly EmployeeUserService _employeeUserService = new EmployeeUserService();
        private static readonly GeneralService _generalService = new GeneralService();
        private static readonly TransactionService _transactionService = new TransactionService();
        private static readonly UserService _userService = new UserService();

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
        public string InsertTransaction(Transaction acc)
             => _transactionService.Insert(acc);
        [WebMethod]
        public string UpdateTransaction(Transaction acc)
            => _transactionService.Update(acc);
        [WebMethod]
        public string DeleteTransaction(int id)
            => _transactionService.Delete(id);
        [WebMethod]
        public List<Transaction> GetAllTransactions()
            => _transactionService.GetAll();
        [WebMethod]
        public Transaction GetTransactionById(int id)
            => _transactionService.GetById(id);
        [WebMethod]
        public List<Transaction> GetTransactionByPayer(int payerId)
            => _transactionService.GetByPayerId(payerId);
        [WebMethod]
        public List<Transaction> GetTransactionByPayee(int payeeId)
            => _transactionService.GetByPayeeId(payeeId);
        [WebMethod]
        public Transaction GetTransactionByNumber(string number)
            => _transactionService.GetByNumber(number);
        [WebMethod]
        public Transaction GetTransactionByReference(string referenceNumber)
            => _transactionService.GetByReference(referenceNumber);
        [WebMethod]
        public List<Transaction> FilterTransaction(int? payerId, string payerAccount = null, string payerIdentication = null,
            int? payeeId = null, string payeeAccount = null, string payeeIdentification = null, DateTime? creationDateFrom = null,
            DateTime? creationDateTo = null, DateTime? effectiveDateFrom = null, DateTime? effectiveDateTo = null)
            => _transactionService.FilterBy(payerId, payerAccount, payerIdentication, payeeId, payeeAccount,
                payeeIdentification, creationDateFrom, creationDateTo, effectiveDateFrom, effectiveDateTo);
        #endregion

        #region Card 
        [WebMethod]
        public string InsertCard(Card card)
            => _cardService.Insert(card);
        [WebMethod]
        public string UpdateCard(Card card)
            => _cardService.Update(card);
        [WebMethod]
        public string DeleteCard(int id)
            => _cardService.Delete(id);
        [WebMethod]
        public List<Card> GetAllCards()
            => _cardService.GetAll();
        [WebMethod]
        public Card GetCardById(int id)
            => _cardService.GetById(id);
        [WebMethod]
        public Card GetCardByNumber(string number)
            => _cardService.GetByNumber(number);
        [WebMethod]
        public List<Card> GetCardByClient(int clientId)
            => _cardService.GetByClient(clientId);
        #endregion

        #region Client
        [WebMethod]
        public string InsertClient(Client client)
            => _clientService.Insert(client);
        [WebMethod]
        public string UpdateClient(Client client)
            => _clientService.Update(client);
        [WebMethod]
        public string DeleteClient(int id)
            => _clientService.Delete(id);
        [WebMethod]
        public List<Client> GetAllClients()
            => _clientService.GetAll();
        [WebMethod]
        public Client GetClientById(int id)
            => _clientService.GetById(id);
        [WebMethod]
        public Client GetClientByIdentification(string identification)
            => _clientService.GetByIdentification(identification);

        #endregion

        #region Employee User 
        [WebMethod]
        public string InsertEmployeeUser(EmployeeUser user)
            => _employeeUserService.Insert(user);
        [WebMethod]
        public string UpdateEmployeeUser(EmployeeUser user)
            => _employeeUserService.Update(user);
        [WebMethod]
        public string DeleteEmployeeUser(int id)
            => _employeeUserService.Delete(id);
        [WebMethod]
        public List<EmployeeUser> GetAllEmployeeUsers()
            => _employeeUserService.GetAll();
        [WebMethod]
        public EmployeeUser GetEmployeeUserById(int id)
            => _employeeUserService.GetById(id);
        [WebMethod]
        public EmployeeUser GetEmployeeUserByEmployeeId(int employeeId)
           => _employeeUserService.GetByEmployeeId(employeeId);
        [WebMethod]
        public EmployeeUser GetEmployeeUserByUserName(string userName)
            => _employeeUserService.GetByUserName(userName);
        [WebMethod]
        public EmployeeUser GetEmployeeUserByEmail(string email)
            => _employeeUserService.GetByEmail(email);
        [WebMethod]
        public List<EmployeeUser> GetEmployeeUserByRole(int roleId)
            => _employeeUserService.GetByRole(roleId);
        [WebMethod]
        public EmployeeUser ValidateEmployeeUserCredentials(string userName,string password)
                 => _employeeUserService.ValidateCredential(userName,password);
        #endregion

        #region General
        [WebMethod]
        public List<Bank> GetAllBanks()
            => _generalService.GetAllBanks();
        [WebMethod]
        public Bank GetBankById(int bankId)
            => _generalService.GetBankById(bankId);
        [WebMethod]
        public string InsertTransactionType(TransactionType transactionType)
            => _generalService.InsertTransactionType(transactionType);
        [WebMethod]
        public string UpdateTransactionType(TransactionType transactionType)
            => _generalService.UpdateTransactionType(transactionType);
        public string DeleteTransactionType(int id)
            => _generalService.DeleteTransactionType(id);
        [WebMethod]
        public List<TransactionType> GetAllTransactionTypes()
            => _generalService.GetAllTransactionTypes();
        [WebMethod]
        public TransactionType GetTransactionTypeById(int id)
            => _generalService.GetTransactionTypeById(id);
        [WebMethod]
        public TransactionType GetTransactionTypeByName(string name)
            => _generalService.GetTransactionTypeByName(name);

        #endregion

        #region Client User 
        [WebMethod]
        public string InsertUser(User user)
            => _userService.Insert(user);
        [WebMethod]
        public string UpdateUser(User user)
            => _userService.Update(user);
        [WebMethod]
        public string DeleteUser(int id)
            => _userService.Delete(id);
        [WebMethod]
        public List<User> GetAllUsers()
            => _userService.GetAll();
        [WebMethod]
        public User GetUserById(int id)
            => _userService.GetById(id);
        [WebMethod]
        public User GetUserByClientId(int employeeId)
           => _userService.GetByClientId(employeeId);
        [WebMethod]
        public User GetUserByUserName(string userName)
            => _userService.GetByUserName(userName);
        [WebMethod]
        public User GetUserByEmail(string email)
            => _userService.GetByEmail(email);
        [WebMethod]
        public User ValidateUserCredentials(string userName, string password)
           => _userService.ValidateCredential(userName, password);
        #endregion
    }
}
