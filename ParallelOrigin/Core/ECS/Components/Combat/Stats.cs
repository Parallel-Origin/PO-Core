using System.Runtime.CompilerServices;
using LiteNetLib.Utils;
#if CLIENT
using LiteNetLib.Utils;
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components.Combat
{
#if SERVER

    /// <summary>
    ///     Represents a value which can be modified, stores the base value and the modified one
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Stat<T>
    {
        public T BaseValue;
        public T Value;

        public Stat(T baseValue) : this()
        {
            this.BaseValue = baseValue;
            Value = baseValue;
        }

        public Stat(T baseValue, T value)
        {
            this.BaseValue = baseValue;
            this.Value = value;
        }
    }

    // The range with which an entity can attack
    public struct Range
    {
        public Stat<float> Value;
    }

    // Attack speed
    public struct AttackSpeed
    {
        public Stat<float> Value;
    }

    // Physical damage & resistence 
    public struct PhysicalDamage
    {
        public Stat<float> Value;
    }

    // Physical resistence
    public struct PhysicalResistence
    {
        public Stat<float> Value;
    }


    /// <summary>
    ///     A component which stores informations about the health of a entity.
    /// </summary>
    public struct Health : INetSerializable
    {
        public float MaxHealth;
        public float CurrentHealth;

        /// <summary>
        ///     Returns true if the entity is dead
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsDead()
        {
            return CurrentHealth <= 0;
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(MaxHealth);
            writer.Put(CurrentHealth);
        }

        public void Deserialize(NetDataReader reader)
        {
            MaxHealth = reader.GetFloat();
            CurrentHealth = reader.GetFloat();
        }
    }


#elif CLIENT
    /// <summary>
    ///     A component which stores informations about the health of a entity.
    /// </summary>
    [BurstCompile]
    public struct Health : IComponentData, INetSerializable {
        
        public float MaxHealth;
        public float CurrentHealth;
        public bool DestroyOnDeath;

        /// <summary>
        /// Returns true if the entity is dead
        /// </summary>
        /// <returns></returns>
        public bool IsDead() { return CurrentHealth <= 0; }

        public void Serialize(NetDataWriter writer) {
            writer.Put(MaxHealth);
            writer.Put(CurrentHealth);
        }

        public void Deserialize(NetDataReader reader) {
            MaxHealth = reader.GetFloat();
            CurrentHealth = reader.GetFloat();
        }
    }
#endif
}