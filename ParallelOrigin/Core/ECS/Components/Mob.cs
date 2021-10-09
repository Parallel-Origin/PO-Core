#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components {
    
#if SERVER
    
    /// <summary>
    /// A component that marks an <see cref="Entity"/> as a mob ingame
    /// </summary>
    public struct Mob { }
    
#elif CLIENT
    
    /// <summary>
    /// A component that marks an <see cref="Entity"/> as a mob ingame
    /// </summary>
    [BurstCompile]
    public struct Mob : IComponentData{ }
    
#endif
}