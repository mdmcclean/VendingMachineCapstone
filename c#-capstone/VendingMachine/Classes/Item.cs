using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{

    /// <summary>
    /// Different objects contained in the Vending Machine
    /// </summary>
    public abstract class Item
    {

        #region Properties

        //the properties are the properties of the items in the vending machine
        //all need to be initialized when it goes into the dictionary
        /// <summary>
        /// Title of Item read in
        /// </summary>
        public string ProductName { get; }
        /// <summary>
        /// Cost of Item
        /// </summary>
        public double Price { get; }
        /// <summary>
        /// Different Type of item (ex: Candy, Chip, Drink, Gum)
        /// </summary>
        public abstract string Type { get; }
        /// <summary>
        /// Amount of Item Remaining
        /// </summary>
        public int AmountLeft {get; set;}
        /// <summary>
        /// Amount of Item Sold
        /// </summary>
        public int AmountSold {
            get
            {
                return 5 - AmountLeft;
            }
        }
        public string DisplayQty
        {
            get
            {
                if(AmountLeft == 0)
                {
                    return "SOLD OUT";
                }
                else
                {
                    return AmountLeft.ToString();
                }
            }
        }

        #endregion

        #region Constructors

        
        /// <summary>
        /// Constructor to be called from sub sub class when an item goes into the dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        public Item(string name, double price)
        {
            ProductName = name;
            Price = price;
            AmountLeft = 5;
        }
        
        /// <summary>
        /// Constructor to be called by subclass when project is initialized
        /// </summary>
        public Item() { }

        #endregion

        #region Methods
        
        public Item Clone()
        {
            Item result = null;
            if(this is Gum)
            {
                result = new Gum(ProductName, Price);
                result.AmountLeft = AmountLeft;
            }
            else if (this is Drink)
            {
                result = new Drink(ProductName, Price);
                result.AmountLeft = AmountLeft;
            }
            else if (this is Candy)
            {
                result = new Candy(ProductName, Price);
                result.AmountLeft = AmountLeft;
            }
            else if (this is Chip)
            {
                result = new Chip(ProductName, Price);
                result.AmountLeft = AmountLeft;
            }
            return result;
        }

        /// <summary>
        /// Method is overriden by subclass
        /// </summary>
        /// <returns></returns>
        public abstract string ReturnSound();

        #endregion
    }
}
