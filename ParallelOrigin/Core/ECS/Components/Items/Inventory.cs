#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components.Items {
    
#if SERVER
    
    /// <summary>
    ///     A component attachable to a entity which contains variables to get represented as a item.
    /// </summary>
    public struct Item {

        public uint amount;
        public bool stackable;
    }
    
    /// <summary>
    ///  This class represents the local player inventory.
    ///  It is a <see cref="IBufferElementData" /> because it has no predefined size and therefore cannot be a <see cref="IComponentData" />
    /// </summary>
    public struct Inventory {
        public ReferencesBag items;
    }

    public struct AddedToInventory {
        public ReferencesBag added;
    }
    
    public struct RemovedFromInventory {
        public ReferencesBag removed;
    }

#elif CLIENT 
    
    /// <summary>
    ///     This class represents the local player inventory.
    ///     It is a <see cref="IBufferElementData" /> because it has no predefined size and therefore cannot be a <see cref="IComponentData" />
    /// </summary>
    [BurstCompile]
    public struct Inventory : IComponentData {
        public ReferencesBag items;
    }

    [BurstCompile]
    public struct AddedToInventory : IComponentData {
        public ReferencesBag added;
    }

    [BurstCompile]
    public struct RemovedFromInventory : IComponentData {
        public ReferencesBag removed;
    }
    
        /// <summary>
    ///     A component attachable to a entity which contains variables to get represented as a item.
    /// </summary>
    [BurstCompile]
    public struct Item : IComponentData {

        public int amount;
        public bool stackable;
    }
#endif
}