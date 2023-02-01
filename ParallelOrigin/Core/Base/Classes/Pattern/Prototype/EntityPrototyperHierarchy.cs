#if CLIENT
using Unity.Entities;
#elif SERVER
using DefaultEcs;
#endif
using System;
using ParallelOrigin.Core.Base.Interfaces.Prototype;

namespace ParallelOrigin.Core.Base.Classes.Pattern.Prototype {


#if CLIENT
    /// <summary>
    ///  A <see cref="IPrototyper{I,T}" /> that is used with artemis odb to clone {@link com.artemis.Entity} by a path using
    ///  the registered <see cref="IPrototyper{I,T}" />
    /// </summary>
    public class EntityPrototyperHierarchy : PrototyperHierarchy<Entity> {

        /// <summary>
        ///     Constructs a Hierarchy with the required methods to identify the path of the hierachy to the registered <see cref="IPrototyper{I,T}" />
        /// </summary>
        public EntityPrototyperHierarchy() : base() { }

    }

#elif SERVER

    /// <summary>
    ///     A <see cref="IPrototyper{I,T}" /> that is used with artemis odb to clone {@link com.artemis.Entity} by a path using
    ///     the registered <see cref="IPrototyper{I,T}" />
    /// </summary>
    public class EntityPrototyperHierarchy : PrototyperHierarchy<Entity>
    {
        /// <summary>
        ///     Constructs a Hierarchy with the required methods to identify the path of the hierachy to the registered <see cref="IPrototyper{I,T}" />
        ///     TODO : Find way to pool the string below because every clone or has operation will create one new string which is bad for memory and performance
        /// </summary>
        public EntityPrototyperHierarchy() : base()
        { }
    }

#endif
}