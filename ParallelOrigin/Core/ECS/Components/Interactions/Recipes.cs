using System;
using ParallelOrigin.Core.ECS.Components.Items;
using ParallelOriginGameServer.Server.Extensions;

namespace ParallelOrigin.Core.ECS.Components.Interactions {


    /// <summary>
    /// Represents an ingredient.
    /// </summary>
    public struct Ingredient {
        
        public string type;    // The item type... 3:1 is wood for example
        public uint amount;
        public bool consume;
    }

    /// <summary>
    /// The craftable result
    /// </summary>
    public struct Craftable {

        public string type;   // The item type... 2:1 is gold for example
        public uint amount;
    }

    /// <summary>
    /// The recipe, containing ingredients and a craftable result. 
    /// </summary>
    public struct Recipe {

        public Ingredient[] ingredients;
        public Craftable[] craftables;
    }

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

    /// <summary>
    /// The building recipe, determining what is builded when and where...
    /// Additional conditions like required items, level or whatever can be extra fields in this struct. 
    /// </summary>
    public struct BuildingRecipe {

        public Recipe recipe;

        public BuildSpot spot;
        public BuildCondition buildCondition;
        public float duration;
    }
    
    /// <summary>
    /// The actual component for a player which defines his building recipes
    /// </summary>
    public struct BuildRecipes {

        public BuildingRecipe[] recipes;
    }
}