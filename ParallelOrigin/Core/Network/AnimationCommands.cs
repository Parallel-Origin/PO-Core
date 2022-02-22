using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.Network {

    /// <summary>
    /// Represents a bool params for animation controller
    /// </summary>
    public struct BoolParams : INetSerializable{

        public string boolName;
        public bool activated;

        public void Serialize(NetDataWriter writer) {
            writer.PutFixedString(boolName, (ushort)boolName.Length);
            writer.Put(activated);
        }

        public void Deserialize(NetDataReader reader) {
            boolName = reader.GetFixedString();
            activated = reader.GetBool();
        }
    }
}