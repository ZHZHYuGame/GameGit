using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1
{
    internal class NineFour
    {
        // 启动方法
        public void Start()
        {
            // 创建一个长度为 30 的整数数组
            int[] arr = new int[30];
            // 创建一个随机数生成器
            Random r = new Random();
            // 使用循环为数组随机赋值
            for (int i = 0; i < arr.Length; i++)
            {
                // 生成 -10 到 99 之间的随机整数赋值给数组元素
                arr[i] = r.Next(-10, 100);
            }
            // 遍历数组并输出每个随机生成的数
            foreach (var item in arr)
            {
                Console.WriteLine("随机出的数：" + item);
            }

            // 创建当前类的实例
            NineFour nn = new NineFour();
            // 调用插入排序方法对随机生成的数组进行排序
            nn.InsterArr(arr);
        }

        // 插入排序方法
        public void InsterArr(int[] arr)
        {
            // 用于临时存储待插入的元素
            int temp;
            // 用于遍历已排序部分的索引
            int j;
            // 从数组的第二个元素开始遍历
            for (int i = 1; i < arr.Length; i++)
            {
                // 将当前元素存储在临时变量中
                temp = arr[i];
                // 设置 j 为当前元素的前一个位置
                j = i - 1;
                // 当 j 大于等于 0 且临时变量小于当前 j 位置的元素时，将较大的元素向后移动一位
                while (j >= 0 && temp < arr[j])
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                // 将临时变量插入到正确的位置
                arr[j + 1] = temp;
            }
            // 输出提示信息
            Console.WriteLine("插入排序后：");
            // 遍历排序后的数组并输出每个元素
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
        }
    }
}