using LiteNetLib.Utils;
using ParallelOrigin.Core.ECS;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.Network {
    /// <summary>
    /// Represents a bool params for animation controller
    /// </summary>
    public struct AnimationParamCommand : INetSerializable 
    {

        public EntityLink EntityLink;
        public string BoolName;
        public bool Activated;

        public void Serialize(NetDataWriter writer) 
        {
            writer.Put(EntityLink);
            writer.PutFixedString(BoolName, (ushort)BoolName.Length);
            writer.Put(Activated);
        }

        public void Deserialize(NetDataReader reader) 
        {
            EntityLink.Deserialize(reader);
            BoolName = reader.GetFixedString();
            Activated = reader.GetBool();
        }
    }

    /// <summary>
    /// Represents a animation trigger. 
    /// </summary>
    public struct AnimationTriggerCommand : INetSerializable 
    {

        public EntityLink EntityLink;
        public string TriggerName;

        public void Serialize(NetDataWriter writer) 
        {
            writer.Put(EntityLink);
            writer.PutFixedString(TriggerName, (ushort)TriggerName.Length);
        }

        public void Deserialize(NetDataReader reader) 
        {
            EntityLink.Deserialize(reader);
            TriggerName = reader.GetFixedString();
        }
    }
}