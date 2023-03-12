using LiteNetLib.Utils;
using ParallelOrigin.Core.ECS;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.Network {
    
    /// <summary>
    ///     A command used to signalise the server to build a certain recipe...
    /// </summary>
    public struct BuildCommand : INetSerializable
    {
        public EntityLink builder;
        public string recipe;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(builder);
            writer.PutFixedString(recipe, (ushort)recipe.Length);
        }

        public void Deserialize(NetDataReader reader)
        {
            builder.Deserialize(reader);
            recipe = reader.GetFixedString();
        }
    }
    
    /// <summary>
    ///     A command used to send a pickup request for an item with an amount to the server.
    ///     References the popup from which this command resulted. 
    /// </summary>
    public struct PickupCommand : INetSerializable
    {
        public EntityLink popup;
        public uint amount;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(popup);
            writer.Put(amount);
        }

        public void Deserialize(NetDataReader reader)
        {
            popup.Deserialize(reader);
            amount = reader.GetUInt();
        }
    }
}