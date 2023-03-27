using System.Collections.Generic;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Base.Classes;
using ParallelOrigin.Core.ECS.Components.Interactions;

#if CLIENT
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Script.Extensions;
#elif SERVER
using System.Runtime.InteropServices;
using Arch.LowLevel;
#endif

namespace ParallelOrigin.Core.Extensions
{
    /// <summary>
    ///     An extenions which contains primary methods for serializing and deserializing certain data types.
    ///     !! Attention !!
    ///     Components do mostly inherit INetSerializable while pure data structs shouldnt inherit that interface, thats because of purity.
    /// </summary>
    public static class NetworkSerializerExtensions
    {
        /// <summary>
        ///     Serializes an <see cref="string" /> and also sends its size, this way we need to allocate less on the receiving client.
        ///     Uses ushort because most string lengths wont be that great at all.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="str"></param>
        /// <param name="size"></param>
        /// <param name="sendStringSize"></param>
        public static void PutFixedString(this NetDataWriter writer, string str, ushort size)
        {
            writer.Put(size);
            writer.Put(str, size);
        }

        /// <summary>
        ///     Deserializes an string which was serialized and sended with its size ( <see cref="PutFixedString" /> )
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static string GetFixedString(this NetDataReader reader)
        {
            var size = reader.GetUShort();
            return reader.GetString(size);
        }

        /// <summary>
        ///     Serializes an <see cref="Vector2d" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="vector2d"></param>
        public static void Put(this NetDataWriter writer, ref Vector2d vector2d)
        {
            writer.Put((float)vector2d.x);
            writer.Put((float)vector2d.y);
        }


        /// <summary>
        ///     Deserializes an <see cref="Vector2d" />
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Vector2d GetVector2d(this NetDataReader reader)
        {
            var x = reader.GetFloat();
            var y = reader.GetFloat();

            return new Vector2d { x = x, y = y };
        }

        /// <summary>
        ///     Serializes a string list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="list"></param>
        public static void PutArray<T>(this NetDataWriter writer, T[] array) where T : struct, INetSerializable
        {
            if (array == null)
            {
                writer.Put(0);
                return;
            }

            writer.Put(array.Length);
            for (var index = 0; index < array.Length; index++)
                writer.Put(array[index]);
        }


        /// <summary>
        ///     Deserializes an list of string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetArray<T>(this NetDataReader reader, ref T[] array) where T : struct, INetSerializable
        {
            var size = reader.GetInt();
            array = array ?? new T[size];

            for (var index = 0; index < size; index++) array[index].Deserialize(reader);
        }
        
#if SERVER
        /// <summary>
        ///     Deserializes an list of string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetList<T>(this NetDataReader reader, ref UnsafeList<T> list) where T : unmanaged, INetSerializable
        {
            var size = reader.GetInt();
            list.EnsureCapacity(size);
            for (var index = 0; index < size; index++) list[index].Deserialize(reader);
        }
#endif
        
        /// <summary>
        ///     Serializes a string list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="list"></param>
        public static void PutList<T>(this NetDataWriter writer, IList<T> list) where T : struct, INetSerializable
        {
            writer.Put(list.Count);
            for (var index = 0; index < list.Count; index++)
                writer.Put(list[index]);
        }

        /// <summary>
        ///     Deserializes an list of string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetList<T>(this NetDataReader reader, ref List<T> list) where T : struct, INetSerializable
        {
            var size = reader.GetInt();
            list = list ?? new List<T>(size);

            for (var index = 0; index < size; index++) list[index].Deserialize(reader);
        }

        /// <summary>
        ///     Serializes a string list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="list"></param>
        public static void PutList(this NetDataWriter writer, IList<string> list)
        {
            writer.Put(list.Count);
            for (var index = 0; index < list.Count; index++)
            {
                var item = list[index];
                writer.PutFixedString(item, (ushort)item.Length);
            }
        }

