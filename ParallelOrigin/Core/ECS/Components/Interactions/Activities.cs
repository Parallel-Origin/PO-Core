#if SERVER
using DefaultEcs;
#endif

namespace ParallelOrigin.Core.ECS.Components.Interactions {

#if SERVER

    /// <summary>
    /// A component which lets an entity chop a resource entity ( tree ) down. 
    /// </summary>
    public struct Chop {
        public Entity target;
    }
    
#endif
}