using LiteNetLib.Utils;

namespace ParallelOrigin.Core.Network {

    /// <summary>
    /// Possible entity op codes
    /// </summary>
    public static class EntityOpCode {

        public const byte CREATE = 0;
        public const byte ADD = 1;
        public const byte SET = 2;
        public const byte REMOVE = 3;
        public const byte DELETE = 4;
    }
    
    /// <summary>
    /// Represents an entity command which will be send to the client for either creating or deleting an entity. 
    /// </summary>
    public struct EntityCommand : INetSerializable {

        public ulong id;
        public string type;
        public byte opcode;

        public void Serialize(NetDataWriter writer) {
            writer.Put(id);
            writer.Put(type, type.Length);
            writer.Put(opcode);
        }

        public void Deserialize(NetDataReader reader) {
            id = reader.GetULong();
            type = reader.GetString(32);
            opcode = reader.GetByte();
        }
    }
    
    /**
     * An entity command with one of its direct components to save bandwith.
     */
    public struct EntityCommand<T1> : INetSerializable where T1 : struct, INetSerializable {

        public EntityCommand command;
        public T1 t1Component;
        
        public void Serialize(NetDataWriter writer) {
            writer.Put(command);
            writer.Put(t1Component);
        }

        public void Deserialize(NetDataReader reader) {
            command = reader.Get<EntityCommand>();
            t1Component = reader.Get<T1>();
        }
    }

    /**
     * An entity command with two of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2> : INetSerializable where T1 : struct, INetSerializable where T2 : struct, INetSerializable {

        public EntityCommand command;

        public T1 t1Component;
        public T2 t2Component;

        public void Serialize(NetDataWriter writer) {
            writer.Put(command);
            writer.Put(t1Component);
            writer.Put(t2Component);
        }

        public void Deserialize(NetDataReader reader) {
            command = reader.Get<EntityCommand>();
            t1Component = reader.Get<T1>();
            t2Component = reader.Get<T2>();
        }
    }
    
    /**
     * An entity command with three of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2, T3> : INetSerializable where T1 : struct, INetSerializable where T2 : struct, INetSerializable where T3 : struct, INetSerializable {

        public EntityCommand command;

        public T1 t1Component;
        public T2 t2Component;
        public T3 t3Component;

        public void Serialize(NetDataWriter writer) {
            writer.Put(command);
            writer.Put(t1Component);
            writer.Put(t2Component);
            writer.Put(t3Component);
        }

        public void Deserialize(NetDataReader reader) {
            command = reader.Get<EntityCommand>();
            t1Component = reader.Get<T1>();
            t2Component = reader.Get<T2>();
            t3Component = reader.Get<T3>();
        }
    }
    
    /**
     * An entity command with four of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2, T3, T4> : INetSerializable where T1 : struct, INetSerializable where T2 : struct, INetSerializable where T3 : struct, INetSerializable where T4 : struct, INetSerializable {

        public EntityCommand command;

        public T1 t1Component;
        public T2 t2Component;
        public T3 t3Component;
        public T4 t4Component;

        public void Serialize(NetDataWriter writer) {
            writer.Put(command);
            writer.Put(t1Component);
            writer.Put(t2Component);
            writer.Put(t3Component);
            writer.Put(t4Component);
        }

        public void Deserialize(NetDataReader reader) {
            command = reader.Get<EntityCommand>();
            t1Component = reader.Get<T1>();
            t2Component = reader.Get<T2>();
            t3Component = reader.Get<T3>();
            t4Component = reader.Get<T4>();
        }
    }
    
    /**
     * An entity command with four of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2, T3, T4, T5> : INetSerializable where T1 : struct, INetSerializable where T2 : struct, INetSerializable where T3 : struct, INetSerializable where T4 : struct, INetSerializable where T5 : struct, INetSerializable {

        public EntityCommand command;

        public T1 t1Component;
        public T2 t2Component;
        public T3 t3Component;
        public T4 t4Component;
        public T5 t5Component;

        public void Serialize(NetDataWriter writer) {
            writer.Put(command);
            writer.Put(t1Component);
            writer.Put(t2Component);
            writer.Put(t3Component);
            writer.Put(t4Component);
            writer.Put(t5Component);
        }

        public void Deserialize(NetDataReader reader) {
            command = reader.Get<EntityCommand>();
            t1Component = reader.Get<T1>();
            t2Component = reader.Get<T2>();
            t3Component = reader.Get<T3>();
            t4Component = reader.Get<T4>();
            t5Component = reader.Get<T5>();
        }
    }
    
    /**
     * An entity command with four of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2, T3, T4, T5, T6> : INetSerializable where T1 : struct, INetSerializable where T2 : struct, INetSerializable where T3 : struct, INetSerializable where T4 : struct, INetSerializable where T5 : struct, INetSerializable where T6 : struct, INetSerializable {

        public EntityCommand command;

        public T1 t1Component;
        public T2 t2Component;
        public T3 t3Component;
        public T4 t4Component;
        public T5 t5Component;
        public T6 t6Component;

        public void Serialize(NetDataWriter writer) {
            writer.Put(command);
            writer.Put(t1Component);
            writer.Put(t2Component);
            writer.Put(t3Component);
            writer.Put(t4Component);
            writer.Put(t5Component);
            writer.Put(t6Component);
        }

        public void Deserialize(NetDataReader reader) {
            command = reader.Get<EntityCommand>();
            t1Component = reader.Get<T1>();
            t2Component = reader.Get<T2>();
            t3Component = reader.Get<T3>();
            t4Component = reader.Get<T4>();
            t5Component = reader.Get<T5>();
            t6Component = reader.Get<T6>();
        }
    }

    /**
     * An entity command with four of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2, T3, T4, T5, T6, T7> : INetSerializable where T1 : struct, INetSerializable where T2 : struct, INetSerializable where T3 : struct, INetSerializable where T4 : struct, INetSerializable where T5 : struct, INetSerializable where T6 : struct, INetSerializable where T7 : struct, INetSerializable {

        public EntityCommand command;

        public T1 t1Component;
        public T2 t2Component;
        public T3 t3Component;
        public T4 t4Component;
        public T5 t5Component;
        public T6 t6Component;
        public T7 t7Component;

        public void Serialize(NetDataWriter writer) {
            writer.Put(command);
            writer.Put(t1Component);
            writer.Put(t2Component);
            writer.Put(t3Component);
            writer.Put(t4Component);
            writer.Put(t5Component);
            writer.Put(t6Component);
            writer.Put(t7Component);
        }

        public void Deserialize(NetDataReader reader) {
            command = reader.Get<EntityCommand>();
            t1Component = reader.Get<T1>();
            t2Component = reader.Get<T2>();
            t3Component = reader.Get<T3>();
            t4Component = reader.Get<T4>();
            t5Component = reader.Get<T5>();
            t6Component = reader.Get<T6>();
            t6Component = reader.Get<T7>();
        }
    }
    
    /// <summary>
    /// An component command which either adds, updates or removes an component <see cref="T"/> from an existing entity. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct ComponentCommand<T> : INetSerializable where T : struct, INetSerializable {

        public ulong id;
        public T component;
        public byte opcode;
        
        public void Serialize(NetDataWriter writer) {
            writer.Put(id);
            writer.Put(component);
            writer.Put(opcode);
        }

        public void Deserialize(NetDataReader reader) {
            id = reader.GetULong();
            component = reader.Get<T>();
            opcode = reader.GetByte();
        }
    }
}