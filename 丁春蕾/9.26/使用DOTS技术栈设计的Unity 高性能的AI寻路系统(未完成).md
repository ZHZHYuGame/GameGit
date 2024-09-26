- 开发前需要了解到的的东西:
	- [[Prompts]]
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
	- [[Unity AINavigation]]
	- NativeArray使用的时候的坑:
	  collapsed:: true
		- [[Unity.Collections]]
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
		  collapsed:: true
			- ### 1.   `Allocator.Persistent`
			- **定义**：分配的内存会在应用程序的整个生命周期内存在，直到显式释放。
			- **作用**：适用于需要长时间使用的数据，例如游戏对象的状态、配置数据等。
			- **注意**：使用后必须手动释放内存，以避免内存泄漏。
			- ### 2.   `Allocator.Temp`
			- **定义**：分配的内存会在当前帧结束时自动释放。
			- **作用**：适用于短期使用的数据，例如临时计算或处理的数据。
			- **注意**：不能在多个帧内使用，因为它会在当前帧结束时被释放。
			- ### 3.   `Allocator.TempJob`
			- **定义**：分配的内存会在当前作业完成后自动释放。
			- **作用**：适用于在作业中使用的临时数据，能够保证在作业执行期间内存有效。
			- **注意**：与 `Allocator.Temp` 类似，但适合于作业系统，确保在作业完成后才释放内存。
			- ### 4.   `Allocator.None`
			- **定义**：不分配任何内存。
			- **作用**：用于表示未分配的状态，通常在条件下不需要使用 `NativeArray` 时使用。
			- **注意**：在使用 `Allocator.None` 时，不能尝试访问或释放内存。
			- ### 5.   `Allocator.Malloc`
			- **定义**：使用手动内存分配。
			- **作用**：适合在底层管理内存时使用，允许更细粒度的控制。
			- **注意**：需要开发者自行管理内存的分配和释放，适合高级用户。
			- ### 6.   `Allocator.Invalid`
			- **定义**：无效分配类型。
			- **作用**：用于指示一个无效或未定义的分配状态。
			- **注意**：通常不应在正常的内存分配中使用。
			- ### 7.   `Allocator.Default`
			- **定义**：使用默认的分配器。
			- **作用**：根据上下文使用适当的分配器。
			- **注意**：适合希望简化内存管理的场景。
		- 在选择合适的 `Allocator` 时，考虑以下因素：
		  collapsed:: true
			- **数据生命周期**：选择 `Persistent` 适合长期数据，`Temp` 和 `TempJob` 适合临时数据。
			- **性能需求**：适当使用 `Temp` 和 `TempJob` 可以减少内存开销，提高性能。
			- **内存管理复杂性**：`Malloc` 提供更多控制，但增加了内存管理的复杂性。
	- 了解螺旋搜索算法;
- 系统开发的原因(待完成):
  collapsed:: true
	- 为什么选择DOTS?(以下回答需要基于对导航计算的优化在哪里细节处请梳理明白)
	  collapsed:: true
		- 选择Burst的原因:
		- 选择NativeCollections的原因:
		- 选择JobSystem的原因:
	- 为什么你的导航系统更符合你们的SLG项目:
	  collapsed:: true
		- 这里可以介绍设计思路:
			- 为什么不选择AStar?
			- 为什么不使用Unity自带的导航系统?
				- unity AI,动态更新能力有限，生成耗时,	依赖预计算网格，内存消耗大,内置功能较为固定，扩展性有限;
			- 自己设计的导航系统需要满足项目的什么需求?
			- 为什么选择DOTS?
	- 架构设计:
	  collapsed:: true
		- 使用Scriptable配置导航网格的生成信息;
		- 使用Burst实现
		- 通过场景几何生成导航网格（NavMesh），用于表示可行走区域。
		- 采用 **高效的空间划分** 技术（如四叉树或八叉树）来优化导航网格的查询和更新效率。
		- 支持 **动态更新**，即在场景中添加或删除障碍物时，可以实时更新导航网格。
- NavMeshAI导航系统的实现的内部原理:
  collapsed:: true
	- Unity的导航系统用于智能地移动游戏中的角色（或称为代理），主要解决两个问题：如何在场景中找到目标位置，以及如何动态地移动到该位置。
	- #### 1.   **可行走区域**
	  collapsed:: true
		- **导航数据**：导航系统创建可行走区域，定义代理可以站立和移动的地方。
		- **导航网格（NavMesh）**：通过场景几何体生成的表面，储存为凸多边形，便于表示无障碍区域和相邻多边形的信息。
	- #### 2.   **路径查找**
	  collapsed:: true
		- **路径寻找**：通过将起点和终点映射到最近的多边形，使用A*算法搜索路径，访问邻接多边形，找到从起点到终点的多边形序列。
	- #### 3.   **路径跟随**
	  collapsed:: true
		- **走廊（Corridor）**：描述从起点到终点的多边形序列。代理通过朝向下一个可见的走廊角落来移动。
		- **多代理场景**：在多个代理同时移动时，代理需要偏离原路径以避免碰撞。
	- #### 4.   **避免障碍物**
	  collapsed:: true
		- **转向逻辑**：根据下一个角落的位置，计算所需的方向和速度。
		- **障碍物避免**：使用互惠速度障碍（RVO）来预测和防止与其他代理或导航网格边缘的碰撞。
	- #### 5.   **移动代理**
	  collapsed:: true
		- **最终速度**：计算代理的最终速度，模拟代理的动态模型，允许更自然的移动方式。
		- **动画系统**：将计算出的速度用于角色的根运动或由导航系统控制移动。
	- #### 6.   **全局与局部导航**
	  collapsed:: true
		- **全局导航**：用于跨越整个世界找到走廊，处理成本较高。
		- **局部导航**：高效地向下一个角落移动，避免与其他代理或移动物体的碰撞。
	- #### 7.   **障碍物处理**
	  collapsed:: true
		- **局部障碍物避免**：处理移动障碍物，允许代理预测性地避开。
		- **NavMesh雕刻**：处理静态障碍物，创建阻挡代理路径的NavMesh孔。
	- #### 8.   **NavMesh链接**
	  collapsed:: true
		- **跳跃或穿越**：使用NavMesh Link组件标注在不可行走区域的特殊动作，允许代理通过该链接导航。
	- #### 9.   **体素化**
	  collapsed:: true
		- **NavMesh烘焙过程**：通过体素化将场景几何体转化为NavMesh，以增加准确性。
		- **体素大小**：调整体素大小以平衡NavMesh创建速度和准确性，建议为代理半径选择合适的体素大小。
