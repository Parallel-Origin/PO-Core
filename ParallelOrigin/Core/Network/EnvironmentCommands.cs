using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;
using ParallelOriginGameServer.Server.Utils;

namespace ParallelOrigin.Core.Network {
    
    /// <summary>
    /// A packet which updates the clients map to show specific coordinates. 
    /// </summary>
    public struct MapCommand : INetSerializable{
        
        public Vector2d Position { get; set; }
        public byte OpCode { get; set; }

        public void Serialize(NetDataWriter writer) {
            NetworkSerializerExtensions.SerializeVector2d(writer, Position);
            writer.Put(OpCode);
        }

        public void Deserialize(NetDataReader reader) {
            Position = NetworkSerializerExtensions.DeserializeVector2d(reader);
            OpCode = reader.GetByte();
        }
    }
}