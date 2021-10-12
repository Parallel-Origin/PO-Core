using LiteNetLib.Utils;
using ParallelOrigin.Core.ECS.Components;
using ParallelOrigin.Core.Extensions;
using ParallelOriginGameServer.Server.Utils;

namespace ParallelOrigin.Core.Network {

    /// <summary>
    /// A login command which is used to login an player
    /// </summary>
    public struct LoginCommand : INetSerializable {

        public string Username { get; set; }
        public string Password { get; set; }

        public void Serialize(NetDataWriter writer) {
            writer.Put(Username);
            writer.Put(Password);
        }

        public void Deserialize(NetDataReader reader) {
            Username = reader.GetString();
            Password = reader.GetString();
        }
    }
    
    /// <summary>
    /// A login command which is used to register an player. 
    /// </summary>
    public struct RegisterCommand : INetSerializable {

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        
        public Gender Gender { get; set; }
        public Vector2d Position { get; set; }

        public void Serialize(NetDataWriter writer) {
            writer.Put(Username);
            writer.Put(Password);
            writer.Put(Email);
            writer.Put((sbyte)Gender);
            NetworkSerializerExtensions.SerializeVector2d(writer, Position);
        }

        public void Deserialize(NetDataReader reader) {
            Username = reader.GetString();
            Password = reader.GetString();
            Email = reader.GetString();
            Gender = (Gender)reader.GetSByte();
            Position = NetworkSerializerExtensions.DeserializeVector2d(reader);
        }
    }

    /// <summary>
    /// An enum of possible errors. 
    /// </summary>
    public enum Error : sbyte{
        USERNAME_TAKEN, BAD_USERNAME, USERNAME_SHORT, 
        BAD_PASSWORD,
        EMAIL_TAKEN, BAD_EMAIL, 
    }
    
    /// <summary>
    /// Represents an error. 
    /// </summary>
    public struct ErrorCommand : INetSerializable{

        public Error Error { get; set; }

        public void Serialize(NetDataWriter writer) {
            writer.Put((byte)Error);
        }
        public void Deserialize(NetDataReader reader) {
            Error = (Error)reader.GetSByte();
        }
    }
    
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