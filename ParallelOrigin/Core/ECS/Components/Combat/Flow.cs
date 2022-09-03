using System.Collections.Generic;

#if SERVER
using DefaultEcs;
#endif

namespace ParallelOrigin.Core.ECS.Components.Combat {

#if SERVER
        
    // Marker to mark an entity as damage dealing 
    public struct Damage {

        public Entity sender;
        public Entity receiver;

    }

    // Marks an entity that is in combat with another one 
    public struct Attacks {

        public float intervall;
        public HashSet<Entity> entities;

    }
    
#endif

}