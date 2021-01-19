using CoreBll;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
namespace BanCore.Services
{
    public class CardService
    {
        private static CoreDbEntities db = new CoreDbEntities();
        public string Insert(Card card)
        {
            if (ValidationHandler.ValidateCard(card))
            {
                try
                {
                    db.Database.BeginTransaction();
                    ObjectParameter newId = new ObjectParameter("NewId", typeof(int));
                    card.Number = RandomNumberGenerator.GenerateMasterCardNumber();
                    db.InsertOrUpdateCard(card.Id, card.OwnerId, card.Number, card.CutOffDate, card.PaymentLimitDate,
                        (int)Status.Active, card.Limit, card.CurrencyTypesId, card.Balance,newId);
                    db.Database.CurrentTransaction.Commit();
                    return "Tarjeta Creada";
                }
                catch (Exception e)
                {
                    db.Database.CurrentTransaction.Rollback();
                }
            }
            return "La tarjeta no puede ser registrada, favor verifique los campos requeridos";
        }
        public string Update(Card card)
        {
            if (ValidationHandler.ValidateCard(card))
            {
                try
                {
                    db.Database.BeginTransaction();
                    ObjectParameter newId = new ObjectParameter("NewId", typeof(int));
                    db.InsertOrUpdateCard(card.Id, card.OwnerId, card.Number, card.CutOffDate, card.PaymentLimitDate,
                    card.StatusId, card.Limit, card.CurrencyTypesId, card.Balance, newId);
                    db.Database.CurrentTransaction.Commit();
                    return "Tarjeta Modificada";
                }
                catch (Exception e)
                {
                    db.Database.CurrentTransaction.Rollback();
                }
            }
            return "La tarjeta no puede ser modificada, favor verifique los campos requeridos";
        }
        public string Delete(int id)
        {
            var card = GetById(id);
            if (card.Id == 0)
                return "Esta tarjeta no existe";
            card.Status = Status.Inactive;
            Update(card);

            return "Tarjeta Desactivada.";
        }
        public List<Card> GetAll()
        {
            List<Card> cards = null ;
            db.Database.Connection.Open();
            var rows = db.getcards(null, null, null).ToList();
            db.Database.Connection.Close();
            if (rows.Any())
            {
                cards = new List<Card>();
                foreach (var r in rows)
                {
                    cards.Add(new Card
                    {
                        //Available = r.Available,
                        Balance = (float?)r.Balance,
                        CreationDate = (DateTime)r.CreationDate,
                        CurrencyTypes = (CurrencyType)r.CurrencyTypes,
                        //CutOffBalance = (float)r.CutOffBalance, 
                        CutOffDate = (DateTime?)r.CutOffDate,
                        //ExpiredAmount = (float)r.ExpiredAmount,
                        //ExpiredBill = (float)r.ExpiredBill,
                        Id = (int)r.Id,
                        //LastBalance = (float)r.LastBalance,
                        //LastPayment = (float)r.LastPayment,
                        //LastPaymentDate = (DateTime)r.LastPaymentDate,
                        Limit = (float)r.Limit,
                        //MinimumPayment = (float)r.MinimumPayment,
                        Number = r.Number,
                        OwnerId = (int)r.OwnerId,
                        Status = (Status)r.StatusId
                    });
                }
            }
            return cards;
        }
        public Card GetById(int id)
        {
            Card card = null;
            var rows = db.getcards(id,null,null).ToList();
            if (rows.Count() > 0)
            {

                var r = rows.FirstOrDefault();
                card = new Card
                {
                    //Available = r.Available,
                    Balance = (float?)r.Balance,
                    CreationDate = (DateTime)r.CreationDate,
                    CurrencyTypes = (CurrencyType)r.CurrencyTypes,
                    //CutOffBalance = (float)r.CutOffBalance, 
                    CutOffDate = (DateTime?)r.CutOffDate,
                    //ExpiredAmount = (float)r.ExpiredAmount,
                    //ExpiredBill = (float)r.ExpiredBill,
                    Id = (int)r.Id,
                    //LastBalance = (float)r.LastBalance,
                    //LastPayment = (float)r.LastPayment,
                    //LastPaymentDate = (DateTime)r.LastPaymentDate,
                    Limit = (float)r.Limit,
                    //MinimumPayment = (float)r.MinimumPayment,
                    Number = r.Number,
                    OwnerId = (int)r.OwnerId,
                    Status = (Status)r.StatusId
                };
            }
            return card;
        }
        public List<Card> GetByClient(int clientId)
        {
            List<Card> cards = null;
            var rows = db.getcards(null, clientId, null).ToList();
            if (rows.Any())
            {
                cards = new List<Card>();
                foreach (var r in rows)
                {
                    cards.Add(new Card
                    {
                        //Available = r.Available,
                        Balance = (float?)r.Balance,
                        CreationDate = (DateTime)r.CreationDate,
                        CurrencyTypes = (CurrencyType)r.CurrencyTypes,
                        //CutOffBalance = (float)r.CutOffBalance, 
                        CutOffDate = (DateTime?)r.CutOffDate,
                        //ExpiredAmount = (float)r.ExpiredAmount,
                        //ExpiredBill = (float)r.ExpiredBill,
                        Id = (int)r.Id,
                        //LastBalance = (float)r.LastBalance,
                        //LastPayment = (float)r.LastPayment,
                        //LastPaymentDate = (DateTime)r.LastPaymentDate,
                        Limit = (float)r.Limit,
                        //MinimumPayment = (float)r.MinimumPayment,
                        Number = r.Number,
                        OwnerId = (int)r.OwnerId,
                        Status = (Status)r.StatusId
                    });
                }
            }
            return cards;
        }
        public Card GetByNumber(string number)
        {
            Card card = null;
            var rows = db.getcards(null,null, number).ToList();
            if (rows.Count() > 0)
            {
                var r = rows.FirstOrDefault();
                card = new Card
                {
                    //Available = r.Available,
                    Balance = (float?)r.Balance,
                    CreationDate = (DateTime)r.CreationDate,
                    CurrencyTypes = (CurrencyType)r.CurrencyTypes,
                    //CutOffBalance = (float)r.CutOffBalance, 
                    CutOffDate = (DateTime?)r.CutOffDate,
                    //ExpiredAmount = (float)r.ExpiredAmount,
                    //ExpiredBill = (float)r.ExpiredBill,
                    Id = (int)r.Id,
                    //LastBalance = (float)r.LastBalance,
                    //LastPayment = (float)r.LastPayment,
                    //LastPaymentDate = (DateTime)r.LastPaymentDate,
                    Limit = (float)r.Limit,
                    //MinimumPayment = (float)r.MinimumPayment,
                    Number = r.Number,
                    OwnerId = (int)r.OwnerId,
                    Status = (Status)r.StatusId
                };
            }
            return card;
        }
    }
}