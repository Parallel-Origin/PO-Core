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
            Username = reader.GetString(20);
            Password = reader.GetString(20);
        }
    }

    /// <summary>
    /// Represents an response which is being send to the client once the login was sucesfull. 
    /// </summary>
    public struct LoginResponse : INetSerializable {

        public Character Character { get; set; }

        public void Serialize(NetDataWriter writer) {
            writer.Put(Character);
        }

        public void Deserialize(NetDataReader reader) {
            Character = reader.Get<Character>();
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
}