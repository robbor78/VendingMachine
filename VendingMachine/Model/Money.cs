using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
  /// <summary>
  /// This class holds a set of coins and provides various operations (addition, subtraction).
  /// 
  /// e.g. this class is the float in the vending machine but also represents the coins a user
  /// enters into the vending machine.
  /// </summary>
  public class Money
  {
    public Money()
    {
      coins = new Dictionary<DenominationEnum, Coins>();

      //populate with "all" known coin-types
      coins.Add(DenominationEnum.TenCents, new Coins() { Coin = new Coin() { Denomination = DenominationEnum.TenCents }, NumberOfCoins = 0 });
      coins.Add(DenominationEnum.TwentyCents, new Coins() { Coin = new Coin() { Denomination = DenominationEnum.TwentyCents }, NumberOfCoins = 0 });
      coins.Add(DenominationEnum.FiftyCents, new Coins() { Coin = new Coin() { Denomination = DenominationEnum.FiftyCents }, NumberOfCoins = 0 });
      coins.Add(DenominationEnum.OneEuro, new Coins() { Coin = new Coin() { Denomination = DenominationEnum.OneEuro }, NumberOfCoins = 0 });
      coins.Add(DenominationEnum.TwoEuro, new Coins() { Coin = new Coin() { Denomination = DenominationEnum.TwoEuro }, NumberOfCoins = 0 });
    }

    public Money(Money copy) : this()
    {
      Add(copy);
    }

    public decimal Total
    {
      get
      {
        //Coins are stored as cents but the total is in euros.
        return coins.Sum(x => x.Value.Total) / 100.0M;
      }
    }

    public void Add(DenominationEnum faceValue, int amount)
    {
      if (!coins.ContainsKey(faceValue))
      {
        coins.Add(faceValue, new Coins() { Coin = new Coin() { Denomination = faceValue } });
      }
      coins[faceValue].NumberOfCoins += amount;
    }

    public void Add(Money money)
    {
      var changeCoins = money.coins.Values;
      foreach (Coins changeCoin in changeCoins)
      {
        DenominationEnum faceValue = changeCoin.Coin.Denomination;
        coins[faceValue].NumberOfCoins += changeCoin.NumberOfCoins;
      }
    }

    public void Subtract(Money money)
    {
      var changeCoins = money.coins.Values;
      foreach (Coins changeCoin in changeCoins)
      {
        DenominationEnum faceValue = changeCoin.Coin.Denomination;
        coins[faceValue].NumberOfCoins -= changeCoin.NumberOfCoins;
      }
    }

    public Coins this[DenominationEnum faceValue]
    {
      get { return coins[faceValue]; }
    }

    public override string ToString()
    {
      string result = String.Empty;
      var changeCoins = coins.Values;
      foreach (Coins changeCoin in changeCoins)
      {
        DenominationEnum faceValue = changeCoin.Coin.Denomination;
        result += "Coin: " + (int)faceValue + "->" + changeCoin.NumberOfCoins + " ";
      }
      return result;
    }

    protected Dictionary<DenominationEnum, Coins> coins;
  }
}
