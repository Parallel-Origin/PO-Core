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
    public struct OnClickedChop{
    }
}