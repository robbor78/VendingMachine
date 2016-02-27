using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
  /// <summary>
  /// The results of a vending machine request.
  /// </summary>
  public enum ResultEnum
  {
    None,
    Ok,
    NoChange,
    NotEnoughMoney,
    NoProduct
  }
}
