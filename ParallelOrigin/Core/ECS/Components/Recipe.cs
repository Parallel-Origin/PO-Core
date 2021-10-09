#if CLIENT
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
#endif

namespace ParallelOrigin.Core.ECS.Components {
    
    /// <summary>
    ///  A buffer element that acts as an storage for required items of a <see cref="Recipe" />
    /// </summary>
    #if CLIENT
    [BurstCompile]
    #endif
    public struct RecipeIngredient {

        public short amount;
        public short meshID;
        public short localizationID;
        public short iconID;
        
        public string typeID;
    }

#if SERVER
    
    /// <summary>
    ///  A component that represents an recipe and stores all data for being displayed in some menu.
    /// </summary>
    public struct Recipe  {
        
        public float duration;
        public short amount;
        public short iconID;
        
        public string typeID;
        public List<RecipeIngredient> ingredients;
    }
    
#elif CLIENT 

    /// <summary>
    ///  A component that represents an recipe and stores all data for being displayed in some menu.
    /// </summary>
    [BurstCompile]
    public struct Recipe : IComponentData {
        
        public float duration;
        public short amount;
        public short iconID;
        
        public FixedString32 typeID;
        public UnsafeList<RecipeIngredient> ingredients;
    }

#endif
}