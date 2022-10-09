
using System;

#if SERVER
using System.Collections.Concurrent;
using ConcurrentCollections;
using DefaultEcs;
using ParallelOrigin.Core.Base.Classes;
#endif

namespace ParallelOrigin.Core.ECS.Components.Environment {

#if SERVER

    /// <summary>
    /// Marks an entity as being able to load chunks. 
    /// </summary>
    public struct ChunkLoader {
        
        public Grid current;
        public Grid previous;
    }
        
#endif
}