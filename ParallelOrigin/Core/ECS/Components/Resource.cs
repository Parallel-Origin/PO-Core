
using LiteNetLib.Utils;
#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components {


#if SERVER

    /// <summary>
    ///  Just a Tag Component which clarifies that the Entity attached is a Resource
    /// </summary>
    public struct Resource : INetSerializable {
        
        public void Serialize(NetDataWriter writer) {  }
        public void Deserialize(NetDataReader reader) {  }
    }
    
#elif CLIENT

    /// <summary>
    ///  Just a Tag Component which clarifies that the Entity attached is a Resource
    /// </summary>
    [BurstCompile]
    public struct Resource : IComponentData, INetSerializable {
    
        public void Serialize(NetDataWriter writer) {  }
        public void Deserialize(NetDataReader reader) {  }
    }
    
#endif
}