- 阅读前需要了解,.NET(老的Mono)的垃圾回收机制;Unity的垃圾回收机制;了解Unity的内存管理细节;非托管内存的内存释放的问题
- 内存优化的核心问题操作
	- 减少过多的堆内存分配和释放;
	- 避免内存的碎片化,和避免内存泄漏的问题;
	- 协调托管和非托管内存的释放的问题。(非托管内存的内存释放的问题需要提前了解)
	- 解决游戏运行时候的因为GC问题导致的内存抖动的问题;
	- 内存问题的监控;
- 最佳实践有哪些?
	- 合理利用托管内存的垃圾回收
	  collapsed:: true
		- 1. Start（） 或 Awake（） 期间捕获任何引用，并在以后的函数（如 Update（） 或 LateUpdate（））中重复使用。
		- 使用 StringBuilder(Zstring) C# 类在运行时动态生成复杂字符串
		- 删除不再需要的 Debug.Log（） 调用，因为它们仍会在应用程序的所有构建版本中执行
		- 游戏如果对内存需求比较大的时候,根据Unity拓展堆内存的特性,可以对堆内存进行提前的扩大,即在游戏加载的阶段分配内存,提前`System.GC.Collect（）`调用
	- 避免昂贵的操作
	  id:: 66ed3411-6872-40f9-a49d-cf3a3e0af1ae
	  collapsed:: true
		- 避免使用Linq([[在Unity里面使用Linq和Lambda的时候需要注意的地方]]),手动编写高效算法减少CPU和GC;
		- 避免昂贵的unity API,可以使用一些无GC的API
		  collapsed:: true
			- 大多数涉及搜索整个场景图以查找一些匹配的游戏对象列表,可以通过缓存引用或为游戏对象实现管理器组件以在运行时跟踪引用来避免在游戏运行时这些操作。
			- ```cs
			  GameObject.SendMessage()
			      GameObject.BroadcastMessage()//比正常的函数调用慢1000倍;
			      UnityEngine.Object.Find()
			      UnityEngine.Object.FindWithTag()
			      UnityEngine.Object.FindObjectOfType()
			      UnityEngine.Object.FindObjectsOfType()
			      UnityEngine.Object.FindGameObjectsWithTag()
			      UnityEngine.Object.FindGameObjectsWithTag()
			  ```
		- 避免不必要的拆装箱
		-
	- 缓存引用或者是利用对象池优化(也可放在CPU优化的地方)
	  collapsed:: true
		- 成本相对于存储指针的内存成本，重复函数调用（如 GetComponent<T>（） 和 Camera.main）的成本更高。
		  collapsed:: true
			- Camera.main 只在下方使用 FindGameObjectsWithTag（），这会在场景图中搜索带有“MainCamera”标签的摄像机对象，成本很高。
			- 避免 GetComponent（string）
			  使用 GetComponent（） 时，存在一些不同的重载。请务必始终使用基于类型的实现，而不是使用基于字符串的搜索重载。在场景中按字符串搜索比按 Type 搜索成本要高得多。
			  （好） 组件 GetComponent（类型类型）/（好） T GetComponent<T>（）
			  （坏） 组件 GetComponent（字符串）
		- 关于对象池:(其实就是缓存优化来解决内存分配的核心问题)
			- 对象池是降低连续对象分配和解除分配成本的常用技术。通过分配大量相同对象并重用此池中的非活动可用实例来实现的，而不是随着时间的推移不断生成和销毁对象。对象池非常适合在应用程序期间具有可变生命周期的可重用组件。
	- ((66ed3d87-737d-4d93-99c3-aa1f45c7665f))
	- 对垃圾回收器进行操作;规避垃圾回收器带来的内存抖动的风险
	  collapsed:: true
		- **禁用垃圾收集**：在需要减少垃圾收集引起的CPU使用率激增时使用，但需小心管理内存。
		- **快速和频繁进行垃圾收集**：适合频繁分配小块内存的游戏，通过定期请求垃圾收集来减少性能影响。前提是如果堆内存特别小的话
		- **慢速但不频繁进行垃圾收集**：适合内存分配较少的游戏，通过手动扩展堆来减少垃圾收集频率。
		- 使用增量垃圾收集(渐进式垃圾回收)简单讲就是将垃圾收集过程分散到多个帧中。
- 关于增量式垃圾回收(除了WEBGL之外所有的平台都支持增量垃圾回收)
	- 特点
	  collapsed:: true
		- 增量式垃圾收集是 Unity 使用的默认垃圾收集方法。Unity 仍然使用 Boehm–Demers–Weiser 垃圾收集器，但是以增量模式运行。Unity 不会在每次运行时进行完整的垃圾收集，而是将垃圾收集工作负载拆分到多个帧中。这意味着，不必单次长时间中断程序的执行来让垃圾收集器完成工作，Unity 会进行多次短时间的中断。虽然这不能整体上加快垃圾收集速度，但将工作负载分布到多个帧可以极大减少垃圾收集“尖峰”破坏应用程序流畅性的问题。
		- 启用垂直同步之后,主线程在等待渲染线程更新完成之前的时候,应用程序使用 Vsync 或 Application.targetFrameRate，Unity 根据剩余的可用帧时间来调整分配给垃圾收集的时间。Unity 可以及时运行垃圾收集（否则需要等待），从而以最小的性能影响执行垃圾收集。
		- 如果将 VSync Count 设置为 Don’t Sync 之外的选项（在项目的 Quality 设置中或通过 Application.VSync 属性），或者启用 Application.targetFrameRate 属性），则 Unity 会自动使用给定帧末尾剩余的空闲时间来进行增量式垃圾收集。
	- 优点
	  collapsed:: true
		- 增量垃圾收集可以减轻垃圾收集尖峰的问题。
	- 缺点
	  collapsed:: true
		- 增量垃圾收集中断工作时，它将中断标记阶段（该阶段扫描所有托管对象以确定哪些对象仍在使用中以及可以清除哪些对象）。当对象之间的大多数引用在工作片段之间不变时，拆分标记阶段没有问题。对象引用会改变时，必须在下一次迭代中再次扫描那些对象。因此，太多的更改会使增量垃圾回收器不堪重负，并导致标记遍历永远不能完成，因为它总是有更多的工作要做；在这种情况下，垃圾收集会退回到进行完整的非增量收集。
		- 此外，在使用增量垃圾收集时，只要引用发生更改，Unity 就需要生成其他代码（称为写屏障）来通知垃圾收集（因此垃圾收集将知道是否需要重新扫描对象）。更改引用时，这会增加一些开销，可能会对某些托管代码产生明显的性能影响。
		- 尽管如此，大多数典型的 Unity 项目（如果有这样的“典型”Unity 项目）仍可从增量垃圾收集中受益，尤其是项目遭受垃圾收集尖峰时。
	- 解决方式:
	  collapsed:: true
		- 始终使用性能分析器来验证您的游戏或程序是否按预期执行
- [[Unity中的内存管理]]
-
- Reference
	- [了解自动内存管理 - Unity 手册](https://docs.unity.cn/cn/2021.1/Manual/UnderstandingAutomaticMemoryManagement.html#incremental_gc)