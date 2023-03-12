using LiteNetLib.Utils;
using ParallelOrigin.Core.ECS;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.Network {
    /// <summary>
    /// Represents a bool params for animation controller
    /// </summary>
    public struct AnimationParamCommand : INetSerializable 
    {

        public EntityLink entityLink;
        public string boolName;
        public bool activated;

        public void Serialize(NetDataWriter writer) 
        {
            writer.Put(entityLink);
            writer.PutFixedString(boolName, (ushort)boolName.Length);
            writer.Put(activated);
        }

        public void Deserialize(NetDataReader reader) 
        {
            entityLink.Deserialize(reader);
            boolName = reader.GetFixedString();
            activated = reader.GetBool();
        }
    }

    /// <summary>
    /// Represents a animation trigger. 
    /// </summary>
    public struct AnimationTriggerCommand : INetSerializable 
    {

        public EntityLink entityLink;
        public string triggerName;

        public void Serialize(NetDataWriter writer) 
        {
            writer.Put(entityLink);
            writer.PutFixedString(triggerName, (ushort)triggerName.Length);
        }

        public void Deserialize(NetDataReader reader) 
        {
            entityLink.Deserialize(reader);
            triggerName = reader.GetFixedString();
        }
    }
}