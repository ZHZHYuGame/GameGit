using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1
{
    internal class NineFive
    {
        /// <summary>
        /// 启动方法，用于初始化数组、打印原始数组、排序数组并验证排序结果
        /// </summary>
        public void Start()
        {
            int[] arr = new int[30]; // 创建一个长度为30的整型数组
            Random r = new Random(); // 创建一个Random对象用于生成随机数

            // 填充数组，每个元素的值在-10到100之间
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = r.Next(-10, 100); // 生成一个范围在-10到100的随机数
            }

            // 打印数组中的所有元素
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
            Console.WriteLine("-------------"); // 打印分隔线

            // 创建NineFive的一个实例
            NineFive a = new NineFive();
            // 对数组进行选择排序
            a.SelectSort(arr);

            // 验证数组是否已经排序
            bool isSorted = true; // 初始化为已排序
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] > arr[i + 1]) // 如果当前元素大于下一个元素，则未排序
                {
                    isSorted = false;
                    break; // 退出循环
                }
            }
            // 输出数组是否已排序的结果
            Console.Write(isSorted);
        }

        /// <summary>
        /// 使用选择排序算法对数组进行排序
        /// </summary>
        /// <param name="A">要排序的数组</param>
        public void SelectSort(int[] A)
        {
            // 外层循环遍历每个元素，找到最小值并放到当前索引位置
            for (int i = 0; i < A.Length - 1; i++)
            {
                int index = i; // 假设当前位置的元素是最小的
                // 内层循环找到从当前位置到数组末尾的最小元素
                for (int j = i + 1; j < A.Length; j++)
                {
                    if (A[index] > A[j]) // 如果找到比当前最小元素更小的值
                    {
                        index = j; // 更新最小元素的索引
                    }
                }
                // 如果找到的最小元素不在当前位置，则交换两个元素
                if (index != i)
                {
                    // 使用加减法交换两个元素的值
                    A[index] = A[index] + A[i];
                    A[i] = A[index] - A[i];
                    A[index] = A[index] - A[i];
                }
            }

            // 打印排序后的数组
            foreach (var item in A)
            {
                Console.WriteLine(item);
            }
        }
    }
}
