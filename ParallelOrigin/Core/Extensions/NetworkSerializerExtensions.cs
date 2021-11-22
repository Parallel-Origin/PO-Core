using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Base.Classes;

#if CLIENT
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Script.Extensions;
#elif SERVER

#endif

namespace ParallelOrigin.Core.Extensions {
    
    /// <summary>
    /// An extenions which contains primary methods for serializing and deserializing certain data types. 
    /// </summary>
    public static class NetworkSerializerExtensions {


        /// <summary>
        /// Serializes an <see cref="Vector2d"/>
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="vector2d"></param>
        public static void SerializeVector2d(NetDataWriter writer, Vector2d vector2d) {
            writer.Put((float)vector2d.x);
            writer.Put((float)vector2d.y);
        }
        
        /// <summary>
        /// Deserializes an <see cref="Vector2d"/>
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Vector2d DeserializeVector2d(NetDataReader reader) {

            var x = reader.GetFloat();
            var y = reader.GetFloat();

            return new Vector2d { x = x, y = y };
        }

        /// <summary>
        /// Serializes an <see cref="Grid"/>
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="grid"></param>
        public static void SerializeGrid(NetDataWriter writer, Grid grid) {
            writer.Put(grid.x);
            writer.Put(grid.y);
        }
        
        /// <summary>
        /// Deserializes an <see cref="Grid"/>
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="grid"></param>
        public static Grid DeserializeGrid(NetDataReader reader) {

            var x = reader.GetUShort();
            var y = reader.GetUShort();

            return new Grid(x, y);
        }

        /// <summary>
        /// Serializes a string list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="list"></param>
        public static void SerializeArray<T>(NetDataWriter writer, T[] array) where T : struct, INetSerializable{
            
            writer.Put(array.Length);
            for(var index = 0; index < array.Length; index++)
                writer.Put(array[index]);
        }
        
        /// <summary>
        /// Deserializes an list of string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void DeserializeArray<T>(NetDataReader reader, ref T[] array) where T : struct, INetSerializable{
            
            var size = reader.GetInt();
            array = array ?? new T[size]; 
            
            for (var index = 0; index < size; index++) {

                var value = reader.Get<T>();
                array[index] = value;
            }
        }
        
        /// <summary>
        /// Serializes a string list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="list"></param>
        public static void SerializeList<T>(NetDataWriter writer, IList<T> list) where T : struct, INetSerializable{
            
            writer.Put(list.Count);
            for(var index = 0; index < list.Count; index++)
                writer.Put(list[index]);
        }

        /// <summary>
        /// Deserializes an list of string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void DeserializeList<T>(NetDataReader reader, ref List<T> list) where T : struct, INetSerializable{
            
            var size = reader.GetInt();
            list = list ?? new List<T>(size); 
            
