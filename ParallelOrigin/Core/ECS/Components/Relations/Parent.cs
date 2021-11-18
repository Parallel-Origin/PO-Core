
using System.Collections.Generic;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;
#if CLIENT
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
#endif

namespace ParallelOrigin.Core.ECS.Components.Relations {

#if SERVER
    
    /// <summary>
    ///   A component which stores a "Parent-Child" relation to its <see cref="Child" />
    ///   Represented by a buffer ( aka. Burst-List ), a parent entity basically has a list of those <see cref="Parent" /> components instead of a single one with a list of childs
    /// </summary>
    public struct Parent : INetSerializable{
        
        public List<EntityReference> children;
        public void Serialize(NetDataWriter writer) { NetworkSerializerExtensions.SerializeList(writer, children); }
        public void Deserialize(NetDataReader reader) { NetworkSerializerExtensions.DeserializeList(reader, ref children); }
    }    
    
#elif CLIENT

    /// <summary>
    ///     A component which stores a "Parent-Child" relation to its <see cref="Child" />
    ///     Represented by a buffer ( aka. Burst-List ), a parent entity basically has a list of those <see cref="Parent" /> components instead of a single one with a list of childs
    /// </summary>
    [BurstCompile]
    public struct Parent : IComponentData, INetSerializable{
            
        public ReferencesBag children;

        public void Serialize(NetDataWriter writer) { writer.Put(children); }
        public void Deserialize(NetDataReader reader) { children = reader.Get<ReferencesBag>(); }
        }
    
#endif
}