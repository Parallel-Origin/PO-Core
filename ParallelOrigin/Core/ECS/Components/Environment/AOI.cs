using System.Collections.Generic;

#if SERVER
using System.Drawing;
using ParallelOrigin.Core.ECS.Components.Reactive;
using ParallelOriginGameServer.Server.Extensions;
using ParallelOriginGameServer.Server.Systems;
#endif

namespace ParallelOrigin.Core.ECS.Components.Environment {

#if SERVER
    
    /// <summary>
    /// Marks an entity as dirty for network, gets consumed by the network to send the changes to users. 
    /// </summary>
    public struct DirtyNetworkTransform { }
    
    /// <summary>
    /// Marks an entity as dirty for updating its health in the network. 
    /// </summary>
    public struct DirtyNetworkHealth{}
    
    /// <summary>
    /// Marks an entity for being able to 
    /// </summary>
    public struct AOI {
        public float range;
        public HashSet<QuadEntity> inAOI;
    }

    /// <summary>
    /// Marks an entity with a list of entities that lately entered its aoi
    /// </summary>
    public struct AOIEntered {
        public HashSet<QuadEntity> entered;
    }

    /// <summary>
    /// Marks an entity with a list of entities which lately left its aoi 
    /// </summary>
    public struct AOILeft {
        public HashSet<QuadEntity> left;
    }
    
#endif
}