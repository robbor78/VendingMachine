using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine.Change
{
  public interface IChangeAlgorithm
  {
    /// <summary>
    /// Given the tendered amount (in coins) returns the change amount (in coins) 
    /// and updates the float.
    /// </summary>
    /// <param name="moneyFloat">Float (money in the till)</param>
    /// <param name="tendered">The amount of money received from the customer.</param>
    /// <param name="price">The price the customer must pay.</param>
    /// <param name="change">The balance of money received when the amount tendered is greater than the amount due.</param>
    /// <returns>False if change cannot be provided. True otherwise.</returns>
    bool DetermineChange(Money moneyFloat, Money tendered, decimal price, out Money change);
  }
}
