using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using CoreBll;
namespace BanCore.Services
{
    public class GeneralService
    {
        private static CoreDbEntities db = new CoreDbEntities();
        #region Bank
        public List<Bank> GetAllBanks()
        {
            List<Bank> banks = null;
            var rows = db.getbanks(null, null).ToList();
            if (rows.Any())
            {
                banks = new List<Bank>();
                foreach (var r in rows)
                {
                    banks.Add(new Bank
                    {
                        Id = (int)r.Id,
                        Identification = r.Identification,
                        Name = r.Name
                    });
                }
            }
            return banks;
        }
        public Bank GetBankById(int id)
        {
            Bank bank = null;
            var rows = db.getbanks(id, null).ToList();
            if (rows.Count() == 1)
            {
                var r = rows.FirstOrDefault();
                bank = new Bank
                {
                    Id = (int)r.Id,
                    Identification = r.Identification,
                    Name = r.Name
                };
            }
            return bank;
        }
        #endregion

        #region Transaction Types
        public string InsertTransactionType(TransactionType type)
        {
            if (ValidationHandler.ValidateTransactionType(type))
            {
                try
                {
                    db.Database.BeginTransaction();
                    ObjectParameter newId = new ObjectParameter("NewId", typeof(int));
                    db.InsertOrUpdateTransactionType(type.Id, type.Name, type.Description,(int)Status.Active, newId);
                    db.Database.CurrentTransaction.Commit();
                    return "Tipo de Transaccio Creado";
                }
                catch (Exception e)
                {
                    db.Database.CurrentTransaction.Rollback();
                }
            }
            return "El typee no puede ser registrado, favor verifique los campos requeridos";
        }
        public string UpdateTransactionType(TransactionType type)
        {
            if (ValidationHandler.ValidateTransactionType(type))
            {
                try
                {
                    db.Database.BeginTransaction();
                    ObjectParameter newId = new ObjectParameter("NewId", typeof(int));
                    db.InsertOrUpdateTransactionType(type.Id, type.Name, type.Description, type.StatusId, newId);
                    db.Database.CurrentTransaction.Commit();
                    return "Tipo de Transaccion Modificado";
                }
                catch (Exception e)
                {
                    db.Database.CurrentTransaction.Rollback();
                }
            }
            return "El typee no puede ser modificado, favor verifique los campos requeridos";
        }
        public string DeleteTransactionType(int id)
        {
            var TransactionType = GetTransactionTypeById(id);
            if (TransactionType.Id == 0)
                return "Este tipo de transaccion no existe";
            TransactionType.Status = Status.Inactive;
            UpdateTransactionType(TransactionType);

            return "Tipo de Transaccion desactivado.";
        }
        public List<TransactionType> GetAllTransactionTypes()
        {
            List<TransactionType> types = null;
            var rows = db.gettransactiontypes(null, null).ToList();
            if (rows.Any())
            {
                types = new List<TransactionType>();
                foreach (var r in rows)
                {
                    types.Add(new TransactionType
                    {

                        Id = (int)r.Id,
                        Name = r.Name,
                        Description = r.Description,
                        Status = (Status)r.StatusId
                    });
                }
            }
            return types;
        }
        public TransactionType GetTransactionTypeById(int id)
        {
            TransactionType type = null;
            var rows = db.gettransactiontypes(id, null).ToList();
            if (rows.Count() > 0)
            {
                var r = rows.FirstOrDefault();
                type = new TransactionType
                {

                    Id = (int)r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Status = (Status)r.StatusId
                };
            }
            return type;
        }
        public TransactionType GetTransactionTypeByName(string name)
        {
            TransactionType type = null;
            var rows = db.gettransactiontypes(null, name).ToList();
            if (rows.Count() > 0)
            {
                var r = rows.FirstOrDefault();
                type = new TransactionType
                {

                    Id = (int)r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Status = (Status)r.StatusId
                };
            }
            return type;
        }
        #endregion


    }
}