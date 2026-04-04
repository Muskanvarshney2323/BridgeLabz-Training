using System;

namespace ParcelTracker
{
    /// <summary>
    /// Represents a parcel in the tracking system.
    /// </summary>
    public class Parcel : IParcel
    {
        public string ParcelId { get; set; }
        public string Destination { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public CheckpointNode CheckpointHead { get; private set; }
        public bool IsLost { get; set; }

        public Parcel(string parcelId, string destination)
        {
            ParcelId = parcelId;
            Destination = destination;
            Status = "Packed";
            CreatedAt = DateTime.Now;
            CheckpointHead = null;
            IsLost = false;
        }

        /// <summary>
        /// Adds a checkpoint to the parcel's journey (maintaining singly linked list).
        /// </summary>
        public void AddCheckpoint(string checkpointName, string location)
        {
            CheckpointNode newCheckpoint = new CheckpointNode(checkpointName, location);
            
            if (CheckpointHead == null)
            {
                CheckpointHead = newCheckpoint;
            }
            else
            {
                CheckpointNode current = CheckpointHead;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newCheckpoint;
            }

            Status = checkpointName;
        }

        /// <summary>
        /// Displays the complete tracking journey of the parcel.
        /// </summary>
        public void DisplayTrackingJourney()
        {
            if (IsLost)
            {
                Console.WriteLine($"\n❌ WARNING: Parcel {ParcelId} is LOST/MISSING!\n");
                return;
            }

            Console.WriteLine($"\n═══════════════════════════════════════════════");
            Console.WriteLine($"Parcel ID: {ParcelId}");
            Console.WriteLine($"Destination: {Destination}");
            Console.WriteLine($"Current Status: {Status}");
            Console.WriteLine($"Created: {CreatedAt:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"═══════════════════════════════════════════════");
            Console.WriteLine("Tracking Journey:");
            Console.WriteLine("───────────────────────────────────────────────");

            CheckpointNode current = CheckpointHead;
            int stepCount = 1;

            if (current == null)
            {
                Console.WriteLine("No checkpoints recorded yet.");
            }
            else
            {
                while (current != null)
                {
                    Console.WriteLine($"{stepCount}. {current}");
                    current = current.Next;
                    stepCount++;
                }
            }

            Console.WriteLine("═══════════════════════════════════════════════\n");
        }

        public override string ToString()
        {
            return $"Parcel [{ParcelId}] -> {Destination} | Status: {Status}";
        }
    }
}
