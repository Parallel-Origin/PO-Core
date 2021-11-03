using System.Collections;
using System.Collections.Generic;
using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;

#if SERVER

#elif CLIENT
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
#endif

namespace ParallelOrigin.Core.ECS {
    
    #if SERVER
    
    /// <summary>
    /// An struct which stores a list of <see cref="EntityReference"/> for networking and persistence purposes
    /// </summary>
    public struct ReferencesBag : IEnumerable<EntityReference>, INetSerializable {
        
        public List<EntityReference> entities;

        public ReferencesBag(int size) : this() { entities = new List<EntityReference>(size); }

        public IEnumerator<EntityReference> GetEnumerator() { return entities.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public void Serialize(NetDataWriter writer) {
            NetworkSerializerExtensions.SerializeList(writer, entities);
        }

        public void Deserialize(NetDataReader reader) {
            NetworkSerializerExtensions.DeserializeList(reader, ref entities);   
        }
    }
    
    #elif CLIENT 
    
    /// <summary>
    /// An struct which stores a list of <see cref="EntityReference"/> for networking and persistence purposes
    /// </summary>
    [BurstCompatible]
    public struct ReferencesBag : INativeDisposable, INativeList<EntityReference>, IEnumerable<EntityReference> {
        
        public UnsafeList<EntityReference> entities;

        public ReferencesBag(int size, Allocator allocator) { this.entities = new UnsafeList<EntityReference>(size, allocator); }
        public ReferencesBag(UnsafeList<EntityReference> entities) { this.entities = entities; }

        public void Add(EntityReference entityReference){ entities.Add(entityReference); }

        public bool Contains(Entity entity) {

            for (var index = 0; index < Length; index++) {

                var existingEntity = this[index].entity;
                if (existingEntity == entity) return true;
            }

            return false;
        }
        
        public bool Contains(ulong uniqueID) {

            for (var index = 0; index < Length; index++) {

                var existingUniqueID = this[index].uniqueID;
                if (existingUniqueID == uniqueID) return true;
            }

            return false;
        }
        
        public EntityReference this[int index] {
            get => entities[index];
            set => entities[index] = value;
        }

        public ref EntityReference ElementAt(int index) { return ref entities.ElementAt(index); }

        public int Length { get => entities.Length; set => entities.Length = value; }
        public int Capacity { get => entities.Capacity; set => entities.Capacity = value; }
        public bool IsEmpty => entities.IsEmpty;
        
        public IEnumerator<EntityReference> GetEnumerator() {

            for (var index = 0; index < entities.Length; index++) {

                var entityRef = entities[index];
                yield return entityRef;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public void Clear() { entities.Clear(); }
        
        public void Dispose() { entities.Dispose(); }
        public JobHandle Dispose(JobHandle inputDeps) { return entities.Dispose(inputDeps); }
    }

    #endif
}