using System.Globalization;
using LiteNetLib.Utils;
using ParallelOrigin.Core.ECS;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.Network {

    /// <summary>
    /// A command used to signalise the server to build a certain recipe...
    /// </summary>
    public struct BuildCommand : INetSerializable {

        public EntityReference builder;
        public string recipe;

        public void Serialize(NetDataWriter writer) {
            writer.Put(builder);
            writer.PutFixedString(recipe, (ushort)recipe.Length);
        }

        public void Deserialize(NetDataReader reader) {
            builder = reader.Get<EntityReference>();
            recipe = reader.GetFixedString();
        }
    }
}