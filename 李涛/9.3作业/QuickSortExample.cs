using System;
using UnityEngine;

public class QuickSortExample : MonoBehaviour
{
    public int[] array = new int[] { 9, 2, 6, 4, 3, 5, 1 };

    void Start()
    {
        // 打印原始数组
        Debug.Log("Original array: " + String.Join(", ", array));

        // 进行快速排序
        QuickSort(array, 0, array.Length - 1);

        // 打印排序后的数组
        Debug.Log("Sorted array: " + String.Join(", ", array));
    }

    void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            // 获取分区后的位置
            int pivotPosition = Partition(arr, low, high);

            // 递归地对分区前的部分进行快速排序
            QuickSort(arr, low, pivotPosition - 1);

            // 递归地对分区后的部分进行快速排序
            QuickSort(arr, pivotPosition + 1, high);
        }
    }

    int Partition(int[] arr, int low, int high)
    {
        // 选择最右边的元素作为基准
        int pivot = arr[high];
        int i = (low - 1);

        for (int j = low; j < high; j++)
        {
            // 如果当前元素小于或等于pivot
            if (arr[j] <= pivot)
            {
                i++;

                // 交换arr[i]和arr[j]
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        // 交换arr[i+1]和arr[high]（或pivot）
        int temp2 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp2;

        return i + 1;
    }
}