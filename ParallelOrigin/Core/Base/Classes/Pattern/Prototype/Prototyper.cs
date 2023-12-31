using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ParallelOrigin.Core.Base.Interfaces;
using ParallelOrigin.Core.Base.Interfaces.Prototype;

namespace ParallelOrigin.Core.Base.Classes.Pattern.Prototype {

    /// <summary>
    /// A delegate which creates an certain object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate T Creator<out T>();
    
    /// <summary>
    /// A delegate which customizes a certain instance. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate void Customizer<T>(ref T instance);
    
    /// <summary>
    /// A storeable prototype. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Prototype<T>
    {
        public short Id;
        public T Instance;

        public Prototyper<T> Prototyper;
        public Creator<T> Creator;
        public Customizer<T> Customizer;
    }
    
    /// <summary>
    ///     Acts as a storage for cloneable classes which can get registered in order to clone them for instances later on.
    ///     <a href="https://de.wikipedia.org/wiki/Prototyp_(Entwurfsmuster)">Prototype Pattern</a>
    /// </summary>
    /// <typeparam name="T">The type we clone.</typeparam>
    public abstract class Prototyper<T> : IPrototyper<T>
    {
        public Prototyper()
        {
            Ids = new List<short>();
            _prototypes = Array.Empty<Prototype<T>>();
            Initialize();
        }

        private Prototype<T>[] _prototypes; // A cache that stores each instance once for lookup purposes
        public List<short> Ids { get; }
        public Prototype<T>[] Prototypes => _prototypes;

        public abstract void Initialize();

        ////////////////////////////////////
        // Main Methods
        ////////////////////////////////////

        /// <summary>
        ///     Registers the Type to the prototyper for being "cloned" later on by its ID
        ///     Provides several callbacks to determine how this entity should get customized after cloning
        /// </summary>
        /// <param name="id">The Type-ID of the entity we use to clone it</param>
        /// <param name="creator">A function which creates the entity we wanna clone</param>
        /// <param name="configurator">A action that gets called after the entity was created for configurating it</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Register(short id, Creator<T> creator, Customizer<T> configurator)
        {
            if(_prototypes.Length <= id)
                Array.Resize(ref _prototypes, id+1);

            var instanced = creator();
            configurator(ref instanced);
            
            _prototypes[id] = new Prototype<T>{ Id = id, Instance = instanced, Prototyper = this, Creator = creator, Customizer = configurator};
            Ids.Add(id);
            AfterInstanced(id, ref instanced);
        }

        /// <summary>
        ///     Returns a chached registered instance by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get(short id)
        {
            return _prototypes[id] != null ? _prototypes[id].Instance : default;
        }

        /// <summary>
        ///     Spawn and initializes a object of a certain class reference by its id.
        /// </summary>
        /// <param name="id">The Type-ID of the instance we want to clone</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Clone(short id)
        {
            try
            {
                // Constructing entity and configurating it shortly after
                var prototype = _prototypes[id];
                var instance = prototype.Creator();
                prototype.Customizer?.Invoke(ref instance);
                AfterClone(id, ref instance);

                return instance;
            }
            catch (Exception)
            {
                throw new IndexOutOfRangeException("An exception happened while trying to clone [" + id + "] or it does not exist yet in [" + this + "]");
            }
        }

        /// <summary>
        ///     Gets called once the registering completed in order to cache each registered instance once for later lookup purposes.
        /// </summary>
        /// <param name="typeId">The type id of the cloned instance</param>
        /// <param name="clonedInstance">The cloned instance</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void AfterInstanced(short typeId, ref T clonedInstance)
        {
        }

        /// <summary>
        ///     Gets called once the cloning was finished in order to execute logic on the cloned instance.
        /// </summary>
        /// <param name="typeId">Its typeID</param>
        /// <param name="clonedInstance">The already cloned instance we can modify</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void AfterClone(short typeId, ref T clonedInstance)
        {
        }
    }
}