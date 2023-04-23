using System;
using ParallelOrigin.Core.Base.Classes;
using ParallelOrigin.Core.ECS.Components.Items;

#if SERVER


namespace ParallelOrigin.Core.ECS.Components.Environment
{
    /// <summary>
    ///     The noise condition, wether it should be greater or smaller than the desired value.
    /// </summary>
    public enum NoiseCondition
    {
        GREATER,
        SMALLER
    }

    /// <summary>
    ///     Represents a biome.
    /// </summary>
    public struct Biome : IWeight
    {
        public byte BiomeCode;
        public float Weight { get; init; }
    }

    /// <summary>
    ///     A biome which is dominated by european forests mostly
    /// </summary>
    public struct Woodland
    {
        public byte Resolution;
        public BiomeEntity[] SpawnableResources;
        public BiomeEntity[] SpawmableMobs;
    }

    /// <summary>
    ///     A biome which is dominated by plains, grass and rocks mostly. Dry
    /// </summary>
    public struct Grassland
    {
        public byte Resolution;
        public BiomeEntity[] SpawnableResources;
        public BiomeEntity[] SpawmableMobs;
    }

    /// <summary>
    ///     A component which defines sa forest layer.
    ///     Basically a noise 2D grid that should represent possible spawn-points for forests.
    /// </summary>
    public struct ForestLayer
    {
        public NoiseGeocoordinates[,] NoiseData;
    }

    /// <summary>
    ///     A component which defines a rock layer
    ///     Basically a noise 2D grid that should represent possible rock spawn points.
    /// </summary>
    public struct RockLayer
    {
        public NoiseGeocoordinates[,] NoiseData;
    }

    /// <summary>
    ///     An struct which stores a noise value assigned to certain geo-coordinates
    ///     Mostly used for noise layers and by spawners acting on them
    /// </summary>
    public readonly struct NoiseGeocoordinates
    {
        public readonly Vector2d Geocoordinates;
        public readonly float Noise;

        public NoiseGeocoordinates(Vector2d geocoordinates, float noise)
        {
            this.Geocoordinates = geocoordinates;
            this.Noise = noise;
        }
    }

    ///////////////////////////
    /// Biome entities and their attributes
    //////////////////////////

    /// <summary>
    ///     Represents a entity for a biome layer like a tree or other environmental resource with some conditions like weight and noise for being spawned in.
    /// </summary>
    public partial struct BiomeEntity : IWeight
    {
        public string Type;
        public float Weight { get; init; }
    }

    /// <summary>
    ///     Partial biome entity which adds a forestNoise value to it because of the <see cref="ForestLayer" />
    /// </summary>
    public partial struct BiomeEntity
    {
        public NoiseCondition ForestCondition;
        public float ForestNoise;
    }

    /// <summary>
    ///     Partial biome entity which adds a rockNoise value to it because of the <see cref="RockLayer" />
    /// </summary>
    public partial struct BiomeEntity
    {
        public NoiseCondition RockCondition;
        public float RockNoise;
    }

    /// <summary>
    ///     Addition for mob entities which require additional properties like grouping options or day
    /// </summary>
    public partial struct BiomeEntity
    {
        // If the entity is allowed to spawn in packs ( groups ), mostly for mob spawning
        public bool Pack;
        public byte PackSizeMin;
        public byte PackSizeMax;

        // The times when this entity is allowed to spawn, always timespans
        public ValueTuple<TimeSpan, TimeSpan>[] Times;
    }
}

#endif