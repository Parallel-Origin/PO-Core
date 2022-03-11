using System;
using System.Collections.Generic;
using LiteNetLib.Utils;
using ParallelOrigin.Core.ECS.Components.Items;
using ParallelOrigin.Core.Extensions;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;

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

    using ParallelOriginGameServer.Server.Extensions;

    /// <summary>
    /// Represents an ingredient.
    /// </summary>
    public struct Ingredient {
        
        public string type;    // The item type... 3:1 is wood for example
        public byte icon;      // Its icon
        public uint amount;
        public bool consume;

        public Ingredient(string type, byte icon, uint amount, bool consume) {
            this.type = type;
            this.icon = icon;
            this.amount = amount;
            this.consume = consume;
        }
    }

    /// <summary>
    /// The craftable result
    /// </summary>
    public struct Craftable {

        public string type;   // The item type... 2:1 is gold for example
        public byte icon;
        public uint amount;

        public Craftable(string type, byte icon, uint amount) {
            this.type = type;
            this.icon = icon;
            this.amount = amount;
        }
    }

    /// <summary>
    /// The recipe, containing ingredients and a craftable result. 
    /// </summary>
    public struct Recipe : INetSerializable{
        
        public byte describtion;
        
        public Ingredient[] ingredients;
        public Craftable[] craftables;

        public Recipe(byte describtion, Ingredient[] ingredients, Craftable[] craftables) {
            this.describtion = describtion;
            this.ingredients = ingredients;
            this.craftables = craftables;
        }

        public void Serialize(NetDataWriter writer) {
            
            writer.Put(describtion);
            
            writer.Put(ingredients.Length);
            for (var index = 0; index < ingredients.Length; index++) {

                ref var ingredient = ref ingredients[index];
                writer.Put(ref ingredient);
            }
            
            writer.Put(craftables.Length);
            for (var index = 0; index < craftables.Length; index++) {

                ref var craftable = ref craftables[index];
                writer.Put(ref craftable);
            }
        }

        public void Deserialize(NetDataReader reader) {
            
            describtion = reader.GetByte();
            
            ingredients = new Ingredient[reader.GetInt()];
            for (var index = 0; index < ingredients.Length; index++) 
                ingredients[index] = reader.GetIngredient();
            
            craftables = new Craftable[reader.GetInt()];
            for (var index = 0; index < craftables.Length; index++) 
                craftables[index] = reader.GetCraftable();
        }
    }

    /// <summary>
    /// The building recipe, determining what is builded when and where...
    /// Additional conditions like required items, level or whatever can be extra fields in this struct. 
    /// </summary>
    public struct BuildingRecipe : INetSerializable{
        
        public BuildSpot spot;
        public BuildCondition buildCondition;
        public float duration;

        public BuildingRecipe(BuildSpot spot, BuildCondition buildCondition, float duration) {
            this.spot = spot;
            this.buildCondition = buildCondition;
            this.duration = duration;
        }

        public void Serialize(NetDataWriter writer) {
            writer.Put((byte)spot);
            writer.Put((byte)buildCondition);
            writer.Put(duration);
        }

        public void Deserialize(NetDataReader reader) {
            spot = (BuildSpot)reader.GetByte();
            buildCondition = (BuildCondition)reader.GetByte();
            duration = reader.GetFloat();
        }
    }
    
    /// <summary>
    /// The actual component for a player which defines his building recipes
    /// </summary>
    public struct BuildRecipes : INetSerializable{

        public string[] recipes;
        
        public void Serialize(NetDataWriter writer) { writer.PutArray(recipes); }
        public void Deserialize(NetDataReader reader) { recipes = reader.GetStringArray(); }
    }
    
#elif CLIENT

        /// <summary>
    /// Represents an ingredient.
    /// </summary>
    public struct Ingredient {
        
        public FixedString32 type;    // The item type... 3:1 is wood for example
        public byte icon;      // Its icon
        public uint amount;
        public bool consume;

        public Ingredient(FixedString32 type, byte icon, uint amount, bool consume) {
            this.type = type;
            this.icon = icon;
            this.amount = amount;
            this.consume = consume;
        }
    }

    /// <summary>
    /// The craftable result
    /// </summary>
    public struct Craftable {

        public FixedString32 type;   // The item type... 2:1 is gold for example
        public byte icon;
        public uint amount;

        public Craftable(FixedString32 type, byte icon, uint amount) {
            this.type = type;
            this.icon = icon;
            this.amount = amount;
        }
    }

    /// <summary>
    /// The recipe, containing ingredients and a craftable result. 
    /// </summary>
    public struct Recipe {

        public UnsafeList<Ingredient> ingredients;
        public UnsafeList<Craftable> craftables;
        public byte describtion;

        public Recipe(UnsafeList<Ingredient> ingredients, UnsafeList<Craftable> craftables, byte describtion) {
            this.ingredients = ingredients;
            this.craftables = craftables;
            this.describtion = describtion;
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
    public struct BuildRecipes : IComponentData {
        
        /*
        public UnsafeList<BuildingRecipe> recipes;

        public void Serialize(NetDataWriter writer) { writer.Put(ref recipes); }
        public void Deserialize(NetDataReader reader) { recipes = reader.GetBuildingRecipes(); }*/
    }
    
    // This describes the number of buffer elements that should be reserved
// in chunk data for each instance of a buffer. In this case, 8 integers
// will be reserved (32 bytes) along with the size of the buffer header
// (currently 16 bytes on 64-bit targets)
    [InternalBufferCapacity(8)]
    public struct MyBufferElement : IBufferElementData {
        
        // Actual value each buffer element will store.
        public BuildingRecipe recipe;
        //public NativeList<NativeList<int>> test;
        public DynamicBuffer<BuildingRecipe> test;

    }
    
#endif
}