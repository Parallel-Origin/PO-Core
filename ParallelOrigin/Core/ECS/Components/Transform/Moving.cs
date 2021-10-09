

namespace ParallelOrigin.Core.ECS.Components.Transform {
    
#if SERVER
    
    /// <summary>
    /// Marks an entity as moving around in the world.
    /// Normally requires a <see cref="Location"/> and a <see cref="Movement"/>
    /// </summary>
    public struct Moving  {}
    
#elif CLIENT

    /// <summary>
    /// Marks an entity as moving around in the world.
    /// Normally requires a <see cref="Location"/> and a <see cref="Movement"/>
    /// </summary>
    [BurstCompile]
    public struct Moving : IComponentData {}
    
#endif
}