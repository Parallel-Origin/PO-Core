using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;
using ParallelOrigin.Core.Base.Classes;
using ParallelOrigin.Core.ECS;

namespace ParallelOrigin.Core.Network {
    
    /// <summary>
    /// A packet which updates the clients map to show specific coordinates. 
    /// </summary>
    public struct MapCommand : INetSerializable {

        public Vector2d position;
        public byte opCode;

        public void Serialize(NetDataWriter writer) {
            writer.Put(ref position);
            writer.Put(opCode);
        }

        public void Deserialize(NetDataReader reader) {
            position = reader.GetVector2d();
            opCode = reader.GetByte();
        }
    }

    /// <summary>
    /// A command which teleports an entity to a certain position.
    /// </summary>
    public struct TeleportationCommand : INetSerializable {

        public EntityReference entity;
        public Vector2d position;

        public void Serialize(NetDataWriter writer) {
            writer.Put(entity);
            writer.Put(ref position);
        }

        public void Deserialize(NetDataReader reader) {
            entity = reader.Get<EntityReference>();
            position = reader.GetVector2d();
        }
    }
}