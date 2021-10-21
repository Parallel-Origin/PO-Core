
#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
#elif SERVER
using System.Collections.Generic;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;
#endif

namespace ParallelOrigin.Core.ECS.Components.Animations { 
    
#if SERVER

    /// <summary>
    ///  A component which represents the animation that is currently playing or should play for an specific gameobject entity.
    /// </summary>
    public struct Animation : INetSerializable{
        
        public byte controllerID;
        
        public Dictionary<string, byte> overridenAnimationClips;
        public Dictionary<string, bool> boolParams;
        public List<string> triggers;

        public void Serialize(NetDataWriter writer) {
            
            writer.Put(controllerID);
            NetworkSerializerExtensions.SerializeDic(writer, overridenAnimationClips);
            NetworkSerializerExtensions.SerializeDic(writer, boolParams);
            NetworkSerializerExtensions.SerializeList(writer, triggers);
        }

        public void Deserialize(NetDataReader reader) {

            controllerID = reader.GetByte();
            NetworkSerializerExtensions.DeserializeDic(reader, ref overridenAnimationClips);
            NetworkSerializerExtensions.DeserializeDic(reader, ref boolParams);
            NetworkSerializerExtensions.DeserializeList(reader, ref triggers);
        }
    }    
    
    /// <summary>
    ///  A component that stores a map of all valid animations in a key value pair of string and id
    /// </summary>
    public struct AnimationController : INetSerializable {
        
        public Dictionary<string, short> animations;
        
        public void Serialize(NetDataWriter writer) { NetworkSerializerExtensions.SerializeDic(writer, animations); }
        public void Deserialize(NetDataReader reader) { NetworkSerializerExtensions.DeserializeDic(reader, ref animations); }
    }
#elif CLIENT 
    
    /// <summary>
    ///  A component which represents the animation that is currently playing or should play for an specific gameobject entity.
    /// </summary>
    [BurstCompile]
    public struct Animation : IComponentData {
        
        public byte controllerID;
        
        public UnsafeHashMap<FixedString32, byte> overridenAnimationClips;
        public UnsafeHashMap<FixedString32, bool> boolParams;
        public UnsafeList<FixedString32> triggers;
    }
    
    /// <summary>
    ///     A component that stores a map of all valid animations in a key value pair of string and id
    /// </summary>
    [BurstCompile]
    public struct AnimationController : IComponentData {
        public UnsafeHashMap<FixedString32, short> animations;
    }
#endif
    
}