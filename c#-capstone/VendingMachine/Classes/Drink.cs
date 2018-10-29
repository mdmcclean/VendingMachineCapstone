using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    /// <summary>
    /// Object of class is made when an item in vending machine has type drink
    /// </summary>
    public class Drink : Item
    {
        /// <summary>
        /// Assigns Type to Drink
        /// </summary>
        public override string Type { get; } = "Drink";

        /// <summary>
        /// Creates object & passes items into VMItems
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        public Drink(string name, double price) : base(name, price)
        {

        }
        /// <summary>
        /// Method that is overridden from vmitems
        /// </summary>
        /// <returns></returns>
        public override string ReturnSound()
        {
            return "Glug Glug, Yum!";
        }
    }
}
