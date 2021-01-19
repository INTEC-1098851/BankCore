using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBll
{
    public static class BankAccountRandomGenerator
    {
        public static string GenerateRandomBankAccount()
        {
            String startWith = "32";
            Random generator = new Random();
            String r = generator.Next(0, 999999).ToString("D6");
            String aAccounNumber = startWith + r;
            return aAccounNumber;
        }
    }
}
