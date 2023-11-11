using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prim
{
  class Program
  {
    static void Main(string[] args)
    {
      Graph g = new Graph();
      g.ReadGraph("graph1.txt");
      g.PrintGraph();

      g.Prim(0);  // 0번 버텍스부터 시작
    }
  }
}
