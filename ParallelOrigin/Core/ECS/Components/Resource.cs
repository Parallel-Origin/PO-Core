
#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components {


#if SERVER
    
    /// <summary>
    ///  Just a Tag Component which clarifies that the Entity attached is a Resource
    /// </summary>
    public struct Resource { }
    
#elif CLIENT

    /// <summary>
    ///  Just a Tag Component which clarifies that the Entity attached is a Resource
    /// </summary>
    [BurstCompile]
    public struct Resource : IComponentData { }
    
#endif
}