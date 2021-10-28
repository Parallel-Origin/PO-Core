using System.Drawing;
using System.Numerics;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;
using ParallelOriginGameServer.Server.Utils;
using Vector2d = ParallelOriginGameServer.Server.Utils.Vector2d;

#if CLIENT
using Mapbox.Utils;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Quaternion = UnityEngine.Quaternion;
#elif SERVER
using System.Drawing;
using ParallelOriginGameServer.Server.Utils;
using QuadTrees.QTreePointF;
#endif

namespace ParallelOrigin.Core.ECS.Components.Transform {

#if SERVER
    
    
    /// <summary>
    ///  A component Class which stores informations about the Geo-Location of a entity
    /// </summary>
    public struct NetworkTransform : INetSerializable{
        
        public Vector2d pos;
        public Grid chunk;

        public void Serialize(NetDataWriter writer) {
            NetworkSerializerExtensions.SerializeVector2d(writer, pos);
        }

        public void Deserialize(NetDataReader reader) {
            pos = NetworkSerializerExtensions.DeserializeVector2d(reader);
        }
    }
    
    /// <summary>
    /// The network representation of the rotation of an entity
    /// Mainly used for interpolation
    /// </summary>
    public struct NetworkRotation : INetSerializable{
        
        public Quaternion value;

        public void Serialize(NetDataWriter writer) {
            writer.Put(value.X);
            writer.Put(value.Y);
            writer.Put(value.Z);
            writer.Put(value.W);
        }

        public void Deserialize(NetDataReader reader) {

            var x = reader.GetFloat();
            var y = reader.GetFloat();
            var z = reader.GetFloat();
            var w = reader.GetFloat();

            value = new Quaternion { X = x, Y = y, Z = z, W = w };
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
        
        public void Serialize(NetDataWriter writer) {
            NetworkSerializerExtensions.SerializeVector2d(writer, pos);
        }

        public void Deserialize(NetDataReader reader) {
            pos = NetworkSerializerExtensions.DeserializeVector2d(reader);
        }
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