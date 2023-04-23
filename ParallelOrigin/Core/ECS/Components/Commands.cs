using System.Collections.Generic;
using ParallelOrigin.Core.Base.Classes;
#if SERVER
using Arch.Core;
#endif

namespace ParallelOrigin.Core.ECS.Components
{
#if SERVER

    /// <summary>
    ///     Possible chunk perations
    /// </summary>
    public enum ChunkOperation : byte
    {
        CREATE,
        LOADED,
        DELOADED
    }

    /// <summary>
    ///     A command which represents a chunk event or command which can be reacted to
    /// </summary>
    public struct ChunkCommand
    {
        public ChunkOperation Operation;
        public Entity By;

        public HashSet<Grid> Grids;
        public HashSet<ParallelOriginGameServer.Server.Persistence.Chunk> Chunks;
    }

    /// <summary>
    ///     Possible inventory perations
    /// </summary>
    public enum InventoryOperation : byte
    {
        ADD,
        SUBSTRACT
    }

    /// <summary>
    ///     A component for an entity which acts as an inventory command to modify a entities inventory.
    ///     Required for multithreading inventory operations... creating new added items isnt possible otherwise
    ///     Another reason ist that the adding/updating/removing logic for items is handled in its own inventory command system
    /// </summary>
    public struct InventoryCommand
    {
        public string Type;
        public uint Amount;

        public Entity Inventory;
        public InventoryOperation OpCode;

        public InventoryCommand(string type, uint amount, in Entity inventory, InventoryOperation operation) : this()
        {
            this.Type = type;
            this.Amount = amount;
            this.Inventory = inventory;
            OpCode = operation;
        }
    }

    /// <summary>
    ///     A command which should spawn in a popup with certain data.
    /// </summary>
    public struct PopUpCommand
    {
        public string Type;
        public Entity Owner;
        public Entity Target;

        public PopUpCommand(string type, Entity owner, Entity target)
        {
            this.Type = type;
            this.Owner = owner;
            this.Target = target;
        }
    }

#endif
}