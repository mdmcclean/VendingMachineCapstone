using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void VendingMachineTest()
        {
            VendingMachine vm = new VendingMachine();
            //itemDict Key + Product name works
            var inventory = vm.GetInventory();
            Assert.AreEqual("Potato Crisps", inventory["A1"].ProductName, "Should return Potato Crisps");
            Assert.AreEqual(1.80, inventory["B1"].Price, "Price of moonpies should be 1.80");
            Assert.AreEqual("Gum", inventory["D2"].Type, "Gum is the type of Little League Chew");

            //Add Money 1 checked
            vm.AddMoney(VendingMachine.MoneySelection.OneDollar);
            Assert.AreEqual(1, vm.Money, "Only one dollar was added");

            //Add Money 2 on top of Money 1 checked
            vm.AddMoney(VendingMachine.MoneySelection.TwoDollar);
            Assert.AreEqual(3, vm.Money, "2 more dollars was added");
        }
        [TestMethod]
        public void VendingMachineTestMoney2()
        {
            //Add Money 2 checked
            VendingMachine vm = new VendingMachine();
            vm.AddMoney(VendingMachine.MoneySelection.TwoDollar);
            Assert.AreEqual(2, vm.Money, "2 more dollars was added");
        }
        [TestMethod]
        public void VendingMachineTestMoney5()
        {
            //Add Money 5 checked
            VendingMachine vm = new VendingMachine();
            vm.AddMoney(VendingMachine.MoneySelection.FiveDollar);
            Assert.AreEqual(5, vm.Money, "5 more dollars was added");
        }
        [TestMethod]
        public void VendingMachineTestMoney10()
        {
            //Add Money 10 checked
            VendingMachine vm = new VendingMachine();
            vm.AddMoney(VendingMachine.MoneySelection.TenDollar);
            Assert.AreEqual(10, vm.Money, "10 more dollars was added");
        }
    }
}
