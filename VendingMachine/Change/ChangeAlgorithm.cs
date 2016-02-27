using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.Change
{
  /// <summary>
  /// Use a tree structure and a breadth first search to determine the change to give.
  /// 
  /// Some of the code is based on this source: http://patrickjarfish.com/blog/?p=13
  /// </summary>
  public class ChangeAlgorithm : IChangeAlgorithm
  {

    public bool DetermineChange(Money moneyFloat, Money tendered, decimal price, out Money change)
    {
      if (IsTenderedTooLittle(tendered, price, out change)) return false;

      if (IsTenderedEqualPrice(moneyFloat, tendered, price, out change)) return true;

      moneyFloat.Add(tendered); //add the new coins to the float

      change = CalculateChange(moneyFloat, tendered, price); //attempt to calculate the change

      if (change==null)
      {
        moneyFloat.Subtract(tendered); //no change => so revert the float
        return false;
      }
      else
      {
        moneyFloat.Subtract(change); //change found => subtract change from the float
        return true;
      }

    }

    /// <summary>
    /// Uses a tree  structure to determine the change.
    /// </summary>
    /// <param name="moneyFloat"></param>
    /// <param name="tendered"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    private Money CalculateChange(Money moneyFloat, Money tendered, decimal price)
    {
      Tree dataStructure = new Tree(0m, null, GetFloat(moneyFloat));

      decimal desiredDenomination = 100*(tendered.Total - price); //multiple by 100 to work in cents

      Tree solution = null;
      //repeat until a solution is found or when we've returned to the root node
      while (solution == null && solution !=dataStructure)
      {
        solution = dataStructure.Recurse(dataStructure, desiredDenomination);
      }

      if (solution == dataStructure) return null; //no solution found

      Money change = BuildSolution(moneyFloat, solution); //build the change structure

      return change;
    }

    /// <summary>
    /// Helper method to convert from the Money class to a dictionary used by the algorithm.
    /// </summary>
    /// <param name="moneyFloat"></param>
    /// <returns></returns>
    private Dictionary<decimal, int> GetFloat(Money moneyFloat)
    {
      Dictionary<decimal, int> floatDict = new Dictionary<decimal, int>();

      floatDict.Add((decimal)DenominationEnum.TenCents, moneyFloat[DenominationEnum.TenCents].NumberOfCoins);
      floatDict.Add((decimal)DenominationEnum.TwentyCents, moneyFloat[DenominationEnum.TwentyCents].NumberOfCoins);
      floatDict.Add((decimal)DenominationEnum.FiftyCents, moneyFloat[DenominationEnum.FiftyCents].NumberOfCoins);
      floatDict.Add((decimal)DenominationEnum.OneEuro, moneyFloat[DenominationEnum.OneEuro].NumberOfCoins);
      floatDict.Add((decimal)DenominationEnum.TwoEuro, moneyFloat[DenominationEnum.TwoEuro].NumberOfCoins);

      return floatDict;
    }

    /// <summary>
    /// Helper method to convert from the tree solution to the Money structure.
    /// </summary>
    /// <param name="moneyFloat"></param>
    /// <param name="solution"></param>
    /// <returns></returns>
    private Money BuildSolution(Money moneyFloat, Tree solution)
    {
      Money newFloat = new Money();

      newFloat[DenominationEnum.TenCents].NumberOfCoins = solution.cashFloat[(decimal)DenominationEnum.TenCents];
      newFloat[DenominationEnum.TwentyCents].NumberOfCoins = solution.cashFloat[(decimal)DenominationEnum.TwentyCents];
      newFloat[DenominationEnum.FiftyCents].NumberOfCoins = solution.cashFloat[(decimal)DenominationEnum.FiftyCents];
      newFloat[DenominationEnum.OneEuro].NumberOfCoins = solution.cashFloat[(decimal)DenominationEnum.OneEuro];
      newFloat[DenominationEnum.TwoEuro].NumberOfCoins = solution.cashFloat[(decimal)DenominationEnum.TwoEuro];

      Money change = new Money(moneyFloat);
      change.Subtract(newFloat);

      return change;
    }

    private bool IsTenderedTooLittle(Money tendered, decimal price, out Money change)
    {
      change = null;

      if (tendered.Total < price)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    private bool IsTenderedEqualPrice(Money moneyFloat, Money tendered, decimal price, out Money change)
    {
      change = new Money();
      if (price == tendered.Total)
      {
        moneyFloat.Add(tendered);
        return true;
      }
      else
      {
        return false;
      }
    }

  }
}
