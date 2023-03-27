#if SERVER
using Arch.Core;

namespace ParallelOrigin.Core.ECS.Events;

/// <summary>
///     An event fired, once an item was added to an other entities inventory.
/// </summary>
public struct ItemAddedEvent
{
    public Entity Entity;
    public Entity Item;

    public ItemAddedEvent(Entity entity, Entity item)
    {
        Entity = entity;
        Item = item;
    }
}

/// <summary>
///     An event fired, once an item was updated in an other entities inventory.
/// </summary>
public struct ItemUpdatedEvent
{
    public Entity Entity;
    public Entity Item;

    public ItemUpdatedEvent(Entity entity, Entity item)
    {
        Entity = entity;
        Item = item;
    }
}

/// <summary>
///     An event fired, once an item was removed in an other entities inventory.
/// </summary>
public struct ItemRemovedEvent
{
    public Entity Entity;
    public Entity Item;

    public ItemRemovedEvent(Entity entity, Entity item)
    {
        Entity = entity;
        Item = item;
    }
}

#endif