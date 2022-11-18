#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Script.Extensions;
using LiteNetLib.Utils;
#elif SERVER
using DefaultEcs;
using LiteNetLib;
using System.Collections.Generic;
using System.Drawing;
using ConcurrentCollections;
using LiteNetLib.Utils;
using ParallelOriginGameServer.Server.Persistence;
#endif
using System;
using ParallelOrigin.Core.Base.Classes;
using ParallelOrigin.Core.ECS.Components.Interactions;

namespace ParallelOrigin.Core.ECS.Components;

/// <summary>
///     Possible <see cref="Account" /> and <see cref="Character" /> genders
/// </summary>
public enum Gender : sbyte
{
    MALE,
    FEMALE,
    DIVERS
}


#if SERVER

/// <summary>
///     Marks an entity as an command or event which should be processed and destroyed at some point.
/// </summary>
public struct Command
{
}

/// <summary>
///     A component for a <see cref="Entity" /> which acts as a player.
/// </summary>
public struct Character : INetSerializable
{
    public NetPeer peer;

    public string name;
    public string email;
    public string password;

    public Gender gender;
    public Color color;

    public void Serialize(NetDataWriter writer)
    {
        writer.Put(name, name.Length);
        writer.Put(password, password.Length);
        writer.Put(email, email.Length);
    }

    public void Deserialize(NetDataReader reader)
    {
        name = reader.GetString(32);
        email = reader.GetString(32);
        password = reader.GetString(32);
    }
}

/// <summary>
///     A component attachable to a entity which contains variables to get represented as a item.
/// </summary>
public struct Item : INetSerializable
{
    public uint amount;
    public bool stackable;

    public void Serialize(NetDataWriter writer)
    {
        writer.Put(amount);
        writer.Put(stackable);
    }

    public void Deserialize(NetDataReader reader)
    {
        amount = reader.GetUInt();
        stackable = reader.GetBool();
    }
}

/// <summary>
///     Represents an chunk entity in the game.
/// </summary>
public struct Chunk
{
    public Grid grid;
    public DateTime createdOn; // The date and time when it was created
    public DateTime refreshedOn; // The date and time of the last mob spawn 

    public ConcurrentHashSet<Entity> contains; // Required due to fast acess when there many entities inside the chunk
    public NativeList<Entity> loadedBy;
}

/// <summary>
///     Just a Tag Component which clarifies that the Entity attached is a Resource
/// </summary>
public struct Resource : INetSerializable
{
    public void Serialize(NetDataWriter writer)
    {
    }

    public void Deserialize(NetDataReader reader)
    {
    }
}

/// <summary>
///     Represents a Strcuture in the Game
/// </summary>
public struct Structure
{
    public Color color;
    public EntityReference owner;
}

/// <summary>
///     A component that marks an <see cref="Entity" /> as a mob ingame
/// </summary>
public struct Mob
{
}

/// <summary>
///     A component tagging a <see cref="Entity" /> as a popup
/// </summary>
public struct Popup : INetSerializable
{
    // May be null...
    public EntityReference owner;

    // May be null
    public EntityReference target;

    // The option types its gonna have
    public List<string> options;

    public void Serialize(NetDataWriter writer)
    {
        writer.Put(owner);
        writer.Put(target);
    }

    public void Deserialize(NetDataReader reader)
    {
        owner.Deserialize(reader);
        target.Deserialize(reader);
    }
}

/// <summary>
///     A component for a <see cref="Entity" /> which is a option for a <see cref="Popup" />
/// </summary>
public struct Option
{
}

/// <summary>
///     The building recipe, determining what is builded when and where...
///     Additional conditions like required items, level or whatever can be extra fields in this struct.
/// </summary>
public struct BuildingRecipe : INetSerializable
{
    public BuildSpot spot;
    public BuildCondition buildCondition;
    public float duration;

    public BuildingRecipe(BuildSpot spot, BuildCondition buildCondition, float duration)
    {
        this.spot = spot;
        this.buildCondition = buildCondition;
        this.duration = duration;
    }

    public void Serialize(NetDataWriter writer)
    {
        writer.Put((byte)spot);
        writer.Put((byte)buildCondition);
        writer.Put(duration);
    }

    public void Deserialize(NetDataReader reader)
    {
        spot = (BuildSpot)reader.GetByte();
        buildCondition = (BuildCondition)reader.GetByte();
        duration = reader.GetFloat();
    }
}

#elif CLIENT
    /// <summary>
    ///  A component for a <see cref="Entity" /> which acts as a player.
    /// </summary>
    [BurstCompile]
    public struct Character : IComponentData, INetSerializable {
        
        public FixedString32Bytes name;
        public FixedString32Bytes password;
        public FixedString32Bytes email;
        
        public void Serialize(NetDataWriter writer) {

            var nameCached = name.ToStringCached();
            var passwordCached = password.ToStringCached();
            var emailCached = email.ToStringCached();
            
            writer.Put(nameCached, nameCached.Length);
            writer.Put(passwordCached, passwordCached.Length);
            writer.Put(emailCached, emailCached.Length);
        }

        public void Deserialize(NetDataReader reader) {
            name = reader.GetString(32);
            password = reader.GetString(32);
            email = reader.GetString(32);
        }
    }

        /// <summary>
    ///     A component attachable to a entity which contains variables to get represented as a item.
    /// </summary>
    [BurstCompile]
    public struct Item : IComponentData, INetSerializable {

        public uint amount;
        public bool stackable;
        
        public void Serialize(NetDataWriter writer) { 
            writer.Put(amount); 
            writer.Put(stackable); 
        }
        public void Deserialize(NetDataReader reader) { 
            amount = reader.GetUInt();
            stackable = reader.GetBool();
        }
    }
    
        /// <summary>
    ///  Just a Tag Component which clarifies that the Entity attached is a Resource
    /// </summary>
    [BurstCompile]
    public struct Resource : IComponentData, INetSerializable {
    
        public void Serialize(NetDataWriter writer) {  }
        public void Deserialize(NetDataReader reader) {  }
    }
    
        /// <summary>
    /// Represents a Strcuture in the Game
    /// </summary>
    public struct Structure : IComponentData {
        public long ownerID;
    }
    
        /// <summary>
    /// A component that marks an <see cref="Entity"/> as a mob ingame
    /// </summary>
    [BurstCompile]
    public struct Mob : IComponentData{ }
    
        /// <summary>
    ///     A component tagging a <see cref="Entity" /> as a popup
    /// </summary>
    [BurstCompile]
    public struct Popup : IComponentData, INetSerializable {

        // May be null...
        public EntityReference owner;
        
        // May be null
        public EntityReference target;

        public void Serialize(NetDataWriter writer) {
            writer.Put(owner);
            writer.Put(target);
        }

        public void Deserialize(NetDataReader reader) {
            owner.Deserialize(reader);
            target.Deserialize(reader);
        }
    }
    
        /// <summary>
    ///     A component for a <see cref="Entity" /> which is a option for a <see cref="Popup" />
    /// </summary>
    [BurstCompile]
    public struct Option : IComponentData { }
    
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