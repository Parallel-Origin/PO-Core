using System;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.Network {

    /// <summary>
    /// A chat message being send from the server or the client. 
    /// </summary>
    public struct ChatMessageCommand : INetSerializable{
        
        public ulong sender;
        public string senderUsername;
            
        public byte channel;
        public string message;
        public DateTime date;

        public void Serialize(NetDataWriter writer) {
            
            writer.Put(sender);
            writer.PutFixedString(senderUsername, (ushort)senderUsername.Length);
            writer.Put(channel);
            writer.PutFixedString(message, (ushort)message.Length);
            writer.Put(date.ToBinary());
        }

        public void Deserialize(NetDataReader reader) {
            
            sender = reader.GetULong();
            senderUsername = reader.GetFixedString();
            channel = reader.GetByte();
            message = reader.GetFixedString();
            date = DateTime.FromBinary(reader.GetLong());
        }
    }
}