- Cpu优化需要解决的核心问题:
	- 就是减少CPU处理单个问题的时钟的处理时间,增加处理次数;
	- 减少程序的查找,和不必要的遍历;
	- 避免CPU不必要的重复计算;
	- 避免CPU瓶颈对游戏造成卡顿;
- 最佳实践
	- ((66ed3411-6872-40f9-a49d-cf3a3e0af1ae))
	- 缓存引用,不要一直get查找,特别浪费性能;特别是在自己封装的框架里面使用的时候也要注意;
	- 减少Unity 在非托管和托管代码边界之间以及 UnityEngine 代码和应用程序代码之间来回运行的次数;通过此桥接进行上下文切换的成本相当高，即使没有要执行的内容。解决方式有以下:
	  collapsed:: true
		- 减少多个脚本的Update的轮询;尽量使用一个MonoBehaviour来储存所有的游戏逻辑;那么可以减少该生命周期函数的回调次数;
		- 减少空的生命周期函数的出现;
		  collapsed:: true
			- ```cs
			  FixedUpdate（）、LateUpdate（）、OnPostRender“、OnPreRender（）、OnRenderImage（）Update ()等
			  ```
		- 使用单例来操作每一帧的时候必须要使用的API比如`UnityEngine.Physics.Raycast()UnityEngine.Physics.RaycastAll()`这样可以避免在同一帧内多个组件可以使用相同的射线检测的结果,避免CPU不必要的重复计算;(既能减少GC也能减少CPU的压力)
		  id:: 66ed3d87-737d-4d93-99c3-aa1f45c7665f
	- 避免使用接口和虚拟构造
		- 通过接口调用函数调用或调用虚函数通常比使用直接构造或直接函数调用的成本要高得多。如果虚拟功能或接口是不必要的，则应将其删除。
		- 如果使用这些方法可以简化开发协作、代码可读性和代码可维护性，那么这些方法的性能损失是值得的。
		- ，建议不要将字段和函数标记为 virtual，除非明确预期需要覆盖此成员。对于每帧调用多次甚至每帧调用一次的高频代码路径
	- 避免按照值传递结构体???
		- 结构是值类型，当直接传递给函数时，它们的内容会被复制到新创建的实例中。此副本会增加 CPU 成本，以及堆栈上的额外内存。对于小型结构，效果最小，因此可以接受。但是，对于每帧重复调用的函数以及采用大型结构体的函数，请尽可能修改函数定义以按引用传递。
		- [方法参数 - C# 参考 |Microsoft 学习](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/method-parameters)
	- 物理方面
		- 通常，改进物理效果的最简单方法是限制在 Physics 上花费的时间或每秒的迭代次数。这将降低模拟精度。请参阅 Unity 中的 [TimeManager](https://docs.unity3d.com/Manual/class-TimeManager.html)
		  collapsed:: true
			- Unity的物理模拟是以固定时间步(Fixed Timestep)进行的;由TimeManager控制;所以TimeManager直接影响PhysX每秒运行物理更新的频率;
			- #### a)  **固定时间步（Fixed Timestep）**
			  collapsed:: true
				- `Fixed Timestep` 是 Unity 物理引擎的一个参数，它决定了物理系统每秒更新的频率。默认情况下，Unity 的物理更新频率是 **0.02 秒**（即每秒 50 次物理更新）。
				- 每当游戏逻辑运行时，物理引擎会按照这个频率进行物理更新。如果频率较高，物理引擎每秒执行更多次的物理模拟，**模拟的精度也就更高**，物体的运动和碰撞响应会更加流畅且准确。
			- #### b)  **降低模拟精度的原因**
			  collapsed:: true
				- 当你减少 `Fixed Timestep`（例如设置为更大的值，如 0.05 秒），物理更新的频率会降低，Unity 每秒进行的物理模拟次数减少。这会带来两种后果：
				  collapsed:: true
					- **性能提升**：因为每秒计算的次数减少了，CPU 不需要频繁计算物体的运动和碰撞，这会提高整体游戏的性能。
					- **模拟精度降低**：由于更新次数的减少，物体的位置、速度等计算结果会变得不够精确，特别是在高速运动或复杂碰撞的情况下，物理模拟可能变得不太准确，甚至可能出现穿透、跳动等问题。
		- Unity 中的碰撞体类型具有截然不同的性能特征。以下顺序从左到右列出了性能最高的碰撞体到性能最低的碰撞体。避免使用网格碰撞体很重要，因为网格碰撞体的成本要比原始碰撞体高得多。
		- 球体 < 胶囊体 < 盒体 <<< 网格 （凸面） < 网格 （非凸面）
		  collapsed:: true
			- 为什么球体等原始碰撞体的性能更高?
			  collapsed:: true
				- **球体、盒体和胶囊体**都是几何上简单的形状，它们的数学公式易于计算。物理引擎在处理这些形状的碰撞检测时，能够通过简单的数学运算快速确定物体是否发生碰撞。例如：
				  collapsed:: true
					- 球体的碰撞检测只需要计算两球心之间的距离是否小于两球的半径之和。
					- 盒体的碰撞检测可以通过简单的坐标系对齐来判断。
					- 胶囊体的碰撞检测虽然稍微复杂，但仍然依赖于数学模型，计算成本相对较低。
					  
					  **因此，球体、盒体、胶囊体等原始碰撞体占用的计算资源少，性能更高**。
			- 为什么网格碰撞体的性能最低?
			  collapsed:: true
				- **网格碰撞体**（Mesh Collider）是一种基于三角形网格的碰撞体，它可以用来表示任意复杂的形状。由于它包含大量的顶点和面，碰撞检测的复杂度会大幅增加：
				- **凸面网格**（Convex Mesh）限制了碰撞体的形状为凸多面体，这样引擎可以在碰撞检测时使用更优化的算法，但计算成本仍比球体、盒体等简单几何形状高。
				- **非凸面网格**（Non-Convex Mesh）没有形状的限制，可以表示任意形状。非凸面网格的碰撞检测非常复杂，因为引擎必须检查每个面和顶点的相交情况。这种形状的计算复杂度很高，因此性能消耗最大。
			- 碰撞体性能是由什么决定的?
			  collapsed:: true
				- 几何复杂度,决定了算法的复杂度
				- 物理引擎对碰撞体算法的优化能力
		- 针对单个对象的高效的光线碰撞检测:
		  collapsed:: true
			-
		- reference
		  collapsed:: true
			- https://unity3d.com/learn/tutorials/topics/physics/physics-best-practices
			- https://docs.unity3d.com/Manual/class-TimeManager.html
			- https://illogika-studio.gitbooks.io/unity-best-practices/content/physics-rigidbody-interpolation-and-fixedtimestep.html
			- https://www.toptal.com/unity-unity3d/tips-and-practices
		-
	- 动画方面
	  collapsed:: true
		- 通过禁用 Animator 组件来禁用空闲动画（禁用游戏对象不会产生相同的效果）。避免 Animator 位于循环中将值设置为相同事物的设计模式。此技术有相当大的开销，对应用程序没有影响。
		- [Unity - Manual: Performance and optimization (unity3d.com)](https://docs.unity3d.com/Manual/MecanimPeformanceandOptimization.html)
	- 复杂算法
	  collapsed:: true
		- 如果您的应用程序使用复杂的算法，例如反向运动学、路径查找等，请寻找一种更简单的方法或调整相关设置以提高性能(可以研究清楚讲解以下)
	-