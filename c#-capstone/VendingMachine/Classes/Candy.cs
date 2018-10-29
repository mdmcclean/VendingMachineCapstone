using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    /// <summary>
    /// object of class is made when an item in vending machine has type candy
    /// </summary>
    public class Candy : Item
    {
        /// <summary>
        /// Assigns Type to Candy
        /// </summary>
        public override string Type { get; } = "Candy";

        /// <summary>
        /// Creates object & passes items into VMItems
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        public Candy(string name, double price) : base(name, price)
        {

        }
        
        /// <summary>
        /// Method that is overridden from vmitems
        /// </summary>
        /// <returns></returns>
        public override string ReturnSound()
        {
            return "Munch Munch, Yum!";
        }
    }
}
