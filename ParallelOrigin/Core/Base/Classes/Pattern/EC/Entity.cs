#if CLIENT
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Script.Server.Pattern {
    
    /// <summary>
    /// An entity which stores a list of components.
    /// Acts as a dynamic storage for data.
    /// </summary>
    public struct Entity : IEnumerable, IDisposable {

        public UnsafeList<IntPtr> components;
        
        public Entity(int size) { components = new UnsafeList<IntPtr>(size, Allocator.Persistent); }
        

        /// <summary>
        ///Adds an object to the entity as an component
        /// </summary>
        /// <param name="component">The component to add</param>
        /// <typeparam name="T">The component to add</typeparam>
        public void Add(object component) {

            if (component == null) return;
            if (!Contains(component.GetType())) {

                var handle = GCHandle.Alloc(component, GCHandleType.Pinned);
                var ptr = GCHandle.ToIntPtr(handle);
                components.Add(ptr);
            }
        }
        
        /// <summary>
        /// Checks if an component class is inside the entity
        /// </summary>
        /// <typeparam name="T">The component</typeparam>
        /// <returns></returns>
        public bool Contains<T>(){
            
            for (var index = 0; index < components.Length; index++) {

                var ptr = components[index];
                var handle = (GCHandle)ptr;
                var obj = handle.Target;
                
                if(obj is T) return true;
            }

            return false;
        }
        
        /// <summary>
        /// Checks if an component class is inside the entity
        /// </summary>
        /// <typeparam name="T">The component</typeparam>
        /// <returns></returns>
        public bool Contains(Type type){
            
            for (var index = 0; index < components.Length; index++) {

                var ptr = components[index];
                var handle = (GCHandle)ptr;
                var obj = handle.Target;
                
                if(obj.GetType() == type) return true;
            }

            return false;
        }
        
        /// <summary>
        /// Returns a component by its type
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns></returns>
        public ref T GetComponentData<T>() where T : struct{
            
            var ptr = GetPtr<T>();
            if (ptr == IntPtr.Zero) 
                return ref Unsafe.NullRef<T>();

            var handle = (GCHandle)ptr;
            return ref Unsafe.Unbox<T>(handle.Target);
        }
        
        /// <summary>
        /// Returns a component by its type
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns></returns>
        public object GetComponent<T>() where T : class {
            
            var ptr = GetPtr<T>();
            if (ptr == IntPtr.Zero)
                return default;

            var handle = (GCHandle)ptr;
            return handle.Target;
        }
        
        /// <summary>
        /// Returns a component by its type
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns></returns>
        public object GetComponent(Type type){
            
            var ptr = GetPtr(type);
            if (ptr == IntPtr.Zero)
                return default;

            var handle = (GCHandle)ptr;
            return handle.Target;
        }

        /// <summary>
        /// Returns a pointer to the type. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IntPtr GetPtr<T>(){
            
            for (var index = 0; index < components.Length; index++) {

                var ptr = components[index];
                var handle = (GCHandle)ptr;
                var obj = handle.Target;
                
                if(obj is T) return ptr;
            }

            return IntPtr.Zero;
        }
        
        /// <summary>
        /// Returns a pointer to the type. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IntPtr GetPtr(Type type){
            
            for (var index = 0; index < components.Length; index++) {

                var ptr = components[index];
                var handle = (GCHandle)ptr;
                var obj = handle.Target;
                
                if(obj.GetType() == type) return ptr;
            }

            return IntPtr.Zero;
        }
        
        /// <summary>
        /// Removes a component by its condition from the entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Remove<T>(){

            for (var index = components.Length-1; index >= 0; index--) {
                
                var handle = (GCHandle)components[index];
                var obj = handle.Target;

                if (!(obj is T)) continue;
                
                components.RemoveAt(index);
                handle.Free();
            }
        }
        
        /// <summary>
        /// Removes a component by its condition from the entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Remove(Type type) {
            
            if (type == null) return;
            
            for (var index = components.Length-1; index >= 0; index--) {

                var handle = (GCHandle)components[index];
                var obj = handle.Target;

                if (obj.GetType() != type) continue;
                
                components.RemoveAt(index);
                handle.Free();
            }
        }
        
        public IntPtr this[int i] {
            get => components[i];
            set => components[i] = value;
        }

        /// <summary>
        /// Returns the amount of components inside this entity
        /// </summary>
        public int Count => components.Length;

        /// <summary>
        /// Returns an enumerator used for iteration
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator() {
            
            for (var index = 0; index < components.Length; index++) {
                
                var ptr = components[index];
                var handle = (GCHandle)ptr;
                yield return handle.Target;
            }
        }

        public void Dispose() {
            
            // Free all components
            for (var index = components.Length-1; index >= 0; index--) {
                
                var handle = (GCHandle)components[index];
                handle.Free();
            }
            
            // Free list 
            components.Dispose();
        }
    }
}

#endif