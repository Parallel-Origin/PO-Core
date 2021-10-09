
#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components {

#if SERVER
    
    /// <summary>
    ///  A component which stores the progress of a certain task for a <see cref="Entity" />
    ///  One <see cref="Entity" /> is able to contain multiple progresses.
    /// </summary>
    public struct Progress  {
        public float progress;
        public float duration;
    }
    
#elif CLIENT 

    /// <summary>
    ///   A component which stores the progress of a certain task for a <see cref="Entity" />
    ///   One <see cref="Entity" /> is able to contain multiple progresses.
    /// </summary>
    [BurstCompile]
    public struct Progress : IComponentData {
        public float progress;
        public float duration;
    }

#endif
}