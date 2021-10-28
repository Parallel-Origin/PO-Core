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
        
        public ulong id;
        public string tag;
        public string type;

        public void Serialize(NetDataWriter writer) {
            writer.Put(id);
            writer.Put(tag, tag.Length);
            writer.Put(type, type.Length);
        }

        public void Deserialize(NetDataReader reader) {
            id = reader.GetULong();
            tag = reader.GetString(32);
            type = reader.GetString(32);
        }
    }    
    
#elif CLIENT
    
    /// <summary>
    ///  A componentn which stores a unique ID for each entity.
    /// </summary>
    [BurstCompile]
    public struct Identity : IComponentData, INetSerializable {
        
        public ulong id;
        public FixedString32 tag;
        public FixedString32 type;

        public void Serialize(NetDataWriter writer) {
            writer.Put(id);

            var tagCached = tag.ToStringCached();
            var typeCached = type.ToStringCached();
            writer.Put(tagCached, tagCached.Length);
            writer.Put(typeCached, typeCached.Length);
        }

        public void Deserialize(NetDataReader reader) {
            id = reader.GetULong();
            tag = reader.GetString(32);
            type = reader.GetString(32);
        }
    }
    
#endif
}