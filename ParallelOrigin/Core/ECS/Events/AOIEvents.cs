#if SERVER
using Arch.Core;
using Collections.Pooled;

namespace ParallelOrigin.Core.ECS.Events;

/// <summary>
/// Marks an AOI event to signal that its an Entered AOI event
/// </summary>
public readonly struct AoiEnteredEvent
{
    public readonly Entity Entity;
    public readonly PooledSet<EntityLink> Entities;

    public AoiEnteredEvent(Entity entity, PooledSet<EntityLink> entities)
    {
        this.Entity = entity;
        this.Entities = entities;
    }
}

/// <summary>
/// Marks an AOI event to signal that its an Stayed AOI event
/// </summary>
public struct AoiStayedEvent
{
    public readonly Entity Entity;
    public readonly PooledSet<EntityLink> Entities;

    public AoiStayedEvent(Entity entity, PooledSet<EntityLink> entities)
    {
        this.Entity = entity;
        this.Entities = entities;
    }
}

/// <summary>
/// Marks an AOI event to signal that its an left AOI event
/// </summary>
public struct AoiLeftEvent
{
    public readonly Entity Entity;
    public readonly PooledSet<EntityLink> Entities;

    public AoiLeftEvent(Entity entity, PooledSet<EntityLink> entities)
    {
        this.Entity = entity;
        this.Entities = entities;
    }
}

#endif