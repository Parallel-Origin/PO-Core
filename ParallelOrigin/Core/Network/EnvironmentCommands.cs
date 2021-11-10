using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;
using ParallelOrigin.Core.Base.Classes;

namespace ParallelOrigin.Core.Network {
    
    /// <summary>
    /// A packet which updates the clients map to show specific coordinates. 
    /// </summary>
    public struct MapCommand : INetSerializable {

        public Vector2d position;
        public byte opCode;

        public void Serialize(NetDataWriter writer) {
            NetworkSerializerExtensions.SerializeVector2d(writer, position);
            writer.Put(opCode);
        }

        public void Deserialize(NetDataReader reader) {
            position = NetworkSerializerExtensions.DeserializeVector2d(reader);
            opCode = reader.GetByte();
        }
    }
}