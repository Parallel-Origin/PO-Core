using LiteNetLib.Utils;

#if CLIENT
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Burst;
using Script.Extensions;
#endif

namespace ParallelOrigin.Core.ECS.Components {

#if SERVER
    
    /// <summary>
    ///  A componentn which stores a unique ID for each entity.
    /// </summary>
    public struct Identity : INetSerializable {
        
        public long id;
        public string tag;
        public string type;

        public void Serialize(NetDataWriter writer) {
            writer.Put(id);
            writer.Put(tag);
            writer.Put(type);
        }

        public void Deserialize(NetDataReader reader) {
            id = reader.GetLong();
            tag = reader.GetString();
            type = reader.GetString();
        }
    }    
    
#elif CLIENT
    
    /// <summary>
    ///  A componentn which stores a unique ID for each entity.
    /// </summary>
    [BurstCompile]
    public struct Identity : IComponentData, INetSerializable {
        
        public long id;
        public FixedString32 tag;
        public FixedString32 type;

        public void Serialize(NetDataWriter writer) {
            writer.Put(id);
            writer.Put(tag.ToStringCached());
            writer.Put(type.ToStringCached());
        }

        public void Deserialize(NetDataReader reader) {
            id = reader.GetLong();
            tag = reader.GetString(32);
            type = reader.GetString(32);
        }
    }
    
#endif
}