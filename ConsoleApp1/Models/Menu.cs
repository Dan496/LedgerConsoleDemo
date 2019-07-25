using System;
using System.Collections.Generic;
using System.Text;

namespace BankLedgerConsole.Models
{
    // Could convert to a Composite if a more complex menu was required.
    public class MenuOption
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public Action Select { get; set; }
    }
    // Handle Menu Loop and nested menus.
    public class Menu
    {
        // When matched, exit the menu (or the app if the root).
        public string Terminal { get; set; }
        public string Description { get; set; }
        public List<MenuOption> Options { get; set; }
        public void Prompt()
        {
            string selection = "";

            Console.Clear();
            while (selection != Terminal)
            {
                Console.WriteLine(Description);
                Options.ForEach(o => Console.WriteLine($"{o.Key}) {o.Name}"));
                Console.WriteLine($"{Terminal}) Exit");

                selection = Console.ReadLine();

                MenuOption selectedOption = Options.Find(o => o.Key.Equals(selection));
                if (selectedOption != null)
                {
                    selectedOption.Select();
                }
                
                if (selectedOption == null && selection != Terminal)
                {
                    Console.WriteLine("== Invalid Selection ==");
                }
            } 
        }
    }
}
