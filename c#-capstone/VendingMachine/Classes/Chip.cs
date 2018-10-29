using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    /// <summary>
    /// Object of class is made when an item in vending machine has type chip
    /// </summary>
    public class Chip : Item
    {
        /// <summary>
        /// Assigns Type to Chip
        /// </summary>
        public override string Type { get; } = "Chip";

        /// <summary>
        /// Creates object & passes items into VMItem
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        public Chip(string name, double price) : base(name, price)
        {

        }
        
        /// <summary>
        /// Method that is overridden from vmitems
        /// </summary>
        /// <returns></returns>
        public override string ReturnSound()
        {
            return "Crunch Crunch, Yum!";
        }
    }
}
