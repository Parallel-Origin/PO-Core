using System;
using ParallelOrigin.Core.ECS.Components.Items;

namespace ParallelOrigin.Core.ECS.Components.Environment {

#if SERVER

/// <summary>
///     An basic enum for the time
/// </summary>
public enum TimeUnit : byte
{
    MILLISECONDS,
    SECONDS,
    MINUTES,
    HOURS
}

/// <summary>
///     Marks an entity as spawnable
/// </summary>
public struct Spawnable : IWeight
{
    // The weight for this entity to spawn 
    public float NoiseThreshold;
    public float Weight { get; init; }
}

/// <summary>
///     Marks an spawner entity to spawn some stuff on command
/// </summary>
public struct Spawn
{
}

/// <summary>
///     Tags an spawner entitity in a certain intervall with a <see cref="Spawn" /> component
/// </summary>
public struct IntervallSpawner
{
    public ushort Intervall;
    public TimeUnit Unit;

    public DateTime RefreshedOn;
}

#endif
}