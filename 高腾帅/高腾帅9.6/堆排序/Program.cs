using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heapify
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                list.Add(random.Next(100));
                Console.Write(list[i] + ",");
            }
            Console.WriteLine("\n");

            HeapSort(list);

            foreach (var i in list)
            {
                Console.Write(i + ",");
            }
            Console.WriteLine("\n");
        }

        /// <summary>
        /// 堆排序，把最大值放到集合最后
        /// </summary>
        /// <param name="list">需要排序的集合</param>
        private static void HeapSort(List<int> list)
        {
            // list.Count/2-1 是满二叉树最后一个非叶子结点（索引），左右叶子索引 [2*i+1] [2*i+2]
            for (int i = list.Count; i > 1; i--)
            {
                AdjustHeap(list, i / 2 - 1, i);
                list[0] = list[i - 1] + list[0];
                list[i - 1] = list[0] - list[i - 1];
                list[0] = list[0] - list[i - 1];
            }
        }

        /// <summary>
        /// 调整堆，把最大值放到堆顶
        /// </summary>
        /// <param name="list">把集合中的最大值放到堆顶</param>
        /// <param name="parent">满二叉树中最后一个非叶子节点</param>
        /// <param name="length">需要比较最大值的集合长度</param>
        private static void AdjustHeap(List<int> list, int parent, int length)
        {
            for (int i = parent; i >= 0; i--)
            {
                if (list[2 * i + 1] > list[i])
                {
                    list[i] = list[2 * i + 1] + list[i];
                    list[2 * i + 1] = list[i] - list[2 * i + 1];
                    list[i] = list[i] - list[2 * i + 1];
                }
                // 完全二叉树中节点的右节点不一定存在
                if (2 * i + 2 <= length - 1 && list[2 * i + 2] > list[i])
                {
                    list[i] = list[2 * i + 2] + list[i];
                    list[2 * i + 2] = list[i] - list[2 * i + 2];
                    list[i] = list[i] - list[2 * i + 2];
                }
            }
        }
    }
}
