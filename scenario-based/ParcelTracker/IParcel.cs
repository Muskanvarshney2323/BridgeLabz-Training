using System;

namespace ParcelTracker
{
    /// <summary>
    /// Interface defining the contract for a parcel in the tracking system.
    /// </summary>
    public interface IParcel
    {
        string ParcelId { get; set; }
        string Destination { get; set; }
        string Status { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
