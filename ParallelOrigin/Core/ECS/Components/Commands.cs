using System.Collections.Generic;
using ParallelOrigin.Core.Base.Classes;
#if SERVER
using DefaultEcs;
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
        public ChunkOperation operation;
        public Entity by;

        public HashSet<Grid> grids;
        public HashSet<ParallelOriginGameServer.Server.Persistence.Chunk> chunks;
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
        public string type;
        public uint amount;

        public Entity inventory;
        public InventoryOperation opCode;

        public InventoryCommand(string type, uint amount, in Entity inventory, InventoryOperation operation) : this()
        {
            this.type = type;
            this.amount = amount;
            this.inventory = inventory;
            opCode = operation;
        }
    }

    /// <summary>
    ///     A command which should spawn in a popup with certain data.
    /// </summary>
    public struct PopUpCommand
    {
        public string type;
        public Entity owner;
        public Entity target;

        public PopUpCommand(string type, Entity owner, Entity target)
        {
            this.type = type;
            this.owner = owner;
            this.target = target;
        }
    }

#endif
}