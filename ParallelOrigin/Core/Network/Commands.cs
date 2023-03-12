using System;
using System.Collections;
using System.Collections.Generic;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Base.Classes;
using ParallelOrigin.Core.ECS;
using ParallelOrigin.Core.ECS.Components;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.Network {
    /// <summary>
    ///     Possible states... mostly used for collection stuff
    /// </summary>
    public enum State : byte {

        ADDED,
        UPDATED,
        REMOVED

    }

    /// <summary>
    /// A item with a certain state attached. Usefull for lists or other collections that probably need to synchronize.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Statefull<T> : INetSerializable where T : struct, INetSerializable {

        public State state;
        public T item;

        public Statefull(State state, T item) {
            this.state = state;
            this.item = item;
        }

        public void Serialize(NetDataWriter writer) {
            writer.Put((byte)state);
            writer.Put(item);
        }

        public void Deserialize(NetDataReader reader) {
            state = (State)reader.GetByte();
            item.Deserialize(reader);
        }

    }

    /// <summary>
    ///     An command which batches multiple packets into one huge packet for improving the performance of sending and receiving.
    ///     One huge packet > faster than small packets... however this class doesnt need to be used if the network lib includes automatic packet merging and sending upon our end of the server tick.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct BatchCommand<T> : INetSerializable, IEnumerable<T> where T : struct, INetSerializable {

        /// <summary>
        ///     The size of the <see cref="Data" />-Array. Basically the last index in the array we use.
        ///     Required for pooling purposes.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        ///     The data we wanna serialize. May be bigger than <see cref="Size" />... everything beyond <see cref="Size" /> gets ignored.
        /// </summary>
        public T[] Data { get; set; }

        public void Serialize(NetDataWriter writer) {
            writer.Put(Size);
            for (var index = 0; index < Size; index++)
                writer.Put(Data[index]);
        }

        public void Deserialize(NetDataReader reader) {
            Size = reader.GetInt();
            Data = new T[Size];

            for (var index = 0; index < Size; index++)
                Data[index].Deserialize(reader);
        }

        public ref T this[int index] => ref Data[index];

        public IEnumerator<T> GetEnumerator() {
            for (var index = 0; index < Data.Length; index++) {
                var item = Data[index];
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

    }

    /// <summary>
    /// A command which is used to add or remove items from a certain list. 
    /// </summary>
    /// <typeparam name="I"></typeparam>
    /// <typeparam name="T"></typeparam>
    public struct CollectionCommand<Id, T, I> : INetSerializable where Id : struct, INetSerializable where T : struct, INetSerializable where I : struct, INetSerializable {

        public Id identifier;
        public Statefull<I>[] items;

        public CollectionCommand(ref Id identifier, int capacity) : this() {
            this.identifier = identifier;
            items = new Statefull<I>[capacity];
        }

        public void Serialize(NetDataWriter writer) {
            writer.Put(identifier);
            writer.PutArray(items);
        }

        public void Deserialize(NetDataReader reader) {

            identifier.Deserialize(reader);
            reader.GetArray(ref items);
        }

        public ref Statefull<I> this[int index] => ref items[index];

        public I this[State state, int index] {
            set => items[index] = new Statefull<I>(state, value);
        }

    }

    /// <summary>
    ///     An enum of possible errors.
    /// </summary>
    public enum Error : byte {

        USERNAME_TAKEN,
        BAD_USERNAME,
        USERNAME_SHORT,
        BAD_PASSWORD,
        EMAIL_TAKEN,
        BAD_EMAIL

    }

    /// <summary>
    ///     Represents an error.
    /// </summary>
    public struct ErrorCommand : INetSerializable {

        public Error Error { get; set; }

        public void Serialize(NetDataWriter writer) { writer.Put((byte)Error); }

        public void Deserialize(NetDataReader reader) { Error = (Error)reader.GetSByte(); }

    }

    /// <summary>
    ///     A login command which is used to login an player
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
    ///     Represents an response which is being send to the client once the login was sucesfull.
    /// </summary>
    public struct LoginResponse : INetSerializable {

        public Character Character { get; set; }

        public void Serialize(NetDataWriter writer) { writer.Put(Character); }

        public void Deserialize(NetDataReader reader) { Character.Deserialize(reader); }

    }

    /// <summary>
    ///     A login command which is used to register an player.
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
            var position = Position;
            writer.Put(ref position);
        }

        public void Deserialize(NetDataReader reader) {
            Username = reader.GetString();
            Password = reader.GetString();
            Email = reader.GetString();
            Gender = (Gender)reader.GetSByte();
            Position = reader.GetVector2d();
        }

    }

    /// <summary>
    ///     Represents a click which was send from the client to the server to interact with entities.
    /// </summary>
    public struct ClickCommand : INetSerializable {

        public EntityLink clicker;
        public EntityLink clicked;

        public void Serialize(NetDataWriter writer) {
            writer.Put(clicker);
            writer.Put(clicked);
        }

        public void Deserialize(NetDataReader reader) {
            clicker.Deserialize(reader);
            clicked.Deserialize(reader);
        }

    }

    /// <summary>
    ///     Represents an double click which was send from the client to the server to move the avatar.
    /// </summary>
    public struct DoubleClickCommand : INetSerializable {

        public EntityLink clicker;
        public Vector2d position;

        public void Serialize(NetDataWriter writer) {
            writer.Put(clicker);
            writer.Put(ref position);
        }

        public void Deserialize(NetDataReader reader) {
            clicker.Deserialize(reader);
            position = reader.GetVector2d();
        }

    }
}