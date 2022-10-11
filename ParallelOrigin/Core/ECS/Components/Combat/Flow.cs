using System.Collections.Generic;

#if SERVER
using DefaultEcs;
#endif

namespace ParallelOrigin.Core.ECS.Components.Combat {

#if SERVER

    /// <summary>
    /// Marks an entity event as healing 
    /// </summary>
    public struct Heal {

        public Entity receiver;
        public float value;
    }
    
    // Marker to mark an entity as damage dealing 
    public struct Damage {

        public Entity sender;
        public Entity receiver;

        public bool killed;  // If the damage actually killed the entity 
    }

    // Marks an entity that is in combat with another one 
    public struct Attacks {

        public float intervall;
        public HashSet<Entity> entities;

    }
    
    // Marks an entity as dead, doesnt mean that its destroyed... its health is just below zero 
    public struct Dead{}

    /// <summary>
    /// Entities marked with that component will be respawned
    /// </summary>
    public struct OnDeathRespawn {
        public float timeInMs;
        public float intervall;
    }
    
#endif

}