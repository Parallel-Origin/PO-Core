#if CLIENT
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;
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

namespace ParallelOrigin.Core.ECS.Components
{
#if SERVER

    /// <summary>
    ///     A component which represents the animation that is currently playing or should play for an specific gameobject entity.
    /// </summary>
    public struct Animation : INetSerializable
    {
        public byte controllerID;
        public IBehaviourTreeNode behaviourTree;
        
        public Dictionary<string, byte> overridenAnimationClips;
        public TrackedDictionary<string, bool> boolParams; // Because a marker component doesnt make sense here... we will never listen to started or ended animations
        public List<string> triggers;

        public Animation(byte controllerId, IBehaviourTreeNode behaviourTree) : this()
        {
            controllerID = controllerId;
            this.behaviourTree = behaviourTree;
            triggers = new List<string>(4);
            boolParams = new TrackedDictionary<string, bool>(4);
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(controllerID);
            writer.PutDic(overridenAnimationClips);
            writer.PutDic(boolParams);
            writer.PutList(triggers);
        }

        public void Deserialize(NetDataReader reader)
        {
            // Required because ref doesnt work otherwhise below 
            boolParams = new TrackedDictionary<string, bool>(4);
            var dic = boolParams.Dictionary;

            controllerID = reader.GetByte();
            reader.GetDic(ref overridenAnimationClips);
            reader.GetDic(ref dic);
            reader.GetList(ref triggers);
        }
    }
#elif CLIENT
    /// <summary>
    ///  A component which represents the animation that is currently playing or should play for an specific gameobject entity.
    /// </summary>
    [BurstCompile]
    public struct Animation : IComponentData, INetSerializable {
        
        public byte controllerID;
        
        public UnsafeParallelHashMap<FixedString32Bytes, byte> overridenAnimationClips;
        public UnsafeParallelHashMap<FixedString32Bytes, bool> boolParams;
        public UnsafeList<FixedString32Bytes> triggers;
        
        public void Serialize(NetDataWriter writer) { throw new System.NotImplementedException(); }

        public void Deserialize(NetDataReader reader) {
            controllerID = reader.GetByte();
            NetworkSerializerExtensions.GetDic(reader, ref overridenAnimationClips);
            NetworkSerializerExtensions.GetDic(reader, ref boolParams);
            NetworkSerializerExtensions.GetList(reader, ref triggers);
        }
    }
    
    /// <summary>
    ///     A component that stores a map of all valid animations in a key value pair of string and id
    /// </summary>
    [BurstCompile]
    public struct AnimationController : IComponentData {
        public UnsafeParallelHashMap<FixedString32Bytes, short> animations;
    }
#endif
}