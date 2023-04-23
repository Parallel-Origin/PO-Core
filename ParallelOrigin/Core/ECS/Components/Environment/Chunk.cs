#if SERVER
using ParallelOrigin.Core.Base.Classes;
#endif

namespace ParallelOrigin.Core.ECS.Components.Environment
{
#if SERVER

    /// <summary>
    ///     Marks an entity as being able to load chunks.
    /// </summary>
    public struct ChunkLoader
    {
        public Grid Current;
        public Grid Previous;
    }

#endif
}