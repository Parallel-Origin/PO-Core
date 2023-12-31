#if CLIENT
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Script.Extensions;
using LiteNetLib.Utils;
#elif SERVER
using Arch.Core;
using LiteNetLib;
using System.Collections.Generic;
using System.Drawing;
using Arch.LowLevel;
using ConcurrentCollections;
using LiteNetLib.Utils;
using ParallelOriginGameServer.Server.Persistence;
using ParallelOriginGameServer.Server.ThirdParty;
#endif
using System;
using System.Runtime.InteropServices;
using ParallelOrigin.Core.Base.Classes;
using ParallelOrigin.Core.ECS.Components.Interactions;


namespace ParallelOrigin.Core.ECS.Components {

    /// <summary>
    ///     Possible <see cref="Account" /> and <see cref="Character" /> genders
    /// </summary>
    public enum Gender : sbyte {

        Male,
        Female,
        Divers

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
    public Handle<NetPeer> Peer;

    public string Name;
    public string Email;
    public string Password;

    public void Serialize(NetDataWriter writer)
    {
        writer.Put(Name, Name.Length);
        writer.Put(Password, Password.Length);
        writer.Put(Email, Email.Length);
    }

    public void Deserialize(NetDataReader reader)
    {
        Name = reader.GetString(32);
        Email = reader.GetString(32);
        Password = reader.GetString(32);
    }
}

/// <summary>
///     A component attachable to a entity which contains variables to get represented as a item.
/// </summary>
public struct Item : INetSerializable
{
    public uint Amount;
    public bool Stackable;

    public void Serialize(NetDataWriter writer)
    {
        writer.Put(Amount);
        writer.Put(Stackable);
    }

    public void Deserialize(NetDataReader reader)
    {
        Amount = reader.GetUInt();
        Stackable = reader.GetBool();
    }
}

/// <summary>
///     Represents an chunk entity in the game.
/// </summary>
public struct Chunk : IDisposable
{
    public Grid Grid;
    public DateTime CreatedOn; // The date and time when it was created
    public DateTime RefreshedOn; // The date and time of the last mob spawn 

    public Handle<HashSet<Entity>> Contains; // Required due to fast acess when there many entities inside the chunk
    public UnsafeList<Entity> LoadedBy;

    public Chunk(Grid grid)
    {
        this.Grid = grid;
        CreatedOn = DateTime.UtcNow;
        RefreshedOn = DateTime.UtcNow;
        Contains = new HashSet<Entity>(126).ToHandle();
        LoadedBy = new UnsafeList<Entity>(4);
    }

    public void Dispose()
    {
        Contains.Remove();
        LoadedBy.Dispose();
    }
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
    public EntityLink Owner;
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
    public EntityLink Owner;

    // May be null
    public EntityLink Target;

    // The option types its gonna have
    public Handle<List<string>> Options;

    public Popup(params string[] options)
    {
        Owner = EntityLink.Null;
        Target = EntityLink.Null;
        this.Options = new List<string>(options).ToHandle();
    }

    public void Serialize(NetDataWriter writer)
    {
        writer.Put(Owner);
        writer.Put(Target);
    }

    public void Deserialize(NetDataReader reader)
    {
        Owner.Deserialize(reader);
        Target.Deserialize(reader);
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
    public BuildSpot Spot;
    public BuildCondition BuildCondition;
    public float Duration;

    public BuildingRecipe(BuildSpot spot, BuildCondition buildCondition, float duration)
    {
        this.Spot = spot;
        this.BuildCondition = buildCondition;
        this.Duration = duration;
    }

    public void Serialize(NetDataWriter writer)
    {
        writer.Put((byte)Spot);
        writer.Put((byte)BuildCondition);
        writer.Put(Duration);
    }

    public void Deserialize(NetDataReader reader)
    {
        Spot = (BuildSpot)reader.GetByte();
        BuildCondition = (BuildCondition)reader.GetByte();
        Duration = reader.GetFloat();
    }
}

#elif CLIENT
    /// <summary>
    ///  A component for a <see cref="Entity" /> which acts as a player.
    /// </summary>
    [BurstCompile]
    public struct Character : IComponentData, INetSerializable {

        public FixedString32Bytes Name;
        public FixedString32Bytes Password;
        public FixedString32Bytes Email;

        public void Serialize(NetDataWriter writer) {

            var nameCached = Name.ToStringCached();
            var passwordCached = Password.ToStringCached();
            var emailCached = Email.ToStringCached();

            writer.Put(nameCached, nameCached.Length);
            writer.Put(passwordCached, passwordCached.Length);
            writer.Put(emailCached, emailCached.Length);
        }

        public void Deserialize(NetDataReader reader) {
            Name = reader.GetString(32);
            Password = reader.GetString(32);
            Email = reader.GetString(32);
        }

    }

    /// <summary>
    ///     A component attachable to a entity which contains variables to get represented as a item.
    /// </summary>
    [BurstCompile]
    public struct Item : IComponentData, INetSerializable {

        public uint Amount;
        public bool Stackable;

        public void Serialize(NetDataWriter writer) {
            writer.Put(Amount);
            writer.Put(Stackable);
        }

        public void Deserialize(NetDataReader reader) {
            Amount = reader.GetUInt();
            Stackable = reader.GetBool();
        }

    }

    /// <summary>
    ///  Just a Tag Component which clarifies that the Entity attached is a Resource
    /// </summary>
    [BurstCompile]
    public struct Resource : IComponentData, INetSerializable {

        public void Serialize(NetDataWriter writer) { }
        public void Deserialize(NetDataReader reader) { }

    }

    /// <summary>
    /// Represents a Strcuture in the Game
    /// </summary>
    public struct Structure : IComponentData {

        public long OwnerID;

    }

    /// <summary>
    /// A component that marks an <see cref="Entity"/> as a mob ingame
    /// </summary>
    [BurstCompile]
    public struct Mob : IComponentData { }

    /// <summary>
    ///     A component tagging a <see cref="Entity" /> as a popup
    /// </summary>
    [BurstCompile]
    public struct Popup : IComponentData, INetSerializable {

        // May be null...
        public EntityLink Owner;

        // May be null
        public EntityLink Target;

        public void Serialize(NetDataWriter writer) {
            writer.Put(Owner);
            writer.Put(Target);
        }

        public void Deserialize(NetDataReader reader) {
            Owner.Deserialize(reader);
            Target.Deserialize(reader);
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
    public struct BuildingRecipe : IComponentData, INetSerializable {

        public BuildSpot Spot;
        public BuildCondition BuildCondition;
        public float Duration;

        public BuildingRecipe(BuildSpot spot, BuildCondition buildCondition, float duration) {
            this.Spot = spot;
            this.BuildCondition = buildCondition;
            this.Duration = duration;
        }

        public void Serialize(NetDataWriter writer) {
            writer.Put((byte)Spot);
            writer.Put((byte)BuildCondition);
            writer.Put(Duration);
        }

        public void Deserialize(NetDataReader reader) {
            Spot = (BuildSpot)reader.GetByte();
            BuildCondition = (BuildCondition)reader.GetByte();
            Duration = reader.GetFloat();
        }

    }
#endif
}