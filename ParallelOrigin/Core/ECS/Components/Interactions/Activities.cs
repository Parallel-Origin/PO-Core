
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
        public Entity Target;
    }

    /// <summary>
    ///     A component which represents a build process. 
    /// </summary>
    public struct Build
    {
        public string Type;
        public Ingredient[] Ingredients;
        
        public Vector2d Position;  // The position
        public float Distance;     // Distance to start building
        
        public Entity Entity;      // The newly spawned structure, once target was reached 
        public float Duration;
        
        public bool Abortable; 
    }

    /// <summary>
    ///     A component which makes an entity pickup an target item on collision. 
    /// </summary>
    public struct Pickup
    {
        public Entity Target;
        public uint Amount;
    }

#endif
}