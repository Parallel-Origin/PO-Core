#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components.Combat {
    
#if SERVER

     /// <summary>
     ///  A component which stores informations about the health of a entity.
     /// </summary>
     public struct Health  {
         
         public float maxHealth;
         public float currentHealth;

         /// <summary>
         ///     Returns true if the entity is dead
         /// </summary>
         /// <returns></returns>
         public bool IsDead() { return currentHealth <= 0; }
     }   
     
#elif CLIENT 
    
    /// <summary>
    ///     A component which stores informations about the health of a entity.
    /// </summary>
    [BurstCompile]
    public struct Health : IComponentData {
        
        public float maxHealth;
        public float currentHealth;
        public bool destroyOnDeath;

        /// <summary>
        ///     Returns true if the entity is dead
        /// </summary>
        /// <returns></returns>
        public bool IsDead() { return currentHealth <= 0; }
    }
#endif
}