using LiteNetLib.Utils;
using ParallelOrigin.Core.ECS;
using ParallelOrigin.Core.ECS.Components;
using ParallelOrigin.Core.Extensions;
using ParallelOrigin.Core.Base.Classes;

namespace ParallelOrigin.Core.Network {

    /// <summary>
    /// An command which batches multiple packets into one huge packet for improving the performance of sending and receiving.
    /// One huge packet > faster than small packets. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct BatchCommand<T> : INetSerializable where T : struct, INetSerializable {

        public T[] Data { get; set; }

        public void Serialize(NetDataWriter writer) {
            
            writer.Put(Data.Length);
            for(var index = 0; index < Data.Length; index++)
                writer.Put(Data[index]);
        }

        public void Deserialize(NetDataReader reader) {

            var length = reader.GetInt();
            Data = new T[length];

            for (var index = 0; index < Data.Length; index++)
                Data[index] = reader.Get<T>();
        }
    }

    /// <summary>
    /// A command which is used to add or remove items from a certain list. 
    /// </summary>
    /// <typeparam name="I"></typeparam>
    /// <typeparam name="T"></typeparam>
    public struct CollectiontCommand<Id,T,I> : INetSerializable where Id : struct,INetSerializable where T : struct,INetSerializable where I : struct,INetSerializable{

        public Id identifier;
        public I[] added;
        public I[] removed;

        public void Serialize(NetDataWriter writer) {
            
            writer.Put(identifier);

            NetworkSerializerExtensions.SerializeArray(writer, added);
            NetworkSerializerExtensions.SerializeArray(writer, removed);
        }

        public void Deserialize(NetDataReader reader) {
            
            identifier = reader.Get<Id>();

            NetworkSerializerExtensions.DeserializeArray(reader, ref added);
            NetworkSerializerExtensions.DeserializeArray(reader, ref removed);
        }
    }
    
    
    /// <summary>
    /// An enum of possible errors. 
    /// </summary>
    public enum Error : byte{
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
    /// Represents a click which was send from the client to the server to interact with entities. 
    /// </summary>
    public struct ClickCommand : INetSerializable {

        public EntityReference clicker;
        public EntityReference clicked;
        
        public void Serialize(NetDataWriter writer) {
            writer.Put(clicker);
            writer.Put(clicked);
        }

        public void Deserialize(NetDataReader reader) {
            clicker = reader.Get<EntityReference>();
            clicked = reader.Get<EntityReference>();
        }
    }

    /// <summary>
    /// Represents an double click which was send from the client to the server to move the avatar. 
    /// </summary>
    public struct DoubleClickCommand : INetSerializable{

        public EntityReference clicker;
        public Vector2d position;

        public void Serialize(NetDataWriter writer) {
            writer.Put(clicker);
            NetworkSerializerExtensions.SerializeVector2d(writer, position);
        }

        public void Deserialize(NetDataReader reader) {

            clicker = reader.Get<EntityReference>();
            position = NetworkSerializerExtensions.DeserializeVector2d(reader);
        }
    }
}