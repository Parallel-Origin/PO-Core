using System.Numerics;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Base.Classes;
using ParallelOrigin.Core.Extensions;
#if CLIENT
using ParallelOrigin.Core.Base.Classes;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Quaternion = UnityEngine.Quaternion;
#elif SERVER
#endif

namespace ParallelOrigin.Core.ECS.Components.Transform
{
#if SERVER


    /// <summary>
    ///     A component Class which stores informations about the Geo-Location of a entity
    /// </summary>
    public struct NetworkTransform : INetSerializable
    {
        public Vector2d Pos;
        public Grid Chunk;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(ref Pos);
        }

        public void Deserialize(NetDataReader reader)
        {
            Pos = reader.GetVector2d();
        }
    }

    /// <summary>
    ///     The network representation of the rotation of an entity
    ///     Mainly used for interpolation
    /// </summary>
    public struct NetworkRotation : INetSerializable
    {
        public Quaternion Value;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Value.X);
            writer.Put(Value.Y);
            writer.Put(Value.Z);
            writer.Put(Value.W);
        }

        public void Deserialize(NetDataReader reader)
        {
            var x = reader.GetFloat();
            var y = reader.GetFloat();
            var z = reader.GetFloat();
            var w = reader.GetFloat();

            Value = new Quaternion { X = x, Y = y, Z = z, W = w };
        }
    }

#elif CLIENT
    /// <summary>
    ///  A component Class which stores informations about the Geo-Location of a entity
    /// </summary>
    [BurstCompile]
    public struct NetworkTransform : IComponentData, INetSerializable {
        
        public Vector2d pos;
        public Grid chunk;
        
        public void Serialize(NetDataWriter writer) { writer.Put(ref pos); }

        public void Deserialize(NetDataReader reader) { pos = reader.GetVector2d(); }
    }
    
    /// <summary>
    ///  A component Class which stores informations about the Geo-Location of a entity
    /// </summary>
    [BurstCompile]
    public struct LocalTransform : IComponentData {
        
        public Vector2d pos;
        public Grid chunk;
    }
    

    /// <summary>
    /// The network representation of the rotation of an entity
    /// Mainly used for interpolation
    /// </summary>
    [BurstCompile]
    public struct NetworkRotation : IComponentData, INetSerializable {
        
        public quaternion value;
        
        public void Serialize(NetDataWriter writer) {
            writer.Put(value.value.x);
            writer.Put(value.value.y);
            writer.Put(value.value.z);
            writer.Put(value.value.w);
        }

        public void Deserialize(NetDataReader reader) {

            var x = reader.GetFloat();
            var y = reader.GetFloat();
            var z = reader.GetFloat();
            var w = reader.GetFloat();

            value = new quaternion(x, y, z, w);
        }
    }

#endif
}