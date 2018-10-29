using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone
{
    /// <summary>
    /// Creates Our Main Class of Vending Machine
    /// </summary>
    public class VendingMachine
    {

        #region Properties
        /// <summary>
        /// Sets value to enum (OneDollar = 1, TwoDollar = 2, FiveDollar = 3, TenDollar = 4)
        /// </summary>
        public enum MoneySelection
        {
            OneDollar = 1,
            TwoDollar = 2,
            FiveDollar = 3,
            TenDollar = 4
        }

        /// <summary>
        /// dictionary that stores all the items in the vending machine
        /// </summary>
        private Dictionary<string, Item> Inventory { get; set; }

        /// <summary>
        /// dictionary meant to clone to add extra protection to product
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Item> GetInventory()
        {
            Dictionary<string, Item> result = new Dictionary<string, Item>();
            foreach(var item in Inventory)
            {
                result.Add(item.Key, item.Value.Clone());
            }
            return result;
        }

        /// <summary>
        /// Current Money in Vending Machine
        /// </summary>
        public double Money { get; private set; }

        #endregion

        #region Constructors

        
        /// <summary>
        /// Constructor that is called when the project is initialized
        /// </summary>
        public VendingMachine()
        {
            try
            {
                LoadInventory();
            }
            catch(Exception)
            {
                throw new InventoryException();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method called by subclass when item is purchased from vending machine
        /// </summary>
        /// <param name="slot"></param>
        public void PurchaseItem(string slot)
        {
            //needed to be in the class in order to update Amount Left
            Inventory[slot].AmountLeft = Inventory[slot].AmountLeft - 1;
        }

        //method that is called from the constructor at initialization...
    
        /// <summary>
        /// Makes the dictionary holding all the items in the vending machine
        /// </summary>
        private void LoadInventory()
        {
            Inventory = new Dictionary<string, Item>();

            //pulls in all the items in the vending machine from vendingmachine.csv
            string fullPath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\etc\vendingmachine.csv");
            using (StreamReader sr = new StreamReader(fullPath))
            {
                //takes in every line from vendingmachine.csv
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    //splits every line at '|'
                    string[] productArray = line.Split('|');

                    //if the item type is candy, add a candy object to the dictionary
                    if (productArray[3] == "Candy")
                    {
                        Inventory.Add(productArray[0], new Candy(productArray[1], double.Parse(productArray[2])));
                    }
                    //if the item type is chip, add a chip object to the dictionary
                    else if (productArray[3] == "Chip")
                    {
                        Inventory.Add(productArray[0], new Chip(productArray[1], double.Parse(productArray[2])));
                    }
                    //if the item is gum, add a gum object to the dictionary
                    else if (productArray[3] == "Gum")
                    {
                        Inventory.Add(productArray[0], new Gum(productArray[1], double.Parse(productArray[2])));
                    }
                    //if the item is drink, add a drink object to the dictionary
                    else if (productArray[3] == "Drink")
                    {
                        Inventory.Add(productArray[0], new Drink(productArray[1], double.Parse(productArray[2])));
                    }
                }
            }
        }

        /// <summary>
        /// Recieves in a char object from a menu to enter money into the machine
        /// </summary>
        /// <param name="input"></param>
        public void AddMoney(MoneySelection input)
        {   
            //adds $1 to the vending machine money if the user selects 1
            if (input == MoneySelection.OneDollar)
            {
                Money += 1;
                WriteToLog("FEED MONEY ($1.00):", Money - 1, Money);
            }
            //adds $2 to the vending machine money if the user selects 2
            else if (input == MoneySelection.TwoDollar)
            {
                Money += 2;
                WriteToLog("FEED MONEY ($2.00):", Money - 2, Money);
            }
            //adds $5 to the vending machine money if the user selects 3
            else if (input == MoneySelection.FiveDollar)
            {
                Money += 5;
                WriteToLog("FEED MONEY ($5.00):", Money - 5, Money);
            }
            //adds $10 to the vending machine money if the user selects 4
            //a check was done to make sure the input is either 1, 2, 3, or 4
            //before calling method
            else
            {
                Money += 10;
                WriteToLog("FEED MONEY ($10.00):", Money - 10, Money);
            }

        }
        /// <summary>
        /// Purchases a product from the vending machine
        /// </summary>
        /// <param name="selection"></param>
        public void PurchaseProduct(string selection)
        {
            //ensures that the item is available for purchase
            if (Inventory.ContainsKey(selection))
            {
                //ensures that there is enough money in machine & there is still items left
                if (Inventory[selection].AmountLeft > 0 && Money >= Inventory[selection].Price)
                {
                    //calls the purchase item in the baseclass and returns it to the item dictionary
                    PurchaseItem(selection);
                    //subtracts the price of the item from the money in the machine
                    Money = Money - Inventory[selection].Price;
                    //calls the write to log method that tracks each transaction
                    WriteToLog($"{Inventory[selection].ProductName} {selection}", Money + Inventory[selection].Price, Money);
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new Exception();
            }
        }
        /// <summary>
        /// Dispenses the money to the user and returns a string of the quarters dimes and nickels returned
        /// </summary>
        public string DispenseMoney()
        {
            //initialized the variables that will be returned
            string result = "";
            int quarters = 0;
            int dimes = 0;
            int nickels = 0;
            //keeps ths previous money in order to pass to the log file
            double previousMoney = Money;
            //increases money by a penny in order to get around the double floating point problem
            Money = Money + .01;

            //gets the amount of quarters dimes and nickels to return to the user
            if(Money >= 0.25)
            {
                quarters = (int)(Money / 0.25);
                Money = Money - (quarters * 0.25);
            }
            if(Money >= 0.10)
            {
                dimes = (int)(Money / 0.10);
                Money = Money - (dimes * 0.10);
            }
            if(Money >= 0.05)
            {
                nickels = (int)(Money / 0.05);
                Money = Money - (nickels * 0.05);
            }
            
            //sets money to 0 because all the money was returned
            Money = 0;

            //puts together string to return
            result = $"You received {quarters} quarter(s), {dimes} dime(s) and {nickels} nickel(s) in change";

            //calls the write to log method
            WriteToLog("GIVE CHANGE:", previousMoney, Money);
            
            //generates the sales report
            WriteSalesReport();
            return result;
        }

        #endregion

        #region Write to Logs Methods

        /// <summary>
        /// Writes & updates the log
        /// </summary>
        /// <param name="action"></param>
        /// <param name="previousBalance"></param>
        /// <param name="currentBalance"></param>
        public void WriteToLog(string action, double previousBalance, double currentBalance)
        {
            DateTime date = DateTime.Now;
            string todaysDate = date.ToShortDateString();
            string time = date.ToLongTimeString();
            
            //writes to the the Log file
            using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + @"\Log.txt", true))
            {
                sw.WriteLine($"{todaysDate} {time } {action.PadLeft(25)}{previousBalance.ToString("C").PadLeft(13)}{currentBalance.ToString("C").PadLeft(13)}");
            }
        }

        /// <summary>
        /// Generates a sales report / overwrites & updates one if there is one already there
        /// </summary>
        public void WriteSalesReport()
        {
            Dictionary<string, int> removedProducts = new Dictionary<string, int>();
            double totals = 0;
            double addInTotal = 0;
            double finalTotal = 0;

            //creates a dictionary to store the values that will output to the sales report
            Dictionary<string, int> salesdict = new Dictionary<string, int>();

            //if the file doesnt exist, it creates a new file
            if (!File.Exists(Environment.CurrentDirectory + @"\SalesReport.txt"))
            {
                using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + @"\SalesReport.txt", true))
                {
                    foreach (var button in Inventory.Values)
                    {
                        //goes through the dictionary and writes it to the report
                        sw.WriteLine(button.ProductName + "|" + button.AmountSold);

                        // += the totals to get the total sales
                        totals += button.AmountSold * button.Price;
                    }

                    //writes the total sales at the end of the report
                    sw.WriteLine("**TOTAL SALES**" + totals.ToString("C"));
                }
            }

            //does this when there is already a file
            //updates the file with new numbers
            else
            {
                using (StreamReader sr = new StreamReader(Environment.CurrentDirectory + @"\SalesReport.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        //pulls in each line from the sales report and splits it into an array
                        string line = sr.ReadLine();
                        string[] salesSoFar = line.Split('|', '$');

                        
                        foreach (var button in Inventory.Values)
                        {
                            //searches through each item in the dictionary to match it with the line read in
                            if (salesSoFar[0] == button.ProductName)
                            {
                                //when found, the Total Sales amount increases 
                                totals += button.AmountSold * button.Price;
                                //updates the report of total items sold
                                int finalSold = button.AmountSold + int.Parse(salesSoFar[1]);
                                //puts the product and total sold into a dictionary (can override values)
                                salesdict[salesSoFar[0]] = finalSold;
                            }
                            
                        }
                        if(!salesdict.ContainsKey(salesSoFar[0]) && salesSoFar[0] != "**TOTAL SALES**")
                        {
                            removedProducts[salesSoFar[0]] = int.Parse(salesSoFar[1]);
                        }
                        //adds the total sales to the totals for this time
                        if (salesSoFar[0] == "**TOTAL SALES**")
                        {
                            finalTotal = double.Parse(salesSoFar[1]) + totals;
                        }
                        
                    }

                }


                //goes through the created dictionary and overwrites each item in the sales report
                using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + @"\SalesReport.txt", false))
                {
                    foreach (KeyValuePair<string, int> nums in salesdict)
                    {
                        sw.WriteLine(nums.Key + "|" + nums.Value);
                    }
                    foreach (var button in Inventory.Values)
                    {
                        if(salesdict.Keys.Contains(button.ProductName))
                        {}
                        else
                        {
                            //goes through the dictionary and writes it to the report
                            sw.WriteLine(button.ProductName + "|" + button.AmountSold);

                            // += the totals to get the total sales
                            addInTotal += button.AmountSold * button.Price;
                            
                        }
                    }
                    
                foreach(KeyValuePair<string,int> gone in removedProducts)
                    {
                        sw.WriteLine(gone.Key + "|" + gone.Value);
                    }
                 sw.WriteLine("**TOTAL SALES**" + (addInTotal + finalTotal).ToString("C"));

                }
            }
         }
            
     }
    #endregion
}


