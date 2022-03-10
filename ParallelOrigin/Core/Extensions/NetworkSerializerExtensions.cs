using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Base.Classes;
using ParallelOrigin.Core.ECS.Components.Interactions;

#if CLIENT
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Script.Extensions;
#elif SERVER

#endif

namespace ParallelOrigin.Core.Extensions {
    
    /// <summary>
    /// An extenions which contains primary methods for serializing and deserializing certain data types.
    /// !! Attention !!
    /// Components do mostly inherit INetSerializable while pure data structs shouldnt inherit that interface, thats because of purity.
    /// </summary>
    public static class NetworkSerializerExtensions {

        /// <summary>
        /// Serializes an <see cref="string"/> and also sends its size, this way we need to allocate less on the receiving client.
        /// Uses ushort because most string lengths wont be that great at all. 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="str"></param>
        /// <param name="size"></param>
        /// <param name="sendStringSize"></param>
        public static void PutFixedString(this NetDataWriter writer, string str, ushort size) {
            
            writer.Put(size);
            writer.Put(str, size);
        }

        /// <summary>
        /// Deserializes an string which was serialized and sended with its size ( <see cref="PutFixedString"/> )
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static string GetFixedString(this NetDataReader reader) {
            
            var size = reader.GetUShort();
            return reader.GetString(size);
        }
        
        /// <summary>
        /// Serializes an <see cref="Vector2d"/>
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="vector2d"></param>
        public static void Put(this NetDataWriter writer, ref Vector2d vector2d) {
            writer.Put((float)vector2d.x);
            writer.Put((float)vector2d.y);
        }
        
        /// <summary>
        /// Deserializes an <see cref="Vector2d"/>
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Vector2d GetVector2d(this NetDataReader reader) {

            var x = reader.GetFloat();
            var y = reader.GetFloat();

            return new Vector2d { x = x, y = y };
        }
        

        /// <summary>
        /// A method which simply serializes a <see cref="Recipe"/> data struct
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="recipe"></param>
        public static void Put(this NetDataWriter writer, ref Recipe recipe) {

            // Write ingredients
            writer.Put(recipe.ingredients.Length);
            for (var index = 0; index < recipe.ingredients.Length; index++) {

                ref var ingredient = ref recipe.ingredients[index];
                writer.PutFixedString(ingredient.type, (ushort)ingredient.type.Length);
                writer.Put(ingredient.icon);
                writer.Put(ingredient.amount);
                writer.Put(ingredient.consume);
            }
            
            // Write craftables
            writer.Put(recipe.craftables.Length);
            for (var index = 0; index < recipe.craftables.Length; index++) {

                ref var craftable = ref recipe.craftables[index];
                writer.PutFixedString(craftable.type, (ushort)craftable.type.Length);
                writer.Put(craftable.icon);
                writer.Put(craftable.amount);
            }
            
            writer.Put(recipe.describtion);
        }
        
        /// <summary>
        /// A method which simply serializes a <see cref="Recipe"/> data struct
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="recipe"></param>
        public static Recipe GetRecipe(this NetDataReader reader) {

            // Write ingredients
            var recipe = new Recipe();
            var size = reader.GetInt();
            
            recipe.ingredients = new Ingredient[size];
            for (var index = 0; index < size; index++) {

                var ingredient = new Ingredient {
                    type = reader.GetFixedString(),
                    icon = reader.GetByte(),
                    amount = reader.GetUInt(),
                    consume = reader.GetBool()
                };
                
                recipe.ingredients[index] = ingredient;
            }
            
            // Write craftables
            size = reader.GetInt();
            
            recipe.craftables = new Craftable[size];
            for (var index = 0; index < size; index++) {

                var craftable = new Craftable {
                    type = reader.GetFixedString(),
                    icon = reader.GetByte(),
                    amount = reader.GetUInt()
                };

                recipe.craftables[index] = craftable;
            }

            recipe.describtion = reader.GetByte();
            return recipe;
        }
        
