using LiteNetLib.Utils;
using ParallelOrigin.Core.Extensions;
#if CLIENT
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
#endif

namespace ParallelOrigin.Core.ECS.Components.Interactions
{
    /// <summary>
    ///     Possible build spots
    /// </summary>
    public enum BuildSpot : byte
    {
        Spot,
        Tile
    }

    /// <summary>
    ///     Possible build conditions before we can place it on the build spot.
    /// </summary>
    public enum BuildCondition : byte
    {
        None,
        FreeSpace
    }


#if SERVER

    /// <summary>
    ///     Represents an ingredient.
    /// </summary>
    public struct Ingredient
    {
        public string Type; // The item type... 3:1 is wood for example
        public byte Icon; // Its icon
        public byte Localisation;
        public uint Amount;
        public bool Consume;

        public Ingredient(string type, byte icon, byte localisation, uint amount, bool consume)
        {
            this.Type = type;
            this.Icon = icon;
            this.Localisation = localisation;
            this.Amount = amount;
            this.Consume = consume;
        }
    }

    /// <summary>
    ///     The craftable result
    /// </summary>
    public struct Craftable
    {
        public string Type; // The item type... 2:1 is gold for example
        public byte Icon;
        public uint Amount;

        public Craftable(string type, byte icon, uint amount)
        {
            this.Type = type;
            this.Icon = icon;
            this.Amount = amount;
        }
    }

    /// <summary>
    ///     The recipe, containing ingredients and a craftable result.
    /// </summary>
    public struct Recipe : INetSerializable
    {
        public Ingredient[] Ingredients;
        public Craftable[] Craftables;

        public Recipe(Ingredient[] ingredients, Craftable[] craftables)
        {
            this.Ingredients = ingredients;
            this.Craftables = craftables;
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Ingredients.Length);
            for (var index = 0; index < Ingredients.Length; index++)
            {
                ref var ingredient = ref Ingredients[index];
                writer.Put(ref ingredient);
            }

            writer.Put(Craftables.Length);
            for (var index = 0; index < Craftables.Length; index++)
            {
                ref var craftable = ref Craftables[index];
                writer.Put(ref craftable);
            }
        }

        public void Deserialize(NetDataReader reader)
        {
            Ingredients = new Ingredient[reader.GetInt()];
            for (var index = 0; index < Ingredients.Length; index++)
                Ingredients[index] = reader.GetIngredient();

            Craftables = new Craftable[reader.GetInt()];
            for (var index = 0; index < Craftables.Length; index++)
                Craftables[index] = reader.GetCraftable();
        }
    }

    /// <summary>
    ///     The actual component for a player which defines his building recipes
    /// </summary>
    public struct BuildRecipes : INetSerializable
    {
        public string[] Recipes;

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Recipes.Length);
            for (var index = 0; index < Recipes.Length; ++index)
            {
                var item = Recipes[index];
                writer.PutFixedString(item, (ushort)item.Length);
            }
        }

        public void Deserialize(NetDataReader reader)
        {
            Recipes = new string[reader.GetInt()];
            for (var index = 0; index < Recipes.Length; ++index)
                Recipes[index] = reader.GetFixedString();
        }
    }

#elif CLIENT
    /// <summary>
    /// Represents an ingredient.
    /// </summary>
    public struct Ingredient {
        
        public FixedString32Bytes Type;    // The item type... 3:1 is wood for example
        public byte Icon;             // Its icon
        public byte Localisation;
        public uint Amount;
        public bool Consume;

        public Ingredient(FixedString32Bytes type, byte icon, byte localisation, uint amount, bool consume) {
            this.Type = type;
            this.Icon = icon;
            this.Localisation = localisation;
            this.Amount = amount;
            this.Consume = consume;
        }
    }

    /// <summary>
    /// The craftable result
    /// </summary>
    public struct Craftable {

        public FixedString32Bytes Type;   // The item type... 2:1 is gold for example
        public byte Icon;
        public uint Amount;

        public Craftable(FixedString32Bytes type, byte icon, uint amount) {
            this.Type = type;
            this.Icon = icon;
            this.Amount = amount;
        }
    }

    /// <summary>
    /// The recipe, containing ingredients and a craftable result. 
    /// </summary>
    public struct Recipe : IComponentData, INetSerializable{

        public UnsafeList<Ingredient> Ingredients;
        public UnsafeList<Craftable> Craftables;

        public Recipe(UnsafeList<Ingredient> ingredients, UnsafeList<Craftable> craftables) {
            this.Ingredients = ingredients;
            this.Craftables = craftables;
        }

        public void Serialize(NetDataWriter writer) {  }

        public void Deserialize(NetDataReader reader) {

            var size = reader.GetInt();
            Ingredients = new UnsafeList<Ingredient>(size, Allocator.Persistent);
            for (var index = 0; index < size; index++) 
                Ingredients.Add(reader.GetIngredient());

            size = reader.GetInt();
            Craftables = new UnsafeList<Craftable>(size, Allocator.Persistent);
            for (var index = 0; index < size; index++) 
                Craftables.Add(reader.GetCraftable());
        }
    }
    
    /// <summary>
    /// The actual component for a player which defines his building recipes
    /// </summary>
    public struct BuildRecipes : IComponentData, INetSerializable{

        public UnsafeList<FixedString32Bytes> Recipes;
        
        public void Serialize(NetDataWriter writer) { writer.PutList(ref Recipes); }
        public void Deserialize(NetDataReader reader) { reader.GetList(ref Recipes); }
    }

#endif
}