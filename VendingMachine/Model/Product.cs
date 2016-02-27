using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
  public class Product
  {
    public Product(string name, decimal price)
    {
      this.Name = name;
      this.Price = price;
    }

    public Product(Product product)
    {
      this.Name = product.Name;
      this.Price = product.Price;
    }

    public string Name { get; set; }
    public decimal Price { get; set; }

    public override bool Equals(object obj)
    {
      Product other = obj as Product;
      if (obj == null) return false;
      return this.Name == other.Name;
    }

    public override int GetHashCode()
    {
      return Name.GetHashCode();
    }
  }
}
