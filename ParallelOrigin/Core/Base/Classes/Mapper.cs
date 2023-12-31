using System;
using System.Collections.Generic;

namespace ParallelOrigin.Core.Base.Classes {
    /// <summary>
    ///     A simple mapper which is capable of converting a class into another by applying unique logic.
    /// </summary>
    public class Mapper
    {
        /// <summary>
        ///     All registered custom <see cref="IMapper" />'s used for converting
        /// </summary>
        public IDictionary<Type, IMapper> Mappers { get; set; } = new Dictionary<Type, IMapper>();

        /// <summary>
        ///     Adds a mapper which applies unique logic upon the mapping process
        /// </summary>
        /// <param name="mapper"></param>
        /// <typeparam name="F"></typeparam>
        /// <typeparam name="T"></typeparam>
        public void AddMapper<TF, T>(IMapper mapper)
        {
            Mappers[typeof(T)] = mapper;
            Mappers[typeof(TF)] = mapper;
        }

        /// <summary>
        ///     Returns true if theres a unique mapper for the given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool ContainsMapper(Type type)
        {
            return Mappers.ContainsKey(type);
        }

        /// <summary>
        ///     Returns the mapper for the given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IMapper GetMapper(Type type)
        {
            return Mappers[type];
        }

        /// <summary>
        ///     Maps the passed type into another one.
        /// </summary>
        /// <param name="toMap"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public TO Map<T, TO>(in T toMap, object payload = null)
        {
            var type = toMap.GetType();
            if (!ContainsMapper(type)) return default;

            var mapper = GetMapper(type);
            if (mapper is IMapper<T, TO> genericMapper)
                return genericMapper.Map(toMap, payload);

            return default;
        }

        /// <summary>
        ///     Demaps a already mapped type back into its orginal form
        /// </summary>
        /// <param name="toMap"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public T Demap<T, TO>(in TO toMap, object payload = null)
        {
            var type = toMap.GetType();
            if (!ContainsMapper(type)) return default;

            var mapper = GetMapper(type);
            if (mapper is IMapper<T, TO> genericMapper)
                return genericMapper.Demap(toMap, payload);

            return default;
        }

        /// <summary>
        ///     Maps the passed type into another one.
        /// </summary>
        /// <param name="toMap"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public object Map(object toMap, object payload = null)
        {
            var type = toMap.GetType();
            if (!ContainsMapper(type)) return null;

            var mapper = GetMapper(type);
            return mapper.Map(toMap, payload);
        }

        /// <summary>
        ///     Demaps a already mapped type back into its orginal form
        /// </summary>
        /// <param name="toMap"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public object Demap(object toMap, object payload = null)
        {
            var type = toMap.GetType();
            if (!ContainsMapper(type)) return null;

            var mapper = GetMapper(type);
            return mapper.Demap(toMap, payload);
        }

        /// <summary>
        ///     This mapper interface is generic and does not produce any garbage.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="O"></typeparam>
        public interface IMapper<T, TO>
        {
            TO Map(in T obj, object meta);
            T Demap(in TO pojo, object meta);
        }

        /// <summary>
        ///     The mapper interface which is used to apply unique mapping logic. Its non generic and produces garbage.
        /// </summary>
        public interface IMapper
        {
            object Map(object obj, object meta);
            object Demap(object pojo, object meta);
        }
    }
}