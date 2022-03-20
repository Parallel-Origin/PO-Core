
#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components {

#if SERVER

    /// <summary>
    ///  Represents a Strcuture in the Game
    /// </summary>
    public struct Structure {
        public EntityReference owner;
    }
    
#elif CLIENT
    
    /// <summary>
    /// Represents a Strcuture in the Game
    /// </summary>
    public struct Structure : IComponentData {
        public long ownerID;
    }
    
#endif
}