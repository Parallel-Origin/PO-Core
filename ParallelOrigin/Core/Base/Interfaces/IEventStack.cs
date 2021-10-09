using System;
using System.Collections.Generic;
using ParallelOrigin.Core.Base.Interfaces.Storage;

namespace ParallelOrigin.Core.Base.Interfaces {
    
    /// <summary>
    ///     A interface for a Stack with a key value pair and some events.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public interface IEventStack<K, V> : IStack<Tuple<K, V>> {
        
        /// <summary>
        ///     The current key we are at...
        ///     Not listed in the stack yet...
        /// </summary>
        K CurrentKey { get; set; }

        /// <summary>
        ///     The current element we are at...
        ///     Is not listed in the stack yet.
        /// </summary>
        V CurrentElement { get; set; }

        /// <summary>
        ///     A stack of keys for each opened UI-Element
        /// </summary>
        Stack<K> Keys { get; set; }

        /// <summary>
        ///     Stack of our UI-Element history
        /// </summary>
        Stack<V> ElementStack { get; set; }

        /// <summary>
        ///     Gets called, once a element was opened, when the stack was empty before
        /// </summary>
        Action OnFirst { get; set; }

        /// <summary>
        ///     Gets called, once a new element opens
        /// </summary>
        Action<K, V> OnPush { get; set; }

        /// <summary>
        ///     Gets called, once the top element gets pop
        /// </summary>
        Action<K, V> OnPop { get; set; }

        /// <summary>
        ///     Gets called, once all elements are closed
        /// </summary>
        Action OnEmpty { get; set; }
    }
}