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
	static void MergeSort(int[] array, int left, int right)
	{
		if (left < right)
		{
			int mid = (left + right) / 2;

			// 递归排序左半部分
			MergeSort(array, left, mid);
			// 递归排序右半部分
			MergeSort(array, mid + 1, right);
			// 合并已排序的子数组
			Merge(array, left, mid, right);
		}
	}

	static void Merge(int[] array, int left, int mid, int right)
	{
		int n1 = mid - left + 1;
		int n2 = right - mid;

		int[] LeftArray = new int[n1];
		int[] RightArray = new int[n2];

		// 将数据复制到临时数组
		Array.Copy(array, left, LeftArray, 0, n1);
		Array.Copy(array, mid + 1, RightArray, 0, n2);

		int i = 0, j = 0, k = left;

		// 合并临时数组
		while (i < n1 && j < n2)
		{
			if (LeftArray[i] <= RightArray[j])
			{
				array[k] = LeftArray[i];
				i++;
			}
			else
			{
				array[k] = RightArray[j];
				j++;
			}
			k++;
		}

		// 复制剩余元素
		while (i < n1)
		{
			array[k] = LeftArray[i];
			i++;
			k++;
		}

		while (j < n2)
		{
			array[k] = RightArray[j];
			j++;
			k++;
		}
	}

	static void Main()
	{
		int[] array = { 38, 27, 43, 3, 9, 82, 10 };
		Console.WriteLine("原数组: " + string.Join(", ", array));

		MergeSort(array, 0, array.Length - 1);

		Console.WriteLine("排序后数组: " + string.Join(", ", array));
	}
}