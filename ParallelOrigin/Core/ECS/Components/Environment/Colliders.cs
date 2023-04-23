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
        public float Width;
        public float Height;
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
        public readonly Entity First;
        public readonly Entity Second;

        public EnteredCollision(Entity first, Entity second)
        {
            First = first;
            Second = second;
        }
    }
    
    /// <summary>
    /// A marker for marking an <see cref="CollidedEvent"/> entity event as a stayed one : "Stayed". 
    /// </summary>
    public readonly struct Collision
    {
        public readonly Entity First;
        public readonly Entity Second;

        public Collision(Entity first, Entity second)
        {
            First = first;
            Second = second;
        }
    }
    
    /// <summary>
    /// A marker for marking an <see cref="CollidedEvent"/> entity event as a stayed one : "Stayed". 
    /// </summary>
    public readonly struct StayedCollision
    {
        public readonly Entity First;
        public readonly Entity Second;

        public StayedCollision(Entity first, Entity second)
        {
            First = first;
            Second = second;
        }
    }
    
    /// <summary>
    /// A marker for marking an <see cref="CollidedEvent"/> entity event as left.
    /// </summary>
    public readonly struct LeftCollision
    {
        public readonly Entity First;
        public readonly Entity Second;

        public LeftCollision(Entity first, Entity second)
        {
            First = first;
            Second = second;
        }
    }
    
#endif
}