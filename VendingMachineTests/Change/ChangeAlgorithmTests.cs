using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Change;
using VendingMachine.Model;

namespace VendingMachineTests.Change
{
  [TestClass]
  public class ComplexChangeAlgorithmTests
  {
    [TestMethod]
    public void NoChangeRequired()
    {
      Money moneyFloat = new Money();
      decimal originalFloat = moneyFloat.Total;
      ChangeAlgorithm algo = new ChangeAlgorithm();

      decimal price = 2.3M;
      Money tendered = new Money();
      tendered.Add(DenominationEnum.OneEuro, 2);
      tendered.Add(DenominationEnum.TenCents, 3);

      Money change;
      bool result = algo.DetermineChange(moneyFloat, tendered, price, out change);

      Assert.IsTrue(result);

      Assert.AreEqual(0, change.Total, "Change total incorrect. No change expected.");
      Assert.AreEqual(originalFloat + price, moneyFloat.Total, "Float incorrect.");

      Assert.AreEqual(0, change[DenominationEnum.TenCents].NumberOfCoins, "Incorrect change. 10 cents.");
      Assert.AreEqual(0, change[DenominationEnum.TwentyCents].NumberOfCoins, "Incorrect change. 20 cents.");
      Assert.AreEqual(0, change[DenominationEnum.FiftyCents].NumberOfCoins, "Incorrect change. 50 cents.");
      Assert.AreEqual(0, change[DenominationEnum.OneEuro].NumberOfCoins, "Incorrect change. 1 Euro.");
      Assert.AreEqual(0, change[DenominationEnum.TwoEuro].NumberOfCoins, "Incorrect change. 2 Euro.");

      Assert.AreEqual(3, moneyFloat[DenominationEnum.TenCents].NumberOfCoins, "Incorrect float. 10 cents.");
      Assert.AreEqual(0, moneyFloat[DenominationEnum.TwentyCents].NumberOfCoins, "Incorrect float. 20 cents.");
      Assert.AreEqual(0, moneyFloat[DenominationEnum.FiftyCents].NumberOfCoins, "Incorrect float. 50 cents.");
      Assert.AreEqual(2, moneyFloat[DenominationEnum.OneEuro].NumberOfCoins, "Incorrect float. 1 Euro.");
      Assert.AreEqual(0, moneyFloat[DenominationEnum.TwoEuro].NumberOfCoins, "Incorrect float. 2 Euro.");
  
    }

    [TestMethod]
    public void ChangeRequired1()
    {
      Money moneyFloat = SampleFloat();
      decimal originalFloat = moneyFloat.Total;
      ChangeAlgorithm algo = new ChangeAlgorithm();

      decimal price = 2.3M;
      Money tendered = new Money();
      tendered.Add(DenominationEnum.OneEuro, 2);
      tendered.Add(DenominationEnum.TenCents, 4);

      Money change;
      bool result = algo.DetermineChange(moneyFloat, tendered, price, out change);

      Assert.IsTrue(result);

      Assert.AreEqual(0.1M, change.Total, "Change total incorrect. Change expected.");
      Assert.AreEqual(originalFloat + price, moneyFloat.Total, "Float incorrect.");

      Assert.AreEqual(1, change[DenominationEnum.TenCents].NumberOfCoins, "Incorrect change. 10 cents.");
      Assert.AreEqual(0, change[DenominationEnum.TwentyCents].NumberOfCoins, "Incorrect change. 20 cents.");
      Assert.AreEqual(0, change[DenominationEnum.FiftyCents].NumberOfCoins, "Incorrect change. 50 cents.");
      Assert.AreEqual(0, change[DenominationEnum.OneEuro].NumberOfCoins, "Incorrect change. 1 Euro.");
      Assert.AreEqual(0, change[DenominationEnum.TwoEuro].NumberOfCoins, "Incorrect change. 2 Euro.");

      Assert.AreEqual(103, moneyFloat[DenominationEnum.TenCents].NumberOfCoins, "Incorrect float. 10 cents.");
      Assert.AreEqual(100, moneyFloat[DenominationEnum.TwentyCents].NumberOfCoins, "Incorrect float. 20 cents.");
      Assert.AreEqual(100, moneyFloat[DenominationEnum.FiftyCents].NumberOfCoins, "Incorrect float. 50 cents.");
      Assert.AreEqual(102, moneyFloat[DenominationEnum.OneEuro].NumberOfCoins, "Incorrect float. 1 Euro.");
      Assert.AreEqual(100, moneyFloat[DenominationEnum.TwoEuro].NumberOfCoins, "Incorrect float. 2 Euro."); 
    }

