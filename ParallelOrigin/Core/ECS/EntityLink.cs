
#if SERVER
using Arch.Core;
using Arch.Core.Extensions;
using System;
using LiteNetLib.Utils;
using ParallelOriginGameServer.Server.Extensions;

#elif CLIENT
using LiteNetLib.Utils;
using Unity.Collections;
using Unity.Entities;
using Script.Extensions;
#endif

namespace ParallelOrigin.Core.ECS
{
#if SERVER

    /// <summary>
    ///     Represents an reference between entities, great for networking and automatic resolving of those references.
    /// </summary>
    public struct EntityLink : INetSerializable, IEquatable<EntityLink>
    {
        public Entity entity;
        public long uniqueID;

        public EntityLink(Entity entity, long id)
        {
            this.entity = entity;
            uniqueID = id;
        }
        
        public EntityLink(in Entity entity, long id)
        {
            this.entity = entity;
            uniqueID = id;
        }

        public EntityLink(long uniqueId)
        {
            entity = default;
            uniqueID = uniqueId;
        }

        /// <summary>
        ///     Resolves the reference by searching an valid entity from the included uniqueID.
        ///     It resolves only once, once an valid entity was found, it gets attached to <see cref="entity" /> and is returned.
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public Entity Resolve(ref World world)
        {
            if (entity.IsAlive()) return entity;

            entity = world.GetById(uniqueID);
            return entity;
        }

        /// <summary>
        ///     Resolves the reference by searching an valid entity from the included uniqueID.
        ///     It resolves only once, once an valid entity was found, it gets attached to <see cref="entity" /> and is returned.
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public Entity Resolve(World world)
        {
            if (entity.IsAlive()) return entity;

            entity = world.GetById(uniqueID);
            return entity;
        }

        public bool Equals(EntityLink other)
        {
            return entity.Equals(other.entity) && uniqueID == other.uniqueID;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (entity.GetHashCode() * 397) ^ uniqueID.GetHashCode();
            }
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(uniqueID);
        }

        public void Deserialize(NetDataReader reader)
        {
            uniqueID = reader.GetLong();
        }
        
        public static EntityLink NULL => new(-1);
    }

#elif CLIENT
    /// <summary>
    /// Represents an reference between entities, great for networking and automatic resolving of those references. 
    /// </summary>
    public struct EntityLink : INetSerializable{

        public Entity entity;
        public long uniqueID;

        public EntityLink(in Entity entity)  { this.entity = entity; uniqueID = -1; }
        public EntityLink(in long uniqueID) {
            this.entity = Entity.Null;
            this.uniqueID = uniqueID; 
        }
        public EntityLink(in Entity entity, in long uniqueID)  {
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
        public Entity Resolve(ref NativeParallelHashMap<long, Entity> mapping) {

            if (entity != Entity.Null)
                return entity;

            if(!mapping.ContainsKey(uniqueID)) 
                return Entity.Null;
            
            entity = mapping[uniqueID];
            return entity;
        }

        public void Serialize(NetDataWriter writer) { writer.Put(uniqueID); }
        public void Deserialize(NetDataReader reader) { uniqueID = reader.GetLong(); }
    }

#endif
}