using System.Collections.Generic;
using LiteNetLib.Utils;

#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#elif SERVER
using DefaultEcs;
#endif

namespace ParallelOrigin.Core.ECS.Components {

#if SERVER

    /// <summary>
    /// A component tagging a <see cref="Entity" /> as a popup
    /// </summary>
    public struct Popup : INetSerializable{

        // May be null...
        public EntityReference owner;
        
        // May be null
        public EntityReference target;

        // The option types its gonna have
        public List<string> options;

        public void Serialize(NetDataWriter writer) {
            writer.Put(owner);
            writer.Put(target);
        }

        public void Deserialize(NetDataReader reader) {
            owner = reader.Get<EntityReference>();
            target = reader.Get<EntityReference>();
        }
    }
      
#elif CLIENT

    /// <summary>
    ///     A component tagging a <see cref="Entity" /> as a popup
    /// </summary>
    [BurstCompile]
    public struct Popup : IComponentData, INetSerializable {

        // May be null...
        public EntityReference owner;
        
        // May be null
        public EntityReference target;

        public void Serialize(NetDataWriter writer) {
            writer.Put(owner);
            writer.Put(target);
        }

        public void Deserialize(NetDataReader reader) {
            owner = reader.Get<EntityReference>();
            target = reader.Get<EntityReference>();
        }
    }
        
#endif
}