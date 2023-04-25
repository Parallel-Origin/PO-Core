
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
        
        /// <summary>
        ///     A null <see cref="EntityLink"/>.
        /// </summary>
        public static EntityLink Null => new(-1);
        
        /// <summary>
        ///     The underlaying <see cref="EntityReference"/> being cached.
        /// </summary>
        public EntityReference Entity;
        
        /// <summary>
        ///     The unique entity id which we resolve to cache the <see cref="Entity"/> later.
        /// </summary>
        public long UniqueId;

        public EntityLink(Entity entity, long id)
        {
            this.Entity = entity.Reference();
            UniqueId = id;
        }

        public EntityLink(long uniqueId)
        {
            Entity = default;
            UniqueId = uniqueId;
        }

        /// <summary>
        ///     Resolves the reference by searching an valid entity from the included uniqueID.
        ///     It resolves only once, once an valid entity was found, it gets attached to <see cref="Entity" /> and is returned.
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public Entity Resolve(ref World world)
        {
            if (Entity.IsAlive()) return Entity;

            Entity = world.GetById(UniqueId).Reference();
            return Entity;
        }

        /// <summary>
        ///     Resolves the reference by searching an valid entity from the included uniqueID.
        ///     It resolves only once, once an valid entity was found, it gets attached to <see cref="Entity" /> and is returned.
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public Entity Resolve(World world)
        {
            if (Entity.IsAlive()) return Entity;

            Entity = world.GetById(UniqueId).Reference();
            return Entity;
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(UniqueId);
        }

        public void Deserialize(NetDataReader reader)
        {
            Entity = EntityReference.Null;
            UniqueId = reader.GetLong();
        }
        
        public bool Equals(EntityLink other)
        {
            return Entity.Equals(other.Entity) && UniqueId == other.UniqueId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Entity.GetHashCode() * 397) ^ UniqueId.GetHashCode();
            }
        }
        
        public static explicit operator Entity(EntityLink entityReference) => entityReference.Entity;
    }

#elif CLIENT
    /// <summary>
    /// Represents an reference between entities, great for networking and automatic resolving of those references. 
    /// </summary>
    public struct EntityLink : INetSerializable{

        public Entity Entity;
        public long UniqueID;

        public EntityLink(in Entity entity)  { this.Entity = entity; UniqueID = -1; }
        public EntityLink(in long uniqueID) {
            this.Entity = Entity.Null;
            this.UniqueID = uniqueID; 
        }
        public EntityLink(in Entity entity, in long uniqueID)  {
            this.Entity = entity; 
            this.UniqueID = uniqueID;
        }

        /// <summary>
        /// Resolves the reference by searching an valid entity from the included uniqueID.
        /// It resolves only once, once an valid entity was found, it gets attached to <see cref="Entity"/> and is returned. 
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public Entity Resolve(ref EntityManager em) {

            if (Entity != Entity.Null)
                return Entity;

            Entity = em.FindById(UniqueID);
            return Entity;
        }
        
        /// <summary>
        /// Resolves the reference by searching an valid entity from the included uniqueID.
        /// It resolves only once, once an valid entity was found, it gets attached to <see cref="Entity"/> and is returned. 
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public Entity Resolve(EntityManager em) {

            if (Entity != Entity.Null)
                return Entity;

            Entity = em.FindById(UniqueID);
            return Entity;
        }
        
                
        /// <summary>
        /// Resolves the reference by searching an valid entity from the included uniqueID.
        /// It resolves only once, once an valid entity was found, it gets attached to <see cref="Entity"/> and is returned. 
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public Entity Resolve(ref NativeParallelHashMap<long, Entity> mapping) {

            if (Entity != Entity.Null)
                return Entity;

            if(!mapping.ContainsKey(UniqueID)) 
                return Entity.Null;
            
            Entity = mapping[UniqueID];
            return Entity;
        }

        public void Serialize(NetDataWriter writer) { writer.Put(UniqueID); }
        public void Deserialize(NetDataReader reader) { UniqueID = reader.GetLong(); }
    }

#endif
}