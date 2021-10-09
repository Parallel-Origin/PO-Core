#if CLIENT
using Unity.Burst;
using Unity.Entities;
#elif SERVER
using DefaultEcs;
#endif

namespace ParallelOrigin.Core.ECS.Components.Relations {
    
#if SERVER
    
    /// <summary>
    /// Represents a child which has a "Parent-Child" Relation to its parent
    /// </summary>
    public struct Child  {
        public Entity parent;
    }
    
#elif CLIENT

        /// <summary>
    ///     Represents a child which has a "Parent-Child" Relation to its parent
    /// </summary>
    [BurstCompile]
    public struct Child : IComponentData {
        public Entity parent;
    }

#endif
}