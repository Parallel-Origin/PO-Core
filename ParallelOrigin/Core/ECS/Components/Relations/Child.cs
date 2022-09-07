#if CLIENT
using LiteNetLib.Utils;
using Unity.Burst;
using Unity.Entities;
#elif SERVER
using DefaultEcs;
using LiteNetLib.Utils;
#endif

namespace ParallelOrigin.Core.ECS.Components.Relations {
    
#if SERVER
    
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