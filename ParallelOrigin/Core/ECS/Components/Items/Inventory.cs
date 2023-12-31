﻿#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
#elif SERVER
using Arch.Core;
using ParallelOrigin.Core.Base.Classes;
using ParallelOriginGameServer.Server.ThirdParty;
using System.Collections.Generic;
using Arch.LowLevel;
#endif
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.ECS.Components.Items {
#if SERVER

    /// <summary>
    ///     This class represents the local player inventory.
    ///     It is a <see cref="IBufferElementData" /> because it has no predefined size and therefore cannot be a <see cref="IComponentData" />
    /// </summary>
    public struct Inventory : INetSerializable
    {
        public UnsafeList<EntityLink> Items;

        public Inventory(int capacity)
        {
            Items = new UnsafeList<EntityLink>(capacity);
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.PutList(Items);
        }

        public void Deserialize(NetDataReader reader)
        {
            reader.GetList(ref Items);
        }
    }

    /// <summary>
    ///     Marks an item entity and tells us that its in a inventory.
    /// </summary>
    public struct InInventory
    {
        public Entity Inventory;
        public string DropType;
    }
    
    /// <summary>
    ///     Item which is on the ground.
    /// </summary>
    public struct OnGround
    {
        public string PickupType;
    }
    

#elif CLIENT
    /// <summary>
    ///     This class represents the local player inventory.
    ///     It is a <see cref="IBufferElementData" /> because it has no predefined size and therefore cannot be a <see cref="IComponentData" />
    /// </summary>
    [BurstCompile]
    public struct Inventory : IComponentData, INetSerializable{
        
        public UnsafeList<EntityLink> Items;
        
        public void Serialize(NetDataWriter writer) { NetworkSerializerExtensions.PutList(writer, ref Items); }
        public void Deserialize(NetDataReader reader) { NetworkSerializerExtensions.GetList(reader, ref Items); }
    }

    [BurstCompile]
    public struct AddedToInventory : IComponentData {
        public UnsafeList<Entity> Added;
    }

    [BurstCompile]
    public struct RemovedFromInventory : IComponentData {
        public UnsafeList<Entity> Removed;
    }
#endif
}