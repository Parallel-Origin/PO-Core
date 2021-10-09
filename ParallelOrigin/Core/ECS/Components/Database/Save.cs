namespace ParallelOrigin.Core.ECS.Components.Database {
    
    /// <summary>
    /// Marks an entity as being saved into the database
    /// </summary>
    public struct Save {}
    
    /// <summary>
    /// Marks an entity as being updated in the database
    /// </summary>
    public struct Update{}
    
    /// <summary>
    /// Marks an entity as being removed from the database
    /// </summary>
    public struct Remove{}
}