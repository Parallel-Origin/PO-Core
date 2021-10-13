using LiteNetLib.Utils;
using ParallelOrigin.Core.ECS.Components;
using ParallelOrigin.Core.ECS.Components.Transform;

namespace ParallelOrigin.Core.Network {

    public struct IdentityCommand : INetSerializable {

        public ComponentCommand<Identity> command;

        public void Serialize(NetDataWriter writer) { writer.Put(command); }
        public void Deserialize(NetDataReader reader) { reader.Get<ComponentCommand<Identity>>(); }

    }
    
    public struct CharacterCommand : INetSerializable {

        public ComponentCommand<Character> command;

        public void Serialize(NetDataWriter writer) { writer.Put(command); }
        public void Deserialize(NetDataReader reader) { reader.Get<ComponentCommand<Character>>(); }

    }

    public struct NetworkTransformCommand : INetSerializable {

        public ComponentCommand<NetworkTransform> command;

        public void Serialize(NetDataWriter writer) { writer.Put(command); }
        public void Deserialize(NetDataReader reader) { reader.Get<ComponentCommand<NetworkTransform>>(); }

    }
}