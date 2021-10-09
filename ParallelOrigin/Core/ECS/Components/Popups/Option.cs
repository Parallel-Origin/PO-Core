
    
#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components.Popups {
    
#if SERVER
    
    /// <summary>
    ///     A component for a <see cref="Entity" /> which is a option for a <see cref="Popup" />
    /// </summary>
    
    public struct Option  { }
    
#elif CLIENT 
    
    /// <summary>
    ///     A component for a <see cref="Entity" /> which is a option for a <see cref="Popup" />
    /// </summary>
    [BurstCompile]
    public struct Option : IComponentData { }

#endif
}