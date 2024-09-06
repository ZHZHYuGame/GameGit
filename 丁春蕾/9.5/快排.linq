<Query Kind="Statements">
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
	static void Swap(int[] nums, int i, int j)
	{
		int temp = nums[i];
		nums[i] = nums[j];
		nums[j] = temp;
	}

	static int Partition(int[] nums, int left, int right)
	{
		int pivot = nums[left];
		int i = left + 1, j = right;
		while (true)
		{
			while (i <= j && nums[j] >= pivot) j--;
			while (i <= j && nums[i] <= pivot) i++;
			if (i > j) break;
			Swap(nums, i, j);
		}
		Swap(nums, left, j);
		return j;
	}

	static void QuickSort(int[] nums, int left, int right)
	{
		if (left < right)
		{
			int pivotIndex = Partition(nums, left, right);

			// 递归地对基准数左右两边的子数组进行排序  
			QuickSort(nums, left, pivotIndex - 1);
			QuickSort(nums, pivotIndex + 1, right);
		}
	}

	static void Main(string[] args)
	{
		int[] nums = { 10, 7, 8, 9, 1, 5 };
		QuickSort(nums, 0, nums.Length - 1);

		foreach (int num in nums)
		{
			Console.Write(num + " ");
		}
	}
}