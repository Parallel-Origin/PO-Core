namespace ParallelOrigin.Core.ECS.Components.Database {
    
    /// <summary>
    /// An component which stores the database model of an entity
    /// </summary>
    public struct Model {
        public object model;
    }
    
    /// <summary>
    /// Marks an entity as saveable
    /// </summary>
    public struct Saveable{}
    
    /// <summary>
    /// Marks an entity as updateable
    /// </summary>
    public struct Updateable{}
}