        /// <summary>
        ///     Deserializes an list of string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetList(this NetDataReader reader, ref List<string> list)
        {
            var size = reader.GetInt();
            list = list ?? new List<string>(size);

            for (var index = 0; index < size; index++)
            {
                var value = reader.GetFixedString();
                list.Add(value);
            }
        }

        /// <summary>
        ///     Serializes and string byte dic
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void PutDic(this NetDataWriter writer, IDictionary<string, byte> dic)
        {
            // Write overriden anim clips dic
            writer.Put(dic.Count);
            foreach (var kvp in dic)
            {
                var key = kvp.Key;
                var value = kvp.Value;
                writer.PutFixedString(key, (ushort)key.Length);
                writer.Put(value);
            }
        }

        /// <summary>
        ///     Deserializes an dictionary of string and byte
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetDic(this NetDataReader reader, ref Dictionary<string, byte> dic)
        {
            var size = reader.GetInt();
            dic = dic ?? new Dictionary<string, byte>(size);

            for (var index = 0; index < size; index++)
            {
                var key = reader.GetFixedString();
                var value = reader.GetByte();
                dic.Add(key, value);
            }
        }

        /// <summary>
        ///     Serializes and string byte dic
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void PutDic(this NetDataWriter writer, IDictionary<string, short> dic)
        {
            // Write overriden anim clips dic
            writer.Put(dic.Count);
            foreach (var kvp in dic)
            {
                var key = kvp.Key;
                var value = kvp.Value;
                writer.PutFixedString(key, (ushort)key.Length);
                writer.Put(value);
            }
        }

        /// <summary>
        ///     Deserializes an dictionary of string and byte
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetDic(this NetDataReader reader, ref Dictionary<string, short> dic)
        {
            var size = reader.GetInt();
            dic = dic ?? new Dictionary<string, short>(size);

            for (var index = 0; index < size; index++)
            {
                var key = reader.GetFixedString();
                var value = reader.GetByte();
                dic.Add(key, value);
            }
        }

        /// <summary>
        ///     Serializes and string byte dic
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void PutDic(this NetDataWriter writer, IDictionary<string, string> dic)
        {
            // Write overriden anim clips dic
            writer.Put(dic.Count);
            foreach (var kvp in dic)
            {
                var key = kvp.Key;
                var value = kvp.Value;
                writer.PutFixedString(key, (ushort)key.Length);
                writer.PutFixedString(value, (ushort)value.Length);
            }
        }

        /// <summary>
        ///     Deserializes an dictionary of string and byte
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetDic(this NetDataReader reader, ref Dictionary<string, string> dic)
        {
            var size = reader.GetInt();
            dic = dic ?? new Dictionary<string, string>(size);

            for (var index = 0; index < size; index++)
            {
                var key = reader.GetFixedString();
                var value = reader.GetFixedString();
                dic.Add(key, value);
            }
        }

        /// <summary>
        ///     Serializes and string bool list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void PutDic(this NetDataWriter writer, IDictionary<string, bool> dic)
        {
            // Write overriden anim clips dic
            writer.Put(dic.Count);
            foreach (var kvp in dic)
            {
                var key = kvp.Key;
                var value = kvp.Value;
                writer.PutFixedString(key, (ushort)key.Length);
                writer.Put(value);
            }
        }

        /// <summary>
        ///     Deserializes an dictionary of string and bool
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetDic(this NetDataReader reader, ref Dictionary<string, bool> dic)
        {
            var size = reader.GetInt();
            dic = dic ?? new Dictionary<string, bool>(size);

            for (var index = 0; index < size; index++)
            {
                var key = reader.GetFixedString();
                var value = reader.GetBool();
                dic.Add(key, value);
            }
        }


#if SERVER


