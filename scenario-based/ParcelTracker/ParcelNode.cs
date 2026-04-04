namespace ParcelTracker
{
    /// <summary>
    /// Represents a node in the singly linked list of parcels.
    /// </summary>
    public class ParcelNode
    {
        public Parcel Parcel { get; set; }
        public ParcelNode Next { get; set; }

        public ParcelNode(Parcel parcel)
        {
            Parcel = parcel;
            Next = null;
        }
    }
}
