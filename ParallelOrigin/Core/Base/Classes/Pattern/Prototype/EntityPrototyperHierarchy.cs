#if CLIENT
using Unity.Entities;
#elif SERVER
using DefaultEcs;
#endif

namespace ParallelOrigin.Core.Base.Classes.Pattern.Prototype {

    #if CLIENT
    
    /// <summary>
    ///  A <see cref="IPrototyper{I,T}" /> that is used with artemis odb to clone {@link com.artemis.Entity} by a path using
    ///  the registered <see cref="IPrototyper{I,T}" />
    /// </summary>
    public class EntityPrototyperHierarchy : PrototyperHierarchy<string, short, Entity> {

        /// <summary>
        ///     Constructs a Hierarchy with the required methods to identify the path of the hierachy to the registered <see cref="IPrototyper{I,T}" />
        /// </summary>
        public EntityPrototyperHierarchy() : base(strings => {

                try {
                    if (strings.Length <= 1) return strings[0];
                }
                catch (Exception e) {
                    return default;
                }

                var combined = string.Join(":", strings);
                var combinedString = combined.Remove(combined.Length - 1);
                return combinedString;
            },
            (s, s1) => s + ":" + s1,
            s => s.Split(':'), 
            short.Parse) { }
    }

    #elif SERVER 

    /// <summary>
    ///  A <see cref="IPrototyper{I,T}" /> that is used with artemis odb to clone {@link com.artemis.Entity} by a path using
    ///  the registered <see cref="IPrototyper{I,T}" />
    /// </summary>
    public class EntityPrototyperHierarchy : PrototyperHierarchy<string, short, Entity> {

        /// <summary>
        ///     Constructs a Hierarchy with the required methods to identify the path of the hierachy to the registered <see cref="IPrototyper{I,T}" />
        /// </summary>
        public EntityPrototyperHierarchy() : base(strings => {

                try {
                    if (strings.Length <= 1) return strings[0];
                }
                catch (Exception e) {
                    return default;
                }

                var combined = string.Join(":", strings);
                var combinedString = combined.Remove(combined.Length - 1);
                return combinedString;
            },
            (s, s1) => s + ":" + s1,
            s => s.Split(':'), 
            short.Parse) { }
    }
    
    #endif
}