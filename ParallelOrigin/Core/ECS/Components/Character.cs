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
            writer.Put(name, name.Length);
            writer.Put(password, password.Length);
            writer.Put(email, email.Length);
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
        
        public FixedString32Bytes name;
        public FixedString32Bytes password;
        public FixedString32Bytes email;
        
        public void Serialize(NetDataWriter writer) {

            var nameCached = name.ToStringCached();
            var passwordCached = password.ToStringCached();
            var emailCached = email.ToStringCached();
            
            writer.Put(nameCached, nameCached.Length);
            writer.Put(passwordCached, passwordCached.Length);
            writer.Put(emailCached, emailCached.Length);
        }

        public void Deserialize(NetDataReader reader) {
            name = reader.GetString(32);
            password = reader.GetString(32);
            email = reader.GetString(32);
        }
    }

#endif
}