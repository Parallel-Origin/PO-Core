
#if SERVER
using Arch.Core;
using ParallelOrigin.Core.Base.Classes;
#endif

namespace ParallelOrigin.Core.ECS.Components.Interactions
{
#if SERVER
    
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

    /// <summary>
    ///     A component which makes an entity pickup an target item on collision. 
    /// </summary>
    public struct Pickup
    {
        public Entity target;
        public uint amount;
    }

#endif
}