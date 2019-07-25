using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Utility
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
    }
}
