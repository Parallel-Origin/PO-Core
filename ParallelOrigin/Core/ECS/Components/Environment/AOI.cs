using System.Collections.Generic;
using Arch.LowLevel;
using ParallelOriginGameServer.Server.ThirdParty;
#if SERVER
using System;
using Arch.Core;
using Collections.Pooled;
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

        public Handle<PooledSet<EntityLink>> entities;
        public Handle<PooledSet<EntityLink>> entered;
        public Handle<PooledSet<EntityLink>> left;

        public AOI(float range) : this()
        {
            this.range = range;
            entities = new PooledSet<EntityLink>(512).ToHandle();
            entered = new PooledSet<EntityLink>(512).ToHandle();
            left = new PooledSet<EntityLink>(512).ToHandle();
        }
    }

#endif
}