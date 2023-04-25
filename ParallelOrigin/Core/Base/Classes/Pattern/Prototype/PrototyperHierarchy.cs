using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ParallelOrigin.Core.Base.Interfaces.Prototype;
using ParallelOrigin.Core.Extensions;

namespace ParallelOrigin.Core.Base.Classes.Pattern.Prototype {
    
    /// <summary>
    ///     A class that manages multiple {@link IPrototyper} and inserts them into a hierarchy in order to clone their objects by a certain path or id.
    /// </summary>
    /// <typeparam name="TO">The type.</typeparam>
    public class PrototyperHierarchy<TO>
    {
  
        internal IDictionary<string, Prototype<TO>> Instances = new Dictionary<string, Prototype<TO>>();
        internal IDictionary<int, Prototype<TO>> InstancesByHash = new Dictionary<int, Prototype<TO>>();
        internal IDictionary<string, IPrototyper<TO>> PrototypeHierarchy = new Dictionary<string, IPrototyper<TO>>();

        /// <summary>
        ///     Constructs a Hierarchy with the required methods to identify the path of the hierachy to the registered <see cref="IPrototyper{I,T}" />
        /// </summary>
        public PrototyperHierarchy() { }

        /// <summary>
        ///     Registers a <see cref="IPrototyper{I,T}" /> to a global map for quick acess to their instance and clone methods.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="prototyper"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Register(string path, IPrototyper<TO> prototyper)
        {
            PrototypeHierarchy[path] = prototyper;

            // Link every single path to each registered prototyper instance to a map for quick acess
            var ids = prototyper.Ids;
            for (var index = 0; index < ids.Count; index++)
            {
                var id = ids[index];
                var clonePath = path + ":" + id;
                var hash = clonePath.GetDeterministicHashCode();

                var prototype = prototyper.Prototypes[id];
                Instances[clonePath] = prototype;
                InstancesByHash[hash] = prototype;
            }
        }

        /// <summary>
        ///     Searches for a path and checks if theres a prototype registered.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Has(string path)
        {
            return Instances.ContainsKey(path);
        }

        /// <summary>
        ///     Searches for a path and checks if theres a prototype registered.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Has(int path)
        {
            return InstancesByHash.ContainsKey(path);
        }

        /// <summary>
        ///     Finds a registered <see cref="IPrototyper{I,T}" /> by his path and clones a objects by its typeID, the last path addition
        ///     gets used to clone the entity.
        /// </summary>
        /// <param name="path">The path of the prototyper we wanna acess, last path is the typeID of the entity we wanna clone.</param>
        /// <returns>The cloned entity from the prototyper</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TO Clone(string path)
        {
            var prototype = Instances[path];
            return prototype.Prototyper.Clone(prototype.Id);
        }

        /// <summary>
        ///     Finds a registered <see cref="IPrototyper{I,T}" /> by his path and clones a objects by its typeID, the last path addition
        ///     gets used to clone the entity.
        /// </summary>
        /// <param name="path">The path of the prototyper we wanna acess, last path is the typeID of the entity we wanna clone.</param>
        /// <returns>The cloned entity from the prototyper</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TO Clone(int path)
        {
            var prototype = InstancesByHash[path];
            return prototype.Prototyper.Clone(prototype.Id);;
        }

        /// <summary>
        ///     Finds a registered <see cref="IPrototyper{I,T}" /> by his path and clones a objects by its typeID, the last path addition
        ///     gets used to clone the entity.
        /// </summary>
        /// <param name="path">The path of the prototyper we wanna acess, last path is the typeID of the entity we wanna clone.</param>
        /// <returns>The cloned entity from the prototyper</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TO Get(string path)
        {
            return Instances[path].Instance;
        }

        /// <summary>
        ///     Finds a registered <see cref="IPrototyper{I,T}" /> by his path and clones a objects by its typeID, the last path addition
        ///     gets used to clone the entity.
        /// </summary>
        /// <param name="path">The path of the prototyper we wanna acess, last path is the typeID of the entity we wanna clone.</param>
        /// <returns>The cloned entity from the prototyper</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TO Get(int path)
        {
            return InstancesByHash[path].Instance;
        }
    }
}