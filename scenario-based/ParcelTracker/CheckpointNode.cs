using System;

namespace ParcelTracker
{
    /// <summary>
    /// Represents a checkpoint stage in a parcel's journey.
    /// </summary>
    public class CheckpointNode
    {
        public string CheckpointName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Location { get; set; }
        public CheckpointNode Next { get; set; }

        public CheckpointNode(string checkpointName, string location)
        {
            CheckpointName = checkpointName;
            Location = location;
            TimeStamp = DateTime.Now;
            Next = null;
        }

        public override string ToString()
        {
            return $"[{TimeStamp:yyyy-MM-dd HH:mm:ss}] {CheckpointName} at {Location}";
        }
    }
}
