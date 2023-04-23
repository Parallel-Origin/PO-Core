#if SERVER
using System.Collections.Generic;
using Arch.Core;
using Arch.Core.Extensions;
using ParallelOrigin.Core.ECS;

namespace ParallelOrigin.Core.Extensions
{
    /// <summary>
    ///     An method for additional <see cref="EntityLink" /> methods
    /// </summary>
    public static class EntityReferenceExtensions
    {
        /// <summary>
        ///     Finds a certain <see cref="EntityLink" /> by its <see cref="Arch.Core.Entity" /> value.
        /// </summary>
        /// <param name="references"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static EntityLink Find(this IEnumerable<EntityLink> references, ref Entity entity)
        {
            foreach (var reference in references)
                if (reference.Entity.IsAlive() && reference.Entity.Equals(entity))
                    return reference;

            return default;
        }
    }
}

#endif