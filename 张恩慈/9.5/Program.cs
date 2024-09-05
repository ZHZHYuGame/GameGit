using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 1, 2,5, 23, 6 3, 4, 6, 40;
            Console.WriteLine("--------------------原数组--------------------");
            foreach (int a in array)
            {
                Console.Write(a + ",");
            }
            Console.WriteLine();
            InsertSort(array);
            Console.WriteLine("--------------------插入排序--------------------");
            foreach (int a in array)
            {
                Console.Write(a + ",");
            }
            Console.WriteLine();
        }

        private static void InsertSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int insertVal = array[i];
                int insertIndex = i - 1;
                while (insertIndex >= 0 && insertVal < array[insertIndex])
                {
                    array[insertIndex + 1] = array[insertIndex];
                    insertIndex--;
                }
                array[insertIndex + 1] = insertVal;
            }
        }
    }
}
