using System.Collections.Generic;
using DefaultEcs;
using ParallelOrigin.Core.Base.Classes;
using ParallelOriginGameServer.Server.Persistence;

namespace ParallelOriginGameServer.Server.Commands; 

/// <summary>
/// Possible chunk perations
/// </summary>
public enum ChunkOperation : byte{
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