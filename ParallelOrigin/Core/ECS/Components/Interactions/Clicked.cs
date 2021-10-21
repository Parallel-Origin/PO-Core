using System.Collections.Generic;
using DefaultEcs;

namespace ParallelOrigin.Core.ECS.Components.Interactions {
    
    /// <summary>
    /// Marks an entity which was clicked and stores the entities which clicked it. 
    /// </summary>
    public struct Clicked {
        public ISet<Entity> clickers;
    }
}