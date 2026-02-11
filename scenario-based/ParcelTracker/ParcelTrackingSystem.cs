using System;
namespace ParcelTracker
{
    /// <summary>
    /// Manages parcels using a singly linked list data structure.
    /// Handles tracking, updates, and lost parcel management.
    /// </summary>
    public class ParcelTrackingSystem : IParcelTracker
    {
        private ParcelNode head;
        private int totalParcels;

        public ParcelTrackingSystem()
        {
            head = null;
            totalParcels = 0;
        }

        /// <summary>
        /// Adds a new parcel to the tracking system.
        /// </summary>
        public void AddParcel(string parcelId, string destination)
        {
            if (FindParcel(parcelId) != null)
            {
                Console.WriteLine($"❌ Parcel with ID {parcelId} already exists!");
                return;
            }

            Parcel newParcel = new Parcel(parcelId, destination);
            newParcel.AddCheckpoint("Packed", "Warehouse");

            ParcelNode newNode = new ParcelNode(newParcel);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                ParcelNode current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }

            totalParcels++;
            Console.WriteLine($"✓ Parcel {parcelId} added successfully!");
        }

        /// <summary>
        /// Updates the status of a parcel by adding a new checkpoint.
        /// </summary>
        public void UpdateStatus(string parcelId, string newStatus)
        {
            Parcel parcel = FindParcel(parcelId);
            if (parcel == null)
            {
                Console.WriteLine($"❌ Parcel {parcelId} not found!");
                return;
            }

            if (parcel.IsLost)
            {
                Console.WriteLine($"❌ Cannot update status: Parcel {parcelId} is marked as LOST!");
                return;
            }

            string location = GetLocationForStatus(newStatus);
            parcel.AddCheckpoint(newStatus, location);
            Console.WriteLine($"✓ Status updated for Parcel {parcelId}: {newStatus}");
        }

        /// <summary>
        /// Adds a custom checkpoint to a parcel's journey.
        /// </summary>
        public void AddCheckpoint(string parcelId, string checkpoint)
        {
            Parcel parcel = FindParcel(parcelId);
            if (parcel == null)
            {
                Console.WriteLine($"❌ Parcel {parcelId} not found!");
                return;
            }

            if (parcel.IsLost)
            {
                Console.WriteLine($"❌ Cannot add checkpoint: Parcel {parcelId} is marked as LOST!");
                return;
            }

            Console.Write("Enter location for this checkpoint: ");
            string location = Console.ReadLine() ?? "Unknown Location";

            parcel.AddCheckpoint(checkpoint, location);
            Console.WriteLine($"✓ Checkpoint '{checkpoint}' added to Parcel {parcelId}");
        }

        /// <summary>
        /// Displays detailed tracking information for a specific parcel.
        /// </summary>
        public void DisplayTrackingInfo(string parcelId)
        {
            Parcel parcel = FindParcel(parcelId);
            if (parcel == null)
            {
                Console.WriteLine($"❌ Parcel {parcelId} not found!");
                return;
            }

            parcel.DisplayTrackingJourney();
        }

        /// <summary>
        /// Displays all parcels in the system.
        /// </summary>
        public void DisplayAllParcels()
        {
            if (head == null)
            {
                Console.WriteLine("\n📦 No parcels in the system.\n");
                return;
            }

            Console.WriteLine("\n═══════════════════════════════════════════════");
            Console.WriteLine($"Total Parcels: {totalParcels}");
            Console.WriteLine("═══════════════════════════════════════════════");

            ParcelNode current = head;
            int count = 1;

            while (current != null)
            {
                string lostIndicator = current.Parcel.IsLost ? " ❌ [LOST]" : "";
                Console.WriteLine($"{count}. {current.Parcel}{lostIndicator}");
                current = current.Next;
                count++;
            }

            Console.WriteLine("═══════════════════════════════════════════════\n");
        }

        /// <summary>
        /// Marks a parcel as lost/missing (handles null pointer scenario).
        /// </summary>
        public void HandleMissingParcel(string parcelId)
        {
            Parcel parcel = FindParcel(parcelId);
            if (parcel == null)
            {
                Console.WriteLine($"❌ Parcel {parcelId} not found!");
                return;
            }

            parcel.IsLost = true;
            Console.WriteLine($"⚠️  Parcel {parcelId} marked as LOST/MISSING!");
            Console.WriteLine("   Further tracking updates are disabled for this parcel.");
        }

        /// <summary>
        /// Marks a parcel as found (recovers from lost status).
        /// </summary>
        public void RecoverParcel(string parcelId)
        {
            Parcel parcel = FindParcel(parcelId);
            if (parcel == null)
            {
                Console.WriteLine($"❌ Parcel {parcelId} not found!");
                return;
            }

            if (!parcel.IsLost)
            {
                Console.WriteLine($"ℹ️  Parcel {parcelId} is not marked as lost.");
                return;
            }

            parcel.IsLost = false;
            Console.WriteLine($"✓ Parcel {parcelId} recovered and tracking resumed!");
        }

        /// <summary>
        /// Removes a parcel from the tracking system.
        /// </summary>
        public void RemoveParcel(string parcelId)
        {
            if (head == null)
            {
                Console.WriteLine($"❌ Parcel {parcelId} not found!");
                return;
            }

            if (head.Parcel.ParcelId == parcelId)
            {
                head = head.Next;
                totalParcels--;
                Console.WriteLine($"✓ Parcel {parcelId} removed from the system.");
                return;
            }

            ParcelNode current = head;
            while (current.Next != null)
            {
                if (current.Next.Parcel.ParcelId == parcelId)
                {
                    current.Next = current.Next.Next;
                    totalParcels--;
                    Console.WriteLine($"✓ Parcel {parcelId} removed from the system.");
                    return;
                }
                current = current.Next;
            }

            Console.WriteLine($"❌ Parcel {parcelId} not found!");
        }

        /// <summary>
        /// Searches for a parcel by ID (forward traversal through linked list).
        /// </summary>
        private Parcel FindParcel(string parcelId)
        {
            ParcelNode current = head;

            while (current != null)
            {
                if (current.Parcel.ParcelId == parcelId)
                {
                    return current.Parcel;
                }
                current = current.Next;
            }

            return null;
        }

        /// <summary>
        /// Helper method to suggest a location based on status.
        /// </summary>
        private string GetLocationForStatus(string status)
        {
            return status switch
            {
                "Packed" => "Warehouse",
                "Shipped" => "Distribution Center",
                "In Transit" => "Transit Hub",
                "Out for Delivery" => "Delivery Station",
                "Delivered" => "Destination",
                _ => "Transit Location"
            };
        }

        /// <summary>
        /// Displays statistics about the tracking system.
        /// </summary>
        public void DisplayStatistics()
        {
            int lostCount = 0;
            int deliveredCount = 0;
            int inTransitCount = 0;

            ParcelNode current = head;
            while (current != null)
            {
                if (current.Parcel.IsLost)
                    lostCount++;
                else if (current.Parcel.Status == "Delivered")
                    deliveredCount++;
                else if (current.Parcel.Status == "In Transit" || current.Parcel.Status == "Out for Delivery")
                    inTransitCount++;

                current = current.Next;
            }

            Console.WriteLine("\n═══════════════════════════════════════════════");
            Console.WriteLine("Tracking System Statistics");
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine($"Total Parcels: {totalParcels}");
            Console.WriteLine($"Delivered: {deliveredCount}");
            Console.WriteLine($"In Transit: {inTransitCount}");
            Console.WriteLine($"Lost/Missing: {lostCount}");
            Console.WriteLine("═══════════════════════════════════════════════\n");
        }
    }
}
