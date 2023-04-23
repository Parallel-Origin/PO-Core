using System.Collections.Generic;
#if SERVER
using Arch.Core;
using Arch.LowLevel;
using ParallelOriginGameServer.Server.ThirdParty;
#endif

namespace ParallelOrigin.Core.ECS.Components.Combat
{
#if SERVER

    /// <summary>
    ///     Marks an entity event as healing
    /// </summary>
    public struct Heal
    {
        public Entity Receiver;
        public float Value;
    }

    // Marker to mark an entity as damage dealing 
    public struct Damage
    {
        public Entity Sender;
        public Entity Receiver;

        public bool Killed; // If the damage actually killed the entity 
    }

    // Marks an entity that is in combat with another one 
    public struct InCombat
    {
        public float Intervall;
        public Handle<HashSet<Entity>> Entities;

        public InCombat(int capacity)
        {
            Intervall = 0;
            Entities = new HashSet<Entity>(capacity).ToHandle();
        }
    }

    /// <summary>
    ///     Entities marked with that component will be respawned
    /// </summary>
    public struct OnDeathRespawn
    {
        public float TimeInMs;
        public float Intervall;
    }

    /// <summary>
    ///     Marks an entity as attacking.
    /// </summary>
    public struct Attacks
    {
    }

    /// <summary>
    ///     Marks an entity as dead, doesnt mean that its destroyed... its health is just below zero
    /// </summary>
    public struct Dead
    {
    }

#endif
}