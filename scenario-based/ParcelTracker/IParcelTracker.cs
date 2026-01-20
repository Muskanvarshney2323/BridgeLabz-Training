namespace ParcelTracker
{
    /// <summary>
    /// Interface defining the contract for the parcel tracker system.
    /// </summary>
    public interface IParcelTracker
    {
        void AddParcel(string parcelId, string destination);
        void UpdateStatus(string parcelId, string newStatus);
        void AddCheckpoint(string parcelId, string checkpoint);
        void DisplayTrackingInfo(string parcelId);
        void DisplayAllParcels();
        void HandleMissingParcel(string parcelId);
    }
}
