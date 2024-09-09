using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1
{
    internal class NineEight
    {
        public void Start()
        {
            // 创建一个长度为30的整型数组
            int[] arr = new int[30];
            Random r = new Random();

            // 用随机数填充数组，范围从-10到99
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = r.Next(-10, 100);
            }

            // 打印未排序的数组
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }

            // 创建一个NineEight对象，并对数组进行归并排序
            NineEight a = new NineEight();
            a.MergeSort(arr, 0, arr.Length);

            // 打印排序后的数组
            Console.WriteLine("-------------------");
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
        }

        // 归并排序的核心函数
        public void MergeSort(int[] A, int lo, int hi) // 左闭右开区间[lo, hi)
        {
            // 递归的基准条件：如果区间长度小于2，则无需排序
            if (hi - lo < 2)
            {
                return; // 递归退出条件，只有一个元素无需排序
            }

            // 计算中间位置
            int middle = (lo + hi) >> 1;

            // 递归排序左半部分
            MergeSort(A, lo, middle);

            // 递归排序右半部分
            MergeSort(A, middle, hi);

            // 合并已排序的两部分
            MergeSortedArray(ref A, lo, hi);
        }

        // 合并两个已排序的子数组
        private void MergeSortedArray(ref int[] a, int lo, int hi)
        {
            // 创建一个临时数组用于存储合并结果
            int[] temp = new int[hi - lo];
            int i = lo;
            int j;

            // 将原数组中的元素拷贝到临时数组中
            for (j = 0; j < temp.Length; j++)
            {
                temp[j] = a[i++];
            }

            // 初始化临时数组的中间位置
            j = 0;
            int k = temp.Length >> 1;
            int middle = temp.Length >> 1;

            // 合并两个已排序的部分
            // 比较前半部分和后半部分，将小的元素赋值回原数组的对应位置
            while (j < middle && k < temp.Length)
            {
                if (temp[j] <= temp[k])
                {
                    a[lo++] = temp[j++];
                }
                else
                {
                    a[lo++] = temp[k++];
                }
            }

            // 将前半部分中剩余的元素拷贝回原数组
            while (j < middle)
            {
                a[lo++] = temp[j++];
            }

            // 将后半部分中剩余的元素拷贝回原数组
            while (k < temp.Length)
            {
                a[lo++] = temp[k++];
            }
        }
    }
}
