#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components.Lifecycle {
    
#if SERVER

     /// <summary>
     ///  Marks a <see cref="Entity" /> as "created" during this frame...
     ///  Gets removed after the frame.
     /// </summary>
     public struct Created  { }

     /// <summary>
     /// Marks an entity as active and alive. 
     /// </summary>
     public struct Active { }
     
     /// <summary>
     /// Marks an entity as a prefab. Should not take place ingame. 
     /// </summary>
     public struct Prefab{}
     
#elif CLIENT 
    
    /// <summary>
    ///     Marks a <see cref="Entity" /> as "created" during this frame...
    ///     Gets removed after the frame.
    /// </summary>
    [BurstCompile]
    public struct Created : IComponentData { }
    
#endif
}