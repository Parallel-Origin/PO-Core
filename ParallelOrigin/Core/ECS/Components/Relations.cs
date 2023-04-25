using System.Collections.Generic;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;
#if CLIENT
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
#elif SERVER
using Arch.LowLevel;
#endif

namespace ParallelOrigin.Core.ECS.Components
{
#if SERVER

    /// <summary>
    ///     A component which stores a "Parent-Child" relation to its <see cref="Child" />
    ///     Represented by a buffer ( aka. Burst-List ), a parent entity basically has a list of those <see cref="Parent" /> components instead of a single one with a list of childs
    /// </summary>
    public struct Parent : INetSerializable
    {
        public UnsafeList<EntityLink> Children;

        public Parent(int capacity)
        {
            Children = new UnsafeList<EntityLink>(capacity);
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.PutList(Children);
        }

        public void Deserialize(NetDataReader reader)
        {
            reader.GetList(ref Children);
        }
    }

    /// <summary>
    ///     Represents a child which has a "Parent-Child" Relation to its parent
    /// </summary>
    public struct Child : INetSerializable
    {
        public EntityLink Parent;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Parent);
        }

        public void Deserialize(NetDataReader reader)
        {
            Parent.Deserialize(reader);
        }
    }

#elif CLIENT
    /// <summary>
    ///     A component which stores a "Parent-Child" relation to its <see cref="Child" />
    ///     Represented by a buffer ( aka. Burst-List ), a parent entity basically has a list of those <see cref="Parent" /> components instead of a single one with a list of childs
    /// </summary>
    [BurstCompile]
    public struct Parent : IComponentData, INetSerializable{
            
        public UnsafeList<EntityLink> Children;

        public void Serialize(NetDataWriter writer) { NetworkSerializerExtensions.PutList(writer, ref Children); }
        public void Deserialize(NetDataReader reader) { NetworkSerializerExtensions.GetList(reader, ref Children); }
    }
    
            /// <summary>
    ///     Represents a child which has a "Parent-Child" Relation to its parent
    /// </summary>
    [BurstCompile]
    public struct Child : IComponentData, INetSerializable {
            
        public EntityLink Parent;

        public void Serialize(NetDataWriter writer) { writer.Put(Parent); }
        public void Deserialize(NetDataReader reader) {  Parent.Deserialize(reader); }
    }

#endif
}