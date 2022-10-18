using System;

#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#elif SERVER
using DefaultEcs;
#endif

namespace ParallelOrigin.Core.ECS.Components {
    
    /// <summary>
    /// Possible changes to an entity. 
    /// </summary>
    [Flags]
    public enum DirtyFlags : byte {
        None,
        Transform,
        Health,
        Amount,
        Other
    }
    
#if SERVER

    /// <summary>
    /// Marks an entity as loged in
    /// </summary>
    public struct LogedIn{}
    
    /// <summary>
    /// Marks an entity as loged out
    /// </summary>
    public struct LogedOut {}
    
    /// <summary>
    ///  Marks a <see cref="Entity" /> as "created" during this frame...
    ///  Gets removed after the frame.
    /// </summary>
    public struct Created{ }
    
    /// <summary>
    /// Represents an inactive entity, does not mean that this entity is dead.
    /// </summary>
    public struct Inactive{ }

    /// <summary>
    /// Marks an entity as a prefab. Should not take place ingame. 
    /// </summary>
    public struct Prefab{}

    /// <summary>
    /// A Component which is used to check if Entites have been changed, updated or removed
    /// </summary>
    public struct Dirty {
        public DirtyFlags flags;
    }    
    
    /// <summary>
    ///  A component which marks a <see cref="Entity" /> as getting destroyed during the next few seconds. 
    /// </summary>
    public struct DestroyAfter {
        public float seconds;
    }
    
    /// <summary>
    ///     Destroys a <see cref="Entity" /> at the end of the current frame.
    /// </summary>
        
    public struct Destroy{ }
    
    /// <summary>
    ///     Marks a <see cref="Entity" /> as destroyed for the lifecycle...
    /// </summary>
        
    public struct Destroyed{ }

#elif CLIENT 
    
    /// <summary>
    ///     Marks a <see cref="Entity" /> as "created" during this frame...
    ///     Gets removed after the frame.
    /// </summary>
    [BurstCompile]
    public struct Created : IComponentData { }

    /// <summary>
    /// A Component which is used to check if Entites have been changed, updated or removed
    /// </summary>
    [BurstCompile]
    public struct Dirty : IComponentData {
        public bool preventZeroSize;
    }
    
        /// <summary>
    ///     A component which marks a <see cref="Entity" /> as getting destroyed during the next few ticks.
    /// </summary>
    [BurstCompile]
    public struct DestroyAfter : IComponentData {
        public short ticks;
    }

    /// <summary>
    ///     Destroys a <see cref="Entity" /> at the end of the current frame.
    /// </summary>
    [BurstCompile]
    public struct Destroy : IComponentData { }

    /// <summary>
    ///     Marks a <see cref="Entity" /> as destroyed for the lifecycle...
    /// </summary>
    [BurstCompile]
    public struct Destroyed : IComponentData { }
    
#endif
}