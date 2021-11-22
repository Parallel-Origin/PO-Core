using System;
using ParallelOriginGameServer.Server.Extensions;

namespace ParallelOrigin.Core.ECS.Components.Items {

    /// <summary>
    /// Defines a item drop upon a kill or a certain other action.
    /// </summary>
    public struct WeightedItem {
        public uint amount;
        public float weight;
        public string type;
    }
    
    /// <summary>
    /// A drop table which stores multiple drops for an entity. 
    /// </summary>
    public class WeightTable {
        
        public WeightedItem[] weighteds;
        private float totalWeight;

        public WeightTable(params WeightedItem[] weighteds)  { this.weighteds = weighteds; }

        /// <summary>
        /// Calculates the total weight of this table.
        /// </summary>
        /// <returns></returns>
        public float TotalWeight() {
            
            var weight = 0f;
            for (var index = 0; index < weighteds.Length; index++) {

                ref var weighted = ref weighteds[index];
                weight += weighted.weight;

            }

            return weight;
        }
        
        /// <summary>
        /// Returns a weighted item based on its weight.
        /// </summary>
        /// <returns></returns>
        public WeightedItem Get() {

            // Assign total weight
            if (totalWeight == 0)
                totalWeight = TotalWeight();

            var randomVal = RandomExtensions.GetRandom(0, totalWeight);

            // Weight based spawning of the items
            for(var index = 0; index < weighteds.Length; index++) {

                ref var weighted = ref weighteds[index];
                if (weighted.weight <= randomVal) return weighted;
                randomVal -= weighted.weight;
            }
            
            return new WeightedItem{amount = 0, type = string.Empty, weight = -1};
        }
    }

    /// <summary>
    /// Marks an entity as choppable and defines which items it will drop once being harvested. 
    /// </summary>
    public struct Chopable {
        public WeightTable drops;
    }
}