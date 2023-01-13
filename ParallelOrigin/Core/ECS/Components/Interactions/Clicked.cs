using System;
using System.Collections.Generic;
#if SERVER
using DefaultEcs;

#elif CLIENT
using Unity.Entities;
#endif

namespace ParallelOrigin.Core.ECS.Components.Interactions
{
    /// <summary>
    ///     Marks an entity which was clicked and stores the entities which clicked it.
    /// </summary>
    public struct Clicked : IDisposable
    {
        public ISet<Entity> clickers;

        public void Dispose()
        {
            clickers.Clear();
        }
    }

    /// <summary>
    ///     Once clicked, this entity will spawn a popup.
    /// </summary>
    public struct OnClickedSpawnPopUp
    {
        public string type;
    }

    /// <summary>
    ///     A component for a button which makes the clicker chop down the clicked entity.
    /// </summary>
    public struct OnClickedChop
    {
    }

    /// <summary>
    ///     A component for a button which makes the clicker visit the clicked structure ( teleports )....
    /// </summary>
    public struct OnClickedVisit
    {
    }

    // On clicked, attack the entity which this popup targets. 
    public struct OnClickedAttack
    {
    }
    
    // On clicked, pickup the references item
    public struct OnClickedPickup
    {
    }

    /// <summary>
    ///     A component for an option, once it was pressed the whole popup will be marked for being destroyed
    /// </summary>
    public struct OnClickedDestroyPopup
    {
    }
}