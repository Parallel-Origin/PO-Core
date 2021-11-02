using System;
using System.Collections.Generic;

#if SERVER
using DefaultEcs;
#elif CLIENT
using Unity.Entities;
#endif

namespace ParallelOrigin.Core.ECS.Components.Interactions {
    
    /// <summary>
    /// Marks an entity which was clicked and stores the entities which clicked it. 
    /// </summary>
    public struct Clicked : IDisposable{
        
        public ISet<Entity> clickers;
        public void Dispose() => clickers.Clear();
    }

    /// <summary>
    /// Once clicked, this entity will spawn a popup. 
    /// </summary>
    public struct OnClickedSpawnPopUp {
        public string type;
    }
}