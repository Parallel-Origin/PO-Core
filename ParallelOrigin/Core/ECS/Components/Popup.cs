#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components {

#if SERVER
    
    /// <summary>
    /// A component tagging a <see cref="Entity" /> as a popup
    /// </summary>
    public struct Popup { }
      
#elif CLIENT
    
    /// <summary>
    ///     A component tagging a <see cref="Entity" /> as a popup
    /// </summary>
    [BurstCompile]
    public struct Popup : IComponentData { }
        
#endif
}