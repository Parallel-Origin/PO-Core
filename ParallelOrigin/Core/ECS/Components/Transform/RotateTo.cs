
#if CLIENT
using Unity.Burst;
using Unity.Entities;
#elif SERVER
using ParallelOriginGameServer.Server.Utils;
#endif

namespace ParallelOrigin.Core.ECS.Components.Transform {
    
#if SERVER
    
    /// <summary>
    ///  A component which gets assigned to an entity for rotating to a position
    /// </summary>
    public struct RotateTo  {
        public float speed;
        public Vector2d target;
    }
    
#elif CLIENT
    
    /// <summary>
    ///     A component which gets assigned to an entity for rotating to a position
    /// </summary>
    public struct RotateTo : IComponentData {
        public float speed;
        public Vector2d target;
    }

#endif
}