            for (var index = 0; index < size; index++) {

                var value = reader.Get<T>();
                list.Add(value);
            }
        }
        
        /// <summary>
        /// Serializes a string list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="list"></param>
        public static void SerializeList(NetDataWriter writer, IList<string> list) {
            
            writer.Put(list.Count);
            for(var index = 0; index < list.Count; index++)
                writer.Put(list[index]);
        }

        /// <summary>
        /// Deserializes an list of string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void DeserializeList(NetDataReader reader, ref List<string> list) {
            
            var size = reader.GetInt();
            list = list ?? new List<string>(size); 
            
            for (var index = 0; index < size; index++) {

                var value = reader.GetString(10);
                list.Add(value);
            }
        }

        /// <summary>
        ///  Serializes and string byte dic
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void SerializeDic(NetDataWriter writer, IDictionary<string,byte> dic) {
            
            // Write overriden anim clips dic
            writer.Put(dic.Count);
            foreach (var kvp in dic) {

                var key = kvp.Key;
                var value = kvp.Value;
                writer.Put(key, key.Length);
                writer.Put(value);
            }
        }

        /// <summary>
        /// Deserializes an dictionary of string and byte
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void DeserializeDic(NetDataReader reader, ref Dictionary<string, byte> dic) {
            
            var size = reader.GetInt();
             dic = dic ?? new Dictionary<string, byte>(size); 
            
            for (var index = 0; index < size; index++) {

                var key = reader.GetString(10);
                var value = reader.GetByte();
                dic.Add(key, value);
            }
        }
        
        /// <summary>
        ///  Serializes and string byte dic
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void SerializeDic(NetDataWriter writer, IDictionary<string,short> dic) {
            
            // Write overriden anim clips dic
            writer.Put(dic.Count);
            foreach (var kvp in dic) {
                
                var key = kvp.Key;
                var value = kvp.Value;
                writer.Put(key, key.Length);
                writer.Put(value);
            }
        }

        /// <summary>
        /// Deserializes an dictionary of string and byte
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void DeserializeDic(NetDataReader reader, ref Dictionary<string, short> dic) {
            
            var size = reader.GetInt();
            dic = dic ?? new Dictionary<string, short>(size);
            
            for (var index = 0; index < size; index++) {

                var key = reader.GetString(10);
                var value = reader.GetByte();
                dic.Add(key, value);
            }
        }
        
        /// <summary>
        ///  Serializes and string byte dic
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void SerializeDic(NetDataWriter writer, IDictionary<string,string> dic) {
            
            // Write overriden anim clips dic
            writer.Put(dic.Count);
            foreach (var kvp in dic) {
                
                var key = kvp.Key;
                var value = kvp.Value;
                writer.Put(key, key.Length);
                writer.Put(value);
            }
        }

        /// <summary>
        /// Deserializes an dictionary of string and byte
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void DeserializeDic(NetDataReader reader, ref Dictionary<string, string> dic) {
            
            var size = reader.GetInt();
            dic = dic ?? new Dictionary<string, string>(size);
            
            for (var index = 0; index < size; index++) {

                var key = reader.GetString(10);
                var value = reader.GetString();
                dic.Add(key, value);
            }
        }
        
        /// <summary>
        /// Serializes and string bool list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void SerializeDic(NetDataWriter writer, IDictionary<string,bool> dic) {
            
            // Write overriden anim clips dic
            writer.Put(dic.Count);
            foreach (var kvp in dic) {
                
                var key = kvp.Key;
                var value = kvp.Value;
                writer.Put(key, key.Length);
                writer.Put(value);
            }
        }

        /// <summary>
        /// Deserializes an dictionary of string and bool
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void DeserializeDic(NetDataReader reader, ref Dictionary<string, bool> dic) {
            
            var size = reader.GetInt();
            dic = dic ?? new Dictionary<string, bool>(size);
            
            for (var index = 0; index < size; index++) {

                var key = reader.GetString(10);
                var value = reader.GetBool();
                dic.Add(key, value);
            }
        }

#if CLIENT
        
        /// <summary>
        /// Serializes a string list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="list"></param>
        public static void SerializeList<T>(NetDataWriter writer, ref UnsafeList<T> list) where T : unmanaged, INetSerializable{
            
            writer.Put(list.length);
            for(var index = 0; index < list.Length; index++)
                writer.Put(list[index]);
        }

        /// <summary>
        /// Deserializes an list of string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void DeserializeList<T>(NetDataReader reader, ref UnsafeList<T> list) where T : unmanaged, INetSerializable{
            
            var size = reader.GetInt();
            if(!list.IsCreated) list =  new UnsafeList<T>(size, Allocator.Persistent); 
            
            for (var index = 0; index < size; index++) {

                var value = reader.Get<T>();
                list.Add(value);
            }
        }
        
        /// <summary>
        /// Serializes and string bool list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void SerializeDic(NetDataWriter writer, UnsafeHashMap<FixedString32, short> dic) {
            
            // Write overriden anim clips dic
            writer.Put(dic.Count());
            foreach (var kvp in dic) {
                
                var key = kvp.Key;
                var value = kvp.Value;
                writer.Put(key.ToStringCached(), key.Length);
                writer.Put(value);
            }
        }

        /// <summary>
        /// Deserializes an dictionary of string and bool
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void DeserializeDic(NetDataReader reader, ref UnsafeHashMap<FixedString32, short> dic) {
            
            var size = reader.GetInt();
            if(!dic.IsCreated) dic = new UnsafeHashMap<FixedString32, short>(size, Allocator.Persistent);
            
            for (var index = 0; index < size; index++) {

                var key = reader.GetString(10);
                var value = reader.GetShort();
                dic.Add(key, value);
            }
        }
        
        /// <summary>
        /// Serializes and string bool list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void SerializeDic(NetDataWriter writer, UnsafeHashMap<FixedString32, FixedString32> dic) {
            
            // Write overriden anim clips dic
            writer.Put(dic.Count());
            foreach (var kvp in dic) {
                
                var key = kvp.Key;
                var value = kvp.Value;
                writer.Put(key.ToStringCached(), key.Length);
                writer.Put(value.ToStringCached());
            }
        }

        /// <summary>
        /// Deserializes an dictionary of string and bool
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void DeserializeDic(NetDataReader reader, ref UnsafeHashMap<FixedString32, FixedString32> dic) {
            
            var size = reader.GetInt();
            if(!dic.IsCreated) dic = new UnsafeHashMap<FixedString32, FixedString32>(size, Allocator.Persistent);
            
            for (var index = 0; index < size; index++) {

                var key = reader.GetString(10);
                var value = reader.GetString(10);
                dic.Add(key, value);
            }
        }
#endif
    }
}