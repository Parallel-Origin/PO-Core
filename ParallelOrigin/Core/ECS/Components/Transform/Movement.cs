
#if CLIENT
using LiteNetLib.Utils;
using ParallelOrigin.Core.Base.Classes;
using Unity.Burst;
using Unity.Entities;
#elif SERVER
using ParallelOrigin.Core.Base.Classes;
#endif
using LiteNetLib.Utils;

namespace ParallelOrigin.Core.ECS.Components.Transform {

#if SERVER
    
    /// <summary>
    ///  A component which selects a target position which gets processed by a system to move the <see cref="Location" /> to the <see cref="target" />
    /// </summary>
    public struct Movement : INetSerializable {

        public float speed;
        public Vector2d target;

        public void Serialize(NetDataWriter writer) { writer.Put(speed); }
        public void Deserialize(NetDataReader reader) { speed = reader.GetFloat(); }
    }
    
    /// <summary>
    /// Marks an entity as moving this frame. 
    /// </summary>
    public struct Moving{}
    
#elif CLIENT

    /// <summary>
    /// Marks an entity as moving around in the world.
    /// Normally requires a <see cref="Location"/> and a <see cref="Movement"/>
    /// </summary>
    [BurstCompile]
    public struct Movement : IComponentData, INetSerializable {
        
        public float speed;
        public Vector2d target;
        
        public void Serialize(NetDataWriter writer) { writer.Put(speed); }
        public void Deserialize(NetDataReader reader) { speed = reader.GetFloat(); }
    }
    
    [BurstCompile]
    public struct Moving : IComponentData{}

#endif
}