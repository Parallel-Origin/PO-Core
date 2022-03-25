using System;
using ParallelOrigin.Core.Base.Interfaces.Prototype;

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
    public class EntityPrototyperHierarchy : PrototyperHierarchy<short, Entity> {

        /// <summary>
        ///     Constructs a Hierarchy with the required methods to identify the path of the hierachy to the registered <see cref="IPrototyper{I,T}" />
        /// </summary>
        public EntityPrototyperHierarchy() : base(path => {
                
                unsafe{
                    
                    fixed(char *ptr = path) {

                        var current = ptr+path.Length-1;
                        for (var index = path.Length-1; index >= 0; index--) {
                            
                            current--;
                            if(*current != ':') continue;
                        
                            var length = current - ptr;
                            var splittedPath = new string(ptr, 0, (int)length);
                            var type = short.Parse(new string(ptr, index, path.Length - index));

                            return new ValueTuple<string, short>(splittedPath, type);
                        }
                    }
                }

                return default;
            },
            (s, s1) => s + ":" + s1) { }
    }

    #elif SERVER 

    /// <summary>
    ///  A <see cref="IPrototyper{I,T}" /> that is used with artemis odb to clone {@link com.artemis.Entity} by a path using
    ///  the registered <see cref="IPrototyper{I,T}" />
    /// </summary>
    public class EntityPrototyperHierarchy : PrototyperHierarchy<short, Entity> {

        /// <summary>
        ///  Constructs a Hierarchy with the required methods to identify the path of the hierachy to the registered <see cref="IPrototyper{I,T}" />
        ///  TODO : Find way to pool the string below because every clone or has operation will create one new string which is bad for memory and performance
        /// </summary>
        public EntityPrototyperHierarchy() : base(path => {

                // Loop over path from back to for
                for (var index = path.Length-1; index >= 0; index--) {

                    // Watch for the split char
                    var currentChar = path[index];
                    if (currentChar != ':') continue;

                    // Split into two, the path and the type and return them 
                    var pathSpan = path.AsSpan(0, index);
                    var typeSpan = path.AsSpan(index+1, (path.Length-1)-index);

                    var splitPath = pathSpan.ToString();
                    var type = short.Parse(typeSpan);
                    return new ValueTuple<string, short>(splitPath, type);
                }

                return default;
            },
            (s, s1) => s + ":" + s1){}
    }
    
    #endif
}