
#if CLIENT
using ParallelOrigin.Core.Base.Classes;
using Unity.Burst;
using Unity.Entities;
#elif SERVER
using ParallelOrigin.Core.Base.Classes;
#endif

namespace ParallelOrigin.Core.ECS.Components.Transform {

#if SERVER
    
    /// <summary>
    ///  A component which selects a target position which gets processed by a system to move the <see cref="Location" /> to the <see cref="target" />
    /// </summary>
    public struct Movement  {
        public float speed;
        public Vector2d target;
    }
    
    /// <summary>
    /// Marks an entity as moving this frame. 
    /// </summary>
    public struct Moving{}
    
#elif CLIENT

    /// <summary>
    /// Marks an entity as moving around in the world.
    /// Normally requires a <see cref="Location"/> and a <see cref="Movement"/>
    /// </summary>
    [BurstCompile]
    public struct Movement : IComponentData {
        public float speed;
        public Vector2d target;
    }
    
    [BurstCompile]
    public struct Moving : IComponentData{}

#endif
}