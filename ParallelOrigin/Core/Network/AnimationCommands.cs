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

    /// <summary>
    /// A simple struct representing a animation command representing a list of changed animation states.
    /// Just because we cant use a alias to hide this generic shit. 
    /// </summary>
    public struct AnimationCommand : INetSerializable{
        
        public BatchCommand<CollectionItem<BoolParams>> animationChanges;
        
        public void Serialize(NetDataWriter writer) { writer.Put(animationChanges); }
        public void Deserialize(NetDataReader reader) { animationChanges = reader.Get<BatchCommand<CollectionItem<BoolParams>>>(); }

    }
}