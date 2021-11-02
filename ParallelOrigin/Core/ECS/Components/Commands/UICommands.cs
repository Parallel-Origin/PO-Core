using DefaultEcs;
using ParallelOrigin.Core.ECS.Components.Interactions;

namespace ParallelOriginGameServer.Server.Commands {

    /// <summary>
    /// A command which should spawn in a popup with certain data. 
    /// </summary>
    public struct PopUpCommand {
        
        public string type;
        public Entity owner;
        public Entity target;
    }
}