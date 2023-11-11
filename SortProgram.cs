using System;

namespace SortProgram
{
    class Program
    {
        static int N = 100;
        static int[] a = new int[N];
        static void Main(string[] args)
        {
            RandomInit();
            PrintArray();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            BubbleSort();
            watch.Stop();
            var elap = watch.ElapsedTicks;
            Console.WriteLine("시간측정 Ticks :"+elap);

            RandomInit();
            watch = System.Diagnostics.Stopwatch.StartNew();
            SelectionSort();
            elap = watch.ElapsedTicks;
            Console.WriteLine("시간측정 Ticks :"+elap);

            RandomInit();
            watch = System.Diagnostics.Stopwatch.StartNew();
            InsertionSort();
            elap = watch.ElapsedTicks;
            Console.WriteLine("시간측정 Ticks :" + elap);

            RandomInit();
            watch = System.Diagnostics.Stopwatch.StartNew();
            ShellSort();
            elap = watch.ElapsedTicks;
            Console.WriteLine("시간측정 Ticks :" + elap);

            RandomInit();
            watch = System.Diagnostics.Stopwatch.StartNew();
            HeapSort();
            elap = watch.ElapsedTicks;
            Console.WriteLine("시간측정 Ticks :" + elap);
        }
            private static void HeapSort()
        {
            for (int i = N / 2 - 1; i >= 0; i--)
                DownHeap(a, N, i);

            for(int i = N - 1; i >= 0; i--)
            {
                //루트와 맨 뒤의 값을 바꾸어 준다
                int t = a[0];
                a[0] = a[i];
                a[i] = t;
                DownHeap(a, i, 0); // 루트에서 다운힙 과정 
            }
            Console.WriteLine("\nHeap Sort : ");
            PrintArray();

        }

        private static void DownHeap(int[] a, int n, int i)
        {
            int largest = i;
            int left = 2 * i;
            int right = 2 * i + 1;
            if (left < n && a[left] > a[largest])
                largest = left;
            if(right < n && a[right] > a[largest])
                largest = right;
            if(largest != i)
            {
                Swap(i, largest);
                DownHeap(a, n, largest);
            }
        }

        private static void ShellSort()
        {
            int[] h = { 0, 1, 4, 10, 23, 57, 132, 301, 701, 1750 };
            int index = 0;
            while (h[index] < N / 2)
                index++;

            int gap = h[index];
            while(gap > 0)
            {
                Console.WriteLine("gap = {0}",gap);
                for(int i = gap; i < N; i++)
                {
                    int current = a[i];
                    int j = i;
                    while (j >= gap && a[j - gap] > current)
                    {
                        a[j] = a[j - gap];
                        j = j - gap;
                    }
                    a[j] = current;
                }
                gap = h[--index];
            }
            Console.WriteLine("\nShell Sort : ");
            PrintArray();
        }

        private static void InsertionSort()
        {
           for (int i = 0; i < N; i++)
            {
                int current = a[i];
                int j = i - 1;
                while(j>=0 && a[j] > current)
                {
                    a[j + 1] = a[j];
                    j = j - 1;
                }
                a[j + 1] = current;
            }
            Console.WriteLine("\nInsertion Sort : ");
            PrintArray();
        }

        private static void SelectionSort()
        {
            for (int i = 0; i < N - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < N; j++)
                {
                    if (a[min] > a[j])
                        min = j;

                }
                Swap(i, min);
                
            }
            Console.WriteLine("\nSelection Sort : ");
            PrintArray();
        }
        private static void BubbleSort()
        {
            for (int i = N - 1; i > 0; i--)
                for (int j = 0; j < i; j++)
                    if (a[j] > a[j + 1])
                        Swap(j, j + 1);
            Console.WriteLine("\nBubble Sort : ");
            PrintArray();
           // Array.Sort(a);
        }

        private static void Swap(int i, int j)
        {
            int t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        private static void RandomInit()
        {
            Random r = new Random();
            for (int i = 0; i < N; i++)
                a[i] = r.Next(1000);
        }

        private static void PrintArray()
        {
           foreach(var i in a)
                Console.Write(i+ " ");
           Console.WriteLine();
        }
    }
}
