namespace ParallelOrigin.Core.ECS.Components.Interactions {

    /// <summary>
    /// Once clicked, this entity will spawn a popup. 
    /// </summary>
    public struct OnClickedSpawnPopUp {
        public string type;
    }

    /// <summary>
    /// A component for a button which makes the clicker chop down the clicked entity.
    /// </summary>
    public struct OnClickedChop{ }
    
    /// <summary>
    /// A component for a button which makes the clicker visit the clicked structure ( teleports )....
    /// </summary>
    public struct OnClickedVisit{}
    
    /// <summary>
    /// A component for an option, once it was pressed the whole popup will be marked for being destroyed
    /// </summary>
    public struct OnClickedDestroyPopup{}
}