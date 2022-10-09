
using System.Collections.Generic;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;
#if CLIENT
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
#endif

namespace ParallelOrigin.Core.ECS.Components {

#if SERVER
    
    /// <summary>
    ///   A component which stores a "Parent-Child" relation to its <see cref="Child" />
    ///   Represented by a buffer ( aka. Burst-List ), a parent entity basically has a list of those <see cref="Parent" /> components instead of a single one with a list of childs
    /// </summary>
    public struct Parent : INetSerializable{
        
        public List<EntityReference> children;
        public void Serialize(NetDataWriter writer) { writer.PutList(children); }
        public void Deserialize(NetDataReader reader) { reader.GetList(ref children); }
    }    
    
    /// <summary>
    /// Represents a child which has a "Parent-Child" Relation to its parent
    /// </summary>
    public struct Child : INetSerializable{
        
        public EntityReference parent;
        
        public void Serialize(NetDataWriter writer) { writer.Put(parent); }
        public void Deserialize(NetDataReader reader) { parent.Deserialize(reader); }
    }
    
#elif CLIENT

    /// <summary>
    ///     A component which stores a "Parent-Child" relation to its <see cref="Child" />
    ///     Represented by a buffer ( aka. Burst-List ), a parent entity basically has a list of those <see cref="Parent" /> components instead of a single one with a list of childs
    /// </summary>
    [BurstCompile]
    public struct Parent : IComponentData, INetSerializable{
            
        public UnsafeList<EntityReference> children;

        public void Serialize(NetDataWriter writer) { NetworkSerializerExtensions.PutList(writer, ref children); }
        public void Deserialize(NetDataReader reader) { NetworkSerializerExtensions.GetList(reader, ref children); }
    }
    
            /// <summary>
    ///     Represents a child which has a "Parent-Child" Relation to its parent
    /// </summary>
    [BurstCompile]
    public struct Child : IComponentData, INetSerializable {
            
        public EntityReference parent;

        public void Serialize(NetDataWriter writer) { writer.Put(parent); }
        public void Deserialize(NetDataReader reader) {  parent.Deserialize(reader); }
    }
    
#endif
}