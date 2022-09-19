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
    /// Represents a value which can be modified, stores the base value and the modified one 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Stat<T> {

        public T baseValue;
        public T value;

        public Stat(T baseValue) : this() {
            this.baseValue = baseValue;
            this.value = baseValue;
        }

        public Stat(T baseValue, T value) {
            this.baseValue = baseValue;
            this.value = value;
        }
    }

    // The range with which an entity can attack
    public struct Range {
        public Stat<float> range;
    }

    // Attack speed
    public struct AttackSpeed {
        public Stat<float> speed;
    }

    // Physical damage & resistence 
    public struct PhysicalDamage {
        public Stat<float> damage;
    }

    // Physical resistence
    public struct PhysicalResistence {
        public Stat<float> resistence;
    }


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
     
     // Marks an entity as dead, doesnt mean that its destroyed... its health is just below zero 
     public struct Dead{}
     
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