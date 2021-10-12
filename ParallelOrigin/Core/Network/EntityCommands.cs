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
            writer.Put(Type);
            writer.Put(OpCode);
        }

        public void Deserialize(NetDataReader reader) {
            Id = reader.GetLong();
            Type = reader.GetString();
            OpCode = reader.GetByte();
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