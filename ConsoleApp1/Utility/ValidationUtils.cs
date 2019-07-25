using System;
using System.Collections.Generic;
using System.Text;

namespace BankLedgerConsole.Utility
{
    static class ValidationUtils
    {
        public delegate bool Validator(string input);

        public static string ValidateInput(Validator Predicate)
        {
            var response = Console.ReadLine();

            while (!Predicate(response))
            {
                Console.WriteLine("== Invalid Input ==");
                response = Console.ReadLine();
            }

            return response;
        }

        public static bool IsPositiveDecimalValue(string input)
        {
            decimal amount = 0m;

            try
            {
                amount = Decimal.Parse(input);
            }
            catch (Exception e)
            {
                return false;
            }

            if (amount > 0m)
            {
                return true;
            }
            return false;
        }
    }
}
