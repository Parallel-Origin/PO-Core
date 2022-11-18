using System;
using System.Collections.Generic;

namespace ParallelOrigin.Core.Base.Interfaces.Observer {
    /// <summary>
    ///     A interface for a class which stores params for dynamic params pass trough.
    /// </summary>
    public interface IParams
    {
        /// <summary>
        ///     Returns all contained params
        /// </summary>
        IDictionary<int, Tuple<Type, object>> ParamList { get; set; }

        /// <summary>
        ///     Returns true if the param order equals the passed trough param order.
        /// </summary>
        /// <param name="paramTypes"></param>
        /// <returns></returns>
        bool Contains(params Type[] paramTypes);


        /// <summary>
        ///     Returns true if the place is of type
        /// </summary>
        /// <param name="place">The index of the param</param>
        /// <param name="type">The type</param>
        /// <returns>True if the index type == the class Type</returns>
        bool IsType(int place, Type type);


        /// <summary>
        ///     Returns true if the place param "==" the value
        /// </summary>
        /// <param name="place">The index of the param</param>
        /// <param name="value">The value we want to compare to the place</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>True if the value is the same as the value at the index.</returns>
        bool IsType<T>(int place, T value);

        /// <summary>
        ///     Returns the tuple for a certain place.
        /// </summary>
        /// <param name="place"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetAs<T>(int place);
    }
}