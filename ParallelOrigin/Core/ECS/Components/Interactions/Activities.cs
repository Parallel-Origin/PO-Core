#if SERVER
using DefaultEcs;
using ParallelOrigin.Core.Base.Classes;
#endif

namespace ParallelOrigin.Core.ECS.Components.Interactions {

#if SERVER
    
    /// <summary>
    /// An activity which assign a component <see cref="T"/> to the entity when it reached a certain destination. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct OnReach<T> {

        public Vector2d destination;
        public float distance;
        public bool cancelable;
        
        public T component;
    }
    
    /// <summary>
    /// A component which lets an entity chop a resource entity ( tree ) down. 
    /// </summary>
    public struct Chop {
        public Entity target;
    }

    /// <summary>
    /// A component which represents a build command for an entity to construct a certain structure.
    /// </summary>
    public struct Build {

        public string type;    // Structure to build
        public Entity entity;  // The newly spaned structure 
    }
    
#endif
}