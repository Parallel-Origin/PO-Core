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
#elif CLIENT
using System.Drawing;
using ParallelOriginGameServer.Server.Utils;
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
            NetworkSerializerExtensions.SerializeGrid(writer, chunk);
        }

        public void Deserialize(NetDataReader reader) {
            pos = NetworkSerializerExtensions.DeserializeVector2d(reader);
            chunk = NetworkSerializerExtensions.DeserializeGrid(reader);
        }
    }
    
    /// <summary>
    /// The network representation of the rotation of an entity
    /// Mainly used for interpolation
    /// </summary>
    public struct NetworkRotation  {
        public Quaternion value;
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
            NetworkSerializerExtensions.SerializeGrid(writer, chunk);
        }

        public void Deserialize(NetDataReader reader) {
            pos = NetworkSerializerExtensions.DeserializeVector2d(reader);
            chunk = NetworkSerializerExtensions.DeserializeGrid(reader);
        }
    }
    
    /// <summary>
    ///  A component Class which stores informations about the Geo-Location of a entity
    /// </summary>
    [BurstCompile]
    public struct Transform : IComponentData {
        
        public Vector2d pos;
        public Vector2d chunk;
    }
    
        /// <summary>
    /// The network representation of the rotation of an entity
    /// Mainly used for interpolation
    /// </summary>
    [BurstCompile]
    public struct NetworkRotation : IComponentData {
        public quaternion value;
    }
    
#endif
}