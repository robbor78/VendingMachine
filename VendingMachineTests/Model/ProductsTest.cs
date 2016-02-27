using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Model;

namespace VendingMachineTests.Model
{
  [TestClass]
  public class ProductsTest
  {
    [TestMethod]
    public void IsAvailableTest_Empty()
    {
      Products target = new Products();
      bool result = target.IsAvailable("Fanta");
      Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsAvailableTest_OutOfStock()
    {
      Products target = new Products();
      target.Add(new Product("Coca Cola", 1.0M), 100);
      bool result = target.IsAvailable("Fanta");
      Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsAvailableTest_SoldOut()
    {
      Products target = new Products();
      target.Add(new Product("Coca Cola", 1.0M), 100);
      target.Add(new Product("Fanta", 1.0M), 0); 
      bool result = target.IsAvailable("Fanta");
      Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsAvailableTest_InStock()
    {
      Products target = new Products();
      target.Add(new Product("Coca Cola", 1.0M), 100);
      target.Add(new Product("Fanta", 1.0M), 100); 
      bool result = target.IsAvailable("Fanta");
      Assert.IsTrue(result);
    }

    [TestMethod]
    public void AddStockTest()
    {
      Products target = new Products();
      target.Add(new Product("Fanta", 1.0M), 1);
      bool result = target.IsAvailable("Fanta");
      Assert.IsTrue(result);
    }

    [TestMethod]
    public void RemoveStockTest()
    {
      Products target = new Products();
      target.Add(new Product("Fanta", 1.0M), 1);
      target.Remove("Fanta", 1); 
      bool result = target.IsAvailable("Fanta");
      Assert.IsFalse(result);
    }

    [TestMethod]
    public void CountTest_Empty()
    {
      Products target = new Products();
      int count = target.Count("Fanta");
      Assert.AreEqual((int)0, count);
    }

  }
}
