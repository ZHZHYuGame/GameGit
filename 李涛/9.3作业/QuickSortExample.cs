using System;
using UnityEngine;

public class QuickSortExample : MonoBehaviour
{
    public int[] array = new int[] { 9, 2, 6, 4, 3, 5, 1 };

    void Start()
    {
        // ��ӡԭʼ����
        Debug.Log("Original array: " + String.Join(", ", array));

        // ���п�������
        QuickSort(array, 0, array.Length - 1);

        // ��ӡ����������
        Debug.Log("Sorted array: " + String.Join(", ", array));
    }

    void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            // ��ȡ�������λ��
            int pivotPosition = Partition(arr, low, high);

            // �ݹ�ضԷ���ǰ�Ĳ��ֽ��п�������
            QuickSort(arr, low, pivotPosition - 1);

            // �ݹ�ضԷ�����Ĳ��ֽ��п�������
            QuickSort(arr, pivotPosition + 1, high);
        }
    }

    int Partition(int[] arr, int low, int high)
    {
        // ѡ�����ұߵ�Ԫ����Ϊ��׼
        int pivot = arr[high];
        int i = (low - 1);

        for (int j = low; j < high; j++)
        {
            // �����ǰԪ��С�ڻ����pivot
            if (arr[j] <= pivot)
            {
                i++;

                // ����arr[i]��arr[j]
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        // ����arr[i+1]��arr[high]����pivot��
        int temp2 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp2;

        return i + 1;
    }
}