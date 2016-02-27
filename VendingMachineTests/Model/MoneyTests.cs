using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Model;

namespace VendingMachineTests.Model
{
  [TestClass]
  public class MoneyTests
  {
    [TestMethod]
    public void AddChange()
    {
      Money change = SampleMoney();

      Money target = new Money();
      target.Add(change);

      Assert.AreEqual(change.Total, target.Total, "Total incorrect.");

    }

    [TestMethod]
    public void SubtractChange()
    {
      Money change = SampleMoney();

      Money target = new Money();
      target.Add(change);
      target.Subtract(change);

      Assert.AreEqual(0.0M, target.Total, "Total incorrect.");
    }

    private Money SampleMoney()
    {
      Money money = new Money();
      money.Add(DenominationEnum.TenCents, 100);
      money.Add(DenominationEnum.TwentyCents, 100);
      money.Add(DenominationEnum.FiftyCents, 100);
      money.Add(DenominationEnum.OneEuro, 100);
      money.Add(DenominationEnum.TwoEuro, 100);
      return money;
    }
  }
}
