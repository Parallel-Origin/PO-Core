using System.Collections.Generic;
using Arch.LowLevel;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;
using ParallelOriginGameServer.Server.ThirdParty;
#if CLIENT
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
#endif

namespace ParallelOrigin.Core.ECS.Components
{
#if SERVER

    /// <summary>
    ///     A component that stores a map of all valid localisations in a key value pair of string and id
    /// </summary>
    public struct Localizations : INetSerializable
    {
        public Handle<Dictionary<string, short>> localizations;
        public Handle<Dictionary<string, string>> uniqueLocalizations;

        public void Serialize(NetDataWriter writer)
        {
            writer.PutDic(localizations.Get());
            writer.PutDic(uniqueLocalizations.Get());
        }

        public void Deserialize(NetDataReader reader)
        {
            //reader.GetDic(ref localizations);
            //reader.GetDic(ref uniqueLocalizations);
        }
    }

    /// <summary>
    ///     A component which makes the ui element include its owner name as a unique localisation, requires a also attached <see cref="Localizations" />
    /// </summary>
    public struct OwnerNameLocalisation
    {
    }

#elif CLIENT
    /// <summary>
    ///  A component that stores a map of all valid localisations in a key value pair of string and id
    /// </summary>
    [BurstCompile]
    public struct Localizations : IComponentData, INetSerializable {
        
        public UnsafeParallelHashMap<FixedString32Bytes, short> localizations;
        public UnsafeParallelHashMap<FixedString32Bytes, FixedString32Bytes> uniqueLocalizations;

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