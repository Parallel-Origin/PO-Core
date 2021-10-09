using System;
using System.Collections.Generic;

namespace ParallelOrigin.Core.Base.Interfaces.Prototype {

    /// <summary>
    ///     A interface for a object which acts as a prototyper for cloning registered object types.
    /// </summary>
    /// <typeparam name="T">The type id we require to clone a instance</typeparam>
    /// <typeparam name="T">The type we wanna clone</typeparam>
    public interface IPrototyper<I, T> {

        /// <summary>
        ///     Registers the Type to the prototyper for being "cloned" later on by its ID
        ///     Provides several callbacks to determine how this entity should get customized after cloning
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="creator"></param>
        /// <param name="configurator"></param>
        void Register(I ID, Func<T> creator, Action<T> configurator);

        /// <summary>
        /// Returns the chached instance
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(I id);
        
        /// <summary>
        ///     Spawn and initializes a object of a certain class reference by its id.
        /// </summary>
        /// <param name="ID">The Type-ID of the instance we want to clone</param>
        /// <returns></returns>
        T Clone(I ID);

        /// <summary>
        ///     Gets called once the registering completed in order to cache each registered instance once for later lookup purposes. 
        /// </summary>
        /// <param name="typeID">The type id of the cloned instance</param>
        /// <param name="clonedInstance">The cloned instance</param>
        void AfterInstanced(I typeID, T clonedInstance);

        
        /// <summary>
        ///     Gets called once the cloning was finished in order to execute logic on the cloned instance.
        /// </summary>
        /// <param name="typeID">The type id of the cloned instance</param>
        /// <param name="clonedInstance">The cloned instance</param>
        void AfterClone(I typeID, T clonedInstance);

        /// <summary>
        /// The creators used for creating the entities
        /// </summary>
        IDictionary<I, Func<T>> Creators { get; set; } // Stores all suppliers for creating instances of the types
        
        /// <summary>
        /// The customizers getting applied after the creator was called to customize each entity further.
        /// </summary>
        IDictionary<I, Action<T>> Customizer { get; set; } // Gets called after creating a certain instance for configuration
    }
}