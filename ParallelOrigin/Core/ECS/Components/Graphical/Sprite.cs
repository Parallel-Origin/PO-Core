#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components.Graphical {
    
#if SERVER

    /// <summary>
    /// Represents a reference to a sprite, icon or logo for a entity.
    /// </summary>
    
    public struct Sprite  {
                
        /// <summary>
        ///     The ID of a mesh from the internal database we wanna reference for this entity
        /// </summary>
        public short id;
    }
#elif CLIENT 
    
    /// <summary>
    /// Represents a reference to a sprite, icon or logo for a entity.
    /// </summary>
    [BurstCompile]
    public struct Sprite : IComponentData {
                
        /// <summary>
        ///  The ID of a mesh from the internal database we wanna reference for this entity
        /// </summary>
        public short id;
    }
#endif
}