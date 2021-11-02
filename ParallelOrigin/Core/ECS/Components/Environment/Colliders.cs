using System.Collections.Generic;
using ParallelOriginGameServer.Server.Extensions;

namespace ParallelOrigin.Core.ECS.Components.Environment {

    /// <summary>
    /// Represents a simple 2D box collider.
    /// </summary>
    public struct BoxCollider {
        public float width;
        public float height;
    }

    /// <summary>
    /// An marker entity telling the collision system that this entity is able to receive various collision events as components.
    /// This basically includes the entity in the collision checking... so it should only be attached to entities which really require collision events of any sort. 
    /// </summary>
    public struct CollisionReceiver { }

    /// <summary>
    /// A struct which stores active collisions with a bunch of other entities from this frame.
    /// </summary>
    public struct Collisions {
        public HashSet<QuadEntity> collisions;
    }

    /// <summary>
    /// A struct which marks an entity which received a bunch of newly entered collisions
    /// </summary>
    public struct CollisionsEntered {
        public HashSet<QuadEntity> entered;
    }

    /// <summary>
    /// A struct which tells an entity which collisions left
    /// </summary>
    public struct CollisionsLeft {
        public HashSet<QuadEntity> left;
    }
}