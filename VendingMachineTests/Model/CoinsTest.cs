using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Model;

namespace VendingMachineTests.Model
{
  [TestClass]
  public class CoinsTest
  {
    [TestMethod]
    public void TotalTest()
    {
      Coins target = new Coins() { Coin = new Coin() { Denomination = DenominationEnum.TenCents }, NumberOfCoins = 2 };
      Assert.AreEqual(20.0M, target.Total, "Coins total incorrect.");
    }
  }
}
