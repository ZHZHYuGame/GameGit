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


int [] a=[1,3,42,3,4,5];

Sort(a);

a.Dump();


void Sort(int[] array)
{
	int n = array.Length;
	for (int i = 1; i < n; ++i)
	{
		int key = array[i]; // 将当前元素作为待插入的元素
		int j = i - 1;

		// 将大于key的元素向后移动一位
		while (j >= 0 && array[j] > key)
		{
			array[j + 1] = array[j];
			j = j - 1;
		}
		array[j + 1] = key;
	}
}