using LiteNetLib.Utils;

namespace ParallelOrigin.Core.Network {
    
    /// <summary>
    /// Represents an entity command which will be send to the client for either creating or deleting an entity. 
    /// </summary>
    public struct EntityCommand : INetSerializable{
        
        public long Id { get; set; }
        public string Type { get; set; }

        public byte OpCode { get; set; }

        public void Serialize(NetDataWriter writer) {
            writer.Put(Id);
            writer.Put(Type, Type.Length);
            writer.Put(OpCode);
        }

        public void Deserialize(NetDataReader reader) {
            Id = reader.GetLong();
            Type = reader.GetString(32);
            OpCode = reader.GetByte();
        }
    }
    
    /**
     * An entity command with one of its direct components to save bandwith.
     */
    public struct EntityCommand<T1> : INetSerializable where T1 : struct, INetSerializable {

        public EntityCommand Command { get; set; }
        public T1 T1Component { get; set; }
        
        public void Serialize(NetDataWriter writer) {
            writer.Put(Command);
            writer.Put(T1Component);
        }

        public void Deserialize(NetDataReader reader) {
            Command = reader.Get<EntityCommand>();
            T1Component = reader.Get<T1>();
        }
    }

    /**
     * An entity command with two of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2> : INetSerializable where T1 : struct, INetSerializable where T2 : struct, INetSerializable {

        public EntityCommand Command { get; set; }
        
        public T1 T1Component { get; set; }
        public T2 T2Component { get; set; }

        public void Serialize(NetDataWriter writer) {
            writer.Put(Command);
            writer.Put(T1Component);
            writer.Put(T2Component);
        }

        public void Deserialize(NetDataReader reader) {
            Command = reader.Get<EntityCommand>();
            T1Component = reader.Get<T1>();
            T2Component = reader.Get<T2>();
        }
    }
    
    /**
     * An entity command with three of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2, T3> : INetSerializable where T1 : struct, INetSerializable where T2 : struct, INetSerializable where T3 : struct, INetSerializable {

        public EntityCommand Command { get; set; }
        
        public T1 T1Component { get; set; }
        public T2 T2Component { get; set; }
        public T3 T3Component { get; set; }

        public void Serialize(NetDataWriter writer) {
            writer.Put(Command);
            writer.Put(T1Component);
            writer.Put(T2Component);
            writer.Put(T3Component);
        }

        public void Deserialize(NetDataReader reader) {
            Command = reader.Get<EntityCommand>();
            T1Component = reader.Get<T1>();
            T2Component = reader.Get<T2>();
            T3Component = reader.Get<T3>();
        }
    }
    
    /**
     * An entity command with four of its direct components to save bandwith.
     */
    public struct EntityCommand<T1, T2, T3, T4> : INetSerializable where T1 : struct, INetSerializable where T2 : struct, INetSerializable where T3 : struct, INetSerializable where T4 : struct, INetSerializable{

        public EntityCommand Command { get; set; }
        
        public T1 T1Component { get; set; }
        public T2 T2Component { get; set; }
        public T3 T3Component { get; set; }
        public T4 T4Component { get; set; }

        public void Serialize(NetDataWriter writer) {
            writer.Put(Command);
            writer.Put(T1Component);
            writer.Put(T2Component);
            writer.Put(T3Component);
            writer.Put(T4Component);
        }

        public void Deserialize(NetDataReader reader) {
            Command = reader.Get<EntityCommand>();
            T1Component = reader.Get<T1>();
            T2Component = reader.Get<T2>();
            T3Component = reader.Get<T3>();
            T4Component = reader.Get<T4>();
        }
    }

    /// <summary>
    /// An component command which either adds, updates or removes an component <see cref="T"/> from an existing entity. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct ComponentCommand<T> : INetSerializable where T : struct, INetSerializable {

        public long Id { get; set; }
        public T Component { get; set; }
        public byte OpCode { get; set; }

        public void Serialize(NetDataWriter writer) {
            writer.Put(Id);
            writer.Put(Component);
            writer.Put(OpCode);
        }

        public void Deserialize(NetDataReader reader) {
            Id = reader.GetLong();
            Component = reader.Get<T>();
            OpCode = reader.GetByte();
        }
    }
}