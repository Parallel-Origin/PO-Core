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
    }
}