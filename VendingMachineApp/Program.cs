using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine;
using VendingMachine.Change;
using VendingMachine.Model;

namespace VendingMachineApp
{
  class Program
  {
    static void Main(string[] args)
    {
      new Program().Run();
    }

    public Program()
    {
      ca = new ChangeAlgorithm();
      vm = new TheVendingMachine(ca);

      FillFloat();

      StockMachine();
    }

    public void Run()
    {
      while (true)
      {
        DisplayProducts();
        string name;
        Money tender;
        bool isProcess = GetUserInput(out name, out tender);
        if (isProcess)
          ProcessInput(name, tender);
      }

    }

    private void ProcessInput(string name, Money tender)
    {
      ProductAndChange pac = vm.buy(name, tender);

      if (pac.Result == ResultEnum.Ok)
      {
        Console.WriteLine("Enjoy!");
        Console.WriteLine("Change:" + pac.Change);
      }
      else
      {
        Console.WriteLine(pac.Message);
      }
    }

    private bool GetUserInput(out string name, out Money tender)
    {
      tender = null;

      Console.WriteLine("Please select a product (empty line to quit):");
      name = Console.ReadLine();


      if (String.IsNullOrEmpty(name))
      {
        Console.WriteLine("Bye");
        System.Environment.Exit(0);
      }

      if (vm.Products[name] == null) return false;


      Console.WriteLine("Enter coins:");
      Console.Write("10 cents: "); string tenCents = Console.ReadLine();
      Console.Write("20 cents: "); string twentyCents = Console.ReadLine();
      Console.Write("50 cents: "); string fiftyCents = Console.ReadLine();
      Console.Write("1 euro: "); string oneEuro = Console.ReadLine();
      Console.Write("2 euro: "); string twoEuro = Console.ReadLine();

      int cents10 = Parse(tenCents);
      int cents20 = Parse(twentyCents);
      int cents50 = Parse(fiftyCents);
      int euro1 = Parse(oneEuro);
      int euro2 = Parse(twoEuro);

      tender = new Money();
      tender.Add(DenominationEnum.TenCents, cents10);
      tender.Add(DenominationEnum.TwentyCents, cents20);
      tender.Add(DenominationEnum.FiftyCents, cents50);
      tender.Add(DenominationEnum.OneEuro, euro1);
      tender.Add(DenominationEnum.TwoEuro, euro2);

      return true;

    }

    private int Parse(string value)
    {
      int i;
      if (int.TryParse(value, out i)) return i;
      return 0;
    }

    private void DisplayProducts()
    {
      var products = vm.Products;

      Console.WriteLine("Product:");
      foreach (Product product in products)
      {
        if (vm.countProduct(product.Name) > 0)
        {
          Console.WriteLine(product.Name + " Price: " + product.Price + " Quantity available: "+products.Count(product.Name));
        }
      }

    }

    private void FillFloat()
    {
      Money moneyFloat = new Money();
      moneyFloat.Add(DenominationEnum.TenCents, 100);
      moneyFloat.Add(DenominationEnum.TwentyCents, 100);
      moneyFloat.Add(DenominationEnum.FiftyCents, 100);
      moneyFloat.Add(DenominationEnum.OneEuro, 100);
      moneyFloat.Add(DenominationEnum.TwoEuro, 100);
      vm.addToFloat(moneyFloat);
    }

    private void StockMachine()
    {
      Product fanta = new Product("Fanta", 1.5M);
      vm.addProduct(fanta, 5);
      Product cocaCola = new Product("Coca Cola", 1.7M);
      vm.addProduct(cocaCola, 5);
      Product bier = new Product("Bier", 1.0M);
      vm.addProduct(bier, 5);

    }

    private IChangeAlgorithm ca;
    private IVendingMachine vm;

  }
}
