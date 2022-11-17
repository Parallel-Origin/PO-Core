using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.Network;

/// <summary>
///     Represents a bool params for animation controller
/// </summary>
public struct BoolParams : INetSerializable
{
    public string boolName;
    public bool activated;

    public void Serialize(NetDataWriter writer)
    {
        writer.PutFixedString(boolName, (ushort)boolName.Length);
        writer.Put(activated);
    }

    public void Deserialize(NetDataReader reader)
    {
        boolName = reader.GetFixedString();
        activated = reader.GetBool();
    }
}

/// <summary>
///     Represents a animation trigger.
/// </summary>
public struct Trigger : INetSerializable
{
    public string triggerName;

    public void Serialize(NetDataWriter writer)
    {
        writer.PutFixedString(triggerName, (ushort)triggerName.Length);
    }

    public void Deserialize(NetDataReader reader)
    {
        triggerName = reader.GetFixedString();
    }
}

/// <summary>
///     A simple struct representing a animation command representing a list of changed animation states.
///     Just because we cant use a alias to hide this generic shit.
/// </summary>
public struct AnimationCommand : INetSerializable
{
    public BatchCommand<CollectionItem<BoolParams>> changedBoolParams;
    public BatchCommand<CollectionItem<Trigger>> triggers;

    public void Serialize(NetDataWriter writer)
    {
        writer.Put(changedBoolParams);
        writer.Put(triggers);
    }

    public void Deserialize(NetDataReader reader)
    {
        changedBoolParams.Deserialize(reader);
        triggers.Deserialize(reader);
    }
}