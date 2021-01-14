using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBll
{
    public class Employee
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public int StatusId { get { return (int)Status; } }

    }
}
