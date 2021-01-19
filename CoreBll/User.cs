using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBll
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public Status Status { get; set; }
        public int StatusId { get { return (int)Status; } }

    }
}
