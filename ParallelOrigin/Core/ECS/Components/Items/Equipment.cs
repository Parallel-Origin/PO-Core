#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
#elif SERVER
using System.Collections.Generic;
#endif

namespace ParallelOrigin.Core.ECS.Components.Items
{
#if SERVER

    /// <summary>
    ///     A component which marks an item as equipable and contains the mesh.
    /// </summary>
    public struct Equipable
    {
        public int Mesh;
        public string Slot;
    }

    /// <summary>
    ///     A struct for an entity which stores pairs of equiped item entities and their slot they are in
    /// </summary>
    public struct Equipment
    {
        public EntityLink Helmet;
        public EntityLink Armor;
        public EntityLink Weapon;
        public EntityLink Boots;
    }

    /// <summary>
    ///     Just an marker component for an entity which is equiped.
    ///     Does not store the equiper entity because its currently not nessecary.
    /// </summary>
    public struct Equiped
    {
    }

#elif CLIENT
    /// <summary>
    /// A struct for an entity which stores pairs of equiped item entities and their slot they are in
    /// </summary>
    [BurstCompatible]
    public struct Equipment : IComponentData {
        public UnsafeParallelHashMap<FixedString32Bytes, Equipable> Equiped;
    }

    /// <summary>
    /// A component which marks an item as equipable and contains the mesh.
    /// </summary>
    [BurstCompatible]
    public struct Equipable : IComponentData {
        
        public int Mesh;
        public FixedString32Bytes Slot;
    }
    
    /// <summary>
    /// Just an marker component for an entity which is equiped.
    /// Does not store the equiper entity because its currently not nessecary. 
    /// </summary>
    [BurstCompatible]
    public struct Equiped : IComponentData{ }

#endif
}