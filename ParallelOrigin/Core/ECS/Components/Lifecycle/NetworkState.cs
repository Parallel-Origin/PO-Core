namespace ParallelOrigin.Core.ECS.Components.Lifecycle {

#if SERVER

    /// <summary>
    /// Marks an entity as loged in
    /// </summary>
    public struct LogedIn {}
    
    /// <summary>
    /// Marks an entity as loged out
    /// </summary>
    public struct LogedOut {}
#endif
}