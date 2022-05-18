using System;
using System.Globalization;
using ParallelOrigin.Core.Base.Classes;
using ParallelOrigin.Core.ECS.Components.Items;

namespace ParallelOrigin.Core.ECS.Components.Environment {

#if SERVER
    
    /// <summary>
    /// An basic enum for the time
    /// </summary>
    public enum TimeUnit : byte{
        MILLISECONDS, SECONDS, MINUTES, HOURS
    }
    
    /// <summary>
    /// Marks an entity as spawnable
    /// </summary>
    public struct Spawnable : IWeight{

        // The weight for this entity to spawn 
        public float weight;
        public float noiseThreshold;
        
        public float Weight { get => weight; set => weight = value; }
    }

    /// <summary>
    /// Marks an spawner entity to spawn some stuff on command
    /// </summary>
    public struct Spawn{}
    
    /// <summary>
    /// Tags an spawner entitity in a certain intervall with a <see cref="Spawn"/> component
    /// </summary>
    public struct IntervallSpawner {
        
        public ushort intervall;
        public TimeUnit unit;
        
        public DateTime refreshedOn;
    }

    /// <summary>
    /// A struct which marks an spawner to spawn in forests
    /// </summary>
    public struct ForestSpawner { }
    
    /// <summary>
    /// A struct marking a spawner to spawn in various mobs 
    /// </summary>
    public struct MobSpawner{}

#endif
}