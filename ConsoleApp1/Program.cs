using BankLedgerConsole.Models;
using BankLedgerConsole.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankLedgerConsole
{
    class Program
    {
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
                            Console.WriteLine("Amount: ");
                            string response = ValidationUtils.ValidateInput(ValidationUtils.IsPositiveDecimalValue);
                            decimal amount = Decimal.Parse(response);

                            Console.WriteLine("Reason: ");
                            response = ValidationUtils.ValidateInput(i => i.Length > 0);

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
                            Console.WriteLine("Amount: ");
                            string response = ValidationUtils.ValidateInput(ValidationUtils.IsPositiveDecimalValue);
                            decimal amount = Decimal.Parse(response);

                            Console.WriteLine("Reason: ");
                            response = ValidationUtils.ValidateInput(i => i.Length > 0);

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
                                                   .ForEach(u => Console.WriteLine($"{u.Timestamp} - {u.TransactionType.ToString()} - {u.Reason} - {u.Amount:C}"));
                        }

                    },
                    new MenuOption
                    {
                        Key = "4",
                        Name = "Balance",
                        Select = () =>
                        {
                            Console.WriteLine($"Balance: {activeUser.Balance:C}");
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
                            Console.WriteLine("Username ('Q' To Exit): ");
                            string response = ValidationUtils.ValidateInput(s => userRepository.Get(s) == null || s == "Q");

                            if (response != "Q")
                            {
                                activeUser = userRepository.Add(response);
                                TransactionMenu.Prompt();
                            }
                        }
                    },
                    new MenuOption
                    {
                        Key = "2",
                        Name = "Login",
                        Select = () =>
                        {
                            Console.WriteLine("Username ('Q' To Exit): ");
                            string response = ValidationUtils.ValidateInput(s => userRepository.Get(s) != null || s == "Q");

                            if (response != "Q")
                            {
                                activeUser = userRepository.Get(response);
                                TransactionMenu.Prompt();
                            }
                        }
                    }
                }
            };

            MainMenu.Prompt();
        }
    }
}
