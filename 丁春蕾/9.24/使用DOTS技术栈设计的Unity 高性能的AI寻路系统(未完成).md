- 需要的东西:
	- [[Burst编译器]]
	- [[Unity DOTS]]
	- 了解Voxel (体素)
	  collapsed:: true
		- a. 点云是三维空间(xyz坐标)点的集合。
		  b. 体素是3D空间的像素。量化的，大小固定的点云。每个单元都是固定大小和离散坐标。
		  c. mesh是面片的集合。
		  d. 多视图表示是从不同模拟视点渲染的2D图像集合。
	- 了解Volume(体积)
	  collapsed:: true
		- **Volume（体积）** 通常指的是一个三维空间区域，可能用于以下目的：
		- **空间划分**：将环境划分为多个体积，以便于管理和查询。每个体积可以包含导航信息、障碍物、可通行区域等。
		- **碰撞检测**：通过定义体积，可以有效地进行碰撞检测，判断代理是否与环境中的其他对象发生碰撞。
		- **区域查询**：在特定的体积内查询或者操作实体，例如查找进入某个区域的所有代理，或在某个区域内执行特定逻辑。
	- 快速的体素遍历算法
	  collapsed:: true
		- [grid.pdf (yorku.ca)](http://www.cse.yorku.ca/~amana/research/grid.pdf)
	- [[JobSystem]]
	- NativeArray使用的时候的坑:
		- 虽然是结构体但是本质上是共享内存的,一个修改全部修改;是结构体,但是为了减少赋值,引用了非托管代码,实现的原理:
		  collapsed:: true
			- ### 1.   **NativeArray 的结构体特性**
			- **值类型**：`NativeArray` 是一个结构体，因此在赋值时，默认会创建一个副本。
			- **内存内容**：虽然 `NativeArray` 是值类型，但它在内部持有对原始数据内存的引用。
			- ### 2.   **内存共享的机制**
			- **实现细节**：`NativeArray` 结构体内部包含一个指向实际数据的指针。这个指针指向的内存区域是分配在托管外的（通常是使用 `UnsafeUtility` 分配的）。
			- **副本的引用**：当您创建 `NativeArray` 的副本时，结构体的成员（如指针）被复制，但指向的数据内存区域是相同的。这意味着多个 `NativeArray` 实例会共享同一块内存空间。
			- ```cs
			  NativeArray<int> originalArray = new NativeArray<int>(5, Allocator.Persistent);
			  NativeArray<int> copyArray = originalArray;
			  	
			  // 修改其中一个副本
			  copyArray[0] = 10;
			  
			  // originalArray[0] 也会变为 10，因为它们共享同一内存
			  ```
		- 分配方式Allocator枚举有多种,区分区别:
- 如何把高性能计算移殖到MonoBehaviour,让DOTS能够为业务逻辑做辅助;
- ECS的工作流;
- 实现一个空间查询的系统:AgentSpatialPartitioningSystem
	- 设计思路:把代理划分为任意大小的单元格;这样可以更加有效率的查找最近代理;(通过多重hash对空间进行分区)
		- 系统里面新创建一个singleton的组件:该组件获取了系统里面的所有的成员变量;然后放在一个结构体里面;在系统更新的时候统一调用:
		  collapsed:: true
			- singleton里面的方法有:
				- QueryLine(寻找到达最快体素的算法)
					- **体素（Voxel）**：体素是三维空间中的一个立方体单元，类似于二维空间中的像素。它是体积的基本单位，用于表示三维空间中的数据。
- 通过DOTS实现高性能的导航碰撞/避障系统:
	- 代理的身体动作:AgentBody
		- 用来储存代理的力,速度,终点,remainingDistance,和控制代理的停止;
	- 实现一个标签组件记录代理的层级;AgentCollider
	- 实现一个处理代理碰撞的系统:
		- 成员字段:
			- 迭代次数(从ColliderSubSettings获取)
			- 一个SystemHandle;对AgentSpatialPartitioningSystem的引用:(作用介绍一下)
- 创建组件之前，需要考虑组件将存储的数据类型以及将在什么上下文中使用它。然后你可以决定使虍哪种组件类型来实现组件。这里需要理清楚数据布局(举个例子):
	- 需要的数据有哪些? 一个预制体;一个预制体需要被实例化的位置;预制体的大小;
	- 根据以上数据创建组件(继承IComponenetData,这里有可能被问到不同的组件类型的不同之处);
	- 下面需要考虑怎么烘焙,烘焙的过程:
	  collapsed:: true
		- (编辑器和ECS组件的值是如何传递的:
			- 通过Authoring Component;然后通过Baker把ECS组件添加到实体上;把创作组件的值填充到ECS组件里面;通过 Authoring 脚本，开发者可以在编辑器中设置数据，而 ECS 运行时则负责处理这些数据。
			- 使用Authoring COmponent本质上是为了过渡*:Authoring 脚本为 ECS 组件赋值的方式利用了 `MonoBehaviour`，但这实际上是 Unity 在推广 DOTS 过程中，为了更好地兼容现有工作流而采取的一种过渡策略。随着对 DOTS 的深入理解，开发者可以逐步过渡到更高效的数据导向编程模式。*
			- 然后就是使用系统来组织数据;--When you enter Play mode, Unity creates a world instance and adds every system to this default world.(创建世界的流程也可深入剖析一下;)
			- 这里需要注意Entity Graphic和Entities这两个可以分开使用;使用EG必须使用URP和HDRP
			-
	- 关于何时使用Job系统?
	  collapsed:: true
		- 如果系统不执行太多工作，例如，如果它只处理少量实体的组件数据，则并行调度作业的开销可能会超过多线程的性能提升。要了解某个作业是否属于这种情况，请使用 CPU 性能分析器来测量 Unity 在多线程和不多线程的情况下运行作业代码所需的时间。如果调度开销使 Unity 使用多线程运行作业代码需要更长的时间，请尝试以下操作来优化作业：
			- `foreach``SystemAPI.Query`
		-
		-
		-
-
- 主线程转移到工作线程的工作有哪些?
	- **路径查找的初始化和更新**：`NavMeshQueryJob` 结构体中的 `Execute` 方法负责执行路径查找逻辑。它在工作线程中处理路径的初始化、状态更新和最终路径结果的获取。
	- **查询状态管理**：路径查找的状态（如 `Allocated`、`InProgress`、`FinishedFullPath` 等）被管理，并且状态更新是异步进行的，这样可以避免在主线程上阻塞。
	- **路径结果获取**：从 `NavMeshQuery` 获取路径结果的操作也是在工作线程中执行的，减少了主线程的负担。
- 实现的方式:
	- 关于AgentsNavigationSettings
	  collapsed:: true
		- 一些Settings,以SO保存,通过PlayerSettings预加载获取;然后设置一个抽象ISubSettings作为AgentsNavigationSettings的子设置;
			- ColliderSubSettings(用于配置与碰撞处理相关的设置。)
				- 通过公开迭代次数,允许开发者配置碰撞解决的精度与性能之间的权衡。通过调整迭代次数，开发者可以在准确性和性能之间做出选择，以适应不同的游戏需求和场景复杂性。
			- NavMeshSubSettings(用于配置与导航网格（NavMesh）相关的设置，主要涉及路径寻找的参数。)
				- **最大迭代次数 (`m_MaxIterations`)**：
					- 指定代理在寻找路径时的最大迭代次数。每次迭代代表访问一个节点。更高的迭代次数可以提高路径的准确性，但会增加性能开销。
				- **每帧最大迭代次数 (`m_IterationsPerFrame`)**：
					- 指定代理在每帧中进行路径寻找的最大迭代次数。更大的值可以加快路径寻找速度，但同样会增加性能负担。
				- **最大路径大小 (`m_MaxPath`)**：
					- 定义代理路径的最大长度，以便限制路径的复杂性和防止过长路径导致的性能问题。
			- SpatialPartitioningSubSettings(AI 寻路中的空间划分设置相关,提供了配置和优化寻路算法所需的参数;)
			  collapsed:: true
				- **单元格大小 (`m_CellSize`)**：
					- 设置空间划分中每个单元格的大小。较小的单元格可以提供更高的精度，但可能增加计算开销。
				- **查询检查模式 (`m_QueryCheck`)**：
					- 定义在查找最近邻居时所检查的最大邻居数量。通过限制检查的邻居数量，可以优化寻路性能。
				- **查询容量 (`m_QueryCapacity`)**：
					- 指定在避碰或碰撞系统中要考虑的最大邻居数量。这个参数帮助管理在复杂场景中可能的碰撞和避免行为。
				- **层名称 (`m_Layers`)**：
					- 存储与导航层相关的名称，可能用于区分不同的对象或区域，以便在寻路时做出更合适的决策。
		- ![image.png](../assets/image_1727140539842_0.png)
		- 使用预加载把导航配置加载到内存里面;
	- NavMeshQueryStatus(几种查询状态)
	  collapsed:: true
		- **None**：
			- 表示没有进行任何查询。
		- **Allocated**：
			- 表示为路径分配了内存，但尚未开始计算。
		- **InProgress**：
			- 表示路径计算正在进行中。
		- **Failed**：
			- 表示路径构建失败，可能是由于目标不可达或者其他原因。
		- **Finished**（已过时）：
			- 这个状态已被标记为过时，不再推荐使用。原本表示路径计算已完成。
		- **FinishedFullPath**：
			- 表示完整路径已成功计算，目标是可达的。
		- **FinishedPartialPath**：
			- 表示部分路径已成功计算，虽然未能完全到达目标，但路径足够接近，可以进行后续处理。
	- 寻路算法的实现:NavMeshPathSystem
	-
- 使用的方式:
	-
- 最佳实践:
	- 一个系统里面有多个成员变量的时候:使用一个singleton进行打包;提升内存的连续性;
-
- 管理组件的方法:
	- 添加组件的几种方式:
		- 添加组件到实体
		- 添加组件到系统;
	- 删除组件的方式:
		- ```cs
		  // 动态添加组件
		  entityManager.AddComponentData(entity, new MyComponent { Value = 20f });
		  
		  // 动态移除组件
		  entityManager.RemoveComponent<MyComponent>(entity);
		  ```
- [[DOTS组件接口]]:
- [[DOTS特性]]
- [[DOTS查询方法]]
- 获取的方式:
	- [Samples (lukaschod.github.io)](https://lukaschod.github.io/agents-navigation-docs/manual/samples.html)
	- 包下载到本地了;以后可以玩玩付费插件,通过在GItHub查源码来获取Package