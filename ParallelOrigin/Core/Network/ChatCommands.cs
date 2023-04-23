using System;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.Network {
    /// <summary>
    ///     A chat message being send from the server or the client.
    /// </summary>
    public struct ChatMessageCommand : INetSerializable
    {
        public long Sender;
        public string SenderUsername;

        public byte Channel;
        public string Message;
        public DateTime Date;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Sender);
            writer.PutFixedString(SenderUsername, (ushort)SenderUsername.Length);
            writer.Put(Channel);
            writer.PutFixedString(Message, (ushort)Message.Length);
            writer.Put(Date.Ticks);
        }

        public void Deserialize(NetDataReader reader)
        {
            Sender = reader.GetLong();
            SenderUsername = reader.GetFixedString();
            Channel = reader.GetByte();
            Message = reader.GetFixedString();
            Date = new DateTime(reader.GetLong());
        }
    }
}