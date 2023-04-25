using System.Collections.Generic;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;
#if CLIENT
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
#elif SERVER
using System.Runtime.CompilerServices;
using ParallelOriginGameServer.Server.ThirdParty;
using Arch.LowLevel;
#endif

namespace ParallelOrigin.Core.ECS.Components
{
#if SERVER

    /// <summary>
    ///     A component that stores a map of all valid localisations in a key value pair of string and id
    /// </summary>
    public struct Localizations : INetSerializable
    {
        public Handle<Dictionary<string, short>> LocalizationsHandle;
        public Handle<Dictionary<string, string>> UniqueLocalizations;

        public Localizations()
        {
            LocalizationsHandle = Handle<Dictionary<string, short>>.NULL;
            UniqueLocalizations = Handle<Dictionary<string, string>>.NULL;
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.PutDic(LocalizationsHandle.Get());
            writer.PutDic(UniqueLocalizations.IsNull() ? Object<Dictionary<string,string>>.Instance :  UniqueLocalizations.Get());
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
        
        public UnsafeParallelHashMap<FixedString32Bytes, short> LocalizationsMap;
        public UnsafeParallelHashMap<FixedString32Bytes, FixedString32Bytes> UniqueLocalizations;

        public void Serialize(NetDataWriter writer) {
            NetworkSerializerExtensions.PutDic(writer, LocalizationsMap);
            NetworkSerializerExtensions.PutDic(writer, UniqueLocalizations);
        }

        public void Deserialize(NetDataReader reader) {
            NetworkSerializerExtensions.GetDic(reader, ref LocalizationsMap);
            NetworkSerializerExtensions.GetDic(reader, ref UniqueLocalizations);
        }
    }

#endif
}