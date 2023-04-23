using System.Collections.Generic;
#if SERVER
using System;
using Arch.Core;
using Collections.Pooled;
using ParallelOriginGameServer.Server.Extensions;
using Arch.LowLevel;
using ParallelOriginGameServer.Server.ThirdParty;
#endif

namespace ParallelOrigin.Core.ECS.Components.Environment
{
#if SERVER

    /// <summary>
    ///     Marks an entity for being able to take part in AOI events. 
    /// </summary>
    public struct Aoi
    {
        public float Range;

        public Handle<PooledSet<EntityLink>> Entities;
        public Handle<PooledSet<EntityLink>> Entered;
        public Handle<PooledSet<EntityLink>> Left;

        public Aoi(float range) : this()
        {
            this.Range = range;
            Entities = new PooledSet<EntityLink>(512).ToHandle();
            Entered = new PooledSet<EntityLink>(512).ToHandle();
            Left = new PooledSet<EntityLink>(512).ToHandle();
        }
    }

#endif
}