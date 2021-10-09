
#if CLIENT
using Unity.Burst;
using Unity.Entities;
#endif


namespace ParallelOrigin.Core.ECS.Components.Reactive {

#if SERVER
    
    /// <summary>
    /// A Component which is used to check if Entites have been changed, updated or removed
    /// </summary>
    public struct Dirty  {
    }    
    
#elif CLIENT

    /// <summary>
    /// A Component which is used to check if Entites have been changed, updated or removed
    /// </summary>
    [BurstCompile]
    public struct Dirty : IComponentData {
        public bool preventZeroSize;
    }
    
#endif
}