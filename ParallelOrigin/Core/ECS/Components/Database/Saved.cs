namespace ParallelOrigin.Core.ECS.Components.Database {
    
    /// <summary>
    /// An marker for an entity which was saved in the database
    /// </summary>
    public struct Saved { }
    
    /// <summary>
    /// An marker for an entity which was updated in the database 
    /// </summary>
    public struct Updated{}
}