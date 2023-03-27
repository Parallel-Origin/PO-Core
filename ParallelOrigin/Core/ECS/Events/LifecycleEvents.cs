using Arch.Core;

namespace ParallelOrigin.Core.ECS.Events;

public struct LoginEvent
{
    public Entity Entity;
}

public struct LogoutEvent
{
    public Entity Entity;
}