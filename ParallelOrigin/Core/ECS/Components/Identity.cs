#if CLIENT
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
#endif

namespace ParallelOrigin.Core.ECS.Components {

#if SERVER
    
    /// <summary>
    ///  A componentn which stores a unique ID for each entity.
    /// </summary>
    public struct Identity  {
        public long id;
        public string tag;
        public string type;
    }    
    
#elif CLIENT
    
    /// <summary>
    ///  A componentn which stores a unique ID for each entity.
    /// </summary>
    [BurstCompile]
    public struct Identity : IComponentData {
        public long id;
        public FixedString32 tag;
        public FixedString32 type;
    }
    
#endif
}