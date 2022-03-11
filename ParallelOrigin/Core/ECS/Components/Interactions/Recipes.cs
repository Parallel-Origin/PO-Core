using System;
using System.Collections.Generic;
using LiteNetLib.Utils;
using ParallelOrigin.Core.ECS.Components.Items;
using ParallelOrigin.Core.Extensions;

#if CLIENT
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
#endif

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
        
        public Ingredient[] ingredients;
        public Craftable[] craftables;

        public Recipe(Ingredient[] ingredients, Craftable[] craftables) {
            this.ingredients = ingredients;
            this.craftables = craftables;
        }

        public void Serialize(NetDataWriter writer) {
            
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
        public byte icon;             // Its icon
        public byte localisation;
        public uint amount;
        public bool consume;

        public Ingredient(FixedString32 type, byte icon, byte localisation, uint amount, bool consume) {
            this.type = type;
            this.icon = icon;
            this.localisation = localisation;
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
    public struct Recipe : IComponentData, INetSerializable{

        public UnsafeList<Ingredient> ingredients;
        public UnsafeList<Craftable> craftables;
        public byte describtion;

        public Recipe(UnsafeList<Ingredient> ingredients, UnsafeList<Craftable> craftables, byte describtion) {
            this.ingredients = ingredients;
            this.craftables = craftables;
            this.describtion = describtion;
        }

        public void Serialize(NetDataWriter writer) { throw new NotImplementedException(); }

        public void Deserialize(NetDataReader reader) {
            
            describtion = reader.GetByte();

            ingredients = new UnsafeList<Ingredient>(reader.GetInt(), Allocator.Persistent);
            for (var index = 0; index < ingredients.Length; index++) 
                ingredients[index] = reader.GetIngredient();
            
            craftables = new UnsafeList<Craftable>(reader.GetInt(), Allocator.Persistent);
            for (var index = 0; index < craftables.Length; index++) 
                craftables[index] = reader.GetCraftable();
        }
    }

    /// <summary>
    /// The building recipe, determining what is builded when and where...
    /// Additional conditions like required items, level or whatever can be extra fields in this struct. 
    /// </summary>
    public struct BuildingRecipe : IComponentData, INetSerializable{
        
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
    
#endif
}