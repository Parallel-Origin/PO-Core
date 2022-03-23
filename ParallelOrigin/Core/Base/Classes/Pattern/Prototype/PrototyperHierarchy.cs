using System;
using System.Collections.Generic;
using ParallelOrigin.Core.Base.Interfaces.Prototype;

namespace ParallelOrigin.Core.Base.Classes.Pattern.Prototype {

    /// <summary>
    ///     A class that manages multiple {@link IPrototyper} and inserts them into a hierarchy in order to clone their objects by a certain path or id.
    /// </summary>
    /// <typeparam name="PATH">The path-type... mostly a string.</typeparam>
    /// <typeparam name="T">The input we take to clone a object from the <see cref="IPrototyper{I,T}" /></typeparam>
    /// <typeparam name="O">The output we await from the <see cref="IPrototyper{I,T}" /></typeparam>
    public class PrototyperHierarchy<T, TO> {
        
        protected Func<string, ValueTuple<string, T>> pathDissembler;
        protected Func<string, string, string> pathExtender;

        protected IDictionary<string, PrototypeNode> prototypeHierarchy = new Dictionary<string, PrototypeNode>();

        /// <summary>
        ///     Constructs a Hierarchy with the required methods to identify the path of the hierachy to the registered <see cref="IPrototyper{I,T}" />
        /// </summary>
        /// <param name="pathCombiner">A callback that is used to combine two paths to one.</param>
        /// <param name="pathExtender">A callback that is used to extend a path to a new layer.</param>
        /// <param name="pathDissembler">A callback that is used to disemble a path into its various layers</param>
        /// <param name="pathToInput">A callback that is used to convert a layer from the path into the input for cloning a object.</param>
        public PrototyperHierarchy(Func<string, ValueTuple<string, T>> pathDissembler, Func<string, string, string> pathExtender) {
            this.pathExtender = pathExtender;
            this.pathDissembler = pathDissembler;
        }


        /// <summary>
        ///     Registers a "root" <see cref="IPrototyper{I,T}" /> to a id which is used to acess that prototyper later on.
        /// </summary>
        /// <param name="path">The id & path which we wanna assign to that prototyper</param>
        /// <param name="prototyper">The prototyper we wanna register.</param>
        /// <exception cref="RuntimeException">A exception getting thrown if a <see cref="IPrototyper{I,T}" /> with that path was already registered</exception>
        public void Register(string path, IPrototyper<T, TO> prototyper) {

            if (!prototypeHierarchy.ContainsKey(path)) {

                var node = new PrototypeNode {path = path, prototype = prototyper};
                prototypeHierarchy.Add(path, node);
            }
            else { throw new SystemException("Prototyper with that key already registered : [" + path + "]"); }
        }

        /// <summary>
        ///     Registers a child <see cref="IPrototyper{I,T}" /> to a existing parent {@link IPrototype}, to use the combined ID for acess.
        /// </summary>
        /// <param name="parent">parent The parents id</param>
        /// <param name="id">The childs id</param>
        /// <param name="prototyper">The <see cref="IPrototyper{I,T}" /> we wanna register to that ID-Path.</param>
        /// <exception cref="RuntimeException"></exception>
        public void Register(string parent, string id, IPrototyper<T, TO> prototyper) {

            if (prototypeHierarchy.ContainsKey(parent)) {

                var node = prototypeHierarchy[parent];

                var child = new PrototypeNode {path = id, prototype = prototyper, parent = node};
                node.childs.Add(child);

                var combinedPath = pathExtender(node.path, child.path);
                prototypeHierarchy.Add(combinedPath, child);

            }
            else { throw new SystemException("Prototyper with that key is not registered yet : [" + id + "]"); }
        }

        /// <summary>
        ///  Searches for a path and checks if theres a prototype registered.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public bool Has(string path, T typeID) {

            if (!prototypeHierarchy.ContainsKey(path)) return false;
            
            var node = prototypeHierarchy[path];
            var found = node.prototype.Get(typeID);

            return !found.Equals(default(TO));
        }

        /// <summary>
        /// Searches for a path and checks if theres a prototype registered.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool Has(string path) {

            // TODO : Find way to check for incoming path without try and catch
            try {
                
                var prototyperPath = pathDissembler(path);
                var node = prototypeHierarchy[prototyperPath.Item1];

                if (node == null) return false;

                var found = node.prototype.Get(prototyperPath.Item2);
                return !found.Equals(default(TO));
            }
            catch (Exception e) { return false; }
        }
        

        /// <summary>
        ///     Finds a registered <see cref="IPrototyper{I,T}" /> by his path and clones a objects by its typeID.
        /// </summary>
        /// <param name="path">The path of the prototyper we wanna acess</param>
        /// <param name="typeID">The type of the entity we wanna clone</param>
        /// <returns>The cloned entity from the prototyper</returns>
        public TO Clone(string path, T typeID) {

            var node = prototypeHierarchy[path];
            return node.prototype.Clone(typeID);
        }


        /// <summary>
        ///     Finds a registered <see cref="IPrototyper{I,T}" /> by his path and clones a objects by its typeID, the last path addition
        ///     gets used to clone the entity.
        /// </summary>
        /// <param name="path">The path of the prototyper we wanna acess, last path is the typeID of the entity we wanna clone.</param>
        /// <returns>The cloned entity from the prototyper</returns>
        public TO Clone(string path) {

            var dissembledPath = pathDissembler(path);
            return Clone(dissembledPath.Item1, dissembledPath.Item2);
        }

        /// <summary>
        ///     Finds a registered <see cref="IPrototyper{I,T}" /> by his path and clones a objects by its typeID.
        /// </summary>
        /// <param name="path">The path of the prototyper we wanna acess</param>
        /// <param name="typeID">The type of the entity we wanna clone</param>
        /// <returns>The cloned entity from the prototyper</returns>
        public TO Get(string path, T typeID) {

            var node = prototypeHierarchy[path];
            return node.prototype.Get(typeID);
        }


        /// <summary>
        ///     Finds a registered <see cref="IPrototyper{I,T}" /> by his path and clones a objects by its typeID, the last path addition
        ///     gets used to clone the entity.
        /// </summary>
        /// <param name="path">The path of the prototyper we wanna acess, last path is the typeID of the entity we wanna clone.</param>
        /// <returns>The cloned entity from the prototyper</returns>
        public TO Get(string path) {

            var dissembledPath = pathDissembler(path);
            return Get(dissembledPath.Item1, dissembledPath.Item2);
        }


        // Recursive Data-Structure for linking the child prototyper to its parent.
        protected class PrototypeNode {
            
            public IList<PrototypeNode> childs = new List<PrototypeNode>();

            public PrototypeNode parent;

            public string path;
            public IPrototyper<T, TO> prototype;
        }
    }
}