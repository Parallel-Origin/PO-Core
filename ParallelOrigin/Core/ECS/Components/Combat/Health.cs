using LiteNetLib.Utils;

#if CLIENT
using LiteNetLib.Utils;
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components.Combat {
    
#if SERVER

     /// <summary>
     ///  A component which stores informations about the health of a entity.
     /// </summary>
     public struct Health : INetSerializable {
         
         public float maxHealth;
         public float currentHealth;

         /// <summary>
         ///     Returns true if the entity is dead
         /// </summary>
         /// <returns></returns>
         public bool IsDead() { return currentHealth <= 0; }

        public void Serialize(NetDataWriter writer) {
            writer.Put(maxHealth);
            writer.Put(currentHealth);
        }

        public void Deserialize(NetDataReader reader) {
            maxHealth = reader.GetFloat();
            currentHealth = reader.GetFloat();
        }
     }   
     
#elif CLIENT 
    
    /// <summary>
    ///     A component which stores informations about the health of a entity.
    /// </summary>
    [BurstCompile]
    public struct Health : IComponentData, INetSerializable {
        
        public float maxHealth;
        public float currentHealth;
        public bool destroyOnDeath;

        /// <summary>
        /// Returns true if the entity is dead
        /// </summary>
        /// <returns></returns>
        public bool IsDead() { return currentHealth <= 0; }

        public void Serialize(NetDataWriter writer) {
            writer.Put(maxHealth);
            writer.Put(currentHealth);
        }

        public void Deserialize(NetDataReader reader) {
            maxHealth = reader.GetFloat();
            currentHealth = reader.GetFloat();
        }
    }
#endif
}