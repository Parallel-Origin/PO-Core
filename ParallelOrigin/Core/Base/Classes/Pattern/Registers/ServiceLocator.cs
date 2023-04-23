using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ParallelOrigin.Core.Base.Classes.Pattern.Registers {
    /// <summary>
    ///     Own implementation of a ServiceLocator
    ///     Stores references to "Services" and Class-Instances for acessing the first found implementation in a static and global way.
    ///     This prevents decoupling
    /// </summary>
    public class ServiceLocator
    {
        private static readonly IDictionary<Type, object> Services = new ConcurrentDictionary<Type, object>();
        private static readonly IDictionary<Type, List<Action<object>>> Await = new ConcurrentDictionary<Type, List<Action<object>>>();

        /// <summary>
        ///     Adds a register to the static registers for easy acess.
        /// </summary>
        /// <param name="toRegister"></param>
        public static void Register<T>(T toRegister)
        {
            var registerType = typeof(T);
            if (!Services.ContainsKey(registerType))
                Services.Add(registerType, toRegister);
            else return;

            foreach (var type in Await)
                if (type.Key.IsInstanceOfType(toRegister))
                    type.Value.ForEach(action => action.Invoke(toRegister));
        }

        /// <summary>
        ///     Adds a register to the static registers for easy acess.
        /// </summary>
        /// <param name="toRegister"></param>
        public static void Register(object toRegister)
        {
            var registerType = toRegister.GetType();
            if (!Services.ContainsKey(registerType))
                Services.Add(registerType, toRegister);
            else return;

            foreach (var type in Await)
                if (type.Key.IsInstanceOfType(toRegister))
                    type.Value.ForEach(action => action.Invoke(toRegister));
        }

        /// <summary>
        ///     Searches for a registered Register-class in a static way.
        ///     Only searches for Registers, not registerables.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetBySubType<T>()
        {
            foreach (var kvp in Services)
            {
                var obj = kvp.Value;
                if (obj is T t) return t;
            }

            return default;
        }

        /// <summary>
        ///     Searches for a registered Register-class in a static way.
        ///     Only searches for Registers, not registerables.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            var type = typeof(T);
            var existing = Services.TryGetValue(type, out var service);
            if (existing) return (T)service;

            return default;
        }

        /// <summary>
        ///     Sets a wait lambda, which gets called once the awaited class gets registered.
        /// </summary>
        /// <param name="registered"></param>
        /// <typeparam name="T"></typeparam>
        public static void Wait<T>(Action<object> registered)
        {
            if (Get<T>() != null)
            {
                registered(Get<T>());
                return;
            }

            if (Await.ContainsKey(typeof(T)))
                Await[typeof(T)].Add(registered);
            else Await.Add(typeof(T), new List<Action<object>> { registered });
        }
    }
}