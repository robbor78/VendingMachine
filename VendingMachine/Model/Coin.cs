using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Model
{
  public class Coin
  {
    public Coin()
    {
      Denomination = 0;
    }

    public DenominationEnum Denomination { get; set; }
  }
}
