
using System.Collections.Generic;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;

#if CLIENT
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
#endif

namespace ParallelOrigin.Core.ECS.Components.UI {

#if SERVER
    
    /// <summary>
    /// A component that stores a map of all valid localisations in a key value pair of string and id
    /// </summary>
    public struct Localizations : INetSerializable{
        
        public Dictionary<string, short> localizations;
        public Dictionary<string, string> uniqueLocalizations;

        public void Serialize(NetDataWriter writer) {
            NetworkSerializerExtensions.SerializeDic(writer, localizations);
            NetworkSerializerExtensions.SerializeDic(writer, uniqueLocalizations);
        }

        public void Deserialize(NetDataReader reader) {
            NetworkSerializerExtensions.DeserializeDic(reader, ref localizations);
            NetworkSerializerExtensions.DeserializeDic(reader, ref uniqueLocalizations);
        }
    }

#elif CLIENT

    /// <summary>
    ///  A component that stores a map of all valid localisations in a key value pair of string and id
    /// </summary>
    [BurstCompile]
    public struct Localizations : IComponentData {
        public UnsafeHashMap<FixedString32, short> localizations;
        public UnsafeHashMap<FixedString32, FixedString32> uniqueLocalizations;
    }
    
#endif
}