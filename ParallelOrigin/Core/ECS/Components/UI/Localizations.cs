
using System.Collections.Generic;

#if CLIENT
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
#endif

namespace ParallelOrigin.Core.ECS.Components.UI {

#if SERVER
    
    /// <summary>
    /// A component that stores a map of all valid localisations in a key value pair of string and id
    /// </summary>
    public struct Localizations {
        public Dictionary<string, short> localizations;
        public Dictionary<string, string> uniqueLocalizations;
    }

#elif CLIENT

    /// <summary>
    ///  A component that stores a map of all valid localisations in a key value pair of string and id
    /// </summary>
    [BurstCompile]
    public struct Localizations : IComponentData {
        public UnsafeHashMap<FixedString32, short> localizations;
        public UnsafeHashMap<FixedString32, FixedString32> uniqueLocalizations;
    }
    
#endif
}