using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;
using VendingMachine.Model;
using VendingMachine.Change;

namespace VendingMachineTests.VendingMachine
{
  [TestClass]
  public class BuyTests
  {
    [TestInitialize()]
    public void Initialize()
    {
      changeAlgorithm = new ChangeAlgorithm(); 
    }

    [TestMethod]
    public void Buy_NoChangeRequired()
    {
      IVendingMachine vm = SampleVendingMachine_FullFloat();

      //23 x 10cents
      Money tendered = new Money();
      tendered.Add(DenominationEnum.TenCents, 23);

      decimal expectedFloatAfterSale = vm.getFloat().Total + fanta.Price;

      ProductAndChange pac = vm.buy(fanta.Name, tendered);

      Assert.IsNotNull(pac);
      Assert.AreEqual(ResultEnum.Ok, pac.Result, "Result incorrect. Product expected.");
      Assert.IsTrue(String.IsNullOrEmpty(pac.Message), "Message incorrect. No message expected.");
      Assert.AreEqual(fanta, pac.Product, "Product incorrect. Fanta expected.");
      Assert.AreEqual(0.0M, pac.Change.Total, "Change incorrect.");
      Assert.AreEqual(0, vm.countProduct(fanta.Name),"Product quantity incorrect. Product quantity not decremented after sale.");
      Assert.AreEqual(expectedFloatAfterSale, vm.getFloat().Total, "Float incorrect. The float after the sale is incorrect.");
    }

    [TestMethod]
    public void Buy_ChangeRequired()
    {
      IVendingMachine vm = SampleVendingMachine_FullFloat();

      Money tendered = new Money();
      tendered.Add(DenominationEnum.TwoEuro, 2);

      decimal expectedFloatAfterSale = vm.getFloat().Total + fanta.Price;
      decimal expectedChange = tendered.Total - fanta.Price;

      ProductAndChange pac = vm.buy(fanta.Name, tendered);

      Assert.IsNotNull(pac);
      Assert.AreEqual(ResultEnum.Ok, pac.Result, "Result incorrect. Product expected.");
      Assert.IsTrue(String.IsNullOrEmpty(pac.Message), "Message incorrect. No message expected.");
      Assert.AreEqual(fanta, pac.Product, "Product incorrect. Fanta expected.");
      Assert.AreEqual(expectedChange, pac.Change.Total, "Change incorrect.");
      Assert.AreEqual(0, vm.countProduct(fanta.Name), "Product quantity not decremented after sale.");
      Assert.AreEqual(expectedFloatAfterSale, vm.getFloat().Total, "The money in the machine after the sale is incorrect.");
    }

    [TestMethod]
    public void Buy_CannotGiveChange()
    {
      IVendingMachine vm = SampleVendingMachine_LimitedFloat();

      Money tendered = new Money();
      tendered.Add(DenominationEnum.TwoEuro, 2);

      decimal expectedFloatAfterSale = vm.getFloat().Total;
      decimal expectedChange = tendered.Total - fanta.Price;

      ProductAndChange pac = vm.buy(fanta.Name, tendered);

      Assert.IsNotNull(pac);
      Assert.AreEqual(ResultEnum.NoChange, pac.Result, "Result incorrect. Product expected.");
      Assert.IsFalse(String.IsNullOrEmpty(pac.Message), "Message incorrect. Message expected.");
      Assert.AreEqual(1, vm.countProduct(fanta.Name), "Product quantity not decremented after sale.");
      Assert.AreEqual(expectedFloatAfterSale, vm.getFloat().Total, "The money in the machine after the sale is incorrect.");
    }

    [TestMethod]
    public void Buy_NotEnoughMoney()
    {
      IVendingMachine vm = new TheVendingMachine(changeAlgorithm);

      Product selection = new Product("Fanta", 1.1M);
      vm.addProduct(selection, 1);
      Money tendered = new Money();
      tendered.Add(DenominationEnum.TenCents, 1);

      ProductAndChange pac = vm.buy(selection.Name, tendered);

      Assert.IsNotNull(pac);
      Assert.AreEqual(ResultEnum.NotEnoughMoney, pac.Result, "Result incorrect. Not enough money expected.");
      Assert.IsFalse(String.IsNullOrEmpty(pac.Message), "Message incorrect.");
    }

    [TestMethod]
    public void Buy_ProductNotAvailable()
    {
      IVendingMachine vm = new TheVendingMachine(changeAlgorithm);

      Product selection = new Product("Fanta", 1.1M);
      Money moneyIn = new Money();
      moneyIn.Add(DenominationEnum.TwoEuro, 1);

      ProductAndChange pac = vm.buy(selection.Name, moneyIn);

      Assert.IsNotNull(pac);
      Assert.AreEqual(ResultEnum.NoProduct, pac.Result, "Result incorrect. No product expected.");
      Assert.IsFalse(String.IsNullOrEmpty(pac.Message), "Message incorrect.");
    }

    /// <summary>
    /// Creates a sample vending machine with a float that has all the coins and a single product.
    /// </summary>
    /// <returns></returns>
    private IVendingMachine SampleVendingMachine_FullFloat()
    {
      IVendingMachine vm = new TheVendingMachine(changeAlgorithm);

      //fill the vending machine with products
      fanta = new Product("Fanta", 2.3M);
      vm.addProduct(fanta, 1);

      //fill the vending machine with change
      Money moneyFloat = new Money();
      moneyFloat.Add(DenominationEnum.TenCents, 100);
      moneyFloat.Add(DenominationEnum.TwentyCents, 100);
      moneyFloat.Add(DenominationEnum.FiftyCents, 100);
      moneyFloat.Add(DenominationEnum.OneEuro, 100);
      moneyFloat.Add(DenominationEnum.TwoEuro, 100);
      vm.addToFloat(moneyFloat);

      return vm;
    }

    /// <summary>
    /// Creates a sample vending machine with a float that has few coins and a single product.
    /// </summary>
    /// <returns></returns>
    private IVendingMachine SampleVendingMachine_LimitedFloat()
    {
      IVendingMachine vm = new TheVendingMachine(changeAlgorithm);

      //fill the vending machine with products
      fanta = new Product("Fanta", 2.3M);
      vm.addProduct(fanta, 1);

      //fill the vending machine with change
      Money moneyFloat = new Money();
      moneyFloat.Add(DenominationEnum.FiftyCents, 1);
      vm.addToFloat(moneyFloat);

      return vm;
    }

    private IChangeAlgorithm changeAlgorithm;
    private Product fanta;
  }
}
