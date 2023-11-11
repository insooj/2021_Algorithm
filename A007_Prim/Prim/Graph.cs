using System;

namespace Prim
{
  
  internal class Graph
  {
    static int MAX = 100; // 최대 버텍스 수
    static int INF = 999;
    int V = 0;  // 그래프 안의 버텍스의 수(파일에서 읽어옴)
    string[] vertex = new string[MAX];
    int[,] adj = new int[MAX,MAX];

    // 해당 index의 버텍스 이름을 가져오기
    public string GetVertex(int i) { return vertex[i]; }

    int MSTWeight = 0;
    internal void Prim(int start)
    {
      bool[] selected = new bool[MAX];
      int[] dist = new int[MAX];

      // 배열 초기화
      for(int i=0; i<V; i++)
      {
        dist[i] = INF;
        selected[i] = false;
      }

      dist[start] = 0;  // 시작 정점
      //selected[start] = true;

      for(int i=0; i<V; i++)
      {
        int u = GetMinVertex(selected, dist);
        //Console.WriteLine("MinVertex : " + u );
        selected[u] = true;

        if (dist[u] == INF)
          return;
        MSTWeight += dist[u];
        Console.Write("{0} ({1}) -> ", GetVertex(u), MSTWeight);

        // dist[] 배열을 업데이트
        for (int v = 0; v < V; v++)
          if (adj[u, v] != INF)
            if (selected[v] == false && adj[u, v] < dist[v])
              dist[v] = adj[u, v];
      }
    }

    // 포함되지 않은 정점 중에서 MST와의 거리가 최소인 정점을 리턴
    // 맨 처음에는 start 버텍스가 리턴됨
    private int GetMinVertex(bool[] selected, int[] dist)
    {
      int minV = 0;
      int minDist = INF;

      for(int v=0; v<V; v++)
      {
        if(!selected[v] && dist[v] < minDist)
        {
          minDist = dist[v];
          minV = v;
        }
      }
      //Console.WriteLine("minDist : "+minDist);
      return minV;
    }

    // 버텍스 추가
    public void InsertVertex(int index, string name) { vertex[index] = name; }

    // 가중치 그래프의 에지를 추가, w는 가중치
    public void InsertEdge(int i, int j, int w)
    {
      adj[i, j] = w;
      adj[j, i] = w;
    }

    // graph.txt의 글자 사이는 \t으로 구분해야 함
    public void ReadGraph(string fileName)
    {
      string[] lines = System.IO.File.ReadAllLines("../../"+fileName);

      V = int.Parse(lines[0]); // 버텍스의 숫자 처리

      for(int i=1; i<lines.Length; i++) // 두번째 줄에서 끝까지
      {
        // A 0 29  999  999  999  10  999
        string[] split = lines[i].Split('\t');
        InsertVertex(i-1, split[0]); 
        for(int j=1; j<split.Length; j++)
        {
          InsertEdge(i - 1, j - 1, int.Parse(split[j]));
        }
      }
    }

    public void PrintGraph()
    {
      Console.WriteLine("Vertex수 : " + V);
      for (int i = 0; i < V; i++)
      {
        Console.Write("{0}", GetVertex(i));
        for (int j = 0; j < V; j++)
        {
          Console.Write("{0,8}", adj[i, j]);
        }
        Console.WriteLine();
      }
    }
  }
}