<Query Kind="Program">
  <NuGetReference>Microsoft.Office.Interop.Excel</NuGetReference>
  <NuGetReference>Microsoft.Office.Interop.Outlook</NuGetReference>
  <NuGetReference>Microsoft.Office.Interop.PowerPoint</NuGetReference>
  <NuGetReference>Microsoft.Office.Interop.Word</NuGetReference>
  <NuGetReference>R3</NuGetReference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>R3</Namespace>
  <Namespace>R3.Collections</Namespace>
  <Namespace>R3.Internal</Namespace>
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>System.Reactive</Namespace>
  <Namespace>System.Reactive.Concurrency</Namespace>
  <Namespace>System.Reactive.Disposables</Namespace>
  <Namespace>System.Reactive.Joins</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
  <Namespace>System.Reactive.PlatformServices</Namespace>
  <Namespace>System.Reactive.Subjects</Namespace>
  <Namespace>System.Reactive.Threading.Tasks</Namespace>
  <Namespace>System.Reactive.Windows.Foundation</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Windows.Forms.DataVisualization.Charting</Namespace>
  <Namespace>Windows.System.Threading</Namespace>
</Query>

class Program
{
	static void Main(string[] args)
	{
		// 定义一个整型数组，用于测试冒泡排序  
		int[] arr = { 64, 34, 25, 12, 22, 11, 90 };

		// 调用冒泡排序函数  
		BubbleSort(arr);

		// 打印排序后的数组  
		Console.WriteLine("Sorted array:");
		for (int i = 0; i < arr.Length; i++)
		{
			Console.Write(arr[i] + " ");
		}
	}

	// 冒泡排序的函数实现  
	static void BubbleSort(int[] arr)
	{
		int n = arr.Length;
		bool swapped; // 用于标记一轮排序中是否发生了交换  

		// 外层循环遍历所有数组元素  
		for (int i = 0; i < n - 1; i++)
		{
			swapped = false; // 重置swapped标志，每一轮开始时都假设没有发生交换  

			// 内层循环进行相邻元素的比较和可能的交换  
			for (int j = 0; j < n - i - 1; j++)
			{
				// 如果当前元素大于下一个元素，则交换它们  
				if (arr[j] > arr[j + 1])
				{
					// 交换arr[j]和arr[j+1]  
					int temp = arr[j];
					arr[j] = arr[j + 1];
					arr[j + 1] = temp;

					// 标记发生了交换  
					swapped = true;
				}
			}

			// 如果这一轮没有发生任何交换，说明数组已经有序，可以提前结束排序  
			if (!swapped)
				break;
		}
	}
}