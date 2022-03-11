
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
            writer.PutDic(localizations);
            writer.PutDic(uniqueLocalizations);
        }

        public void Deserialize(NetDataReader reader) {
            reader.GetDic(ref localizations);
            reader.GetDic(ref uniqueLocalizations);
        }
    }

#elif CLIENT

    /// <summary>
    ///  A component that stores a map of all valid localisations in a key value pair of string and id
    /// </summary>
    [BurstCompile]
    public struct Localizations : IComponentData, INetSerializable {
        
        public UnsafeHashMap<FixedString32, short> localizations;
        public UnsafeHashMap<FixedString32, FixedString32> uniqueLocalizations;

        public void Serialize(NetDataWriter writer) {
            NetworkSerializerExtensions.PutDic(writer, localizations);
            NetworkSerializerExtensions.PutDic(writer, uniqueLocalizations);
        }

        public void Deserialize(NetDataReader reader) {
            NetworkSerializerExtensions.GetDic(reader, ref localizations);
            NetworkSerializerExtensions.GetDic(reader, ref uniqueLocalizations);
        }
    }
    
#endif
}