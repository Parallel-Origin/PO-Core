using System;
using System.Collections.Generic;
using ParallelOrigin.Core.Base.Interfaces.Observer;

namespace ParallelOrigin.Core.Base.Classes.Pattern.Observer {
    /// <summary>
    ///     A class used to pass different class types and values in for allowing to pass unlimited variants of parameters trough the method...
    /// </summary>
    public class Params : IParams
    {
        /// <summary>
        ///     Accepts all Objects and stores them inside a list.
        ///     <code>new Params(new Player(), 10f)</code>
        /// </summary>
        /// <param name="param"></param>
        public Params(params object[] param)
        {
            for (var index = 0; index < param.Length; index++)
            {
                var obj = param[index];
                var classType = obj.GetType();

                ParamList.Add(index, new Tuple<Type, object>(classType, obj));
            }
        }

        /// <summary>
        ///     Returns true if the param order equals the passed trough param order.
        /// </summary>
        /// <param name="paramTypes"></param>
        /// <returns></returns>
        public bool Contains(params Type[] paramTypes)
        {
            foreach (var variable in ParamList)
                if (!paramTypes[variable.Key].Equals(variable.Value.Item1))
                    return false;

            return true;
        }


        /// <summary>
        ///     Returns true if the place is of type
        /// </summary>
        /// <param name="place">The index of the param</param>
        /// <param name="type">The type</param>
        /// <returns>True if the index type == the class Type</returns>
        public bool IsType(int place, Type type)
        {
            return ParamList[place].Item2.Equals(type);
        }


        /// <summary>
        ///     Returns true if the place param "==" the value
        /// </summary>
        /// <param name="place">The index of the param</param>
        /// <param name="value">The value we want to compare to the place</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>True if the value is the same as the value at the index.</returns>
        public bool IsType<T>(int place, T value)
        {
            if (!IsType(place, value.GetType()))
                return false;
            return ParamList[place].Item1.Equals(value);
        }

        /// <summary>
        ///     Returns the tuple for a certain place.
        /// </summary>
        /// <param name="place"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetAs<T>(int place)
        {
            if (ParamList.Count < place)
                return default;

            return (T)ParamList[place].Item2;
        }

        /// <summary>
        ///     Returns all contained params
        /// </summary>
        public IDictionary<int, Tuple<Type, object>> ParamList { get; set; } = new Dictionary<int, Tuple<Type, object>>();
    }
}