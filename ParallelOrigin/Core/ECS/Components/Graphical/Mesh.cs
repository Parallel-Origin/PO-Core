#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components.Graphical {
    
#if SERVER

    /// <summary>
    ///     A component that stores a ID of a particular mesh
    /// </summary>
    public struct Mesh  {
        
        /// <summary>
        ///     The ID of a mesh from the internal database we wanna reference for this entity
        /// </summary>
        public short id;

        /// <summary>
        ///     If true, a system takes care of loading the referenced mesh... if false, you are forced to do that yourself.
        /// </summary>
        public bool instantiate;
    }
    
#elif CLIENT 
    
    /// <summary>
    ///     A component that stores a ID of a particular mesh
    /// </summary>
    public struct Mesh : IComponentData {
        
        /// <summary>
        ///     The ID of a mesh from the internal database we wanna reference for this entity
        /// </summary>
        public short id;

        /// <summary>
        ///     If true, a system takes care of loading the referenced mesh... if false, you are forced to do that yourself.
        /// </summary>
        public bool instantiate;
    }
#endif
}