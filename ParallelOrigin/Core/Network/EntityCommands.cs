using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.Network {
    // reader.Get<T> is bad since it allocates memory... use T.Deserialize() instead

    /// <summary>
    ///     Possible entity op codes
    /// </summary>
    public static class EntityOpCode
    {
        public const byte Create = 0;
        public const byte Add = 1;
        public const byte Set = 2;
        public const byte Remove = 3;
        public const byte Delete = 4;
    }

    /// <summary>
    ///     Represents an entity command which will be send to the client for either creating or deleting an entity.
    /// </summary>
    public struct EntityCommand : INetSerializable
    {
        public long Id;
        public string Type; // Only being used if the type does NOT exist on the client 
        public byte Opcode;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Id);
            writer.PutFixedString(Type, (ushort)Type.Length);
            writer.Put(Opcode);
        }

        public void Deserialize(NetDataReader reader)
        {
            Id = reader.GetLong();
            Type = reader.GetFixedString();
            Opcode = reader.GetByte();
        }
    }

    /**
     * An entity command with one of its direct components to save bandwith.
     */
    public struct EntityCommand<T1> : INetSerializable where T1 : struct, INetSerializable
    {
        public EntityCommand Command;
        public T1 T1Component;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Command);
            writer.Put(T1Component);
        }

        public void Deserialize(NetDataReader reader)
        {
            Command.Deserialize(reader);
            T1Component.Deserialize(reader);
        }
    }

    /**
     * An entity command with two of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2> : INetSerializable where T1 : struct, INetSerializable where T2 : struct, INetSerializable
    {
        public EntityCommand Command;

        public T1 T1Component;
        public T2 T2Component;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Command);
            writer.Put(T1Component);
            writer.Put(T2Component);
        }

        public void Deserialize(NetDataReader reader)
        {
            Command.Deserialize(reader);
            T1Component.Deserialize(reader);
            T2Component.Deserialize(reader);
        }
    }

    /**
     * An entity command with three of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2, T3> : INetSerializable where T1 : struct, INetSerializable where T2 : struct, INetSerializable where T3 : struct, INetSerializable
    {
        public EntityCommand Command;

        public T1 T1Component;
        public T2 T2Component;
        public T3 T3Component;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Command);
            writer.Put(T1Component);
            writer.Put(T2Component);
            writer.Put(T3Component);
        }

        public void Deserialize(NetDataReader reader)
        {
            Command.Deserialize(reader);
            T1Component.Deserialize(reader);
            T2Component.Deserialize(reader);
            T3Component.Deserialize(reader);
        }
    }

    /**
     * An entity command with four of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2, T3, T4> : INetSerializable where T1 : struct, INetSerializable
        where T2 : struct, INetSerializable
        where T3 : struct, INetSerializable
        where T4 : struct, INetSerializable
    {
        public EntityCommand Command;

        public T1 T1Component;
        public T2 T2Component;
        public T3 T3Component;
        public T4 T4Component;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Command);
            writer.Put(T1Component);
            writer.Put(T2Component);
            writer.Put(T3Component);
            writer.Put(T4Component);
        }

        public void Deserialize(NetDataReader reader)
        {
            Command.Deserialize(reader);
            T1Component.Deserialize(reader);
            T2Component.Deserialize(reader);
            T3Component.Deserialize(reader);
            T4Component.Deserialize(reader);
        }
    }

    /**
     * An entity command with four of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2, T3, T4, T5> : INetSerializable where T1 : struct, INetSerializable
        where T2 : struct, INetSerializable
        where T3 : struct, INetSerializable
        where T4 : struct, INetSerializable
        where T5 : struct, INetSerializable
    {
        public EntityCommand Command;

        public T1 T1Component;
        public T2 T2Component;
        public T3 T3Component;
        public T4 T4Component;
        public T5 T5Component;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Command);
            writer.Put(T1Component);
            writer.Put(T2Component);
            writer.Put(T3Component);
            writer.Put(T4Component);
            writer.Put(T5Component);
        }

        public void Deserialize(NetDataReader reader)
        {
            Command.Deserialize(reader);
            T1Component.Deserialize(reader);
            T2Component.Deserialize(reader);
            T3Component.Deserialize(reader);
            T4Component.Deserialize(reader);
            T5Component.Deserialize(reader);
        }
    }

    /**
     * An entity command with four of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2, T3, T4, T5, T6> : INetSerializable where T1 : struct, INetSerializable
        where T2 : struct, INetSerializable
        where T3 : struct, INetSerializable
        where T4 : struct, INetSerializable
        where T5 : struct, INetSerializable
        where T6 : struct, INetSerializable
    {
        public EntityCommand Command;

        public T1 T1Component;
        public T2 T2Component;
        public T3 T3Component;
        public T4 T4Component;
        public T5 T5Component;
        public T6 T6Component;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Command);
            writer.Put(T1Component);
            writer.Put(T2Component);
            writer.Put(T3Component);
            writer.Put(T4Component);
            writer.Put(T5Component);
            writer.Put(T6Component);
        }

        public void Deserialize(NetDataReader reader)
        {
            Command.Deserialize(reader);
            T1Component.Deserialize(reader);
            T2Component.Deserialize(reader);
            T3Component.Deserialize(reader);
            T4Component.Deserialize(reader);
            T5Component.Deserialize(reader);
            T6Component.Deserialize(reader);
        }
    }

    /**
     * An entity command with four of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2, T3, T4, T5, T6, T7> : INetSerializable where T1 : struct, INetSerializable
        where T2 : struct, INetSerializable
        where T3 : struct, INetSerializable
        where T4 : struct, INetSerializable
        where T5 : struct, INetSerializable
        where T6 : struct, INetSerializable
        where T7 : struct, INetSerializable
    {
        public EntityCommand Command;

        public T1 T1Component;
        public T2 T2Component;
        public T3 T3Component;
        public T4 T4Component;
        public T5 T5Component;
        public T6 T6Component;
        public T7 T7Component;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Command);
            writer.Put(T1Component);
            writer.Put(T2Component);
            writer.Put(T3Component);
            writer.Put(T4Component);
            writer.Put(T5Component);
            writer.Put(T6Component);
            writer.Put(T7Component);
        }

        public void Deserialize(NetDataReader reader)
        {
            Command.Deserialize(reader);
            T1Component.Deserialize(reader);
            T2Component.Deserialize(reader);
            T3Component.Deserialize(reader);
            T4Component.Deserialize(reader);
            T5Component.Deserialize(reader);
            T6Component.Deserialize(reader);
            T7Component.Deserialize(reader);
        }
    }

    /**
     * An entity command with four of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2, T3, T4, T5, T6, T7, T8> : INetSerializable where T1 : struct, INetSerializable
        where T2 : struct, INetSerializable
        where T3 : struct, INetSerializable
        where T4 : struct, INetSerializable
        where T5 : struct, INetSerializable
        where T6 : struct, INetSerializable
        where T7 : struct, INetSerializable
        where T8 : struct, INetSerializable
    {
        public EntityCommand Command;

        public T1 T1Component;
        public T2 T2Component;
        public T3 T3Component;
        public T4 T4Component;
        public T5 T5Component;
        public T6 T6Component;
        public T7 T7Component;
        public T8 T8Component;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Command);
            writer.Put(T1Component);
            writer.Put(T2Component);
            writer.Put(T3Component);
            writer.Put(T4Component);
            writer.Put(T5Component);
            writer.Put(T6Component);
            writer.Put(T7Component);
            writer.Put(T8Component);
        }

        public void Deserialize(NetDataReader reader)
        {
            Command.Deserialize(reader);
            T1Component.Deserialize(reader);
            T2Component.Deserialize(reader);
            T3Component.Deserialize(reader);
            T4Component.Deserialize(reader);
            T5Component.Deserialize(reader);
            T6Component.Deserialize(reader);
            T7Component.Deserialize(reader);
            T8Component.Deserialize(reader);
        }
    }

    /// <summary>
    ///     An component command which either adds, updates or removes an component <see cref="T" /> from an existing entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct ComponentCommand<T> : INetSerializable where T : struct, INetSerializable
    {
        public long Id;
        public T Component;
        public byte Opcode;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Id);
            writer.Put(Component);
            writer.Put(Opcode);
        }

        public void Deserialize(NetDataReader reader)
        {
            Id = reader.GetLong();
            Component.Deserialize(reader);
            Opcode = reader.GetByte();
        }
    }
}