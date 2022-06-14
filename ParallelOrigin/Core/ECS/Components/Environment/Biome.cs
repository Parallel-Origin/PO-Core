using ParallelOrigin.Core.Base.Classes;
using ParallelOrigin.Core.ECS.Components.Items;
using ParallelOriginGameServer.Server.Extensions;

namespace ParallelOrigin.Core.ECS.Components.Environment {
    
    /// <summary>
    /// The noise condition, wether it should be greater or smaller than the desired value. 
    /// </summary>
    public enum NoiseCondition {
        GREATER, SMALLER
    }
    
    /// <summary>
    /// Represents a entity for a biome layer with some conditions like weight and noise for being spawned in. 
    /// </summary>
    public partial struct BiomeEntity : IWeight{

        public string type;
        public float weight;

        public float Weight => weight;
    }
    
    /// <summary>
    /// A biome
    /// </summary>
    public struct Biome : IWeight{

        public byte biomeCode;
        public float weight;
        
        public float Weight { get => weight; set => weight = value; }
    }

    /// <summary>
    /// A biome which is dominated by european forests mostly
    /// </summary>
    public struct Woodland{ public BiomeEntity[] spawnables; }
    
    /// <summary>
    /// A biome which is dominated by plains and grass mostly
    /// </summary>
    public struct Grassland{ public BiomeEntity[] spawnables; }
    
    
    /// <summary>
    /// An struct which stores a noise value assigned to certain geo-coordinates
    /// Mostly used for noise layers and by spawners acting on them 
    /// </summary>
    public readonly struct NoiseGeocoordinates {

        public readonly Vector2d geocoordinates;
        public readonly float noise;

        public NoiseGeocoordinates(Vector2d geocoordinates, float noise) {
            this.geocoordinates = geocoordinates;
            this.noise = noise;
        }
    }
    
    /// <summary>
    /// Partial biome entity which adds a forestNoise value to it because of the <see cref="ForestLayer"/>
    /// </summary>
    public partial struct BiomeEntity {
        public NoiseCondition forestCondition;
        public float forestNoise;
    }
    
    /// <summary>
    /// A component which defines sa forest layer.
    /// Basically a noise 2D grid that should represent possible spawn-points for forests. 
    /// </summary>
    public struct ForestLayer {
        
        public ushort resolution;
        public NoiseGeocoordinates[,] noiseData;
    }

    /// <summary>
    /// Partial biome entity which adds a rockNoise value to it because of the <see cref="RockLayer"/>
    /// </summary>
    public partial struct BiomeEntity {
        public NoiseCondition rockCondition;
        public float rockNoise;
    }

    
    /// <summary>
    /// A component which defines a rock layer
    /// Basically a noise 2D grid that should represent possible rock spawn points. 
    /// </summary>
    public struct RockLayer {

        public ushort resolution;
        public NoiseGeocoordinates[,] noiseData;
    }
}