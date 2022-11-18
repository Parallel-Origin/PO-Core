using System;
using System.Collections.Generic;

namespace ParallelOrigin.Core.Base.Classes {
    /// <summary>
    ///     A custom stack implementation, with events and callbacks ( actions )
    ///     Previous elements are located in the stack, the top elements are located in currentKey/currentElement
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class EventStack<TK, T>
    {
        /// <summary>
        ///     The current key we are at...
        ///     Not listed in the stack yet...
        /// </summary>
        public TK CurrentKey { get; set; }

        /// <summary>
        ///     The current element we are at...
        ///     Is not listed in the stack yet.
        /// </summary>
        public T CurrentElement { get; set; }

        /// <summary>
        ///     A stack of keys for each opened UI-Element
        /// </summary>
        public Stack<TK> Keys { get; set; } = new();

        /// <summary>
        ///     Stack of our UI-Element history
        /// </summary>
        public Stack<T> ElementStack { get; set; } = new();

        /// <summary>
        ///     Gets called, once a element was opened, when the stack was empty before
        /// </summary>
        public Action OnFirst { get; set; } = () => { };

        /// <summary>
        ///     Gets called, once a new element opens
        /// </summary>
        public Action<TK, T> OnPush { get; set; } = (key, element) => { };

        /// <summary>
        ///     Gets called, once the top element gets pop
        /// </summary>
        public Action<TK, T> OnPop { get; set; } = (key, element) => { };

        /// <summary>
        ///     Gets called, once all elements are closed
        /// </summary>
        public Action OnEmpty { get; set; } = () => { };

        /// <summary>
        ///     Opens a UI-Element and puts the previous one on top of the stack
        /// </summary>
        /// <param name="value">The key of the element</param>
        public void Push(Tuple<TK, T> value)
        {
            if (CurrentKey != null) Keys.Push(CurrentKey);
            if (CurrentElement != null) ElementStack.Push(CurrentElement);
            if (CurrentKey != null && CurrentElement != null) OnPop(CurrentKey, CurrentElement);

            CurrentKey = value.Item1;
            CurrentElement = value.Item2;
            OnPush(CurrentKey, CurrentElement);
        }

        /// <summary>
        ///     Peeks at the first element in the stack and returns it.
        /// </summary>
        /// <returns></returns>
        public virtual Tuple<TK, T> Peek()
        {
            return new Tuple<TK, T>(CurrentKey, CurrentElement);
        }

        /// <summary>
        ///     Removes a Element from the stack completly
        /// </summary>
        /// <returns></returns>
        public virtual Tuple<TK, T> Pop()
        {
            if (CurrentElement == null && CurrentKey == null) return null;

            // Case if only one last element is left and we pop it
            if (CurrentElement != null && ElementStack.Count == 0)
            {
                OnPop.Invoke(CurrentKey, CurrentElement);

                var popedElementTuple = new Tuple<TK, T>(CurrentKey, CurrentElement);
                CurrentKey = default;
                CurrentElement = default;

                OnEmpty.Invoke();
                return popedElementTuple;
            }

            var lastKey = CurrentKey;
            var lastElement = CurrentElement;
            OnPop(lastKey, lastElement);

            CurrentKey = Keys.Pop();
            CurrentElement = ElementStack.Pop();
            OnPush(CurrentKey, CurrentElement);

            return new Tuple<TK, T>(lastKey, lastElement);
        }

        /// <summary>
        ///     Checks if this stack is empty and returns a bool value.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsEmpty()
        {
            return CurrentElement == null && ElementStack.Count == 0;
        }

        /// <summary>
        ///     Clears the whole stack
        /// </summary>
        public virtual void Clear()
        {
            if (ElementStack.Count != 0)
                for (var index = 0; index <= ElementStack.Count; index++)
                    Pop();

            Pop();
        }
    }
}