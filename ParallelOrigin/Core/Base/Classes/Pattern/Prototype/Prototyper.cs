using System;
using System.Collections.Generic;
using ParallelOrigin.Core.Base.Interfaces;
using ParallelOrigin.Core.Base.Interfaces.Prototype;
using ParallelOriginGameServer;

namespace ParallelOrigin.Core.Base.Classes.Pattern.Prototype {

    /// <summary>
    ///     Acts as a storage for cloneable classes which can get registered in order to clone them for instances later on.
    ///     <p>
    ///         Attention, because of the bad support of deep cloning in java and the incompatibility with lambdas this prototyper wont clone registered classes.
    ///         <p>
    ///             Instead we register a lambda callback for creating a new instance of that object each time we "clone" a object.
    ///             @see <a href="https://de.wikipedia.org/wiki/Prototyp_(Entwurfsmuster)">Prototype Pattern</a>
    /// </summary>
    /// <typeparam name="I">The input type we use to clone</typeparam>
    /// <typeparam name="T">The type we clone</typeparam>
    public abstract class Prototyper<I, T> : IPrototyper<I, T>, IInitialisationable {

        public Prototyper() {

            Instances = new Dictionary<I, T>();
            Creators = new Dictionary<I, Func<T>>();
            Customizer = new Dictionary<I, Action<T>>();

            Initialize();
        }

        public abstract void Initialize();

        ////////////////////////////////////
        // Main Methods
        ////////////////////////////////////

        /// <summary>
        ///     Registers the Type to the prototyper for being "cloned" later on by its ID
        ///     Provides several callbacks to determine how this entity should get customized after cloning
        /// </summary>
        /// <param name="ID">The Type-ID of the entity we use to clone it</param>
        /// <param name="creator">A function which creates the entity we wanna clone</param>
        /// <param name="configurator">A action that gets called after the entity was created for configurating it</param>
        public void Register(I ID, Func<T> creator, Action<T> configurator) {

            Creators.Add(ID, creator);
            if (configurator != null)
                Customizer.Add(ID, configurator);
            
            // Create an instance and add it to the cache for lookups later on
            if (Instances.ContainsKey(ID)) return;
            
            var instanced = Clone(ID);
            Instances[ID] = instanced;
            AfterInstanced(ID, instanced);
        }

        /// <summary>
        /// Returns a chached registered instance by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(I id) {
            return Instances.TryGetValue(id, out var instance) ? instance : default;
        }

        /// <summary>
        ///     Spawn and initializes a object of a certain class reference by its id.
        /// </summary>
        /// <param name="ID">The Type-ID of the instance we want to clone</param>
        /// <returns></returns>
        public T Clone(I ID) {

            try {
                
                // Constructing entity and configurating it shortly after
                var instance = Creators[ID]();
                if(Customizer.TryGetValue(ID, out var customizer)) customizer(instance);
                AfterClone(ID, instance);

                return instance;
            }
            catch (Exception) { throw new IndexOutOfRangeException("An exception happened while trying to clone [" + ID + "] or it does not exist yet in [" + this + "]"); }
        }

        /// <summary>
        ///     Gets called once the registering completed in order to cache each registered instance once for later lookup purposes. 
        /// </summary>
        /// <param name="typeID">The type id of the cloned instance</param>
        /// <param name="clonedInstance">The cloned instance</param>
        public virtual void AfterInstanced(I typeID, T clonedInstance){}

        /// <summary>
        ///     Gets called once the cloning was finished in order to execute logic on the cloned instance.
        /// </summary>
        /// <param name="typeID">Its typeID</param>
        /// <param name="clonedInstance">The already cloned instance we can modify</param>
        public virtual void AfterClone(I typeID, T clonedInstance) { }
        
        private IDictionary<I,T> Instances { get; set; }         // A cache that stores each instance once for lookup purposes
        public IDictionary<I, Func<T>> Creators { get; set; }    // Stores all suppliers for creating instances of the types
        public IDictionary<I, Action<T>> Customizer { get; set; } // Gets called after creating a certain instance for configuration
    }
}