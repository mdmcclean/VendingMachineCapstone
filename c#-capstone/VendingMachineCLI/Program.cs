using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                VendingMenus menu = new VendingMenus(new VendingMachine());
                menu.MainMenu();
            }
            catch(InventoryException)
            {
                Console.WriteLine("Inventory file is corrupt.");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ReadKey();
            }
        }
    }
}
