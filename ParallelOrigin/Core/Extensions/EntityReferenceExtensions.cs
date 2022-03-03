using System.Collections.Generic;
using DefaultEcs;
using ParallelOrigin.Core.ECS;

namespace ParallelOrigin.Core.Extensions {
    
    /// <summary>
    /// An method for additional <see cref="EntityReference"/> methods
    /// </summary>
    public static class EntityReferenceExtensions {

        /// <summary>
        /// Finds a certain <see cref="EntityReference"/> by its <see cref="Entity"/> value. 
        /// </summary>
        /// <param name="references"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static EntityReference Find(this IEnumerable<EntityReference> references, ref Entity entity) {

            foreach (var reference in references) {

                if (reference.entity.IsAlive && reference.entity.Equals(entity))
                    return reference;
            }

            return default;
        }
    }
}