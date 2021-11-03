
using ParallelOrigin.Core.ECS.Components.Interactions;

#if SERVER
using DefaultEcs;
#endif

namespace ParallelOriginGameServer.Server.Commands {

#if SERVER
    
    /// <summary>
    /// A command which should spawn in a popup with certain data. 
    /// </summary>
    public struct PopUpCommand {
        
        public string type;
        public Entity owner;
        public Entity target;
    }

#endif
}