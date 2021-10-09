using System.Drawing;

#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#elif SERVER 
using DefaultEcs;
using DefaultEcs;
#endif

namespace ParallelOrigin.Core.ECS.Components {

    /// <summary>
    /// Possible <see cref="Account"/> and <see cref="Character"/> genders
    /// </summary>
    public enum Gender {
        MALE,FEMALE,DIVERS
    }

    
#if SERVER
    
    /// <summary>
    ///  A component for a <see cref="Entity" /> which acts as a player.
    /// </summary>
    public struct Character  {
        
        public string name;
        public string email;
        public string password;

        public Gender male;
        public Color color;
    }    
    
#elif CLIENT 
    
    /// <summary>
    ///  A component for a <see cref="Entity" /> which acts as a player.
    /// </summary>
    [BurstCompile]
    public struct Character : IComponentData {

        public Entity entity;

        public FixedString32 name;
        public FixedString32 email;
        public FixedString32 password;

        public bool male;
        public Color color;
    }

#endif
}