        /// <summary>
        ///     A method which simply serializes a <see cref="Recipe" /> data struct
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="recipe"></param>
        public static void Put(this NetDataWriter writer, ref Ingredient ingredient)
        {
            // Write ingredients
            writer.PutFixedString(ingredient.type, (ushort)ingredient.type.Length);
            writer.Put(ingredient.icon);
            writer.Put(ingredient.localisation);
            writer.Put(ingredient.amount);
            writer.Put(ingredient.consume);
        }


        /// <summary>
        ///     A method which simply serializes a <see cref="Ingredient" /> data struct
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="recipe"></param>
        public static Ingredient GetIngredient(this NetDataReader reader)
        {
            return new Ingredient
            {
                type = reader.GetFixedString(),
                icon = reader.GetByte(),
                localisation = reader.GetByte(),
                amount = reader.GetUInt(),
                consume = reader.GetBool()
            };
        }

        /// <summary>
        ///     Serializes a simple <see cref="Craftable" />
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="craftable"></param>
        public static void Put(this NetDataWriter writer, ref Craftable craftable)
        {
            // Write craftables
            writer.PutFixedString(craftable.type, (ushort)craftable.type.Length);
            writer.Put(craftable.icon);
            writer.Put(craftable.amount);
        }

        /// <summary>
        ///     Deserializes a <see cref="Craftable" />
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Craftable GetCraftable(this NetDataReader reader)
        {
            return new Craftable
            {
                type = reader.GetFixedString(),
                icon = reader.GetByte(),
                amount = reader.GetUInt()
            };
        }

#elif CLIENT
                /// <summary>
        /// A method which simply serializes a <see cref="Recipe"/> data struct
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="recipe"></param>
        public static void Put(this NetDataWriter writer, ref Ingredient ingredient) {

            // Write ingredients
            writer.PutFixedString(ingredient.type.ToStringCached(), (ushort)ingredient.type.Length);
            writer.Put(ingredient.icon);
            writer.Put(ingredient.localisation);
            writer.Put(ingredient.amount);
            writer.Put(ingredient.consume);
        }
        
                
        /// <summary>
        /// A method which simply serializes a <see cref="Ingredient"/> data struct
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="recipe"></param>
        public static Ingredient GetIngredient(this NetDataReader reader) {
            
            return new Ingredient {
                type = reader.GetFixedString(),
                icon = reader.GetByte(),
                localisation = reader.GetByte(),
                amount = reader.GetUInt(),
                consume = reader.GetBool()
            };
        }

        /// <summary>
        /// Serializes a simple <see cref="Craftable"/>
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="craftable"></param>
        public static void Put(this NetDataWriter writer, ref Craftable craftable) {
            
            // Write craftables
            writer.PutFixedString(craftable.type.ToStringCached(), (ushort)craftable.type.Length);
            writer.Put(craftable.icon);
            writer.Put(craftable.amount);
        }

        /// <summary>
        /// Deserializes a <see cref="Craftable"/>
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Craftable GetCraftable(this NetDataReader reader) {

            return new Craftable {
                    type = reader.GetFixedString(),
                    icon = reader.GetByte(),
                    amount = reader.GetUInt()
            };
        }
        
        /// <summary>
        /// Serializes a string list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="list"></param>
        public static void PutList<T>(this NetDataWriter writer, ref UnsafeList<T> list) where T : unmanaged, INetSerializable{
            
            writer.Put(list.Length);
            for(var index = 0; index < list.Length; index++)
                writer.Put(list[index]);
        }

        /// <summary>
        /// Deserializes an list of string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetList<T>(this NetDataReader reader, ref UnsafeList<T> list) where T : unmanaged, INetSerializable{
            
            var size = reader.GetInt();
            if(!list.IsCreated) list = new UnsafeList<T>(size, Allocator.Persistent); 
            
            for (var index = 0; index < size; index++) {

                var value = reader.Get<T>();
                list.Add(value);
            }
        }
        
        /// <summary>
        /// Deserializes an list of string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void PutList(this NetDataWriter writer, ref UnsafeList<FixedString32Bytes> list) {
            
