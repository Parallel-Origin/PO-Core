using ParallelOrigin.Core.Base.Classes;
using ParallelOrigin.Core.ECS.Components.Items;
using ParallelOriginGameServer.Server.Extensions;

namespace ParallelOrigin.Core.ECS.Components.Environment {

    /// <summary>
    /// Represents a entity for a biome layer with some conditions like weight and noise for being spawned in. 
    /// </summary>
    public struct BiomeEntity : IWeight, INoise{

        public BiomeEntity(string type, float weight, float noise) {
            this.type = type;
            this.weight = weight;
            this.noise = noise;
        }

        public string type;
        public float weight;
        public float noise;
        
        public float Weight => weight;
        public float Noise => noise;
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
    public struct Woodland{}
    
    /// <summary>
    /// A biome which is dominated by plains and grass mostly
    /// </summary>
    public struct Grassland{}
    
    /// <summary>
    /// A biome which is dominated by dry plains and grass mostly... dead trees. 
    /// </summary>
    public struct Steppe{}
    
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
    /// A component which defines sa forest layer.
    /// Basically a noise 2D grid that should represent possible spawn-points for forests. 
    /// </summary>
    public struct ForestLayer {
        
        public ushort resolution;
        public NoiseGeocoordinates[,] noiseData;
        public BiomeEntity[] spawnables;
    }

    /// <summary>
    /// A component which defines a rock layer
    /// Basically a noise 2D grid that should represent possible rock spawn points. 
    /// </summary>
    public struct RockLayer {

        public ushort resolution;
        public NoiseGeocoordinates[,] noiseData;
        public BiomeEntity[] spawnables;
    }
}