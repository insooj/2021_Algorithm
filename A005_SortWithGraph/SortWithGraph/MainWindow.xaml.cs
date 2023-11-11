using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SortWithGraph
{
  /// <summary>
  /// MainWindow.xaml에 대한 상호 작용 논리
  /// </summary>
  public partial class MainWindow : Window
  {
    static int MAX = 1000000;
    int[] a = new int[MAX];
    //int[] a = { 1, 3, 10, 4, 13, 18, 17, 27, 5, 2 };
    //int[] a = { 27, 17, 18 };
    int N = 0;  // 데이터 갯수
    Thread t1;
    private bool timeFlag;

    public MainWindow()
    {
      InitializeComponent();
    }

    private void BtnCreate_Click(object sender, RoutedEventArgs e)
    {
      N = int.Parse(txtData.Text);
      Console.WriteLine(N);

      Random r = new Random();
      for (int i = 0; i < N; i++)
      {
        a[i] = r.Next(3 * N);
      }

      Graph();
      PrintArray(0, N);
    }

    private void Graph()
    {
      canvas.Children.Clear();

      for (int i = 0; i < N; i++)
      {
        Line l = new Line();
        l.X1 = l.X2 = i * 5;
        if (l.X1 > canvas.Width)
          break;
        l.Y1 = canvas.Height - (int)(a[i] / (3.0 * N) * canvas.Height);
        l.Y2 = canvas.Height;
        l.HorizontalAlignment = HorizontalAlignment.Left;
        l.VerticalAlignment = VerticalAlignment.Bottom;
        l.Stroke = Brushes.RoyalBlue;
        l.StrokeThickness = 4;
        canvas.Children.Add(l);
      }
    }

    // 2개 알고리즘의 시간 측정
    private void BtnTime_Click(object sender, RoutedEventArgs e)
    {
      // 시간측정 플래그 세팅
      timeFlag = true;

      var watch = System.Diagnostics.Stopwatch.StartNew();
      BubbleSort();
      watch.Stop();
      long tickBubble = watch.ElapsedTicks;

      // 정렬된 그래프 표시
      Graph();

      // 측정시간을 Canvas에 표시
      TextBlock txtBubble = new TextBlock();
      txtBubble.Text = "Bubble Sort : " + tickBubble + " Ticks.  " + tickBubble / 10000.0 + " ms.";
      txtBubble.Foreground = Brushes.Black;
      txtBubble.Background = Brushes.White;
      Canvas.SetLeft(txtBubble, 100);
      Canvas.SetTop(txtBubble, 100);
      canvas.Children.Add(txtBubble);

      Random r = new Random();
      for (int i = 0; i < N; i++)
      {
        a[i] = r.Next(3 * N);
      }
      //watch.Reset();
      watch = System.Diagnostics.Stopwatch.StartNew();
      DSQSort(a, 0, N - 1);
      watch.Stop();
      long tickQuick = watch.ElapsedTicks;

      // 측정시간을 Canvas에 표시
      TextBlock txtQuick = new TextBlock();
      txtQuick.Text = "Quick Sort : " + tickQuick + " Ticks.  " + tickQuick / 10000.0 + " ms.";
      txtQuick.Foreground = Brushes.Black;
      txtQuick.Background = Brushes.White;
      Canvas.SetLeft(txtQuick, 100);
      Canvas.SetTop(txtQuick, 120);
      canvas.Children.Add(txtQuick);

      for (int i = 0; i < N; i++)
      {
        a[i] = r.Next(3 * N);
      }

      watch = System.Diagnostics.Stopwatch.StartNew();
      MergeSort(a, 0, N - 1);
      watch.Stop();
      long tickMerge = watch.ElapsedTicks;

      // 측정시간을 Canvas에 표시
      TextBlock txtMerge = new TextBlock();
      txtMerge.Text = "Merge Sort : " + tickMerge + " Ticks.  " + tickMerge / 10000.0 + " ms.";
      txtMerge.Foreground = Brushes.Black;
      txtMerge.Background = Brushes.White;
      Canvas.SetLeft(txtMerge, 100);
      Canvas.SetTop(txtMerge, 140);
      canvas.Children.Add(txtMerge);

      timeFlag = false;
    }


    private void BubbleSort()
    {
      for (int i = N - 1; i > 0; i--)
      {
        for (int j = 0; j < i; j++)
        {
          if (a[j] > a[j + 1])
          {
            int t = a[j];
            a[j] = a[j + 1];
            a[j + 1] = t;
          }
        }
        // GUI가 main thread에 속하므로 Dispatcher.Invoke를 사용해서 그래프를 그려야 한다
        if (timeFlag == false)
        {
          Dispatcher.Invoke(new Action(Graph));
          Thread.Sleep(50);
        }
      }
    }

    private void QuickSortMain()
    {
      //QuickSort(a, 0, N - 1);
      DSQSort(a, 0, N - 1); // 자료구조 책에 나온 QuickSort 코드
      PrintArray(0, N);
    }

    private void DSQSort(int[] a, int left, int right)
    {
      if (left < right)
      {
        int q = partition(a, left, right);
        DSQSort(a, left, q - 1);
        DSQSort(a, q + 1, right);
        if (timeFlag == false)
        {
          Dispatcher.Invoke(new Action(Graph));
          Thread.Sleep(50);
        }
      }
    }

    // partition method for DSQSort
    private int partition(int[] a, int left, int right)
    {
      int low = left;
      int high = right + 1;
      int pivot = a[left];

      //Console.WriteLine("partition({0}, {1})", left, right);
      //PrintArray(0, N);

      do
      {
        do
        {
          low++;
        } while (low <= right && a[low] < pivot);
        do
        {
          high--;
        } while (high >= left && a[high] > pivot);
        if (low < high)
        {
          int tmp = a[low];
          a[low] = a[high];
          a[high] = tmp;
          //Console.WriteLine("Swap({0},{1})", low, high);
        }
        //Console.WriteLine("left={0}, right={1}, low={2}, high={3}", left, right, low, high);
      } while (low < high);

      // pivot과 a[high]를 교체
      a[left] = a[high];
      a[high] = pivot;

      //Console.WriteLine("Pivot Swap({0},{1})", left, high);

      return high;
    }

    private void QuickSort(int[] arr, int left, int right)
    {
      if (left < right)
      {
        int pivot = Partition(arr, left, right);
        QuickSort(arr, left, pivot - 1);
        QuickSort(arr, pivot + 1, right);

        Dispatcher.Invoke(new Action(Graph));
        Thread.Sleep(50);
      }
    }

    private void PrintArray(int left, int right)
    {
      for (int i = left; i < right; i++)
      {
        Console.Write(a[i] + ", ");
      }
      Console.WriteLine();
    }

    private int Partition(int[] arr, int left, int right)
    {
      int i = left;
      int j = right;

      //Pivot 값은 0번 인덱스의 값을 가짐
      int pivot = arr[left]; // 배열 가장 왼쪽의 값(피봇 값)

      while (i < j)
      {
        // pivot 보다 큰 값을 만날 때까지 i 증가, 단 배열의 끝을 넘어가면 안됨
        while ((i <= right) && (arr[i] < pivot))
          i++;

        // pivot 보다 작은 값을 만날 때까지 j 감소, 단 배열의 끝을 넘어가면 안됨
        while ((j >= left) && (arr[j] > pivot))
          j--;

        // 바꿀 두 값이 교차되지 않음
        if (i < j)
        {
          int t = arr[i];
          arr[i] = arr[j];
          arr[j] = t;

          //같은 값이 존재 할 경우 
          if (arr[i] == arr[j])
          {
            //Console.WriteLine(".. in if index a[{0}] = a[{1}] = {2}", i, j, arr[i]);
            j--;
          }
        }
      }

      // 교차된 경우 j값을 리턴
      return j;
    }


    private void BtnBubble_Click(object sender, RoutedEventArgs e)
    {
      t1 = new Thread(BubbleSort);
      t1.Start();
    }

    private void BtnQuick_Click(object sender, RoutedEventArgs e)
    {
      t1 = new Thread(QuickSortMain);
      t1.Start();
    }

    private void BtnMerge_Click(object sender, RoutedEventArgs e)
    {
      t1 = new Thread(MergeSortMain);
      t1.Start();
    }

    private void MergeSortMain()
    {
      MergeSort(a, 0, N - 1);
    }

    private void MergeSort(int[] arr, int left, int right)
    {
      if (left < right)
      {
        int mid = (left + right) / 2;
        MergeSort(a, left, mid);
        MergeSort(a, mid + 1, right);
        Merge(a, left, mid, right);
        if (timeFlag == false)
        {
          Dispatcher.Invoke(new Action(Graph));
          Thread.Sleep(50);
        }
      }
    }

    int[] sorted = new int[MAX];
    private void Merge(int[] a, int left, int mid, int right)
    {
      int i, j, k = left;
      for (i = left, j = mid + 1; i <= mid && j <= right;)
      {
        sorted[k++] = (a[i] <= a[j]) ? a[i++] : a[j++];
      }
      if (i > mid) // 왼쪽이 끝남, 오른쪽 복사
        for (int l = j; l <= right; l++)
          sorted[k++] = a[l];
      else // 오른쪽이 끝남, 왼쪽 복사
        for (int l = i; l <= mid; l++)
          sorted[k++] = a[l];

      // sorted[]를 a[]로 복사
      for (int l = left; l <= right; l++)
      {
        a[l] = sorted[l];
      }
    }
  }
}
