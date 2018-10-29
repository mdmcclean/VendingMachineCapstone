using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class VendingMenus
    {
        private VendingMachine _vendingMachine = new VendingMachine();

        #region Constructor

        public VendingMenus(VendingMachine vm)
        {
            _vendingMachine = vm;
        }

        #endregion

        #region Methods

        #region Menus

        public void MainMenu()
        {
            bool exit = false;
            while(!exit)
            {
                try
                {
                    #region menu image
                    Console.Clear();
                    Console.WriteLine(" @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ " +
                    "\n @@            @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@                                 @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@                                 @@        @@@            @@ " +
                    "\n @@            @@@                 *@              @@   @@   @@@            @@ " +
                    "                          ___  ___                                             " +
                    "\n @@            @@@      &@@@@     @@@              @@   @@   @@@            @@ " +
                    "                          |  \\/  | " +
                    "\n @@            @@@      &@@@@    #@@@              @@   @@   @@@            @@ " +
                    "                          | .  . | ___ _ __  _   _  " +
                    "\n @@            @@@    @@@@@@@@@@@@@@@@@@@@@@@@@    @@        @@@            @@ " +
                    "                          | |\\/| |/ _ \\ '_ \\| | | | " +
                    "\n @@            @@@                                 @@@@@@@@@@@@@            @@ " +
                    "                          | |  | |  __/ | | | |_| |" +
                    "\n @@            @@@                                 @@@&    .@@@@            @@ " +
                    "                          \\_|  |_/\\___|_| |_|\\__,_|" +
                    "\n @@            @@@                     @@@(        @@@&    .@@@@            @@ " +
                    "\n @@            @@@                    @@@@@        @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@    @@@@@@@@@@@@@@@@@@@@@@@@@    @@@&    .@@@@            @@ " +
                    "\n @@            @@@                                 @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@              @@                 @@@@@@@@@@@@@            @@ " +
                    "                                  (1) Display Items " +
                    "\n @@            @@@      &@&     @@     @@@@        @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@      &@@     @@     @@@@        @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@    @@@@@@@@@@@@@@@@@@@@@@@@@    @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@                                 @@@@@@@@@@@@@            @@ " +
                    "                                  (2) Purchase                " +
                    "\n @@            @@@                                 @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@     @@@@@    @@@@@              @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@     @@@@@    @@@@@              @@@@@@@@@@@@@            @@ " +
                    "                                  (q) quit" +
                    "\n @@            @@@    @@@@@@@@@@@@@@@@@@@@@@@@@    @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@                                 @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@                                 @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@                                 @@@&....,@@@@            @@ " +
                    "\n @@            @@@                                 @@@&    .@@@@            @@ " +
                    "\n @@            @@@                                 @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@/////////////////////////////////@@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@                                 @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@   @@@@@@@@@@@@@@@@@@@@@@@@@@@   @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@   @@@@@@@@@@@@@@@@@@@@@@@@@@@   @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@                                 @@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@            @@ " +
                    "\n @@            @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@            @@ " +
                    "\n @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ ");
                    #endregion

                    //Console.Clear();
                    //Console.WriteLine("MAIN MENU");
                    //Console.WriteLine("_______________________");
                    //Console.WriteLine("(1) Display Items");
                    //Console.WriteLine("(2) Purchase");
                    //Console.WriteLine("(q) quit");

                    char selection = Console.ReadKey().KeyChar;
                    if (selection == 'q')
                    {
                        exit = true;
                    }
                    else if (selection == '1' || selection == '2')
                    {
                        if (selection == '1')
                        {
                            DisplayMenu();
                        }
                        if (selection == '2')
                        {

                            PurchaseMenu();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not a valid selection\nPress Enter to try again...");
                        Console.ReadKey();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.ReadKey();
                }
            }
        }

        public void DisplayItems()
        {
            Console.WriteLine("".PadLeft(52, '='));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{"SLOT".PadRight(5)}|{"PRODUCT NAME".PadRight(23)}|" +
                    $"{"PRICE".PadLeft(8)}|{"TYPE".PadLeft(8)}|{"QTY".PadLeft(3)}|");
            Console.ResetColor();
            var ourVending = _vendingMachine.GetInventory();
            foreach (KeyValuePair<string, Item> vmitem in ourVending)
            {
                Console.WriteLine("".PadLeft(52, '='));
                Console.WriteLine($"{vmitem.Key.PadRight(5)}|{vmitem.Value.ProductName.PadRight(23)}|"+
                    $"{vmitem.Value.Price.ToString("C").PadLeft(8)}|{vmitem.Value.Type.PadLeft(8)}|{vmitem.Value.DisplayQty.PadLeft(3)}|");
            }
            Console.WriteLine("".PadLeft(52, '='));
        }

        public void DisplayMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                DisplayItems();
                char selection;
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Wow everything looks so good you should go buy something");
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Press any key if you would like to go back");
                selection = Console.ReadKey().KeyChar;

                exit = true;
            }

        }

        public void PurchaseMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("PURCHASE MENU");
                Console.WriteLine("-------------");
                Console.WriteLine("(1) Feed Money");
                Console.WriteLine("(2) Select Product");
                Console.WriteLine("(3) Finish Transaction");
                Console.WriteLine($"Current Money Provided: {_vendingMachine.Money.ToString("C")}");
                Console.WriteLine("Press \'q\' if you would like to go back");
                char input = Console.ReadKey().KeyChar;
                if (input == '1' || input == '2' || input == '3')
                {
                    if (input == '1')
                    {
                        FeedMoney();
                    }
                    else if (input == '2')
                    {
                        SelectProduct(); ; 
                    }
                    else if(input == '3')
                    {
                        FinishTransaction(); 
                    }
                    
                }
                else if (input == 'q' || input == 'Q')
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("That was not an option... Please Try again");
                }
            }

        }

        #endregion

        #region User Input Methods

        public void FeedMoney()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("Enter the corresponding number to increase current money provided...");
                Console.WriteLine("1) $1\n2) $2 \n3) $5 \n4) $10");
                Console.WriteLine($"Current Money Provided: {_vendingMachine.Money.ToString("C")}");
                Console.WriteLine("Press \'q\' and enter if you would like to go back");
                char input = Console.ReadKey().KeyChar;

                if (input == '1' || input == '2' || input == '3' || input == '4')
                {
                    if(input == '1')
                    {
                        _vendingMachine.AddMoney(VendingMachine.MoneySelection.OneDollar);
                    }
                    if (input == '2')
                    {
                        _vendingMachine.AddMoney(VendingMachine.MoneySelection.TwoDollar);
                    }
                    if (input == '3')
                    {
                        _vendingMachine.AddMoney(VendingMachine.MoneySelection.FiveDollar);
                    }
                    if (input == '4')
                    {
                        _vendingMachine.AddMoney(VendingMachine.MoneySelection.TenDollar);
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nThank you. Current Money: {_vendingMachine.Money.ToString("C")}");
                    Console.Beep(200, 1000);
                    Console.ResetColor();
                }
                else if (input == 'q' || input == 'Q')
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("That was not an option... Please Try again");
                }

            }
        }

        public void SelectProduct()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                DisplayItems();

                string selection;
                Console.WriteLine($"Current Money Provided: {_vendingMachine.Money.ToString("C")}");
                Console.WriteLine("Enter Slot Number");
                Console.WriteLine("Press \'q\' and enter if you would like to go back");
                selection = Console.ReadLine().ToUpper();
                if (_vendingMachine.GetInventory().ContainsKey(selection))
                {
                    if (_vendingMachine.GetInventory()[selection].AmountLeft == 0)
                    {
                        Console.WriteLine($"{_vendingMachine.GetInventory()[selection].ProductName} is SOLD OUT");
                        Console.WriteLine("Press any key to make another selection");
                        Console.ReadKey();
                    }
                    else if(_vendingMachine.Money < _vendingMachine.GetInventory()[selection].Price)
                    { 
                    
                        Console.WriteLine($"INSUFFICIENT FUNDS \nfeed me more money");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                    }
                    else
                    {
                        _vendingMachine.PurchaseProduct(selection);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine(_vendingMachine.GetInventory()[selection].ReturnSound());
                        Console.Beep(10000, 2000);
                        Console.ResetColor();
                    }
                    
                }
                else if (selection == "Q")
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Not a valid selection...\nPress any key to make another selection");
                    Console.ReadKey();
                }
            }
        }

        public void FinishTransaction()
        {
            Console.Clear();
            Console.WriteLine($"Current Money Provided: {_vendingMachine.Money.ToString("C")}");
            Console.WriteLine(_vendingMachine.DispenseMoney());
            Console.WriteLine("Have a nice day... Press any key to conitinue");
            Console.ReadKey();
        }

        #endregion

        #endregion
    }
}
