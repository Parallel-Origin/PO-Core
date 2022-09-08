using System.Collections.Generic;
using ParallelOrigin.Core.Base.Classes;

#if SERVER
using DefaultEcs;
using ParallelOriginGameServer.Server.Persistence;
#endif

namespace ParallelOriginGameServer.Server.Commands {

#if SERVER
    
    /// <summary>
    /// Possible chunk perations
    /// </summary>
    public enum ChunkOperation : byte {

        CREATE,
        LOADED,
        DELOADED

    }

    /// <summary>
    /// A command which represents a chunk event or command which can be reacted to 
    /// </summary>
    public struct ChunkCommand {

        public ChunkOperation operation;
        public Entity by;

        public HashSet<Grid> grids;
        public HashSet<Chunk> chunks;

    }

#endif
}