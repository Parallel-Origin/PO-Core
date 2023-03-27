using System.Collections.Generic;

#if SERVER
using System;
using Collections.Pooled;
using ParallelOriginGameServer.Server.Extensions;
using Arch.Core;
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
    /// A marker for marking an <see cref="CollidedEvent"/> entity event as a new one : "Entered". 
    /// </summary>
    public readonly struct EnteredCollision
    {
        public readonly Entity _first;
        public readonly Entity _second;

        public EnteredCollision(Entity first, Entity second)
        {
            _first = first;
            _second = second;
        }
    }
    
    /// <summary>
    /// A marker for marking an <see cref="CollidedEvent"/> entity event as a stayed one : "Stayed". 
    /// </summary>
    public readonly struct Collision
    {
        public readonly Entity _first;
        public readonly Entity _second;

        public Collision(Entity first, Entity second)
        {
            _first = first;
            _second = second;
        }
    }
    
    /// <summary>
    /// A marker for marking an <see cref="CollidedEvent"/> entity event as a stayed one : "Stayed". 
    /// </summary>
    public readonly struct StayedCollision
    {
        public readonly Entity _first;
        public readonly Entity _second;

        public StayedCollision(Entity first, Entity second)
        {
            _first = first;
            _second = second;
        }
    }
    
    /// <summary>
    /// A marker for marking an <see cref="CollidedEvent"/> entity event as left.
    /// </summary>
    public readonly struct LeftCollision
    {
        public readonly Entity _first;
        public readonly Entity _second;

        public LeftCollision(Entity first, Entity second)
        {
            _first = first;
            _second = second;
        }
    }
    
#endif
}