    [TestMethod]
    public void ChangeRequired2()
    {
      Money moneyFloat = SampleFloat();
      decimal originalFloat = moneyFloat.Total;
      ChangeAlgorithm algo = new ChangeAlgorithm();

      decimal price = 1.7M;
      Money tendered = new Money();
      tendered.Add(DenominationEnum.TwoEuro, 2);

      Money change;
      bool result = algo.DetermineChange(moneyFloat, tendered, price, out change);

      Assert.IsTrue(result);
      Assert.AreEqual(tendered.Total-price, change.Total, "Change total incorrect. Change expected.");
      Assert.AreEqual(originalFloat + price, moneyFloat.Total,"Float incorrect.");

      Assert.AreEqual(1, change[DenominationEnum.TenCents].NumberOfCoins, "Incorrect change. 10 cents.");
      Assert.AreEqual(1, change[DenominationEnum.TwentyCents].NumberOfCoins, "Incorrect change. 20 cents.");
      Assert.AreEqual(0, change[DenominationEnum.FiftyCents].NumberOfCoins, "Incorrect change. 50 cents.");
      Assert.AreEqual(0, change[DenominationEnum.OneEuro].NumberOfCoins, "Incorrect change. 1 Euro.");
      Assert.AreEqual(1, change[DenominationEnum.TwoEuro].NumberOfCoins, "Incorrect change. 2 Euro.");

      Assert.AreEqual(99, moneyFloat[DenominationEnum.TenCents].NumberOfCoins, "Incorrect float. 10 cents.");
      Assert.AreEqual(99, moneyFloat[DenominationEnum.TwentyCents].NumberOfCoins, "Incorrect float. 20 cents.");
      Assert.AreEqual(100, moneyFloat[DenominationEnum.FiftyCents].NumberOfCoins, "Incorrect float. 50 cents.");
      Assert.AreEqual(100, moneyFloat[DenominationEnum.OneEuro].NumberOfCoins, "Incorrect float. 1 Euro.");
      Assert.AreEqual(101, moneyFloat[DenominationEnum.TwoEuro].NumberOfCoins, "Incorrect float. 2 Euro."); 

    }

    [TestMethod]
    public void ChangeRequired_Only10CentCoinsInFloat()
    {
      Money moneyFloat = new Money();
      moneyFloat.Add(DenominationEnum.TenCents, 30); //only 10 cent coins in float
      decimal originalFloat = moneyFloat.Total;
      ChangeAlgorithm algo = new ChangeAlgorithm();

      decimal price = 1.4M;
      Money tendered = new Money();
      tendered.Add(DenominationEnum.TwoEuro, 1);

      Money change;
      bool result = algo.DetermineChange(moneyFloat, tendered, price, out change);

      Assert.IsTrue(result);
      Assert.AreEqual(tendered.Total - price, change.Total, "Change total incorrect. Change expected.");
      Assert.AreEqual(originalFloat + price, moneyFloat.Total, "Float incorrect.");

      Assert.AreEqual(6, change[DenominationEnum.TenCents].NumberOfCoins,"Incorrect change. 10 cents.");
      Assert.AreEqual(0, change[DenominationEnum.TwentyCents].NumberOfCoins,"Incorrect change. 20 cents.");
      Assert.AreEqual(0, change[DenominationEnum.FiftyCents].NumberOfCoins,"Incorrect change. 50 cents.");
      Assert.AreEqual(0, change[DenominationEnum.OneEuro].NumberOfCoins,"Incorrect change. 1 Euro.");
      Assert.AreEqual(0, change[DenominationEnum.TwoEuro].NumberOfCoins,"Incorrect change. 2 Euro.");

      Assert.AreEqual(24, moneyFloat[DenominationEnum.TenCents].NumberOfCoins,"Incorrect float. 10 cents." );
      Assert.AreEqual(0, moneyFloat[DenominationEnum.TwentyCents].NumberOfCoins, "Incorrect float. 20 cents.");
      Assert.AreEqual(0, moneyFloat[DenominationEnum.FiftyCents].NumberOfCoins, "Incorrect float. 50 cents.");
      Assert.AreEqual(0, moneyFloat[DenominationEnum.OneEuro].NumberOfCoins, "Incorrect float. 1 Euro.");
      Assert.AreEqual(1, moneyFloat[DenominationEnum.TwoEuro].NumberOfCoins, "Incorrect float. 2 Euro.");
    }

