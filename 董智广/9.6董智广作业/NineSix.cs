using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1
{
    internal class NineSix
    {
        public void Start()
        {
            int[] arr = new int[30];
            Random r = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = r.Next(-100, 100);
            }

            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }

            NineSix a = new NineSix();
            a.ShellSort(arr);

            bool ison = true;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] > arr[i + 1])
                {
                    ison = false;
                }
            }
            Console.Write(ison);
        }


        public void ShellSort(int[] arr)
        {
            for (int gap = arr.Length; gap > 0; gap /= 2)
            {
                for (int i = gap; i < arr.Length; i++)
                {
                    int temp = arr[i];
                    int j = i - gap;

                    while (j >= 0 && temp < arr[j])
                    {
                        arr[j + gap] = arr[j];
                        j -= gap;
                    }
                    arr[j + gap] = temp;
                }
            }

            Console.WriteLine("_---------");
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
        }
    }
}
