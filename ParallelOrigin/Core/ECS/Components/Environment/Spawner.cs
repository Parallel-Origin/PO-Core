using System;
using System.Globalization;
using ParallelOriginGameServer.Server.Utils;

namespace ParallelOrigin.Core.ECS.Components.Environment {

    /// <summary>
    /// An basic enum for the time
    /// </summary>
    public enum TimeUnit : byte{
        MILLISECONDS, SECONDS, MINUTES, HOURS
    }
    
    /// <summary>
    /// Marks an entity as spawnable
    /// </summary>
    public struct Spawnable {

        // The weight for this entity to spawn 
        public ushort weight;
        public float noiseThreshold;
    }

    /// <summary>
    /// Marks an spawner entity to spawn some stuff 
    /// </summary>
    public struct Spawn{}
    
    /// <summary>
    /// Tags an spawner entitity in a certain intervall with 
    /// </summary>
    public struct IntervallSpawner {
        
        public ushort intervall;
        public TimeUnit unit;
        
        public DateTime refreshedOn;
    }

    /// <summary>
    /// An struct which stores a noise value assigned to certain geo-coordinates for each lookup acess. 
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
    /// A component which acts as an spawner to spawn in other entities and the environment based on a regular intervall. 
    /// </summary>
    public struct ForestLayer {
        
        public ushort resolution;
        public FastNoiseLite noise;

        public NoiseGeocoordinates[,] noiseGeocoordinates;
    }

    public struct ForestSpawner { }
}