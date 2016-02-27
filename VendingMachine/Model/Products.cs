using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
  public class Products : IEnumerable<Product>
  {
    public Products()
    {
      quantities = new Dictionary<string, int>();
      products = new Dictionary<string, Product>();
    }

    public Product this[string name]
    {
      get
      {
        if (String.IsNullOrEmpty(name)) return null;
        if (products.ContainsKey(name))
        {
          return products[name];
        }
        return null;
      }
    }

    public bool IsAvailable(string name)
    {
      if (String.IsNullOrEmpty(name)) return false;

      return quantities.ContainsKey(name) && quantities[name] > 0;

    }

    public void Remove(string product, int quantity)
    {
      if (product == null) return;
      if (quantities.ContainsKey(product))
      {
        if (quantities[product] >= quantity)
          quantities[product] -= quantity;
      }
    }

    public void Add(Product product, int quantity)
    {
      if (product == null || String.IsNullOrEmpty(product.Name)) return;
      if (!quantities.ContainsKey(product.Name))
      {
        quantities.Add(product.Name, 0);
        products.Add(product.Name, new Product(product));
      }
      quantities[product.Name] += quantity;
    }

    public int Count(string product)
    {
      if (product == null) return 0;

      if (products.ContainsKey(product))
      {
        return quantities[product];
      }
      else
      {
        return 0;
      }
    }

    public IEnumerator<Product> GetEnumerator()
    {
      return products.Values.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    private Dictionary<string, Product> products;
    private Dictionary<string, int> quantities;

  }
}
