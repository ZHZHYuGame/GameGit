using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 每天一个算法
{
    //冒泡排序
    class Program
    {
        static void Main()
        {
            int[] array = { 5, 2, 8, 1, 9 };
            Console.WriteLine("原始数组：");
            PrintArray(array);

            BubbleSort(array);

            Console.WriteLine("排序后的数组：");
            PrintArray(array);
            Console.ReadKey();
        }

        static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }

        static void PrintArray(int[] arr)
        {
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
    }
}
