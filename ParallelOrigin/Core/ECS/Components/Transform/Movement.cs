
#if CLIENT
using Unity.Burst;
using Unity.Entities;
#elif SERVER
using ParallelOriginGameServer.Server.Utils;
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
    
#elif CLIENT

    /// <summary>
    /// Marks an entity as moving around in the world.
    /// Normally requires a <see cref="Location"/> and a <see cref="Movement"/>
    /// </summary>
    [BurstCompile]
    public struct Moving : IComponentData {}

#endif
}