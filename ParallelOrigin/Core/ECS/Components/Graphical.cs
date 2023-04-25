using LiteNetLib.Utils;
#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
#endif

namespace ParallelOrigin.Core.ECS.Components
{
#if SERVER

    /// <summary>
    ///     A component that stores a ID of a particular mesh
    /// </summary>
    public struct Mesh : INetSerializable
    {
        /// <summary>
        ///     The ID of a mesh from the internal database we wanna reference for this entity
        /// </summary>
        public short Id;

        /// <summary>
        ///     If true, a system takes care of loading the referenced mesh... if false, you are forced to do that yourself.
        /// </summary>
        public bool Instantiate;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Id);
            writer.Put(Instantiate);
        }

        public void Deserialize(NetDataReader reader)
        {
            Id = reader.GetShort();
            Instantiate = reader.GetBool();
        }
    }

    /// <summary>
    ///     Represents a reference to a sprite, icon or logo for a entity.
    /// </summary>
    public struct Sprite : INetSerializable
    {
        /// <summary>
        ///     The ID of a mesh from the internal database we wanna reference for this entity
        /// </summary>
        public short Id;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Id);
        }

        public void Deserialize(NetDataReader reader)
        {
            Id = reader.GetShort();
        }
    }

#elif CLIENT
    /// <summary>
    ///     A component that stores a ID of a particular mesh
    /// </summary>
    public struct Mesh : IComponentData, INetSerializable {
        
        /// <summary>
        ///     The ID of a mesh from the internal database we wanna reference for this entity
        /// </summary>
        public short ID;

        /// <summary>
        ///     If true, a system takes care of loading the referenced mesh... if false, you are forced to do that yourself.
        /// </summary>
        public bool Instantiate;

        public void Serialize(NetDataWriter writer) {
            writer.Put(ID);
            writer.Put(Instantiate);
        }

        public void Deserialize(NetDataReader reader) {
            ID = reader.GetShort();
            Instantiate = reader.GetBool();
        }
    }
    

    /// <summary>
    /// Represents a reference to a sprite, icon or logo for a entity.
    /// </summary>
    [BurstCompile]
    public struct Sprite : IComponentData, INetSerializable {
                
        /// <summary>
        ///  The ID of a mesh from the internal database we wanna reference for this entity
        /// </summary>
        public short ID;
        
        public void Serialize(NetDataWriter writer) { writer.Put(ID); }
        public void Deserialize(NetDataReader reader) { ID = reader.GetShort(); }
    }
        
   /// <summary>
    /// A struct which marks an entity as invisible to deactivate its rendering. 
    /// </summary>
    [BurstCompatible]
    public struct Invisible : IComponentData{}
#endif
}