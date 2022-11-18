using System;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.Network {
    /// <summary>
    ///     A chat message being send from the server or the client.
    /// </summary>
    public struct ChatMessageCommand : INetSerializable
    {
        public long sender;
        public string senderUsername;

        public byte channel;
        public string message;
        public DateTime date;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(sender);
            writer.PutFixedString(senderUsername, (ushort)senderUsername.Length);
            writer.Put(channel);
            writer.PutFixedString(message, (ushort)message.Length);
            writer.Put(date.Ticks);
        }

        public void Deserialize(NetDataReader reader)
        {
            sender = reader.GetLong();
            senderUsername = reader.GetFixedString();
            channel = reader.GetByte();
            message = reader.GetFixedString();
            date = new DateTime(reader.GetLong());
        }
    }
}