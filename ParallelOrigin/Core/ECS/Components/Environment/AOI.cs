using System.Collections.Generic;
#if SERVER
using System;
using Collections.Pooled;
using ParallelOriginGameServer.Server.Extensions;
#endif

namespace ParallelOrigin.Core.ECS.Components.Environment
{
#if SERVER

    /// <summary>
    ///     Marks an entity for being able to
    /// </summary>
    public struct AOI
    {
        public float range;
        public HashSet<QuadEntity> inAOI;
    }

    /// <summary>
    ///     Marks an entity with a list of entities that lately entered its aoi
    /// </summary>
    public struct AOIEntered : IDisposable
    {
        public PooledSet<QuadEntity> entered; // Pooled to reduce memory usage -> Exists one frame only

        public void Dispose()
        {
            entered.Dispose();
        }
    }

    /// <summary>
    ///     Marks an entity with a list of entities which lately left its aoi
    /// </summary>
    public struct AOILeft : IDisposable
    {
        public PooledSet<QuadEntity> left; // Pooled to reduce memory usage  -> Exists one frame only

        public void Dispose()
        {
            left.Dispose();
        }
    }

#endif
}