    [TestMethod]
    public void ChangeRequired_TenderVeryHigh()
    {
      Money moneyFloat = new Money();
      moneyFloat.Add(DenominationEnum.TenCents, 30);
      decimal originalFloat = moneyFloat.Total;
      ChangeAlgorithm algo = new ChangeAlgorithm();

      decimal price = 1.4M;
      Money tendered = new Money();
      tendered.Add(DenominationEnum.TwoEuro, 2);

      Money change;
      bool result = algo.DetermineChange(moneyFloat, tendered, price, out change);

      Assert.IsTrue(result);
      Assert.AreEqual(tendered.Total - price, change.Total, "Change total incorrect. Change expected.");
      Assert.AreEqual(originalFloat + price, moneyFloat.Total, "Float incorrect.");

      Assert.AreEqual(6, change[DenominationEnum.TenCents].NumberOfCoins, "Incorrect change. 10 cents.");
      Assert.AreEqual(0, change[DenominationEnum.TwentyCents].NumberOfCoins, "Incorrect change. 20 cents.");
      Assert.AreEqual(0, change[DenominationEnum.FiftyCents].NumberOfCoins, "Incorrect change. 50 cents.");
      Assert.AreEqual(0, change[DenominationEnum.OneEuro].NumberOfCoins, "Incorrect change. 1 Euro.");
      Assert.AreEqual(1, change[DenominationEnum.TwoEuro].NumberOfCoins, "Incorrect change. 2 Euro.");

      Assert.AreEqual(24, moneyFloat[DenominationEnum.TenCents].NumberOfCoins, "Incorrect float. 10 cents.");
      Assert.AreEqual(0, moneyFloat[DenominationEnum.TwentyCents].NumberOfCoins, "Incorrect float. 20 cents.");
      Assert.AreEqual(0, moneyFloat[DenominationEnum.FiftyCents].NumberOfCoins, "Incorrect float. 50 cents.");
      Assert.AreEqual(0, moneyFloat[DenominationEnum.OneEuro].NumberOfCoins, "Incorrect float. 1 Euro.");
      Assert.AreEqual(1, moneyFloat[DenominationEnum.TwoEuro].NumberOfCoins, "Incorrect float. 2 Euro.");
    }


    [TestMethod]
    public void CannotGiveChange_EmptyFloat()
    {
      Money moneyFloat = new Money();
      decimal originalFloat = moneyFloat.Total;
      ChangeAlgorithm algo = new ChangeAlgorithm();

      decimal price = 0.1M;
      Money tendered = new Money();
      tendered.Add(DenominationEnum.TwentyCents, 1);

      Money change;
      bool result = algo.DetermineChange(moneyFloat, tendered, price, out change);

      Assert.IsFalse(result);

      Assert.AreEqual(originalFloat, moneyFloat.Total, "Float incorrect.");

      Assert.AreEqual(0, moneyFloat[DenominationEnum.TenCents].NumberOfCoins, "Incorrect float. 10 cents.");
      Assert.AreEqual(0, moneyFloat[DenominationEnum.TwentyCents].NumberOfCoins, "Incorrect float. 20 cents.");
      Assert.AreEqual(0, moneyFloat[DenominationEnum.FiftyCents].NumberOfCoins, "Incorrect float. 50 cents.");
      Assert.AreEqual(0, moneyFloat[DenominationEnum.OneEuro].NumberOfCoins, "Incorrect float. 1 Euro.");
      Assert.AreEqual(0, moneyFloat[DenominationEnum.TwoEuro].NumberOfCoins, "Incorrect float. 2 Euro.");
    }

    [TestMethod]
    public void CannotGiveChange_FloatNotEmpty()
    {
      Money moneyFloat = new Money();
      moneyFloat.Add(DenominationEnum.FiftyCents, 1);
      decimal originalFloat = moneyFloat.Total;
      ChangeAlgorithm algo = new ChangeAlgorithm();

      decimal price = 0.1M;
      Money tendered = new Money();
      tendered.Add(DenominationEnum.TwentyCents, 1);

      Money change;
      bool result = algo.DetermineChange(moneyFloat, tendered, price, out change);

      Assert.IsFalse(result);

      Assert.AreEqual(originalFloat, moneyFloat.Total, "Float incorrect.");

      Assert.AreEqual(0, moneyFloat[DenominationEnum.TenCents].NumberOfCoins, "Incorrect float. 10 cents.");
      Assert.AreEqual(0, moneyFloat[DenominationEnum.TwentyCents].NumberOfCoins, "Incorrect float. 20 cents.");
      Assert.AreEqual(1, moneyFloat[DenominationEnum.FiftyCents].NumberOfCoins, "Incorrect float. 50 cents.");
      Assert.AreEqual(0, moneyFloat[DenominationEnum.OneEuro].NumberOfCoins, "Incorrect float. 1 Euro.");
      Assert.AreEqual(0, moneyFloat[DenominationEnum.TwoEuro].NumberOfCoins, "Incorrect float. 2 Euro.");
    }

    private Money SampleFloat()
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
