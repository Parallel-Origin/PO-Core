using LiteNetLib.Utils;
using ParallelOrigin.Core.Base.Classes;
using ParallelOrigin.Core.ECS;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.Network {
    /// <summary>
    ///     A packet which updates the clients map to show specific coordinates.
    /// </summary>
    public struct MapCommand : INetSerializable
    {
        public Vector2d Position;
        public byte OpCode;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(ref Position);
            writer.Put(OpCode);
        }

        public void Deserialize(NetDataReader reader)
        {
            Position = reader.GetVector2d();
            OpCode = reader.GetByte();
        }
    }

    /// <summary>
    ///     A command which teleports an entity to a certain position.
    /// </summary>
    public struct TeleportationCommand : INetSerializable
    {
        public EntityLink Entity;
        public Vector2d Position;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Entity);
            writer.Put(ref Position);
        }

        public void Deserialize(NetDataReader reader)
        {
            Entity.Deserialize(reader);
            Position = reader.GetVector2d();
        }
    }
}