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
using Arch.LowLevel;
using ParallelOriginGameServer.Server.ThirdParty;
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
        
        public Handle<Dictionary<string, byte>> overridenAnimationClips;
        public Handle<TrackedDictionary<string, bool>> boolParams; // Because a marker component doesnt make sense here... we will never listen to started or ended animations
        public Handle<List<string>> triggers;

        public Animation(byte controllerId, IBehaviourTreeNode behaviourTree) : this()
        {
            controllerID = controllerId;
            this.behaviourTree = behaviourTree;
            triggers = new List<string>(4).ToHandle();
            boolParams = new TrackedDictionary<string, bool>(4).ToHandle();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(controllerID);
            writer.PutDic(overridenAnimationClips.Get());
            writer.PutDic(boolParams.Get());
            writer.PutList(triggers.Get());
        }

        public void Deserialize(NetDataReader reader)
        {
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