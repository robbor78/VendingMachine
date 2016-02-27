using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Change;
using VendingMachine.Model;

namespace VendingMachine
{
  public class TheVendingMachine : IVendingMachine
  {
    public TheVendingMachine(IChangeAlgorithm changeAlgorithm)
    {
      products = new Products();
      moneyFloat = new Money();
      this.changeAlgorithm = changeAlgorithm;
    }

    public ProductAndChange buy(string selection, Money tendered)
    {
      if (String.IsNullOrEmpty(selection) || !products.IsAvailable(selection))
      {
        return new ProductAndChange()
        {
          Result = ResultEnum.NoProduct,
          Message = selection + " out of stock."
        };
      }

      Product product = products[selection];

      if (tendered==null || product.Price > tendered.Total)
      {
        return new ProductAndChange()
        {
          Result = ResultEnum.NotEnoughMoney,
          Message = "Not enough money."
        };
      }

      Money change;
      bool changeResult = changeAlgorithm.DetermineChange(moneyFloat,tendered, product.Price, out change);

      if (changeResult)
      {
        products.Remove(selection, 1);

        return new ProductAndChange()
        {
          Result = ResultEnum.Ok,
          Product = product,
          Change = change
        };

      }
      else
      {
        return new ProductAndChange()
        {
          Result = ResultEnum.NoChange,
          Message = "Cannot provide change."
        };
      }

    }

    public void addProduct(Product product, int quantity)
    {
      products.Add(product, quantity);
    }

    public void removeProduct(string product, int quantity)
    {
      products.Remove(product, quantity);
    }

    public int countProduct(string product)
    {
      return products.Count(product);
    }

    public Products Products { get { return products; } }

    public void addToFloat(Money change)
    {
      moneyFloat.Add(change);
    }

    public void removeFromFloat(Money change)
    {
      moneyFloat.Add(change);
    }

    public Money getFloat()
    {
      return moneyFloat;
    }

    private Products products;
    private Money moneyFloat;
    private IChangeAlgorithm changeAlgorithm;
  }
}
