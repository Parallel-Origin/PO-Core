using System.Collections.Generic;
using DefaultEcs;
#if SERVER
using System;
using Collections.Pooled;
using ParallelOriginGameServer.Server.Extensions;
#endif

namespace ParallelOrigin.Core.ECS.Components.Environment
{
#if SERVER

    /// <summary>
    ///     Represents a simple 2D box collider.
    /// </summary>
    public struct BoxCollider
    {
        public float width;
        public float height;
    }

    /// <summary>
    ///     An marker entity telling the collision system that this entity is able to do collisions.
    /// </summary>
    public struct Rigidbody
    {
    }

    /// <summary>
    ///     A struct which stores active collisions with a bunch of other entities from this frame.
    /// </summary>
    public readonly struct CollidedEvent
    {
        public readonly Entity _first;
        public readonly Entity _second;

        public CollidedEvent(Entity first, Entity second)
        {
            _first = first;
            _second = second;
        }
    }

    /// <summary>
    /// A marker for marking an <see cref="CollidedEvent"/> entity event as a new one : "Entered". 
    /// </summary>
    public readonly struct EnteredCollision
    {
    }
    
    /// <summary>
    /// A marker for marking an <see cref="CollidedEvent"/> entity event as a stayed one : "Stayed". 
    /// </summary>
    public readonly struct StayedCollision
    {
    }
    
    /// <summary>
    /// A marker for marking an <see cref="CollidedEvent"/> entity event as left.
    /// </summary>
    public readonly struct LeftCollision
    {
    }
    
#endif
}