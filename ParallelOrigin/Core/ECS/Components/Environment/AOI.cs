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
    }

    /// <summary>
    /// AOI Event
    /// </summary>
    public readonly struct AOIEvent
    {
        public readonly Entity first;
        public readonly Entity second;

        public AOIEvent(Entity first, Entity second)
        {
            this.first = first;
            this.second = second;
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