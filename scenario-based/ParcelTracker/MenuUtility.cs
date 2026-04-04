using System;
namespace ParcelTracker
{
    /// <summary>
    /// Menu utility class to handle user interactions.
    /// </summary>
    public class MenuUtility
    {
        private readonly ParcelTrackingSystem trackingSystem;

        public MenuUtility(ParcelTrackingSystem system)
        {
            trackingSystem = system;
        }

        /// <summary>
        /// Displays the main menu and handles user input.
        /// </summary>
        public void DisplayMainMenu()
        {
            while (true)
            {
                Console.WriteLine("\n╔════════════════════════════════════════════════╗");
                Console.WriteLine("║         PARCEL TRACKING SYSTEM MENU            ║");
                Console.WriteLine("╚════════════════════════════════════════════════╝");
                Console.WriteLine("1. Add New Parcel");
                Console.WriteLine("2. Update Parcel Status");
                Console.WriteLine("3. Add Custom Checkpoint");
                Console.WriteLine("4. View Parcel Tracking Info");
                Console.WriteLine("5. View All Parcels");
                Console.WriteLine("6. Mark Parcel as Lost");
                Console.WriteLine("7. Recover Lost Parcel");
                Console.WriteLine("8. Remove Parcel");
                Console.WriteLine("9. View System Statistics");
                Console.WriteLine("10. Exit");
                Console.WriteLine("╚════════════════════════════════════════════════╝");
                Console.Write("Enter your choice (1-10): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        AddNewParcel();
                        break;
                    case "2":
                        UpdateParcelStatus();
                        break;
                    case "3":
                        AddCustomCheckpoint();
                        break;
                    case "4":
                        ViewTrackingInfo();
                        break;
                    case "5":
                        trackingSystem.DisplayAllParcels();
                        break;
                    case "6":
                        MarkParcelAsLost();
                        break;
                    case "7":
                        RecoverLostParcel();
                        break;
                    case "8":
                        RemoveParcel();
                        break;
                    case "9":
                        trackingSystem.DisplayStatistics();
                        break;
                    case "10":
                        Console.WriteLine("\n👋 Thank you for using Parcel Tracking System. Goodbye!\n");
                        return;
                    default:
                        Console.WriteLine("❌ Invalid choice. Please enter a number between 1-10.");
                        break;
                }
            }
        }

        /// <summary>
        /// Gets user input to add a new parcel.
        /// </summary>
        private void AddNewParcel()
        {
            Console.WriteLine("\n─── Add New Parcel ───");
            Console.Write("Enter Parcel ID: ");
            string parcelId = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(parcelId))
            {
                Console.WriteLine("❌ Parcel ID cannot be empty!");
                return;
            }

            Console.Write("Enter Destination: ");
            string destination = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(destination))
            {
                Console.WriteLine("❌ Destination cannot be empty!");
                return;
            }

            trackingSystem.AddParcel(parcelId, destination);
        }

        /// <summary>
        /// Gets user input to update parcel status.
        /// </summary>
        private void UpdateParcelStatus()
        {
            Console.WriteLine("\n─── Update Parcel Status ───");
            Console.Write("Enter Parcel ID: ");
            string parcelId = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(parcelId))
            {
                Console.WriteLine("❌ Parcel ID cannot be empty!");
                return;
            }

            Console.WriteLine("\nAvailable Status Options:");
            Console.WriteLine("1. Shipped");
            Console.WriteLine("2. In Transit");
            Console.WriteLine("3. Out for Delivery");
            Console.WriteLine("4. Delivered");
            Console.Write("Select status (1-4) or enter custom status: ");

            string statusChoice = Console.ReadLine() ?? "";
            string newStatus = statusChoice switch
            {
                "1" => "Shipped",
                "2" => "In Transit",
                "3" => "Out for Delivery",
                "4" => "Delivered",
                _ => statusChoice
            };

            if (string.IsNullOrWhiteSpace(newStatus))
            {
                Console.WriteLine("❌ Status cannot be empty!");
                return;
            }

            trackingSystem.UpdateStatus(parcelId, newStatus);
        }

        /// <summary>
        /// Gets user input to add a custom checkpoint.
        /// </summary>
        private void AddCustomCheckpoint()
        {
            Console.WriteLine("\n─── Add Custom Checkpoint ───");
            Console.Write("Enter Parcel ID: ");
            string parcelId = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(parcelId))
            {
                Console.WriteLine("❌ Parcel ID cannot be empty!");
                return;
            }

            Console.Write("Enter Checkpoint Name: ");
            string checkpointName = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(checkpointName))
            {
                Console.WriteLine("❌ Checkpoint name cannot be empty!");
                return;
            }

            trackingSystem.AddCheckpoint(parcelId, checkpointName);
        }

        /// <summary>
        /// Gets user input to view tracking information.
        /// </summary>
        private void ViewTrackingInfo()
        {
            Console.WriteLine("\n─── View Tracking Information ───");
            Console.Write("Enter Parcel ID: ");
            string parcelId = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(parcelId))
            {
                Console.WriteLine("❌ Parcel ID cannot be empty!");
                return;
            }

            trackingSystem.DisplayTrackingInfo(parcelId);
        }

        /// <summary>
        /// Gets user input to mark a parcel as lost.
        /// </summary>
        private void MarkParcelAsLost()
        {
            Console.WriteLine("\n─── Mark Parcel as Lost ───");
            Console.Write("Enter Parcel ID: ");
            string parcelId = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(parcelId))
            {
                Console.WriteLine("❌ Parcel ID cannot be empty!");
                return;
            }

            trackingSystem.HandleMissingParcel(parcelId);
        }

        /// <summary>
        /// Gets user input to recover a lost parcel.
        /// </summary>
        private void RecoverLostParcel()
        {
            Console.WriteLine("\n─── Recover Lost Parcel ───");
            Console.Write("Enter Parcel ID: ");
            string parcelId = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(parcelId))
            {
                Console.WriteLine("❌ Parcel ID cannot be empty!");
                return;
            }

            trackingSystem.RecoverParcel(parcelId);
        }

        /// <summary>
        /// Gets user input to remove a parcel.
        /// </summary>
        private void RemoveParcel()
        {
            Console.WriteLine("\n─── Remove Parcel ───");
            Console.Write("Enter Parcel ID: ");
            string parcelId = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(parcelId))
            {
                Console.WriteLine("❌ Parcel ID cannot be empty!");
                return;
            }

            Console.Write("Are you sure you want to remove this parcel? (yes/no): ");
            string confirmation = Console.ReadLine() ?? "";

            if (confirmation.ToLower() == "yes")
            {
                trackingSystem.RemoveParcel(parcelId);
            }
            else
            {
                Console.WriteLine("❌ Operation cancelled.");
            }
        }
    }
}
