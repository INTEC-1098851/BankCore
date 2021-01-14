using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBll
{
    public class EmployeeUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public Role Role { get; set; }
        public int RoleId { get { return (int)Role; } }
        public Status Status { get; set; }
        public int StatusId { get { return (int)Status; } }

    }
}
