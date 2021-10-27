using System.Collections.Generic;

#if SERVER
using System.Drawing;
using ParallelOriginGameServer.Server.Extensions;
using ParallelOriginGameServer.Server.Systems;
#endif

namespace ParallelOrigin.Core.ECS.Components.Environment {

#if SERVER
    
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
    
#endif
}