- 设计导航系统的时候使用多线程系统进行数据计算优化的思路(待完成):
  collapsed:: true
	- 使用JobSystem 还有IJobEntity结合起来,在Execute的时候进行具有相同组件的实体的遍历:这个时候通过ref关键字对相关需要改变的组件的数据进行计算和更改,而不需要更改的组件呢?使用in关键字进行一个只读的操作,维护数据和组件的一个安全性:当然这是具体的实现细节了,但是因为是多线程的编程也必须保证多线程操作数据的时候的数据安全性;[[C# in 和 ref的区别]][[使用JobSystem的时候的安全性保证]]
- 导航代理的移动的实现:(本质上是根据Navmesh算法获取到的路线然后计算出每帧的移动向量并且应用到代理身上)
  collapsed:: true
	- 导航代理移动的组件包括哪些数据:
	  collapsed:: true
		- ```cs
		   public struct AgentLocomotion : IComponentData
		   {
		       /// <summary>
		       /// Maximum movement speed when moving to destination.
		       /// </summary>
		       public float Speed;
		       /// <summary>
		       /// The maximum acceleration of an agent as it follows a path, given in units / sec^2.
		       /// </summary>
		       public float Acceleration;
		       /// <summary>
		       /// Maximum turning speed in (rad/s) while following a path.
		       /// </summary>
		       public float AngularSpeed;
		       /// <summary>
		       /// Stop within this distance from the target position.
		       /// </summary>
		       public float StoppingDistance;
		       /// <summary>
		       /// Should the agent brake automatically to avoid overshooting the destination point?
		       /// </summary>
		       public bool AutoBreaking;
		  
		       /// <summary>
		       /// Returns default configuration.
		       /// </summary>
		       public static AgentLocomotion Default => new()
		       {
		           Speed = 3.5f,
		           Acceleration = 8,
		           AngularSpeed = math.radians(120),
		           StoppingDistance = 0.1f,
		           AutoBreaking = true,
		       };
		   }
		  ```
	- 控制导航代理移动的系统(`AgentLocomotionSystem`):(代理移动的核心系统)
	  collapsed:: true
		- 该系统属于 `AgentLocomotionSystemGroup`其主要任务是更新代理的运动状态。核心逻辑通过 `AgentLocomotionJob` 执行，每帧都会根据代理的状态和目标位置更新其位置和速度。
		- Job实现的逻辑:(目的主要是对代理的transform的属性进行更新,对代理的AgentBody进行更新)
		  collapsed:: true
			- **DeltaTime的成员变量,Update里面获取到**：系统从 `SystemState` 中获取 `DeltaTime`，代表了帧时间，用于计算平滑的运动和速度变化。
			- **停止逻辑**：首先检查代理是否到达了目标。通过 `body.RemainingDistance` 和 `locomotion.StoppingDistance` 来判断代理是否应该停止。如果已经到达目标，代理速度被设置为 0，并将 `body.IsStopped` 标志设为 `true`，以停止后续更新。
			- **速度计算**：当代理接近目标时，系统会启动自动减速功能（如果 `AutoBreaking` 为 `true`），以避免代理在接近目标时突然停止。通过插值计算（`math.lerp`），根据代理与目标的距离逐步减小最大速度，模拟出平滑的减速过程。
			- **力的计算**：系统对 `body.Force` 进行标准化，确保力的大小始终在合理范围内。然后通过插值更新代理的速度，考虑了代理的最大加速度限制（`locomotion.Acceleration`）。
			- **位置更新**：根据当前速度，系统计算代理的步进值（位移），并确保不会超出目标距离。如果位移过大，系统将调整为只移动到目标点，而不会越过它。
			- **旋转更新**：根据代理的形状（`shape.Type`）更新其旋转。针对不同形状（圆形或圆柱形），系统采用 `math.atan2` 来计算正确的旋转角度，并使用 `math.slerp` 进行平滑插值更新代理的方向。
	- 引导导航代理实现朝目标位置移动的系统:(注意此系统是在导航代理移动的系统组里面的)
	  collapsed:: true
		- 查询的组件有哪些?
			- `AgentBody body`：表示代理的身体，包含运动状态等信息。
			- `AgentLocomotion seeking`：表示代理的移动控制器用于控制寻路或移动逻辑。
			- `LocalTransform transform`：代理的当前位置。
		- 具体的核心的实现的逻辑是什么样的呢?
			- 通过Burst编译器优化然后使用Job进行并行计算;job的执行逻辑如下:
			- `if (body.IsStopped)`：首先检查代理是否处于停止状态，如果是，就直接返回，不进行任何计算。
			- `towards = body.Destination - transform.Position`：计算目标点和当前代理位置之间的向量，表示代理当前所要前进的方向。
			- `distance = math.length(towards)`：计算目标点与代理当前位置的距离。
			- `desiredDirection = distance > math.EPSILON ? towards / distance : float3.zero`：计算出期望的移动方向。为了避免除以零，使用了 `math.EPSILON`（一个非常小的值）来判断是否接近目标点。
			- `body.Force = desiredDirection`：将计算出的移动方向存储到 `body.Force`，这会引导代理向目标移动。
			- `body.RemainingDistance = distance`：记录代理与目标之间的剩余距离。
- 导航代理能够智能停止的系统的实现:
  collapsed:: true
	- ### **1. 核心概念**
	  collapsed:: true
		- **集体思维停止（HiveMindStop）**：代理在检测到附近有其他代理已经到达目标并且目的地相似时，可以选择提前停下，而无需到达其自身的目标。这可以减少多个代理在同一终点处相互挤压。
		- **放弃停止（GiveUpStop）**：代理在重复遇到阻碍（如碰到其他停止的代理）时，会逐步累积疲劳值（`FatigueSpeed`），一旦累积到一定程度，代理就会选择停止。
	- ### 2.  **组件概述**
		- `AgentSmartStop`：这是代理智能停止的核心数据组件，包含了两个子功能：
			- `HiveMindStop`：用于集体思维停止的相关配置。
			- `GiveUpStop`：用于放弃停止的相关配置。
		- `GiveUpStopTimer`：记录放弃停止的进度，追踪代理是否由于反复碰撞而选择停止。
	- ### 3.  **系统架构**
		- `AgentSmartStopSystem` 是处理代理智能停止逻辑的系统，负责检查代理是否应该根据附近的其他代理或自身累积的疲劳值来做出停靠决策。
			- #### `OnCreate` ：
			- 系统在创建时，初始化 `ComponentLookup<AgentSmartStop>`，这是一个用于查询代理智能停止组件的数据结构。
			- #### `OnUpdate` ：
			- 每帧更新时，首先获取空间分区系统的实例（`AgentSpatialPartitioningSystem.Singleton`），用于高效地查询代理附近的其他代理。
			- 然后，调用 `AgentSmartStopJob`，遍历所有包含智能停止组件的代理，并对其当前位置、速度、疲劳状态等信息进行更新。
			- `AgentSmartStopJob `
		- **AgentSmartStopJob 核心逻辑**：
			- **目的地更新**：当 `GiveUpStop` 启用时，如果代理的目的地发生变化，更新计时器的 `Destination` 并重置 `Progress`。
			- **附近代理检查**：通过空间分区系统的 `QuerySphere`，在指定半径内查找附近的代理。如果发现附近的代理已经停下并且其目的地与当前代理相似，则当前代理可以提前停止。这就是"集体思维停止"的实现。
			- **放弃停止逻辑**：如果代理频繁与其他停下的代理相撞，累积疲劳值（`FatigueSpeed`），一旦达到上限，代理会停下。否则，累积的疲劳值会逐步恢复（`RecoverySpeed`）。
	- ### 4.  **挑战与解决方案**
		- **高效空间查询**：在大规模群体中，查询附近的代理可能会导致性能瓶颈。通过使用空间分区系统（`Spatial.QuerySphere`）和并行化处理（Burst + Job System），可以大幅提高性能，减少每帧的计算量。
		- **智能停靠决策**：在处理智能停靠时，面临的挑战是如何让代理在群组中做出合理的停靠决策，避免卡顿或不自然的行为。解决方案是引入"集体思维停止"和"放弃停止"两个机制，分别针对代理接近相似目标点的情况和代理长期被阻挡的情况。
	- ### 5.  **总结(这也是使用这个脚本解决的问题)**
	  collapsed:: true
		- 这个系统通过智能停靠机制，让代理在群体中表现得更加智能，避免了传统寻路系统中的拥堵和卡住问题。集体思维停止和放弃停止功能为代理提供了动态调整的能力，使其能够根据场景中其他代理的状态或自身的困境决定是否提前停下。
- 实现一个空间查询的系统:AgentSpatialPartitioningSystem
  collapsed:: true
	- 定义的数据结构有哪些?
	  collapsed:: true
		- ```cs
		          NativeList<Agent> m_Agents;
		          NativeList<Entity> m_Entities;
		          NativeList<AgentBody> m_Bodies;
		          NativeList<AgentShape> m_Shapes;
		          NativeList<LocalTransform> m_Transforms;
		          EntityQuery m_Query;用来延迟执行查询提高性能
		          NativeParallelMultiHashMap<int3, int> m_Map;//用来存储实体的信息的核心的数据结构;值是实体的索引用来减少内存使用;键是分区的体素坐标;
		  ```
	- 整个系统就是围绕着多重哈希映射的一个结构实现的一个查找操作查找涉及到一个螺旋搜索算法;然后通过对实现`ISpatialQueryEntity`的实体的组件进行一个方法的调用,执行查找之后的逻辑;把处理的逻辑抽象出来了;
	  id:: 66f4c531-c7c2-4aa4-be90-fc69fe7f5def
	- 查找周围实体的时候使用了一个[[螺旋搜索]]的算法:
	  collapsed:: true
		- **边界管理**：在原始代码中，管理了 `point`、`min` 和 `max` 作为边界，确保在查询时不会越界。
		- **方向变化**：在原始代码中，使用 `step` 变量来改变搜索方向，类似于在这个示例中通过更新 `top`、`bottom`、`left` 和 `right` 来控制搜索的方向。
		- **判断层级:**
		- **判断周围的最大的实体查找的数量;**
		- **实体处理**：在原始代码中，处理找到的实体（如调用 `action.Execute`）;
	- 设计思路:把代理划分为任意大小的单元格;这样可以更加有效率的查找最近代理;(通过多重hash对空间进行分区)
	  collapsed:: true
		- 系统里面新创建一个singleton的组件:该组件获取了系统里面的所有的成员变量;然后放在一个结构体里面;在系统更新的时候统一调用:
- 导航碰撞系统的实现:
  collapsed:: true
	- ### 1. **核心概念**
		- 系统的主要目标是让导航代理之间能够互相检测碰撞并进行调整，避免相互穿透和碰撞。这是通过空间划分(未完成)（Spatial Partitioning）和迭代的碰撞检测与解决来实现的。系统通过 `AgentColliderSystem` 进行更新，每帧执行一定次数的碰撞检测迭代。碰撞检测考虑了不同形状（圆形和圆柱形）的代理，通过计算它们的位置和半径来检测是否发生了碰撞。如果发生碰撞，会根据渗透深度计算出修正位移，并应用于代理的位置，防止代理重叠。
	- ### 2. **组件概述**
	  collapsed:: true
		- **AgentCollider**：标记代理是否需要与附近代理进行碰撞检测。包含一个 `Layers` 字段，用于决定代理在哪些导航层上进行碰撞检测。
		- **ColliderSubSettings**：提供系统运行时的可配置参数，如每帧进行的碰撞解决迭代次数，影响碰撞解决的精度与性能。
		- **AgentColliderSystem**：该系统是整个碰撞解决逻辑的核心，负责调度空间划分系统和碰撞检测任务。根据配置，系统在每帧内多次迭代以提升碰撞解决的精度。
		- **AgentColliderJob**：包含实际的碰撞检测与解决逻辑。通过执行 `Execute` 方法，在代理间进行空间查询，判断是否发生了碰撞，计算位移修正，并更新代理位置。
		- **CirclesCollision 和 CylindersCollision**：这两个结构体 是具体的碰撞处理类，分别用于处理圆形和圆柱形代理之间的碰撞。它们根据代理的形状、速度、距离等参数计算代理之间的碰撞和修正位移。
	- ### 3. **系统架构**
		- **ECS 设计**：该系统基于 ECS，充分利用了 Unity DOTS 的多线程并行处理能力。通过 `IComponentData` 存储数据，`ISystem` 进行系统更新，并通过 `IJobEntity` 实现多线程作业处理。在架构上，系统依赖于空间划分系统 `AgentSpatialPartitioningSystem`，该系统能够有效地缩小碰撞检测的范围，从而减少计算量。
		- **系统更新**：系统在 `OnUpdate` 中通过调度空间划分系统来更新代理的空间信息，并在每帧执行多次碰撞检测迭代。通过 `Job` 系统并行执行代理间的碰撞检测逻辑，减少主线程的负载。
		- **空间查询**：在 `AgentColliderJob` 中，系统通过调用 `Spatial.QueryCircle` 或 `Spatial.QueryCylinder` 进行空间查询，找到所有在一定范围内可能发生碰撞的代理，并逐一处理。
	- ### 4. **开发过程中的挑战和解决方案**
	- **性能优化**：在大规模场景中，代理数量可能非常庞大，逐一进行碰撞检测的成本很高。为了解决这个问题，系统引入了空间划分技术，减少了碰撞检测的范围。此外，`BurstCompile` 和 `Job` 系统大幅度提升了执行效率，确保碰撞检测在多线程环境下高效运行。
	- **多种代理形状的支持**：系统需要支持不同形状的代理（圆形和圆柱形），并为它们提供不同的碰撞检测算法。通过 `CirclesCollision` 和 `CylindersCollision` 结构体，分别实现了圆形与圆柱形代理之间的碰撞检测逻辑。
	- **稳定性**：在代理数量较多、距离较近时，容易出现不稳定的碰撞解决。为此，系统通过一个 `ResolveFactor` 控制位移修正的强度，防止代理在碰撞解决过程中出现不合理的剧烈移动。
	- ### 5. **总结**
	  这个基于 ECS 的代理碰撞解决系统在优化 `NavMeshAgent` 的一些问题上表现出色，特别是针对代理之间的碰撞检测和解决。相比于传统的 Unity NavMesh 系统，这个系统更加高效地处理大规模场景中的碰撞问题，尤其是通过多线程和空间划分技术实现了更好的性能。同时，系统通过可配置参数（如迭代次数）提供了性能与精度的平衡选择，使开发者能够根据实际需求进行调整。
- 导航避障系统的实现:
  collapsed:: true
	- ### 1. **系统设计的核心概念**
	  collapsed:: true
		- 这个系统的核心目的是实现代理（Agent）之间的互相避让（Reciprocal Avoidance），并确保多个代理在导航过程中不会发生碰撞或冲突。与传统的导航方式不同，该系统基于 **Optimal Reciprocal Collision Avoidance (ORCA)** 算法，它通过计算代理的最佳避让路径，动态调整代理的方向和速度来实现高效的避让行为，特别适用于复杂场景下的多代理系统。
	- ### 2. **组件概述**
	  collapsed:: true
		- 在该系统中，核心的 ECS 组件继承自 `IComponentData`，它们存储代理在避让计算中的必要信息：
		- **AgentReciprocalAvoid**：存储与避让逻辑相关的属性，包括代理的避让半径（`Radius`）和可参与避让的导航层（`Layers`）。这个组件用于标记哪些代理需要进行避让计算。
	- ### 3. **系统架构**
	  collapsed:: true
	  系统基于 Unity ECS 和 DOTS 架构，使用 `ISystem` 和 `IJobEntity` 来并行处理代理的避让逻辑。以下是其架构的核心逻辑实现：
	  1. **系统的核心逻辑实现**：
		- **`AgentReciprocalAvoidSystem`**：这个系统在每帧调用时会更新代理的避让逻辑。系统通过 `AgentReciprocalAvoidJob` 作业对每个代理进行避让计算，并且借助 `AgentSpatialPartitioningSystem` 进行空间查询，来确定需要处理避让的附近代理。
		- **`AgentReciprocalAvoidJob`**：作业中使用 ORCA 算法（`OptimalReciprocalCollisionAvoidance`）来计算代理的最佳避让方向和速度。该算法通过代理的当前位置、速度、期望方向、避让半径等参数来计算新速度。
		- **空间查询（`Spatial.QueryCylinder`）**：该部分通过空间划分技术查询附近可能发生碰撞的代理，从而减少计算范围，并提高系统的运行效率。
		- **避让计算**：通过 ORCA 算法动态计算代理的避让方向和速度。系统在查询附近代理后，将这些代理作为障碍加入 ORCA 模型，计算出一个最佳的避让路径，避免代理之间的碰撞。
	- ### 4. **开发过程中的挑战和解决方案**
		- **挑战1：多代理的高效避让计算**  
		  在场景中有大量代理时，逐一计算每个代理的避让是极其耗费性能的。为了解决这一问题，系统使用了 **空间划分** 技术，通过 `AgentSpatialPartitioningSystem` 来有效缩小每个代理的碰撞检测范围，仅计算附近代理的避让行为。
		- **挑战2：高精度的实时避让**  
		  避让逻辑需要保持高精度，特别是在复杂的动态场景中，代理可能会频繁变换位置和速度。ORCA 算法通过数学计算提供了较为稳定的避让方向，同时系统通过可配置的避让半径和迭代步骤，保证了在高负载下依然能进行稳定的避让计算。
		- **挑战3：避免自我干扰**  
		  在处理避让时，系统需要避免代理与自身的干扰。为了处理这种情况，系统在 `Execute` 方法中专门跳过了与自身的计算 (`if (Entity == entity)`)，以确保避免错误计算。
	- ### 5. **总结**
	  这个基于 ECS 和 ORCA 算法的导航系统有效解决了多代理场景下的碰撞和避让问题。与 Unity 原生的 `NavMeshAgent` 系统相比，该系统具有以下优势：
	- **并行处理能力**：通过 DOTS 的 `IJobEntity`，系统能够高效并行计算多个代理的避让行为，显著提升性能。
	- **高效的空间划分**：通过空间划分技术，系统减少了不必要的碰撞检测，提升了避让计算的效率。
	- **灵活的避让机制**：使用 ORCA 算法，系统能够根据代理之间的动态关系，实时计算出最优的避让路径和速度，适应复杂的多代理场景。
	  
	  相比之下，使用传统的 `NavMeshAgent` 可能会出现以下问题：
	- **性能瓶颈**：`NavMeshAgent` 的避让计算是单线程的，面对大量代理时性能表现欠佳，容易导致帧率下降。
	- **避让精度不足**：`NavMeshAgent` 在处理复杂多代理场景时，避让逻辑可能不够精确，容易出现代理重叠或穿模的情况。
	- **缺乏灵活性**：`NavMeshAgent` 提供的避让机制相对固定，难以根据具体需求进行优化和调整，而该系统提供了更加灵活的参数和行为配置。
- 导航寻路系统得实现:(这里需要注意烘焙得 导航数据是如何处理得?动态得烘焙是如何实现?)
	- 两种实现:一种基于DOTS的烘焙:
	- 一种使用Unity自带的烘焙手段
	  collapsed:: true
		- [[NavMeshPath]]
		- [[NavMeshNode]]
	- 怎么利用导航数据寻找最优路径:
	- 引导系统得实现;
- ORCA算法的DOTS实现(未完成):
  collapsed:: true
	- Reference
		- [避障算法 - VO、RVO 以及 ORCA (RVO2) | indienova 独立游戏](https://indienova.com/indie-game-development/vo-rvo-orca/#:~:text=%E5%AF%B9%E4%BA%8E%E9%81%BF%E9%9A%9C%E7%AE%97%E6%B3%95%EF%BC%8C%E7%8E%B0)
- [[MonoBehaviour和ECS得混合]]是如何实现得呢?
  collapsed:: true
	- 在我们平时的需要使用ECS和OOP进行数据交互的时候,在Monobehaviour和ECS沟通得机制里面,ECS实体获取数据得途径是从awake方法里面获取的;
	- 实现[[EntityBehaviour]],通过 MonoBehaviour 的生命周期方法，灵活地控制何时创建、启用或禁用销毁实体，使得游戏逻辑与 ECS 的计算过程相结合，形成一个动态响应的系统。
	- 绑定实体:这里的一个部分是组件添加,一个是对象添加
	  collapsed:: true
		- 组件添加的话,就是普通的轮询;对象添加如下:`AddComponentObject`此时在ECS系统里面的访问就变成了结构体组件变成了引用类型组件,这样会产生开销,因为此方法创建一个同步点，这意味着 EntityManager 等待所有 当前正在运行的作业，以便在添加对象之前完成。之前不能启动其他作业 方法已完成。同步点可能会导致性能下降，因为 ECS 框架可能不会 能够使用所有可用内核的处理能力。;
		  collapsed:: true
			- ```cs
			  using Unity.Entities;
			  using Unity.Transforms;
			  using UnityEngine;
			  
			  public class EnemyMoveSystem : SystemBase
			  {
			      protected override void OnUpdate()
			      {//Transform为引用类型;
			          Entities.ForEach((Entity entity, Transform transform, ref LocalTransform localTransform) =>
			          {
			              // 访问 Transform
			              Vector3 position = transform.position;
			              
			              // 更新 LocalTransform 位置
			              localTransform.Position += new float3(0, 0, 1) * Time.DeltaTime;
			  
			              // 可能需要更新 Transform 的位置
			              transform.position = localTransform.Position;
			          }).Schedule();
			      }
			  }
			  ```
		- ```cs
		          void Awake()
		          {
		              m_Entity = GetOrCreateEntity();
		              var world = World.DefaultGameObjectInjectionWorld;
		              var manager = world.EntityManager;
		  
		              manager.AddComponentData(m_Entity, new LocalTransform
		              {
		                  Position = transform.position,
		                  Rotation = transform.rotation,
		                  Scale = 1,
		              });
		  
		              // Transform access requires this
		              manager.AddComponentObject(m_Entity, transform);
		  
		              manager.AddComponentData(m_Entity, DefaultAgent);
		              if (MotionType != AgentMotionType.Static)
		                  manager.AddComponentData(m_Entity, DefaultBody);
		  #pragma warning disable 0618
		              if (MotionType == AgentMotionType.Steering)
		                  manager.AddComponentData(m_Entity, DefaultSteering);
		  #pragma warning restore 0618
		              if (MotionType == AgentMotionType.DefaultLocomotion)
		                  manager.AddComponentData(m_Entity, DefaultLocomotion);
		          }
		  ```
	- 举例比如:设置agent寻路得时候;提供一个float3类型目的地得点;然后通过内置的API获取ECS组件进行数值的修改操作;
	  collapsed:: true
		- ```cs
		   public AgentBody EntityBody
		   {
		       get => World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<AgentBody>(m_Entity);
		       set => World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(m_Entity, value);
		   }
		  
		   public void SetDestination(float3 target)
		   {
		       var body = EntityBody;
		       body.Destination = target;
		       body.IsStopped = false; 这里就是对实体进行修改操作
		       EntityBody = body;      把Monobehaviour系统的值通过API写入到实体组件里面;
		   }
		   
		  ```
	- 实际得ECS数据得获取流程是:通过baker
	  collapsed:: true
		- ```cs
		      internal class AgentBaker : Baker<AgentAuthoring>
		      {
		          public override void Bake(AgentAuthoring authoring)
		          {
		              var entity = GetEntity(TransformUsageFlags.Dynamic);
		              AddComponent(entity, authoring.DefaultAgent);
		              if (authoring.MotionType != AgentMotionType.Static)
		                  AddComponent(entity, authoring.DefaultBody);
		  #pragma warning disable 0618
		              if (authoring.MotionType == AgentMotionType.Steering)
		                  AddComponent(entity, authoring.DefaultSteering);
		  #pragma warning restore 0618
		              if (authoring.MotionType == AgentMotionType.DefaultLocomotion)
		                  AddComponent(entity, authoring.DefaultLocomotion);
		          }
		      }
		  ```
- 如何处理同步点,解决系统组的同同步的冲突呢?分为几大方面(未完成)
  collapsed:: true
	- **Job System**：
		- 在使用 Job System 时，可以使用 `Dependency` 属性来管理同步点，确保所有的 Job 在执行时的数据是最新的。
	- **Update Order**：
		- 通过设置系统的更新顺序，确保某些系统在其他系统之前执行。例如，先更新物理状态，再更新渲染状态。
	- **EntityManager**：
		- 在访问或修改数据之前，可以检查 EntityManager 的状态，确保数据是最新的。
- [[IJobChunk和IJobEntity的区别]]
  collapsed:: true
	-
- 实现的方式:
  collapsed:: true
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
  collapsed:: true
	-
- 最佳实践:
  collapsed:: true
	- 一个系统里面有多个成员变量的时候:使用一个singleton进行打包;提升内存的连续性;
- 明天需要解决的问题总结:
	- 关注导航路径查询系统的实现;最优路径查找的实现;避障系统的实现;避障算法还有Navmesh系统的一些实现的原理需要搞清楚;
- ---
- Temp
	- 碰撞,避障和NavMesh
	  collapsed:: true
		- 实现碰撞必须用到的四个属性:` public void Execute(Entity otherEntity, AgentBody otherBody, AgentShape otherShape, LocalTransform otherTransform)`
		- 实现了两种碰撞方式:一种是Circle碰撞,一种是Cylinder碰撞,这里和空间划分的脚本那里呼应上了;
		- ### 1.  **系统结构**  ( `AgentColliderSystem` )
		- **`AgentColliderSystem`** 继承自 `ISystem`，是基于 `DOTS` 的系统。系统创建时通过 `OnCreate` 方法获取了 `AgentSpatialPartitioningSystem`（用于空间划分的系统），并从配置中获取了碰撞检测迭代次数 `m_Iterations`。
		- **`OnUpdate`** 中通过 `ScheduleUpdate` 和 `ScheduleParallel` 将碰撞检测任务 `AgentColliderJob` 调度为并行执行的作业（`Job`），每次迭代都会更新空间划分系统，并执行并行的碰撞检测。
		- ### 2.  **碰撞检测作业 (`AgentColliderJob`)**
		- `AgentColliderJob` 实现了 `IJobEntity` 接口，可以在多个线程中并行运行。执行时处理每个实体的碰撞逻辑。
		- **执行流程**：
			- 根据实体的形状类型（圆形或圆柱体），选择不同的碰撞处理方式（`CirclesCollision` 或 `CylindersCollision`）。
			- **空间查询**：系统通过调用 `Spatial.QueryCircle` 或 `Spatial.QueryCylinder`，根据实体的形状和位置查询相邻的其他实体。查询的目的是找到潜在的碰撞对象，并为它们进行碰撞响应计算。
			- **碰撞解决**：如果有碰撞发生，`action.Displacement` 存储了碰撞导致的位移，而 `action.Weight` 表示总的碰撞权重。位移按比例应用到当前实体的位置中。
		- ###   **碰撞响应细节**
		- **`CirclesCollision.Execute`** 方法负责处理圆形实体之间的碰撞检测。
			- **距离计算**：首先计算两个实体的位移向量 `towards` 和它们之间的距离平方 `distancesq`。
			- **碰撞判断**：通过比较距离平方与半径和的平方，来判断是否发生碰撞。如果发生碰撞，计算两个实体的穿透深度 `penetration`。
				- ### **`Weight` 的作用**
					- `Weight` 是用于计算多个碰撞对象之间影响的权重。每次 `CirclesCollision.Execute` 发现一个碰撞时，都会增加 `Weight`，同时累加每次碰撞的位移到 `Displacement` 中。
					- `Weight` 表示当前实体与多少个其他实体发生了碰撞。假设有多个实体发生碰撞，为了得到一个平均的位移方向和大小，使用 `Displacement / Weight` 进行归一化处理。
				- ### **`Displacement` 和 `Weight` 的含义**
				  collapsed:: true
					- **`Displacement`**：累加的位移量，表示这个实体应该沿哪个方向移动，以解决碰撞。每次碰撞会根据穿透深度调整 `Displacement`。
					- **`Weight`**：累计碰撞的次数。每次碰撞时 `Weight++`，代表当前实体在这个更新帧中与多少个其他实体发生了碰撞。
				- ### **`action.Displacement = action.Displacement / action.Weight;` 的意义**
				  collapsed:: true
					- 当 `Weight > 0` 时，这段代码的意思是将总的 `Displacement` 平均化。假设一个实体与多个其他实体发生了碰撞，它的 `Displacement` 代表所有这些碰撞的累加位移。通过 `Displacement / Weight`，我们可以得到平均的位移方向和大小，使实体平滑地移动，而不是单一方向。
					  
					  举个例子：
						- 假设实体 A 与两个实体 B 和 C 发生碰撞，分别计算出与 B 的位移 `D1` 和与 C 的位移 `D2`。
						- 累加后的 `Displacement = D1 + D2`，而 `Weight = 2`（因为发生了两次碰撞）。
						- 归一化后的位移 `Displacement / Weight` 就是 `(D1 + D2) / 2`，这是 A 平均应该移动的方向和大小。
				- 在 `Spatial.QueryCircle` 执行完毕后，`action.Displacement` 累加了与每个碰撞对象的位移，`action.Weight` 则累加了碰撞的次数。
				- **`action.Displacement / action.Weight`** 的操作用于平滑处理多次碰撞的累积效果，避免一次碰撞造成过大的位移，进而保持实体移动的稳定性。
			- **处理碰撞重叠**：如果两个实体位置几乎重叠（距离接近 0），为了避免它们有相同的位移方向，按照实体索引大小分配不同的位移方向。
			- **位移累加**：将穿透深度的位移值累加到 `Displacement` 中，表示这个实体应当沿这个方向移动。
		- ### 4.  **空间划分系统 (`AgentSpatialPartitioningSystem`)**
		- 该系统是为了提高查询效率，使用空间划分技术减少每个实体需要检查的其他实体数量，从而优化整体碰撞检测过程。它根据实体的位置和形状进行空间查询，只检查可能发生碰撞的实体，避免全局碰撞检测的高计算成本。
		- ### 5.  **多线程并行计算**
		- 整个碰撞检测过程通过 DOTS 的 `Job System` 并行化执行。每个 `AgentColliderJob` 会在多个线程中同时处理不同实体的碰撞，大幅度提高了碰撞检测的效率。
- 管理组件的方法:
  collapsed:: true
	- 添加组件的几种方式:
	  collapsed:: true
		- 添加组件到实体
		- 添加组件到系统;
	- 删除组件的方式:
	  collapsed:: true
		- ```cs
		  // 动态添加组件
		  entityManager.AddComponentData(entity, new MyComponent { Value = 20f });
		  
		  // 动态移除组件
		  entityManager.RemoveComponent<MyComponent>(entity);
		  ```
	- 开发问题总结:
		- 取消编译器警告的方法:
		  collapsed:: true
			- ```cs
			  #pragma warning disable 0618	(防止编译器报错:)
			      if (authoring.MotionType == AgentMotionType.Steering)
			        AddComponent(entity, authoring.DefaultSteering);
			  ```
			- 不加这个的话就会报错:
				- ![image.png](../assets/image_1727340421049_0.png)
				-
		- 创建组件之前，需要考虑组件将存储的数据类型以及将在什么上下文中使用它。然后你可以决定使虍哪种组件类型来实现组件。这里需要理清楚数据布局(举个例子):
		  collapsed:: true
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
		- 如何把高性能计算移殖到MonoBehaviour,让DOTS能够为业务逻辑做辅助;
		- 开发导航系统的时候遇到的困难的技术点在哪里?
		  collapsed:: true
			- 一个是对DOTS的技术栈的不熟悉;
				- 使用NativeArray的使用的时候遇到的坑;
					- burst编译器;
					- 系统组在项目开发DOTS项目里面的作用:
				-
			- 遇到的问题最好是和项目整体搭边遇到的问题:
		- ECS控制系统的执行流程(这是一个难点;)
		  collapsed:: true
			- 比如在空间状态更新的系统的依赖必须实时更新:
		- 怎么实现高效的空间划分和高效的空间查找
		- EntityQUery的延迟执行机制对性能进行一个合理的优化;
		- MonoBehaviour和ECS结合开发遇到的难题?如何处理的?
		- 使用多重哈希映射管理的时候可能会遇到的问题:
			- 一个实体的体积可能会超过多个网格空间的大小,处理的逻辑是怎么样的?
		- 遇到需要动态分配内存的场景的时候如何解决问题呢?
		  collapsed:: true
			- 一个用于避免动态分配的结构体:
			  collapsed:: true
				- 避免动态分配的好处有什么?
					- 这里限制了最大的空间容量:所以这是一个可以讲解的优化的点
				- ```cs
				  FixedEntries
				      unsafe struct FixedEntries
				      {
				          public const int Capacity = 32;
				  
				          float2 m_Center;
				          fixed int m_Indices[Capacity];
				          fixed float m_Distances[Capacity];
				          int m_Length;
				  
				          public int this[int index] => m_Indices[index];
				  
				          public FixedEntries(float2 center, int defaultIndex, float defaultDistance, int length)
				          {
				              // TODO: check that length is less than capacity
				  
				              m_Center = center;
				              m_Length = length;
				  
				              // Additional entry will act as null terminator
				              length++;
				  
				              for (int entryIndex = 0; entryIndex < length; entryIndex++)
				              {
				                  m_Indices[entryIndex] = defaultIndex;
				                  m_Distances[entryIndex] = defaultDistance;
				              }
				          }
				  
				          [MethodImpl(MethodImplOptions.AggressiveInlining)]
				          public bool Add(int index, float2 point)
				          {
				              float distance = distancesq(m_Center, point);
				  
				              int minEntryIndex = -1;
				              float minDistance = distance;
				  
				              // Find min distance entry that is smaller than requested distance
				              for (int entryIndex = m_Length; entryIndex-- > 0;)
				              {
				                  // All indices must be unique
				                  if (index == m_Indices[entryIndex])
				                      return false;
				  
				                  if (minDistance <= m_Distances[entryIndex])
				                  {
				                      minEntryIndex = entryIndex;
				                      minDistance = m_Distances[entryIndex];
				                  }
				              }
				  
				              // Failed to find min entry index
				              if (minEntryIndex == -1)
				                  return true;
				  
				              // Update entry with new one
				              m_Indices[minEntryIndex] = index;
				              m_Distances[minEntryIndex] = distance;
				  
				              return true;
				          }
				      }
				  }
				  ```
		- AddBuffer的时候
			- 在 Authoring 组件中使用 `AddBuffer<NavMeshNode>(entity);` 添加了一个 `NavMeshNode` 类型的缓冲区后，在 System 中查询时，可以使用 `DynamicBuffer<NavMeshNode>` 来访问和操作这个缓冲区。
		- DOTS里面如何合理得使用引用类型
			- NavMeshData和共享组件得处理;
		- 遇到的算法上的问题非常多:螺旋搜索算法,避障算法等等;
	- DOTS的优化小技巧:
	  collapsed:: true
		- 使用EntityIndInQuery特性优化查询:
			- ### 1.   **索引访问**
			  collapsed:: true
				- **获取实体索引**：
					- 当一个实体在一个查询中被处理时，`[EntityIndexInQuery]` 允许您获取该实体在查询结果中的索引。这在处理多个实体时非常有用，可以帮助您快速找到当前实体的相对位置。
			- ### 2.   **性能优化**
			  collapsed:: true
				- **避免查找开销**：
					- 通过直接访问索引，可以减少对其他数据结构（如数组或列表）的查找开销，从而提高性能，尤其是在处理大量实体时。
			- ### 3.   **用法示例**
			  collapsed:: true
				- 在作业中使用该特性时，您可以像下面这样定义一个结构体：
				  ```cs
				  public struct MyJob : IJobEntity
				  {
				    public void Execute([EntityIndexInQuery] int index, Entity entity)
				    {
				        // 使用 index 进行相关操作
				    }
				  }
				  ```
			- ### 4.   **结合其他特性**
			  collapsed:: true
				- **与组件结合使用**：
					- 通常与其他组件特性结合使用，例如 `ComponentDataFromEntity`，可以根据索引快速访问和修改与实体相关的组件数据。
	- 系统的缺点:
	  collapsed:: true
		- 如果代理数量少使用这个不值当:因为查询的时候涉及到大量使用多线程系统;把JobSystem的开销切换上下文的开销说一下:
	- 数据结构是如何实现快速刷新的()
	  collapsed:: true
		- 利用多线程每一帧都改变非托管数组的容量
		- 进行并行的增删改查的操作;
	- CPU优化(一直容易忽略的是减少结构体的复制):
	  collapsed:: true
		- 使用内联优化可以提升执行速度;但是可能会造成代码碰撞:
		- 使用ref
		  collapsed:: true
			- ```cs
			  减少结构体的复制
			  public void ProcessStruct(ref LargeStruct largeStruct)
			  {
			      // 处理 largeStruct
			  }
			  
			  // 调用
			  LargeStruct myStruct = new LargeStruct();
			  ProcessStruct(ref myStruct); // 传递引用，不复制
			  ```
		- Const,Static,readonly理清楚这些关键字得内存分配得位置:
		  collapsed:: true
			- **`const`**：
				- 编译时常量，嵌入到调用代码中，不占用运行时内存。
			- **`static`**：
				- 类级别的成员，存储在静态存储区，所有实例共享，减少内存使用。
			- **`readonly`**：
				- 实例级别的只读字段，存储在堆中，每个实例都有独立副本，适合需要在构造时初始化的字段。
		-
	- 内存优化:
	  collapsed:: true
		- 可以尝试如下栈分配来进行内存管理减少分配:0GC大量压榨CPU;通过甚至可以减少值拷贝;
			- ```cs
			   private void Update()
			   {
			       for (int i = 0; i < 1000; i++)
			       {
			           MyStruct st = new MyStruct(1000);
			       }
			       MyStruct st1 = new MyStruct(1000);
			   }
			  
			   unsafe public struct MyStruct
			   {
			       fixed int numbers[1000];
			       public MyStruct(int num)
			       {
			           for (int i = 0; i < num; i++)
			           {
			               numbers[i] = i;
			           }
			       }
			   }
			  ```
		- 使用stackalloc 在栈上分配:首先，减少堆分配的数量和大小。 其次，减少复制数据的频率。将 `class` 类型转换为 `struct` 类型。 使用 `ref` safety [功能](https://learn.microsoft.com/zh-cn/dotnet/csharp/advanced-topics/performance/)来保留语义并最大程度地减少额外的复制。
		- 数组的动态分配和静态分配的写法的区别?
		  collapsed:: true
			- ```cs
			  静态分配:
			  unsafe struct FixedArrayExample
			  {
			      public const int Capacity = 32;
			      fixed int m_Indices[Capacity];
			  }
			  动态分配:
			  class HeapArrayExample
			  {
			      private int[] m_Indices;
			  
			      public HeapArrayExample(int capacity)
			      {
			          m_Indices = new int[capacity]; // 在堆上分配
			      }
			  }
			  ```
		- 我在结构体里面使用fixed关键字定义的数组,是已知内存大小这个是在如果我栈上分配的这个结构体示例,这两个数组所在的位置也是栈上是吗?此时并不会在堆上开辟内存空间是吗?我一直以为无法怎结构体所在的内存块里面分配数组;其实是可以的
		  collapsed:: true
			- 使用 `fixed` 关键字定义的数组大小在编译时已知，这意味着它们的内存分配是静态的，不需要动态分配。
			- ### 2.   **栈分配与堆分配**
			- **栈上分配**：
				- 当你将 `FixedEntries` 作为局部变量使用时，它会在栈上分配内存。此时，`m_Indices` 和 `m_Distances` 数组也存储在栈上的同一内存块中。
			- **堆上分配**：
				- 如果 `FixedEntries` 作为类的成员变量，并且该类的实例分配在堆上，那么整个结构体（包括固定数组）会在堆上分配内存。这种情况下，数组的内存位置和结构体的内存位置是相同的，但它们是在堆上分配的。
- [[DOTS组件接口]]:
- [[DOTS特性]]
- [[DOTS查询方法]]
- [[NavMeshAPI记录]]
- Navmesh寻路的Demo文档获取的方式:
	- [Samples (lukaschod.github.io)](https://lukaschod.github.io/agents-navigation-docs/manual/samples.html)