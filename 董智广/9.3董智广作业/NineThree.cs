using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1
{
    internal class NineThree
    {
        Random r = new Random();
        int[] arr = new int[10];

        public void Start()
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = r.Next(-10, 100);
            }
            Console.WriteLine("本次随机到的10个数字为：");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length-1; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        arr[i] = arr[i] + arr[j];
                        arr[j] = arr[i] - arr[j];
                        arr[i] = arr[i] - arr[j];
                    }
                }
            }

            foreach (var item in arr)
            {
                Console.WriteLine("降序排序:"+item);
            }
            Console.WriteLine("-----------------");
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if (arr[i] < arr[j])
                    {
                        arr[i] = arr[i] + arr[j];
                        arr[j] = arr[i] - arr[j];
                        arr[i] = arr[i] - arr[j];
                    }
                }
            }

            foreach (var item in arr)
            {
                Console.WriteLine("升序排序:" + item);
            }
            Console.WriteLine("-----------------");

            int max = -1;
            
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i]>max)
                {
                    max = arr[i];
                }
            }
            Console.WriteLine("最大值:"+max);
            Console.WriteLine("-----------------");
            float num = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                num += arr[i];
            }
            Console.WriteLine("平均值："+num/arr.Length);
        }
    }
}
