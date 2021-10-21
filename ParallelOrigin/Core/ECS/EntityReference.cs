

using ParallelOriginGameServer.Server.Extensions;
#if SERVER
using DefaultEcs;
using LiteNetLib.Utils;

#elif CLIENT
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
        public long uniqueID;

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
        
        public void Serialize(NetDataWriter writer) { writer.Put(uniqueID); }

        public void Deserialize(NetDataReader reader) { uniqueID = reader.GetLong(); }
    }
    
#elif CLIENT

    /// <summary>
    /// Represents an reference between entities, great for networking and automatic resolving of those references. 
    /// </summary>
    public struct EntityReference {

        public Entity entity;
        public long uniqueID;

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
        public Entity Resolve(ref NativeHashMap<long, Entity> mapping) {

            if (entity != Entity.Null)
                return entity;

            if(!mapping.ContainsKey(uniqueID)) 
                return Entity.Null;
            
            entity = mapping[uniqueID];
            return entity;
        }
    }
    
#endif
}