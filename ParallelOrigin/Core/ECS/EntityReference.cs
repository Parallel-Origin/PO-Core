

#if SERVER
using DefaultEcs;
using LiteNetLib.Utils;
using ParallelOriginGameServer.Server.Extensions;
#elif CLIENT
using LiteNetLib.Utils;
using Unity.Collections;
using Unity.Entities;
using Script.Extensions;
#endif

namespace ParallelOrigin.Core.ECS {
    
#if SERVER
    
    /// <summary>
    /// Represents an reference between entities, great for networking and automatic resolving of those references. 
    /// </summary>
    public struct EntityReference : INetSerializable{

        public Entity entity;
        public ulong uniqueID;

        public EntityReference(in Entity entity, in ulong id) : this() {
            this.entity = entity;
            this.uniqueID = id;
        }

        public EntityReference(in ulong uniqueId) : this() { uniqueID = uniqueId; }

        /// <summary>
        /// Resolves the reference by searching an valid entity from the included uniqueID.
        /// It resolves only once, once an valid entity was found, it gets attached to <see cref="entity"/> and is returned. 
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public Entity Resolve(ref World world) {

            if (entity.IsAlive) return entity;
            
            entity = world.GetById(uniqueID);
            return entity;
        }
        
        /// <summary>
        /// Resolves the reference by searching an valid entity from the included uniqueID.
        /// It resolves only once, once an valid entity was found, it gets attached to <see cref="entity"/> and is returned. 
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public Entity Resolve(World world) {

            if (entity.IsAlive) return entity;
            
            entity = world.GetById(uniqueID);
            return entity;
        }
        
        public void Serialize(NetDataWriter writer) { writer.Put(uniqueID); }

        public void Deserialize(NetDataReader reader) { uniqueID = reader.GetULong(); }
    }
    
#elif CLIENT

    /// <summary>
    /// Represents an reference between entities, great for networking and automatic resolving of those references. 
    /// </summary>
    public struct EntityReference : INetSerializable{

        public Entity entity;
        public ulong uniqueID;

        public EntityReference(in ulong uniqueID) : this() { this.uniqueID = uniqueID; }
        public EntityReference(in Entity entity, in ulong uniqueID) : this() {
            this.entity = entity; 
            this.uniqueID = uniqueID;
        }

        /// <summary>
        /// Resolves the reference by searching an valid entity from the included uniqueID.
        /// It resolves only once, once an valid entity was found, it gets attached to <see cref="entity"/> and is returned. 
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public Entity Resolve(ref EntityManager em) {

            if (entity != Entity.Null)
                return entity;

            entity = em.FindById(uniqueID);
            return entity;
        }
        
        /// <summary>
        /// Resolves the reference by searching an valid entity from the included uniqueID.
        /// It resolves only once, once an valid entity was found, it gets attached to <see cref="entity"/> and is returned. 
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public Entity Resolve(EntityManager em) {

            if (entity != Entity.Null)
                return entity;

            entity = em.FindById(uniqueID);
            return entity;
        }
        
                
        /// <summary>
        /// Resolves the reference by searching an valid entity from the included uniqueID.
        /// It resolves only once, once an valid entity was found, it gets attached to <see cref="entity"/> and is returned. 
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public Entity Resolve(ref NativeHashMap<ulong, Entity> mapping) {

            if (entity != Entity.Null)
                return entity;

            if(!mapping.ContainsKey(uniqueID)) 
                return Entity.Null;
            
            entity = mapping[uniqueID];
            return entity;
        }

        public void Serialize(NetDataWriter writer) { writer.Put(uniqueID); }
        public void Deserialize(NetDataReader reader) { uniqueID = reader.GetULong(); }
    }
    
#endif
}