            writer.Put(list.Length);
            for (var index = 0; index < list.Length; index++) {

                var item = list[index];
                writer.PutFixedString(item.ToStringCached(), (ushort)item.Length);
            }
        }
        
        /// <summary>
        /// Deserializes an list of string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetList(this NetDataReader reader, ref UnsafeList<FixedString32Bytes> list) {
            
            var size = reader.GetInt();
            if(!list.IsCreated) list = new UnsafeList<FixedString32Bytes>(size, Allocator.Persistent); 
            
            for (var index = 0; index < size; index++) {

                var value = reader.GetFixedString();
                list.Add(value);
            }
        }
        
        /// <summary>
        /// Serializes and string bool list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void PutDic(this NetDataWriter writer, UnsafeParallelHashMap<FixedString32Bytes, short> dic) {
            
            // Write overriden anim clips dic
            writer.Put(dic.Count());
            foreach (var kvp in dic) {
                
                var key = kvp.Key;
                var value = kvp.Value;
                writer.PutFixedString(key.ToStringCached(), (ushort)key.Length);
                writer.Put(value);
            }
        }

        /// <summary>
        /// Deserializes an dictionary of string and bool
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetDic(this NetDataReader reader, ref UnsafeParallelHashMap<FixedString32Bytes, short> dic) {
            
            var size = reader.GetInt();
            if(!dic.IsCreated) dic = new UnsafeParallelHashMap<FixedString32Bytes, short>(size, Allocator.Persistent);
            
            for (var index = 0; index < size; index++) {

                var key = reader.GetFixedString();
                var value = reader.GetShort();
                dic.Add(key, value);
            }
        }
        
        /// <summary>
        /// Deserializes an dictionary of string and bool
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetDic(this NetDataReader reader, ref UnsafeParallelHashMap<FixedString32Bytes, byte> dic) {
            
            var size = reader.GetInt();
            if(!dic.IsCreated) dic = new UnsafeParallelHashMap<FixedString32Bytes, byte>(size, Allocator.Persistent);
            
            for (var index = 0; index < size; index++) {

                var key = reader.GetFixedString();
                var value = reader.GetByte();
                dic.Add(key, value);
            }
        }
        
        /// <summary>
        /// Deserializes an dictionary of string and bool
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetDic(this NetDataReader reader, ref UnsafeParallelHashMap<FixedString32Bytes, bool> dic) {
            
            var size = reader.GetInt();
            if(!dic.IsCreated) dic = new UnsafeParallelHashMap<FixedString32Bytes, bool>(size, Allocator.Persistent);
            
            for (var index = 0; index < size; index++) {

                var key = reader.GetFixedString();
                var value = reader.GetBool();
                dic.Add(key, value);
            }
        }
        
        /// <summary>
        /// Serializes and string bool list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void PutDic(this NetDataWriter writer, UnsafeParallelHashMap<FixedString32Bytes, FixedString32Bytes> dic) {
            
            // Write overriden anim clips dic
            writer.Put(dic.Count());
            foreach (var kvp in dic) {
                
                var key = kvp.Key;
                var value = kvp.Value;
                writer.PutFixedString(key.ToStringCached(), (ushort)key.Length);
                writer.PutFixedString(value.ToStringCached(), (ushort)value.Length);
            }
        }

        /// <summary>
        /// Deserializes an dictionary of string and bool
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetDic(this NetDataReader reader, ref UnsafeParallelHashMap<FixedString32Bytes, FixedString32Bytes> dic) {
            
            var size = reader.GetInt();
            if(!dic.IsCreated) dic = new UnsafeParallelHashMap<FixedString32Bytes, FixedString32Bytes>(size, Allocator.Persistent);
            
            for (var index = 0; index < size; index++) {

                var key = reader.GetFixedString();
                var value = reader.GetFixedString();
                dic.Add(key, value);
            }
        }
#endif
    }
}