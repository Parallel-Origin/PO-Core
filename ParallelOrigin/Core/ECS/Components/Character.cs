using System.Drawing;
using LiteNetLib.Utils;
using Script.Extensions;

#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
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
    public struct Character {

        public NetPeer peer;
        
        public string name;
        public string email;
        public string password;

        public Gender gender;
        public Color color;
    }    
    
#elif CLIENT 
    
    /// <summary>
    ///  A component for a <see cref="Entity" /> which acts as a player.
    /// </summary>
    [BurstCompile]
    public struct Character : IComponentData, INetSerializable {

        public Entity entity;

        public FixedString32 name;
        public FixedString32 email;
        public FixedString32 password;
        
        public void Serialize(NetDataWriter writer) {
            writer.Put(name.ToStringCached());
            writer.Put(email.ToStringCached());
            writer.Put(password.ToStringCached());
        }

        public void Deserialize(NetDataReader reader) {
            name = reader.GetString(32);
            email = reader.GetString(32);
            password = reader.GetString(32);
        }
    }

#endif
}