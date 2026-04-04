using System;
using System.Collections.Generic;

/// <summary>
/// Problem 3: Hospital Triage System
/// Simulate a hospital triage system using a PriorityQueue where patients 
/// with higher severity are treated first.
/// Example:
/// Patients: [ ("John", 3), ("Alice", 5), ("Bob", 2) ]
/// Order: Alice (5), John (3), Bob (2)
/// </summary>
class HospitalTriageProgram
{
    class Patient
    {
        public string Name { get; set; }
        public int Severity { get; set; } // 1-10 scale

        public Patient(string name, int severity)
        {
            Name = name;
            Severity = severity;
        }

        public override string ToString()
        {
            return $"{Name} (Severity: {Severity})";
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║        Hospital Triage System                     ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

        try
        {
            // Test case 1: Basic triage
            Console.WriteLine("=== Test Case 1: Basic Triage ===");
            List<(string name, int severity)> patients1 = new List<(string, int)>
            {
                ("John", 3),
                ("Alice", 5),
                ("Bob", 2)
            };
            
            Console.WriteLine("Patients Arriving:");
            foreach (var p in patients1)
            {
                Console.WriteLine($"  {p.name}: Severity {p.severity}");
            }
            
            Console.WriteLine("\nTreatment Order:");
            ProcessTriage(patients1);
            Console.WriteLine();

            // Test case 2: Multiple patients with same severity
            Console.WriteLine("=== Test Case 2: Multiple with Same Severity ===");
            List<(string name, int severity)> patients2 = new List<(string, int)>
            {
                ("Patient A", 5),
                ("Patient B", 5),
                ("Patient C", 3),
                ("Patient D", 8),
                ("Patient E", 3)
            };
            
            Console.WriteLine("Patients Arriving:");
            foreach (var p in patients2)
            {
                Console.WriteLine($"  {p.name}: Severity {p.severity}");
            }
            
            Console.WriteLine("\nTreatment Order:");
            ProcessTriage(patients2);
            Console.WriteLine();

            // Test case 3: Emergency scenario
            Console.WriteLine("=== Test Case 3: Emergency Scenario ===");
            List<(string name, int severity)> patients3 = new List<(string, int)>
            {
                ("Minor Injury", 1),
                ("Critical Condition", 10),
                ("Moderate Pain", 4),
                ("Serious Fracture", 7),
                ("Headache", 2)
            };
            
            Console.WriteLine("Patients Arriving:");
            foreach (var p in patients3)
            {
                Console.WriteLine($"  {p.name}: Severity {p.severity}");
            }
            
            Console.WriteLine("\nTreatment Order:");
            ProcessTriage(patients3);
            Console.WriteLine();

            // Test case 4: Single patient
            Console.WriteLine("=== Test Case 4: Single Patient ===");
            List<(string name, int severity)> patients4 = new List<(string, int)>
            {
                ("Solo Patient", 5)
            };
            
            Console.WriteLine("Patients Arriving:");
            foreach (var p in patients4)
            {
                Console.WriteLine($"  {p.name}: Severity {p.severity}");
            }
            
            Console.WriteLine("\nTreatment Order:");
            ProcessTriage(patients4);
            Console.WriteLine();

            Console.WriteLine("✓ Hospital Triage System simulation completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    static void ProcessTriage(List<(string name, int severity)> patients)
    {
        // Create priority queue (max heap - highest priority first)
        PriorityQueue<Patient, int> queue = new PriorityQueue<Patient, int>();

        // Add patients to queue
        foreach (var (name, severity) in patients)
        {
            queue.Enqueue(new Patient(name, severity), -severity); // Negative for max heap
        }

        // Process patients
        int orderNumber = 1;
        while (queue.Count > 0)
        {
            Patient patient = queue.Dequeue();
            Console.WriteLine($"  {orderNumber}. {patient}");
            orderNumber++;
        }
    }
}
