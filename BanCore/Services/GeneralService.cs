using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoreBll;
namespace BanCore.Services
{
    public class GeneralService
    {
        private static CoreDbEntities db = new CoreDbEntities();
        #region Bank
        public List<Bank> GetAll()
        {
            List<Bank> banks = new List<Bank>();
            var rows = db.getbanks(null, null);
            if (rows.Count() > 0)
            {
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
        public Bank GetById(int id)
        {
            Bank bank = new Bank();
            var rows = db.getbanks(id, null);
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

        #endregion
    }
}