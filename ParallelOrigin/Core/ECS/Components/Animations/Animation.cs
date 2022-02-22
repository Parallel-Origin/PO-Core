
#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
#elif SERVER
using System.Collections.Generic;
using FluentBehaviourTree;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;
using ParallelOriginGameServer.Server.Network;
#endif

namespace ParallelOrigin.Core.ECS.Components.Animations { 
    
#if SERVER

    /// <summary>
    ///  A component which represents the animation that is currently playing or should play for an specific gameobject entity.
    /// </summary>
    public struct Animation : INetSerializable{
        
        public byte controllerID;
        
        public Dictionary<string, byte> overridenAnimationClips;
        public TrackedDictionary<string, bool> boolParams;         // Because a marker component doesnt make sense here... we will never listen to started or ended animations
        public List<string> triggers;

        public void Serialize(NetDataWriter writer) {
            
            writer.Put(controllerID);
            NetworkSerializerExtensions.SerializeDic(writer, overridenAnimationClips);
            NetworkSerializerExtensions.SerializeDic(writer, boolParams);
            NetworkSerializerExtensions.SerializeList(writer, triggers);
        }

        public void Deserialize(NetDataReader reader) {

            // Required because ref doesnt work otherwhise below 
            boolParams = new TrackedDictionary<string, bool>(4);
            var dic = boolParams.Dictionary;
            
            controllerID = reader.GetByte();
            NetworkSerializerExtensions.DeserializeDic(reader, ref overridenAnimationClips);
            NetworkSerializerExtensions.DeserializeDic(reader, ref dic);
            NetworkSerializerExtensions.DeserializeList(reader, ref triggers);
        }
    }

    /// <summary>
    ///  A component that stores a map of all valid animations in a key value pair of string and id
    /// </summary>
    public struct AnimationController  {

        public IBehaviourTreeNode behaviourTree;
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