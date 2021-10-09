#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components.Network {
    
#if SERVER
    
    /// <summary>
    /// Marks an <see cref="Entity"/> as an MMO-Entity which basically got spawned by the <see cref="Proximation{T,I}"/> API.
    /// </summary>
    public struct MMO {
        
    }
    
#elif CLIENT 
        
    /// <summary>
    /// Marks an <see cref="Entity"/> as an MMO-Entity which basically got spawned by the <see cref="Proximation{T,I}"/> API.
    /// </summary>
    [BurstCompile]
    public struct MMO : IComponentData{
        
    }

#endif
}