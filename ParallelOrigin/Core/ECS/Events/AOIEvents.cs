using Arch.Core;
using Collections.Pooled;

namespace ParallelOrigin.Core.ECS.Events;

/// <summary>
/// Marks an AOI event to signal that its an Entered AOI event
/// </summary>
public readonly struct AOIEnteredEvent
{
    public readonly Entity entity;
    public readonly PooledSet<EntityLink> entities;

    public AOIEnteredEvent(Entity entity, PooledSet<EntityLink> entities)
    {
        this.entity = entity;
        this.entities = entities;
    }
}

/// <summary>
/// Marks an AOI event to signal that its an Stayed AOI event
/// </summary>
public struct AOIStayedEvent
{
    public readonly Entity entity;
    public readonly PooledSet<EntityLink> entities;

    public AOIStayedEvent(Entity entity, PooledSet<EntityLink> entities)
    {
        this.entity = entity;
        this.entities = entities;
    }
}

/// <summary>
/// Marks an AOI event to signal that its an left AOI event
/// </summary>
public struct AOILeftEvent
{
    public readonly Entity entity;
    public readonly PooledSet<EntityLink> entities;

    public AOILeftEvent(Entity entity, PooledSet<EntityLink> entities)
    {
        this.entity = entity;
        this.entities = entities;
    }
}