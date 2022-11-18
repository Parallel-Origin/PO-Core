#if SERVER
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
        private float totalWeight;

        public T[] weighteds;

        public WeightTable(params T[] weighteds)
        {
            this.weighteds = weighteds;
        }

        /// <summary>
        ///     Calculates the total weight of this table.
        /// </summary>
        /// <returns></returns>
        public float TotalWeight()
        {
            var weight = 0f;
            for (var index = 0; index < weighteds.Length; index++)
            {
                ref var weighted = ref weighteds[index];
                weight += weighted.Weight;
            }

            return weight;
        }

        /// <summary>
        ///     Returns a weighted item based on its weight.
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            // Assign total weight
            if (totalWeight == 0)
                totalWeight = TotalWeight();

            var randomVal = RandomExtensions.GetRandom(0, totalWeight);

            // Weight based spawning of the items
            for (var index = 0; index < weighteds.Length; index++)
            {
                ref var weighted = ref weighteds[index];
                if (randomVal < weighted.Weight) return weighted;
                randomVal -= weighted.Weight;
            }

            return default;
        }
    }

    /// <summary>
    ///     Defines a entity with a certain weight, for example for spawning processes.
    /// </summary>
    public struct WeightedEntity : IWeight
    {
        public string type;
        public float Weight { get; set; }
    }

    /// <summary>
    ///     Defines a item drop upon a kill or a certain other action.
    /// </summary>
    public struct WeightedItem : IWeight
    {
        public uint amount;
        public string type;

        public float Weight { get; set; }
    }

    /// <summary>
    ///     Marks an entity as choppable and defines which items it will drop once being harvested.
    /// </summary>
    public struct Chopable
    {
        public WeightTable<WeightedItem> drops;
    }
#endif
}