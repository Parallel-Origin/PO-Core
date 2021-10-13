using System.Drawing;
using LiteNetLib.Utils;

#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Script.Extensions;
#elif SERVER 
using DefaultEcs;
using DefaultEcs;
using LiteNetLib;
#endif

namespace ParallelOrigin.Core.ECS.Components {

    /// <summary>
    /// Possible <see cref="Account"/> and <see cref="Character"/> genders
    /// </summary>
    public enum Gender : sbyte {
        MALE,FEMALE,DIVERS
    }

    
#if SERVER
    
    /// <summary>
    ///  A component for a <see cref="Entity" /> which acts as a player.
    /// </summary>
    public struct Character : INetSerializable {

        public NetPeer peer;
        
        public string name;
        public string email;
        public string password;

        public Gender gender;
        public Color color;

        public void Serialize(NetDataWriter writer) {
            writer.Put(name);
            writer.Put(password);
            writer.Put(email);
        }

        public void Deserialize(NetDataReader reader) {
            name = reader.GetString(32);
            email = reader.GetString(32);
            password = reader.GetString(32);
        }
    }    
    
#elif CLIENT 
    
    /// <summary>
    ///  A component for a <see cref="Entity" /> which acts as a player.
    /// </summary>
    [BurstCompile]
    public struct Character : IComponentData, INetSerializable {

        public Entity entity;

        public FixedString32 name;
        public FixedString32 password;
        public FixedString32 email;
        
        public void Serialize(NetDataWriter writer) {
            writer.Put(name.ToStringCached());
       writer.Put(password.ToStringCached());
            writer.Put(email.ToStringCached());
        }

        public void Deserialize(NetDataReader reader) {
            name = reader.GetString(32);
            password = reader.GetString(32);
            email = reader.GetString(32);
        }
    }

#endif
}