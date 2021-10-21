using System.Collections.Generic;
using LiteNetLib.Utils;
using ParallelOriginGameServer.Server.Utils;

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
            writer.Put(vector2d.x);
            writer.Put(vector2d.y);
        }
        
        /// <summary>
        /// Deserializes an <see cref="Vector2d"/>
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Vector2d DeserializeVector2d(NetDataReader reader) {

            var x = reader.GetDouble();
            var y = reader.GetDouble();

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
            list ??= new List<string>(size);
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
            foreach (var (key, value) in dic) {
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
            dic ??= new Dictionary<string, byte>(size);
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
            foreach (var (key, value) in dic) {
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
            dic ??= new Dictionary<string, short>(size);
            for (var index = 0; index < size; index++) {

                var key = reader.GetString(10);
                var value = reader.GetByte();
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
            foreach (var (key, value) in dic) {
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
            dic ??= new Dictionary<string, bool>(size);
            for (var index = 0; index < size; index++) {

                var key = reader.GetString(10);
                var value = reader.GetBool();
                dic.Add(key, value);
            }
        }
    }
}