#if SERVER
using DefaultEcs;
using ParallelOrigin.Core.Base.Classes;
#endif

namespace ParallelOrigin.Core.ECS.Components.Interactions
{
#if SERVER

    /// <summary>
    ///     An activity which assign a component <see cref="T" /> to the entity when it reached a certain destination.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct OnReach<T>
    {
        public Vector2d destination;
        public float distance;
        public bool cancelable;

        public T component;
    }

    /// <summary>
    ///     A component which lets an entity chop a resource entity ( tree ) down.
    /// </summary>
    public struct Chop
    {
        public Entity target;
    }

    /// <summary>
    ///     A component which represents a build process. 
    /// </summary>
    public struct Build
    {
        public string type;
        public Ingredient[] ingredients;
        
        public Vector2d position;  // The position
        public float distance;     // Distance to start building
        
        public Entity entity;      // The newly spawned structure, once target was reached 
        public float duration;
        
        public bool abortable; 
    }

#endif
}