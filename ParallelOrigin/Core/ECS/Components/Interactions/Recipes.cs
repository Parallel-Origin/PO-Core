using System;
using LiteNetLib.Utils;
using ParallelOrigin.Core.ECS.Components.Items;
using ParallelOrigin.Core.Extensions;
using ParallelOriginGameServer.Server.Extensions;

namespace ParallelOrigin.Core.ECS.Components.Interactions {
    
    
    /// <summary>
    /// Possible build spots
    /// </summary>
    public enum BuildSpot : byte{
        TILE
    }

    /// <summary>
    /// Possible build conditions before we can place it on the build spot. 
    /// </summary>
    public enum BuildCondition : byte{
        FREE_SPACE
    }
    
    
#if SERVER

    /// <summary>
    /// Represents an ingredient.
    /// </summary>
    public struct Ingredient {
        
        public string type;    // The item type... 3:1 is wood for example
        public uint amount;
        public bool consume;

        public Ingredient(string type, uint amount, bool consume) {
            this.type = type;
            this.amount = amount;
            this.consume = consume;
        }
    }

    /// <summary>
    /// The craftable result
    /// </summary>
    public struct Craftable {

        public string type;   // The item type... 2:1 is gold for example
        public uint amount;

        public Craftable(string type, uint amount) {
            this.type = type;
            this.amount = amount;
        }
    }

    /// <summary>
    /// The recipe, containing ingredients and a craftable result. 
    /// </summary>
    public struct Recipe {

        public Ingredient[] ingredients;
        public Craftable[] craftables;

        public Recipe(Ingredient[] ingredients, Craftable[] craftables) {
            this.ingredients = ingredients;
            this.craftables = craftables;
        }
    }

    /// <summary>
    /// The building recipe, determining what is builded when and where...
    /// Additional conditions like required items, level or whatever can be extra fields in this struct. 
    /// </summary>
    public struct BuildingRecipe {

        public Recipe recipe;

        public BuildSpot spot;
        public BuildCondition buildCondition;
        public float duration;

        public BuildingRecipe(Recipe recipe, BuildSpot spot, BuildCondition buildCondition, float duration) {
            this.recipe = recipe;
            this.spot = spot;
            this.buildCondition = buildCondition;
            this.duration = duration;
        }
    }
    
    /// <summary>
    /// The actual component for a player which defines his building recipes
    /// </summary>
    public struct BuildRecipes : INetSerializable{

        public BuildingRecipe[] recipes;
        
        public void Serialize(NetDataWriter writer) { writer.Put(recipes); }
        public void Deserialize(NetDataReader reader) { recipes = reader.GetBuildingRecipes(); }
    }
    
#elif CLIENT

#endif
}