using LiteNetLib.Utils;
using ParallelOrigin.Core.ECS;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.Network{

    /// <summary>
    /// Represents a bool params for animation controller
    /// </summary>
    public struct AnimationParamCommand : INetSerializable 
    {

        public EntityReference entityReference;
        public string boolName;
        public bool activated;

        public void Serialize(NetDataWriter writer) 
        {
            writer.Put(entityReference);
            writer.PutFixedString(boolName, (ushort)boolName.Length);
            writer.Put(activated);
        }

        public void Deserialize(NetDataReader reader) 
        {
            entityReference.Deserialize(reader);
            boolName = reader.GetFixedString();
            activated = reader.GetBool();
        }
    }

    /// <summary>
    /// Represents a animation trigger. 
    /// </summary>
    public struct AnimationTriggerCommand : INetSerializable 
    {

        public EntityReference entityReference;
        public string triggerName;

        public void Serialize(NetDataWriter writer) 
        {
            writer.Put(entityReference);
            writer.PutFixedString(triggerName, (ushort)triggerName.Length);
        }

        public void Deserialize(NetDataReader reader) 
        {
            entityReference.Deserialize(reader);
            triggerName = reader.GetFixedString();
        }
    }
}