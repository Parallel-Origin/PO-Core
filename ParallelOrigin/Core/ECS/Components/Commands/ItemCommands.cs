#if SERVER
using DefaultEcs;
#endif

namespace ParallelOriginGameServer.Server.Commands {

#if SERVER
    
    /// <summary>
    /// Possible inventory perations
    /// </summary>
    public enum InventoryOperation {
        ADD,SUBSTRACT
    }
    
    /// <summary>
    /// A component for an entity which acts as an inventory command to modify a entities inventory.
    /// Required for multithreading inventory operations... creating new added items isnt possible otherwise
    /// Another reason ist that the adding/updating/removing logic for items is handled in its own inventory command system
    /// </summary>
    public struct InventoryCommand {
        
        public string type;
        public uint amount;
        
        public Entity inventory;
        public InventoryOperation opCode;

        public InventoryCommand(string type, uint amount, in Entity inventory, InventoryOperation operation) : this() {
            this.type = type;
            this.amount = amount;
            this.inventory = inventory;
            this.opCode = operation;
        }
    }
    
#endif
}