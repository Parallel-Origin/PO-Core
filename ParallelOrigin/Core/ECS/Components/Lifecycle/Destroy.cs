#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components.Lifecycle {
    
#if SERVER

        /// <summary>
        ///  A component which marks a <see cref="Entity" /> as getting destroyed during the next few seconds. 
        /// </summary>
        public struct DestroyAfter {
            public float seconds;
        }
    
        /// <summary>
        ///     Destroys a <see cref="Entity" /> at the end of the current frame.
        /// </summary>
        
        public struct Destroy  { }
    
        /// <summary>
        ///     Marks a <see cref="Entity" /> as destroyed for the lifecycle...
        /// </summary>
        
        public struct Destroyed  { }
        
#elif CLIENT 
    
    /// <summary>
    ///     A component which marks a <see cref="Entity" /> as getting destroyed during the next few ticks.
    /// </summary>
    [BurstCompile]
    public struct DestroyAfter : IComponentData {
        public short ticks;
    }

    /// <summary>
    ///     Destroys a <see cref="Entity" /> at the end of the current frame.
    /// </summary>
    [BurstCompile]
    public struct Destroy : IComponentData { }

    /// <summary>
    ///     Marks a <see cref="Entity" /> as destroyed for the lifecycle...
    /// </summary>
    [BurstCompile]
    public struct Destroyed : IComponentData { }
    
#endif
}