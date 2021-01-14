using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoreBll;
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
                    db.InsertOrUpdateCard(card.Id, card.OwnerId, card.Number, card.CutOffDate, card.PaymentLimitDate,
                        (int)Status.Active, card.Limit, card.CurrencyTypesId, card.Balance,null);
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
                    db.InsertOrUpdateCard(card.Id, card.OwnerId, card.Number, card.CutOffDate, card.PaymentLimitDate,
                    (int)Status.Active, card.Limit, card.CurrencyTypesId, card.Balance, null);
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
            List<Card> cards = new List<Card>();
            var rows = db.getcards(null, null, null);
            if (rows.Count() > 0)
            {
                foreach (var r in rows)
                {
                    cards.Add(new Card
                    {
                        //Available = r.Available,
                        Balance = (float)r.Balance,
                        CreationDate = (DateTime)r.CreationDate,
                        CurrencyTypes = (CurrencyType)r.CurrencyTypes,
                        //CutOffBalance = (float)r.CutOffBalance, 
                        CutOffDate = (DateTime)r.CutOffDate,
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
            Card card = new Card();
            var rows = db.getcards(id,null,null);
            if (rows.Count() == 1)
            {
                var r = rows.FirstOrDefault();
                card = new Card
                {
                    //Available = r.Available,
                    Balance = (float)r.Balance,
                    CreationDate = (DateTime)r.CreationDate,
                    CurrencyTypes = (CurrencyType)r.CurrencyTypes,
                    //CutOffBalance = (float)r.CutOffBalance, 
                    CutOffDate = (DateTime)r.CutOffDate,
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
            List<Card> cards = new List<Card>();
            var rows = db.getcards(null, clientId, null);
            if (rows.Count() > 0)
            {
                foreach (var r in rows)
                {
                    cards.Add(new Card
                    {
                        //Available = r.Available,
                        Balance = (float)r.Balance,
                        CreationDate = (DateTime)r.CreationDate,
                        CurrencyTypes = (CurrencyType)r.CurrencyTypes,
                        //CutOffBalance = (float)r.CutOffBalance, 
                        CutOffDate = (DateTime)r.CutOffDate,
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
            Card card = new Card();
            var rows = db.getcards(null,null, number);
            if (rows.Count() == 1)
            {
                var r = rows.FirstOrDefault();
                card = new Card
                {
                    //Available = r.Available,
                    Balance = (float)r.Balance,
                    CreationDate = (DateTime)r.CreationDate,
                    CurrencyTypes = (CurrencyType)r.CurrencyTypes,
                    //CutOffBalance = (float)r.CutOffBalance, 
                    CutOffDate = (DateTime)r.CutOffDate,
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