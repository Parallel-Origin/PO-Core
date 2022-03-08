using LiteNetLib.Utils;
using ParallelOrigin.Core.ECS;

namespace ParallelOrigin.Core.Network {

    /// <summary>
    /// A command used to signalise the server to build a certain recipe...
    /// </summary>
    public struct BuildCommand : INetSerializable {

        public EntityReference builder;
        public byte buildRecipeIndex;

        public void Serialize(NetDataWriter writer) {
            writer.Put(builder);
            writer.Put(buildRecipeIndex);
        }

        public void Deserialize(NetDataReader reader) {
            builder = reader.Get<EntityReference>();
            buildRecipeIndex = reader.GetByte();
        }
    }
}