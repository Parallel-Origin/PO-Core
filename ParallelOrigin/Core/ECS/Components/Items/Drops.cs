
using System;
#if SERVER
using System.Collections.Generic;
using Arch.LowLevel;
using ParallelOriginGameServer.Server.Extensions;
#endif

namespace ParallelOrigin.Core.ECS.Components.Items
{
#if SERVER


    /// <summary>
    ///     A interface for an item which can be passed to the <see cref="WeightTable{T}" />
    /// </summary>
    public interface IWeight
    {
        public float Weight { get; }
    }

    /// <summary>
    ///     A drop table which stores multiple drops for an entity.
    /// </summary>
    public class WeightTable<T> where T : IWeight
    {
        private float _totalWeight;
        public T[] Weighteds;

        public WeightTable(params T[] weighteds)
        {
            this.Weighteds = weighteds;
        }

        /// <summary>
        ///     Calculates the total weight of this table.
        /// </summary>
        /// <returns></returns>
        public float TotalWeight()
        {
            var weight = 0f;
            for (var index = 0; index < Weighteds.Length; index++)
            {
                ref var weighted = ref Weighteds[index];
                weight += Math.Max(0, weighted.Weight);
            }

            return weight;
        }

        /// <summary>
        ///     Returns one weighted item based on its weight.
        ///     If theres a item with 0 weight, especially on the beginning, it will always return that one. 
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            // Assign total weight
            if (_totalWeight == 0)
                _totalWeight = TotalWeight();

            var randomVal = RandomExtensions.GetRandom(0, _totalWeight);

            // Weight based spawning of the items
            for (var index = 0; index < Weighteds.Length; index++)
            {
                ref var weighted = ref Weighteds[index];
                if (randomVal <= weighted.Weight) return weighted;
                randomVal -= weighted.Weight;
            }

            return default;
        }
        
        /// <summary>
        ///     Returns a weighted item based on its weight.
        /// </summary>
        /// <returns></returns>
        public void Get(List<T> items)
        {
            // Assign total weight
            if (_totalWeight == 0)
                _totalWeight = TotalWeight();

            var randomVal = RandomExtensions.GetRandom(0, _totalWeight);

            // Weight based spawning of the items
            for (var index = 0; index < Weighteds.Length; index++)
            {
                ref var weighted = ref Weighteds[index];
                if (randomVal <= weighted.Weight) items.Add(weighted);
                randomVal -= weighted.Weight;
            }
        }
    }

    /// <summary>
    ///     Defines a entity with a certain weight, for example for spawning processes.
    /// </summary>
    public struct WeightedEntity : IWeight
    {
        public string Type;
        public float Weight { get; set; }
    }

    /// <summary>
    ///     Defines a item drop upon a kill or a certain other action.
    /// </summary>
    public struct WeightedItem : IWeight
    {
        public uint Amount;
        public string Type;

        public float Weight { get; set; }
    }

    /// <summary>
    ///     Defines what an entity rewards with as loot after being used. 
    /// </summary>
    public struct Loot
    {
        public Handle<WeightTable<WeightedItem>> LootHandle;
    }
    
    /// <summary>
    ///     Defines what an entity drops upon death. 
    /// </summary>
    public struct Drops
    {
        public Handle<WeightTable<WeightedItem>> DropsHandle;
    }
#endif
}