        /// <summary>
        /// Serializes a array full of <see cref="BuildingRecipe"/>
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="recipes"></param>
        public static void Put(this NetDataWriter writer, BuildingRecipe[] recipes) {

            // Write all recipes and then the additional building information
            writer.Put(recipes.Length);
            for (var index = 0; index < recipes.Length; index++) {

                ref var recipe = ref recipes[index];
                writer.Put(ref recipe.recipe);
                writer.Put((byte)recipe.spot);
                writer.Put((byte)recipe.buildCondition);
                writer.Put(recipe.duration);
            }
        }
        
        /// <summary>
        /// Serializes a array full of <see cref="BuildingRecipe"/>
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="recipes"></param>
        public static BuildingRecipe[] GetBuildingRecipes(this NetDataReader reader) {

            // Write all recipes and then the additional building information
            var size = reader.GetInt();
            var recipes = new BuildingRecipe[size];
            
            for (var index = 0; index < size; index++) {

                ref var recipe = ref recipes[index];
                recipe.recipe = reader.GetRecipe();
                recipe.spot = (BuildSpot)reader.GetByte();
                recipe.buildCondition = (BuildCondition)reader.GetByte();
                recipe.duration = reader.GetFloat();
            }

            return recipes;
        }

        /// <summary>
        /// Serializes a string list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="list"></param>
        public static void PutArray<T>(this NetDataWriter writer, T[] array) where T : struct, INetSerializable{

            if (array == null) {
                writer.Put(0);
                return;
            }
            
            writer.Put(array.Length);
            for(var index = 0; index < array.Length; index++)
                writer.Put(array[index]);
        }
        
        /// <summary>
        /// Deserializes an list of string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetArray<T>(this NetDataReader reader, ref T[] array) where T : struct, INetSerializable{
            
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
        public static void PutList<T>(this NetDataWriter writer, IList<T> list) where T : struct, INetSerializable{
            
            writer.Put(list.Count);
            for(var index = 0; index < list.Count; index++)
                writer.Put(list[index]);
        }

        /// <summary>
        /// Deserializes an list of string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetList<T>(this NetDataReader reader, ref List<T> list) where T : struct, INetSerializable{
            
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
        public static void PutList(this NetDataWriter writer, IList<string> list) {
            
            writer.Put(list.Count);
            for (var index = 0; index < list.Count; index++) {
                var item = list[index];
                writer.PutFixedString(item, (ushort)item.Length);
            }
        }

        /// <summary>
        /// Deserializes an list of string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetList(this NetDataReader reader, ref List<string> list) {
            
            var size = reader.GetInt();
            list = list ?? new List<string>(size); 
            
            for (var index = 0; index < size; index++) {

                var value = reader.GetFixedString();
                list.Add(value);
            }
        }

        /// <summary>
        ///  Serializes and string byte dic
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void PutDic(this NetDataWriter writer, IDictionary<string,byte> dic) {
            
            // Write overriden anim clips dic
            writer.Put(dic.Count);
            foreach (var kvp in dic) {

                var key = kvp.Key;
                var value = kvp.Value;
                writer.PutFixedString(key, (ushort)key.Length);
                writer.Put(value);
            }
        }

        /// <summary>
        /// Deserializes an dictionary of string and byte
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetDic(this NetDataReader reader, ref Dictionary<string, byte> dic) {
            
            var size = reader.GetInt();
             dic = dic ?? new Dictionary<string, byte>(size); 
            
            for (var index = 0; index < size; index++) {

                var key = reader.GetFixedString();
                var value = reader.GetByte();
                dic.Add(key, value);
            }
        }
        
        /// <summary>
        ///  Serializes and string byte dic
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void PutDic(this NetDataWriter writer, IDictionary<string,short> dic) {
            
            // Write overriden anim clips dic
            writer.Put(dic.Count);
            foreach (var kvp in dic) {
                
                var key = kvp.Key;
                var value = kvp.Value;
                writer.PutFixedString(key, (ushort)key.Length);
                writer.Put(value);
            }
        }

