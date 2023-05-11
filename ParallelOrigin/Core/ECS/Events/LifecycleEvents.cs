#if SERVER
using Arch.Core;
using ParallelOrigin.Core.Base.Classes;
using ParallelOrigin.Core.ECS.Components;

namespace ParallelOrigin.Core.ECS.Events;

/// <summary>
///     The <see cref="CreateEvent"/> struct
///     represents a <see cref="Entity"/> creation event.
/// </summary>
public ref struct CreateEvent
{
    public Entity Entity;
    public ref Identity Identity;
    
    public CreateEvent(Entity entity, ref Identity identity)
    {
        Entity = entity;
        Identity = ref identity;
    }
}

/// <summary>
///     The <see cref="DestroyEvent"/> struct
///     represents a <see cref="Entity"/> destruction event.
/// </summary>
public ref struct DestroyEvent
{
    public Entity Entity;
    public ref Identity Identity;
    
    public DestroyEvent(Entity entity, ref Identity identity)
    {
        Entity = entity;
        Identity = ref identity;
    }
}

/// <summary>
///     The <see cref="ChunkCreatedEvent"/> struct
///     represents an event for a newly created chunk <see cref="Entity"/>. 
/// </summary>
public ref struct ChunkCreatedEvent
{
    public Entity Entity;
    public Grid Grid;

    public ChunkCreatedEvent(Entity entity, Grid grid)
    {
        Entity = entity;
        Grid = grid;
    }
}

/// <summary>
///     The <see cref="ChunkDestroyedEvent"/> struct
///     represents an event for a destroyed chunk <see cref="Entity"/>. 
/// </summary>
public ref struct ChunkDestroyedEvent
{
    public Entity Entity;
    public Grid Grid;

    public ChunkDestroyedEvent(Entity entity, Grid grid)
    {
        Entity = entity;
        Grid = grid;
    }
}

public struct LoginEvent
{
    public Entity Entity;

    public LoginEvent(Entity entity)
    {
        Entity = entity;
    }
}

public struct LogoutEvent
{
    public Entity Entity;

    public LogoutEvent(Entity entity)
    {
        Entity = entity;
    }
}
#endif