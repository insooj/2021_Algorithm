using System;

namespace QSort
{
  class Program
  {
    static void Main(string[] args)
    {
      //int[] nArr = new int[] { 1, 4, 3, 5, 9, 6, 2, 7, 8, 10 };
      //int[] arr = new int[] { 25, 23, 22, 5, 22, 17, 18, 0, 34, 15, 8, 25 };
      //int[] arr = new int[] { 3, 15, 27, 0, 3, 9, 0, 35, 25, 19, 35, 7 };
      int[] arr = new int[] { 57, 332, 490, 491, 484, 421, 585, 243, 585, 547, 499, 0, 438, 212, 489, 7, 521, 390, 421, 47, 242, 185, 24, 310, 589, 268, 457, 133, 445, 482, 98, 241, 321, 367, 295, 144, 369, 253, 133, 324, 302, 556, 215, 514, 154, 476, 240, 331, 455, 301, 79, 126, 32, 383, 95, 472, 307, 180, 502, 215, 563, 451, 398, 103, 449, 258, 278, 70, 516, 344, 237, 268, 256, 97, 344, 285, 569, 109, 155, 113, 28, 126, 278, 144, 402, 572, 208, 537, 271, 422, 437, 189, 350, 517, 360, 450, 157, 112, 65, 496, 197, 169, 414, 110, 63, 411, 469, 535, 38, 409, 502, 197, 24, 389, 187, 436, 173, 254, 300, 476, 49, 340, 399, 94, 507, 48, 517, 338, 336, 493, 128, 457, 44, 259, 515, 458, 312, 167, 80, 590, 102, 273, 499, 461, 519, 239, 164, 561, 329, 524, 277, 503, 411, 188, 446, 456, 370, 319, 203, 14, 494, 130, 198, 145, 281, 45, 153, 365, 473, 329, 124, 5, 173, 309, 374, 376, 441, 537, 174, 267, 484, 556, 8, 412, 216, 224, 45, 455, 412, 58, 88, 592, 564, 66, 96, 571, 74, 353, 180, 474, 0,
 };
      QuickSort(arr, 0, arr.Length - 1);

      for (int i = 0; i < arr.Length; i++)
        Console.Write(arr[i] + "\t");
      Console.WriteLine();
    }

    private static void QuickSort(int[] arr, int left, int right)
    {
      Console.WriteLine("QuickSort({0}, {1})", left, right);
      for (int i = 0; i < arr.Length; i++)
        Console.Write(arr[i] + "\t");
      Console.WriteLine();

      if (left < right)
      {
        int iPivot = Partition(arr, left, right); // 피봇의 인덱스
        Console.WriteLine(" pivot index = " + iPivot);
        QuickSort(arr, left, iPivot - 1);
        QuickSort(arr, iPivot + 1, right);
      }
    }

    private static int Partition(int[] arr, int left, int right)
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
            Console.WriteLine(".. in if index a[{0}] = a[{1}] = {2}", i, j, arr[i]);
            j--;
          }
        }
      }

      // 교차된 경우 j값을 리턴
      return j;
    }
  }
}