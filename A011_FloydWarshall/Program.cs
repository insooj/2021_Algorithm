using System;

namespace FloydWarshall
{
  class Program
  {
    static int V = 5;  // Number of vertices
    const int Inf = 100;  // Infinity, not connected

    static void Main(string[] args)
    {
      int[,] graph = {
        { 0, 4, 2, 5, Inf },
        { Inf, 0, 1, Inf, 4 },
        { 1, 3, 0, 1, 2 },
        { -2, Inf, Inf, 0, 2 },
        { Inf, -3, 3, 1, 0 }
      };

      FloydWarshall(graph, V);
    }

    private static void FloydWarshall(int[,] graph, int V)
    {
      Console.WriteLine("graph");
      PrintGraph(graph, V);

      int[,] next = new int[V, V];

      for (int i = 0; i < V; i++)
        for (int j = 0; j < V; j++)
          if (i != j)
            next[i, j] = j + 1;

      Console.WriteLine("next");
      PrintNext(next, V);

    // 핵심부분 - 3중 for 문 
      for (int k = 0; k < V; k++)
      {
        for (int i = 0; i < V; i++)
          for (int j = 0; j < V; j++)
            if (graph[i,k] != Inf && graph[k,j] != Inf && 
              graph[i, k] + graph[k, j] < graph[i, j])
            {
              Console.WriteLine("Change: [{0},{1}] = [{2},{3}] + [{4},{5}] = {6}", 
                i, j, i, k, k, j, graph[i, k] + graph[k, j]);
              graph[i, j] = graph[i, k] + graph[k, j];
              next[i, j] = next[i, k];
            }

        Console.WriteLine("Graph({0})", k);
        PrintGraph(graph, V);
        Console.WriteLine("Next({0})", k);
        PrintNext(next, V);
      }

      PrintResult(graph, next);
    }

    private static void PrintNext(int[,] next, int v)
    {
      for (int i = 0; i < v; i++) {
        for (int j = 0; j < v; j++)
          Console.Write("{0,8}", next[i, j]);
        Console.WriteLine();
      }
    }

    private static void PrintGraph(int[,] graph, int V)
    {
      for (int i = 0; i < V; i++)
      {
        for (int j = 0; j < V; j++)
          Console.Write("{0,8}", graph[i, j]);
        Console.WriteLine();
      }
    }

    private static void PrintResult(int[,] graph, int[,] next)
    {
      // index는 0~V-1이고 Vertex는 1~V이므로 
      Console.WriteLine("pair     distance    path");
      for (int i = 0; i < V; i++)
        for (int j = 0; j < V; j++)
          if (i != j)
          {
            int u = i + 1;
            int v = j + 1;
            string path = string.Format("{0} -> {1}    {2,2:G}     {3}", u, v, graph[i, j], u);
            do
            {
              u = next[u - 1, v - 1];
              path += " -> " + u;
            } while (u != v);
            Console.WriteLine(path);
          }        
    }
  }
}
