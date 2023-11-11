using System;

namespace A010_fibonacci
{
  class Program
  {
    static long[] a = new long[100];
    static void Main(string[] args)
    {
      for(int i=1; i<50; i++)
        Console.WriteLine("{0} -> {1}", i, Fibonacci(i)); 
    }

    private static long Fibonacci(int n)
    {
      if (n == 1 || n == 2)
        return 1;
      if (a[n] != 0)
        return a[n];
      return a[n] = Fibonacci(n - 1) + Fibonacci(n - 2);
    }
  }
}
