using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
  public class Coins
  {
    public Coins()
    {
      Coin = new Coin();
      NumberOfCoins = 0;
    }

    public Coin Coin { get; set; }

    public int NumberOfCoins { get; set; }

    public int Total { get { return NumberOfCoins * (int)Coin.Denomination; } }
  }
}
