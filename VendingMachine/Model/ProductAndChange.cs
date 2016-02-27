using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
  /// <summary>
  /// The output of a vending machine request.
  /// </summary>
  public class ProductAndChange
  {
    public ProductAndChange()
    {
      Result = ResultEnum.None;
      Message = null;
      Change = null;
      Product = null;
    }
    public ResultEnum Result { get; set; }
    public string Message { get; set; }
    public Money Change { get; set; }
    public Product Product { get; set; }
  }
}
