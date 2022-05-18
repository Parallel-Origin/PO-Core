using ParallelOrigin.Core.Base.Classes;

namespace ParallelOrigin.Core.ECS.Components.Environment {
    
    /// <summary>
    /// A biome
    /// </summary>
    public struct Biome {

        public float weight;
        public byte biomeCode;
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
        public FastNoiseLite noise;

        public NoiseGeocoordinates[,] noiseGeocoordinates;
    }
}