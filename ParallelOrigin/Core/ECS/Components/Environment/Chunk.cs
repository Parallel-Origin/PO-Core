
using System;

#if SERVER
using System.Collections.Concurrent;
using ConcurrentCollections;
using DefaultEcs;
using ParallelOriginGameServer.Server.Utils;
#endif

namespace ParallelOrigin.Core.ECS.Components.Environment {

#if SERVER
    
    /// <summary>
    /// Represents an chunk entity in the game. 
    /// </summary>
    public struct Chunk {

        public Grid grid;

        public DateTime createdOn;

        public ConcurrentHashSet<Entity> contains;  // Required due to fast acess when there many entities inside the chunk
        public NativeList<Entity> loadedBy;
    }

    /// <summary>
    /// Marks an entity as being able to load chunks. 
    /// </summary>
    public struct ChunkLoader {
        
        public Grid current;
        public Grid previous;
    }
        
#endif
}