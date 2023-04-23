#if SERVER
using Arch.Core;
using ParallelOrigin.Core.ECS.Components;

namespace ParallelOrigin.Core.ECS.Events;

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