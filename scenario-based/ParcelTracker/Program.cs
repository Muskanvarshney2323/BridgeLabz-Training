using System;
namespace ParcelTracker
{
    /// <summary>
    /// Main program entry point for the Parcel Tracking System.
    /// Demonstrates a delivery chain management system using Singly Linked List.
    /// 
    /// Story: A courier company tracks parcels through stages:
    /// Packed → Shipped → In Transit → Out for Delivery → Delivered
    /// 
    /// Features:
    /// - Forward tracking through stages
    /// - Add custom intermediate checkpoints
    /// - Handle lost/missing parcels (null pointer handling)
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║      PARCEL TRACKER - DELIVERY CHAIN            ║");
            Console.WriteLine("║     MANAGEMENT (SINGLY LINKED LIST)             ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            ParcelTrackingSystem trackingSystem = new ParcelTrackingSystem();
            MenuUtility menu = new MenuUtility(trackingSystem);

            // Optional: Add sample data for demonstration
            InitializeSampleData(trackingSystem);

            // Start the menu-driven system
            menu.DisplayMainMenu();
        }

        /// <summary>
        /// Initializes the system with sample parcel data for demonstration.
        /// </summary>
        private static void InitializeSampleData(ParcelTrackingSystem trackingSystem)
        {
            Console.WriteLine("📦 Loading sample parcels for demonstration...\n");

            // Sample Parcel 1
            trackingSystem.AddParcel("PKG001", "New York");
            trackingSystem.UpdateStatus("PKG001", "Shipped");
            trackingSystem.UpdateStatus("PKG001", "In Transit");

            // Sample Parcel 2
            trackingSystem.AddParcel("PKG002", "Los Angeles");
            trackingSystem.UpdateStatus("PKG002", "Shipped");

            // Sample Parcel 3 (Will be marked as lost for demonstration)
            trackingSystem.AddParcel("PKG003", "Chicago");
            trackingSystem.HandleMissingParcel("PKG003");

            // Sample Parcel 4
            trackingSystem.AddParcel("PKG004", "Houston");
            trackingSystem.UpdateStatus("PKG004", "In Transit");
            trackingSystem.UpdateStatus("PKG004", "Out for Delivery");
            trackingSystem.UpdateStatus("PKG004", "Delivered");

            Console.WriteLine("\n✓ Sample data loaded successfully!\n");
        }
    }
}