        /// <summary>
        /// Deserializes an dictionary of string and byte
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetDic(this NetDataReader reader, ref Dictionary<string, short> dic) {
            
            var size = reader.GetInt();
            dic = dic ?? new Dictionary<string, short>(size);
            
            for (var index = 0; index < size; index++) {

                var key = reader.GetFixedString();
                var value = reader.GetByte();
                dic.Add(key, value);
            }
        }
        
        /// <summary>
        ///  Serializes and string byte dic
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void PutDic(this NetDataWriter writer, IDictionary<string,string> dic) {
            
            // Write overriden anim clips dic
            writer.Put(dic.Count);
            foreach (var kvp in dic) {
                
                var key = kvp.Key;
                var value = kvp.Value;
                writer.PutFixedString(key, (ushort)key.Length);
                writer.PutFixedString(value, (ushort)value.Length);
            }
        }

        /// <summary>
        /// Deserializes an dictionary of string and byte
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetDic(this NetDataReader reader, ref Dictionary<string, string> dic) {
            
            var size = reader.GetInt();
            dic = dic ?? new Dictionary<string, string>(size);
            
            for (var index = 0; index < size; index++) {

                var key = reader.GetFixedString();
                var value = reader.GetFixedString();
                dic.Add(key, value);
            }
        }
        
        /// <summary>
        /// Serializes and string bool list
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dic"></param>
        public static void PutDic(this NetDataWriter writer, IDictionary<string,bool> dic) {
            
            // Write overriden anim clips dic
            writer.Put(dic.Count);
            foreach (var kvp in dic) {
                
                var key = kvp.Key;
                var value = kvp.Value;
                writer.PutFixedString(key, (ushort)key.Length);
                writer.Put(value);
            }
        }

        /// <summary>
        /// Deserializes an dictionary of string and bool
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void GetDic(this NetDataReader reader, ref Dictionary<string, bool> dic) {
            
            var size = reader.GetInt();
            dic = dic ?? new Dictionary<string, bool>(size);
            
            for (var index = 0; index < size; index++) {

                var key = reader.GetFixedString();
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
        /// Deserializes an list of string
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dic"></param>
        public static void DeserializeList(NetDataReader reader, ref UnsafeList<FixedString32> list) {
            
            var size = reader.GetInt();
            if(!list.IsCreated) list =  new UnsafeList<FixedString32>(size, Allocator.Persistent); 
            
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
        public static void SerializeDic(NetDataWriter writer, UnsafeHashMap<FixedString32, short> dic) {
            
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
        public static void DeserializeDic(NetDataReader reader, ref UnsafeHashMap<FixedString32, short> dic) {
            
            var size = reader.GetInt();
            if(!dic.IsCreated) dic = new UnsafeHashMap<FixedString32, short>(size, Allocator.Persistent);
            
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
        public static void DeserializeDic(NetDataReader reader, ref UnsafeHashMap<FixedString32, byte> dic) {
            
            var size = reader.GetInt();
            if(!dic.IsCreated) dic = new UnsafeHashMap<FixedString32, byte>(size, Allocator.Persistent);
            
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
        public static void DeserializeDic(NetDataReader reader, ref UnsafeHashMap<FixedString32, bool> dic) {
            
            var size = reader.GetInt();
            if(!dic.IsCreated) dic = new UnsafeHashMap<FixedString32, bool>(size, Allocator.Persistent);
            
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
        public static void SerializeDic(NetDataWriter writer, UnsafeHashMap<FixedString32, FixedString32> dic) {
            
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
        public static void DeserializeDic(NetDataReader reader, ref UnsafeHashMap<FixedString32, FixedString32> dic) {
            
            var size = reader.GetInt();
            if(!dic.IsCreated) dic = new UnsafeHashMap<FixedString32, FixedString32>(size, Allocator.Persistent);
            
            for (var index = 0; index < size; index++) {

                var key = reader.GetFixedString();
                var value = reader.GetFixedString();
                dic.Add(key, value);
            }
        }
#endif
    }
}