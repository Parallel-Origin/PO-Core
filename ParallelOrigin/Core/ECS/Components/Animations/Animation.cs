
#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
#elif SERVER
using System.Collections.Generic;
#endif

namespace ParallelOrigin.Core.ECS.Components.Animations { 
    
#if SERVER

    /// <summary>
    ///  A component which represents the animation that is currently playing or should play for an specific gameobject entity.
    /// </summary>
    
    public struct Animation  {
        
        public byte controllerID;
        
        public Dictionary<string, byte> overridenAnimationClips;
        public Dictionary<string, bool> boolParams;
        public List<string> triggers;
    }    
    
    /// <summary>
    ///  A component that stores a map of all valid animations in a key value pair of string and id
    /// </summary>
    public struct AnimationController  {
        public Dictionary<string, short> animations;
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