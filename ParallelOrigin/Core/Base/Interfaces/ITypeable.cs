namespace ParallelOrigin.Core.Base.Interfaces {
    /// <summary>
    ///     This interface provides methods for a entity/class which belongs to a certain type represented by a id.
    /// </summary>
    public interface ITypeable
    {
        /// <summary>
        ///     The Type-ID
        /// </summary>
        short TypeID { get; set; }
    }
}