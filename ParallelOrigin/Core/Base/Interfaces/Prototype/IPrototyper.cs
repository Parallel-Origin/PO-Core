using System.Collections.Generic;
using ParallelOrigin.Core.Base.Classes.Pattern.Prototype;

namespace ParallelOrigin.Core.Base.Interfaces.Prototype {
    /// <summary>
    ///     A interface for a object which acts as a prototyper for cloning registered object types.
    /// </summary>
    /// <typeparam name="T">The type id we require to clone a instance</typeparam>
    /// <typeparam name="T">The type we wanna clone</typeparam>
    public interface IPrototyper<T>
    {
        /// <summary>
        ///     All registered ids inside the prototyper.
        /// </summary>
        List<short> Ids { get; }

        /// <summary>
        ///     Returns the chached instance
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(short id);

        /// <summary>
        ///     Spawn and initializes a object of a certain class reference by its id.
        /// </summary>
        /// <param name="ID">The Type-ID of the instance we want to clone</param>
        /// <returns></returns>
        T Clone(short ID);
        
        Prototype<T>[] Prototypes { get; }
    }
}