#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components {
    
#if SERVER
    
    /// <summary>
    ///  A component which is used for entitys which are able to be harvested
    /// </summary>
    public struct Harvest  {
        public bool harvestable;
    }
    
#elif CLIENT 

    /// <summary>
    ///  A component which is used for entitys which are able to be harvested
    /// </summary>
    [BurstCompile]
    public struct Harvest : IComponentData {
        public bool harvestable;

        /// <summary>
        ///     The bool which determines if the resource is harvestable
        /// </summary>
        public bool Harvestable {
            get => harvestable;
            set => harvestable = value;
        }
    }
    
#endif
}