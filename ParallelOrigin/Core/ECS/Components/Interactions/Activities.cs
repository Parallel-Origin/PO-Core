using DefaultEcs;

namespace ParallelOrigin.Core.ECS.Components.Interactions {
    
    /// <summary>
    /// A component which lets an entity chop a resource entity ( tree ) down. 
    /// </summary>
    public struct Chop {
        public Entity target;
    }
}