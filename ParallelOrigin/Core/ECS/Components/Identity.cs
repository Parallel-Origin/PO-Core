using LiteNetLib.Utils;
#if CLIENT
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Burst;
using Script.Extensions;
#endif

namespace ParallelOrigin.Core.ECS.Components
{
#if SERVER

    /// <summary>
    ///     A componentn which stores a unique ID for each entity.
    /// </summary>
    public struct Identity : INetSerializable
    {
        public long Id;
        public string Tag;
        public string Type;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Id);
            writer.Put(Tag, Tag.Length);
            writer.Put(Type, Type.Length);
        }

        public void Deserialize(NetDataReader reader)
        {
            Id = reader.GetLong();
            Tag = reader.GetString(32);
            Type = reader.GetString(32);
        }
    }

#elif CLIENT
    /// <summary>
    ///  A componentn which stores a unique ID for each entity.
    /// </summary>
    [BurstCompile]
    public struct Identity : IComponentData, INetSerializable {
        
        public long ID;
        public FixedString32Bytes Tag;
        public FixedString32Bytes Type;

        public void Serialize(NetDataWriter writer) {
            writer.Put(ID);

            var tagCached = Tag.ToStringCached();
            var typeCached = Type.ToStringCached();
            writer.Put(tagCached, tagCached.Length);
            writer.Put(typeCached, typeCached.Length);
        }

        public void Deserialize(NetDataReader reader) {
            ID = reader.GetLong();
            Tag = reader.GetString(32);
            Type = reader.GetString(32);
        }
    }

#endif
}