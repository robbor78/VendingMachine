using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Model;

namespace VendingMachine
{
  public interface IVendingMachine
  {
    ProductAndChange buy(string product, Money tendered);

    void addProduct(Product product, int quantity);
    void removeProduct(string product, int quantity);
    int countProduct(string product);
    Products Products { get; }

    void addToFloat(Money incFloat);
    void removeFromFloat(Money decFloat);
    Money getFloat();
  }
}
