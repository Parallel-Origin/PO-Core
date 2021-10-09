#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components.Graphical {
    
#if SERVER
    
    /// <summary>
    /// A struct which marks an entity as invisible to deactivate its rendering. 
    /// </summary>
    public struct Invisible {}
    
#elif CLIENT 
    
   /// <summary>
    /// A struct which marks an entity as invisible to deactivate its rendering. 
    /// </summary>
    [BurstCompatible]
    public struct Invisible : IComponentData{}
#endif
}