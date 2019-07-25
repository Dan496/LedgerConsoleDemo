using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        public delegate bool Validator(string input);

        public static string ValidateInput(Validator Predicate)
        {
            var response = Console.ReadLine();

            while(!Predicate(response))
            {
                Console.WriteLine("== Invalid Input ==");
                response = Console.ReadLine();
            }

            return response;
        }


        static void Main(string[] args)
        {
            // App Context
            User activeUser = null;
            UserRepository userRepository = new UserRepository();

            Menu TransactionMenu = new Menu
            {
                Description = "View, Add, And Summarize Transactions",
                Terminal = "Q",
                Options = new List<MenuOption>
                {
                    new MenuOption
                    {
                        Key = "1",
                        Name = "Deposit",
                        Select = () =>
                        {
                            string response = "";
                            decimal amount = 0m;

                            do
                            {
                                Console.WriteLine("Amount: ");
                                response = Console.ReadLine();

                                try
                                {
                                    amount = Decimal.Parse(response);
                                }
                                catch(Exception e)
                                {
                                    amount = 0m;
                                }
                                if (amount <= 0m)
                                {
                                    Console.WriteLine("== Invalid Argument ==");
                                }
                            } while (amount <= 0m);

                            do
                            {
                                Console.WriteLine("Reason: ");
                                response = Console.ReadLine();

                                if (response.Length <= 0)
                                {
                                    Console.WriteLine("== Invalid Argument ==");
                                }

                            } while (response.Length <= 0);

                            activeUser.AddTransaction(new Transaction
                            {
                                Timestamp = DateTime.Now,
                                Reason = response,
                                Amount = amount,
                                TransactionType = TransactionType.Deposit
                            });
                        }
                    },
                    new MenuOption
                    {
                        Key = "2",
                        Name = "Withdraw",
                        Select = () =>
                        {
                            string response = "";
                            decimal amount = 0m;

                            do
                            {
                                Console.WriteLine("Amount: ");
                                response = Console.ReadLine();

                                try
                                {
                                    amount = Decimal.Parse(response);
                                }
                                catch(Exception e)
                                {
                                    amount = 0m;
                                }
                                if (amount <= 0m)
                                {
                                    Console.WriteLine("== Invalid Argument ==");
                                }
                            } while (amount <= 0m);

                            do
                            {
                                Console.WriteLine("Reason: ");
                                response = Console.ReadLine();

                                if (response.Length <= 0)
                                {
                                    Console.WriteLine("== Invalid Argument ==");
                                }

                            } while (response.Length <= 0);

                            try
                            {
                                activeUser.AddTransaction(new Transaction
                                {
                                    Timestamp = DateTime.Now,
                                    Reason = response,
                                    Amount = amount,
                                    TransactionType = TransactionType.Withdrawl
                                });
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine("==Transaction Rolled Back To Prevent Overdraw==");
                            }

                        }
                    },
                    new MenuOption
                    {
                        Key = "3",
                        Name = "Transaction History",
                        Select = () =>
                        {
                            activeUser.Transactions.OrderBy(u => u.Timestamp)
                                                   .ToList()
                                                   .ForEach(u => Console.WriteLine($"{u.Timestamp} - {u.TransactionType.ToString()} - {u.Reason} - {u.Amount}"));

                            Console.WriteLine("Press Enter To Continue");
                            Console.ReadLine();
                        }

                    },
                    new MenuOption
                    {
                        Key = "4",
                        Name = "Balance",
                        Select = () =>
                        {
                            Console.WriteLine("Balance: ", activeUser.Balance);
                            Console.WriteLine("Press Enter To Continue");
                            Console.ReadLine();
                        }
                    }
                }
            };

            Menu MainMenu = new Menu
            {
                Description = "Please Login or Register to Continue.",
                Terminal = "Q",
                Options = new List<MenuOption>
                {
                    new MenuOption
                    {
                        Key = "1",
                        Name = "Create New User",
                        Select = () =>
                        {
                            Console.WriteLine("Username: ");
                            string username = ValidateInput(s => userRepository.Get(s) == null);
                            activeUser = userRepository.Add(username);

                            TransactionMenu.Prompt();
                        }
                    },
                    new MenuOption
                    {
                        Key = "2",
                        Name = "Login",
                        Select = () =>
                        {
                            Console.WriteLine("Username: ");
                            string username = ValidateInput(s => userRepository.Get(s) != null);
                            activeUser = userRepository.Get(username);

                            TransactionMenu.Prompt();
                        }
                    }
                }
            };

            MainMenu.Prompt();
        }
    }
}
