using LiteNetLib.Utils;
using ParallelOrigin.Core.ECS;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.Network {
    
    /// <summary>
    ///     A command used to signalise the server to build a certain recipe...
    /// </summary>
    public struct BuildCommand : INetSerializable
    {
        public EntityLink Builder;
        public string Recipe;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Builder);
            writer.PutFixedString(Recipe, (ushort)Recipe.Length);
        }

        public void Deserialize(NetDataReader reader)
        {
            Builder.Deserialize(reader);
            Recipe = reader.GetFixedString();
        }
    }
    
    /// <summary>
    ///     A command used to send a pickup request for an item with an amount to the server.
    ///     References the popup from which this command resulted. 
    /// </summary>
    public struct PickupCommand : INetSerializable
    {
        public EntityLink Popup;
        public uint Amount;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Popup);
            writer.Put(Amount);
        }

        public void Deserialize(NetDataReader reader)
        {
            Popup.Deserialize(reader);
            Amount = reader.GetUInt();
        }
    }
}