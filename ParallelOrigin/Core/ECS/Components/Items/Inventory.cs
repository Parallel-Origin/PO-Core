﻿using System.Collections.Generic;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;

#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
#elif SERVER
using DefaultEcs;
#endif

namespace ParallelOrigin.Core.ECS.Components.Items {
    
#if SERVER
    
    /// <summary>
    ///     A component attachable to a entity which contains variables to get represented as a item.
    /// </summary>
    public struct Item : INetSerializable{

        public uint amount;
        public bool stackable;
        
        public void Serialize(NetDataWriter writer) { 
            writer.Put(amount); 
            writer.Put(stackable); 
        }
        public void Deserialize(NetDataReader reader) { 
            amount = reader.GetUInt();
            stackable = reader.GetBool();
        }
    }
    
    /// <summary>
    ///  This class represents the local player inventory.
    ///  It is a <see cref="IBufferElementData" /> because it has no predefined size and therefore cannot be a <see cref="IComponentData" />
    /// </summary>
    public struct Inventory : INetSerializable{
        
        public List<EntityReference> items;
        public Inventory(int size) { this.items = new List<EntityReference>(size); }
        
        public void Serialize(NetDataWriter writer) { NetworkSerializerExtensions.PutList(writer, items); }
        public void Deserialize(NetDataReader reader) { NetworkSerializerExtensions.GetList(reader, ref items); }
    }

    /// <summary>
    /// Marks an item entity and tells us that its in a inventory. 
    /// </summary>
    public struct InInventory {
        public Entity inventory;
    }
    
    /// <summary>
    /// A component which marks an entity to notify it about newly added items. 
    /// </summary>
    public struct AddedToInventory {
        
        public List<Entity> added;
        public AddedToInventory(int size) { this.added = new List<Entity>(size); }
    }
    
    /// <summary>
    /// A component which marks an entity to notify it about newly added items. 
    /// </summary>
    public struct UpdatedInInventory {
        
        public List<Entity> updated;
        public UpdatedInInventory(int size) { this.updated = new List<Entity>(size); }
    }
    
    /// <summary>
    /// A component which marks an entity to notifty it about newly removed items.
    /// </summary>
    public struct RemovedFromInventory {
        
        public List<Entity> removed;
        public RemovedFromInventory(int size) { this.removed = new List<Entity>(size); }
    }

#elif CLIENT 
    
    /// <summary>
    ///     This class represents the local player inventory.
    ///     It is a <see cref="IBufferElementData" /> because it has no predefined size and therefore cannot be a <see cref="IComponentData" />
    /// </summary>
    [BurstCompile]
    public struct Inventory : IComponentData, INetSerializable{
        
        public UnsafeList<EntityReference> items;
        
        public void Serialize(NetDataWriter writer) { NetworkSerializerExtensions.PutList(writer, ref items); }
        public void Deserialize(NetDataReader reader) { NetworkSerializerExtensions.GetList(reader, ref items); }
    }

    [BurstCompile]
    public struct AddedToInventory : IComponentData {
        public UnsafeList<Entity> added;
    }

    [BurstCompile]
    public struct RemovedFromInventory : IComponentData {
        public UnsafeList<Entity> removed;
    }
    
        /// <summary>
    ///     A component attachable to a entity which contains variables to get represented as a item.
    /// </summary>
    [BurstCompile]
    public struct Item : IComponentData, INetSerializable {

        public uint amount;
        public bool stackable;
        
        public void Serialize(NetDataWriter writer) { 
            writer.Put(amount); 
            writer.Put(stackable); 
        }
        public void Deserialize(NetDataReader reader) { 
            amount = reader.GetUInt();
            stackable = reader.GetBool();
        }
    }
#endif
}