using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBll
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{Name} {LastName}"; } }
        public IdentificationType IdentificationType { get; set; }
        public int IdentificationTypeId { get { return (int)IdentificationType; } }
        public string Identification { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public int GenderId { get { return (int)Gender; } }
        public CurrencyType CurrencyType { get; set; }
        public int CurrencyTypeId { get { return (int)CurrencyType; } }
        public Status Status { get; set; }
        public int StatusId { get { return (int)Status; } }

    }
}
