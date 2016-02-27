using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Change
{
  /// <summary>
  /// The entire code / algorithm comes from Patrick Jarfish. 
  /// http://patrickjarfish.com/blog/?p=13 (14/3/2014)
  /// 
  /// Instances of this class either represent the entire tree or a child node.
  /// 
  /// This Recurse method uses the tree data structure to find the change to give.
  /// 
  /// The algorithm traverses the tree recursively (breadth first).
  /// 
  /// The parent node represents the original float.
  /// 
  /// Each child node represents the float minus one coin (called the denomination).
  /// 
  /// Each node keeps a running total of "its" change. If the nodes running total
  /// equals the required change the algorithm ends with a solution.
  /// 
  /// The algorithm might not return the most efficient sequence of coins. Needs testing.
  /// </summary>
  public class Tree
  {
    public Tree(decimal denomination, Tree parentNode, Dictionary<decimal, int> originalFloat)
    {
      this.parentNode = parentNode;
      this.denomination = denomination;
      runningTotal = ((parentNode != null) ? parentNode.runningTotal : 0) + (denomination);

      //Create a new float
      cashFloat = new Dictionary<decimal, int>();
      foreach (var floatItem in originalFloat)
      {
        cashFloat.Add(floatItem.Key, floatItem.Value);
      }
      //Check not really needed
      if (denomination != 0)
        cashFloat[denomination]--;
    }

    /// <summary>
    /// Recurses the tree to work out the change.
    /// </summary>
    /// <param name="dataTree"></param>
    /// <param name="desiredTotal"></param>
    /// <returns></returns>
    public Tree Recurse(Tree dataTree, decimal desiredTotal)
    {
      Tree foundLeaf = null;
      if (dataTree.children.Count == 0)
      {
        //build children
        for (int i = 0; i < dataTree.cashFloat.Count; i++)
        {
          var item = dataTree.cashFloat.ElementAt(i);
          if (item.Value > 0)
          {
            var newTreeLeaf = new Tree(item.Key, dataTree, dataTree.cashFloat);

            dataTree.children.Add(newTreeLeaf);
            if (newTreeLeaf.runningTotal == desiredTotal)
              return newTreeLeaf;
          }
        }
      }
      else
      {
        //examine children
        bool isContinue = false;
        foreach (var item in dataTree.children)
        {
          if (item.cashFloat.Where(a => a.Value > 0).Any())
          {
            isContinue = true;
            Tree temp = Recurse(item, desiredTotal);
            if (temp != null)
            {
              foundLeaf = temp;
              break;
            }
          }
        }

        if (!isContinue) return this; //no solution found


      }
      return foundLeaf;
    }

    public Dictionary<decimal, int> cashFloat;
    private decimal denomination;
    private decimal runningTotal;
    private Tree parentNode;
    private List<Tree> children = new List<Tree>();
  }
}
