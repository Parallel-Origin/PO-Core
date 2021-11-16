#if SERVER
using DefaultEcs;
#endif

namespace ParallelOriginGameServer.Server.Commands {

#if SERVER
    
    /// <summary>
    /// Possible inventory perations
    /// </summary>
    public enum InventoryOperation {
        ADD,UPDATE,REMOVE
    }
    
    /// <summary>
    /// A component for an entity which acts as an inventory command to modify a entities inventory.
    /// Required for multithreading inventory operations... creating new added items isnt possible otherwise
    /// </summary>
    public struct InventoryCommand {
        
        public string type;
        public uint amount;
        
        public Entity inventory;
        public Entity item;

        public InventoryCommand(string type, uint amount, in Entity inventory) : this() {
            this.type = type;
            this.amount = amount;
            this.inventory = inventory;
        }

        public InventoryCommand(uint amount, in Entity inventory, in Entity item) : this() {
            this.amount = amount;
            this.inventory = inventory;
            this.item = item;
        }
    }
    
#endif
}