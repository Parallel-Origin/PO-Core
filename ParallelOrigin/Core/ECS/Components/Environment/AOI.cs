using System.Collections.Generic;
#if SERVER
using System;
using Collections.Pooled;
using DefaultEcs;
using ParallelOriginGameServer.Server.Extensions;
#endif

namespace ParallelOrigin.Core.ECS.Components.Environment
{
#if SERVER

    /// <summary>
    ///     Marks an entity for being able to take part in AOI events. 
    /// </summary>
    public struct AOI
    {
        public float range;

        public PooledSet<EntityReference> entities = new(512);
        public PooledSet<EntityReference> entered = new(512);
        public PooledSet<EntityReference> left = new(512);

        public AOI(float range) : this()
        {
            this.range = range;
        }
    }

    /// <summary>
    /// AOI Event
    /// </summary>
    public readonly struct AOIEvent
    {
        public readonly Entity aoiEntity;
        public readonly PooledSet<EntityReference> entityReferences;

        public AOIEvent(Entity aoiEntity, PooledSet<EntityReference> entityReferences)
        {
            this.aoiEntity = aoiEntity;
            this.entityReferences = entityReferences;
        }
    }
    
    /// <summary>
    /// Marks an AOI event to signal that its an Entered AOI event
    /// </summary>
    public struct EnteredAOI
    {
    }

    /// <summary>
    /// Marks an AOI event to signal that its an Stayed AOI event
    /// </summary>
    public struct StayedAOI
    {
    }

    /// <summary>
    /// Marks an AOI event to signal that its an left AOI event
    /// </summary>
    public struct LeftAOI
    {
    }

#endif
}