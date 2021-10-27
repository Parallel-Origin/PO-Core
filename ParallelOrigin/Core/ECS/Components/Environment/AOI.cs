using System.Collections.Generic;
using System.Drawing;
using ParallelOriginGameServer.Server.Extensions;
using ParallelOriginGameServer.Server.Systems;

namespace ParallelOrigin.Core.ECS.Components.Environment {
    
    /// <summary>
    /// Marks an entity for being able to 
    /// </summary>
    public struct AOI {
        public float range;
        public HashSet<QuadEntity> inAOI;
    }

    public struct AOIEntered {
        public HashSet<QuadEntity> entered;
    }

    public struct AOILeft {
        public HashSet<QuadEntity> left;
    }
}