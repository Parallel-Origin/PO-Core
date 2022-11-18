using ParallelOrigin.Core.Base.Interfaces.Observer;

namespace ParallelOrigin.Core.Base.Interfaces.Storage {
    /**
 * Interface for a Component that can be attached to a GameObject and can store/hold Data
 * Usecase: Can be attached to hold Data / Datastructures or Packages for the UI
 */
    public interface IStorageComponent
    {
        IParams GetStorage();
        void SetStorage(IParams storageComponent);
    }
}