using System;
using System.Collections.Generic;

namespace LinkedListProblems
{
    
    public class ProcessNode
    {
        public int ProcessID { get; set; }
        public int BurstTime { get; set; }
        public int RemainingTime { get; set; }
        public int Priority { get; set; }
        public ProcessNode Next { get; set; }

        public ProcessNode(int processID, int burstTime, int priority)
        {
            ProcessID = processID;
            BurstTime = burstTime;
            RemainingTime = burstTime;
            Priority = priority;
            Next = null;
        }

        public override string ToString()
        {
            return $"[PID: {ProcessID}, BurstTime: {BurstTime}, Remaining: {RemainingTime}, Priority: {Priority}]";
        }
    }

    public class RoundRobinScheduler
    {
        private ProcessNode head;
        private int timeQuantum;
        private List<int> completionTimes;
        private List<int> turnaroundTimes;
        private List<int> waitingTimes;

        public RoundRobinScheduler(int timeQuantum)
        {
            head = null;
            this.timeQuantum = timeQuantum;
            completionTimes = new List<int>();
            turnaroundTimes = new List<int>();
            waitingTimes = new List<int>();
        }

        /// <summary>
        /// Add a new process at the end of the circular list
        /// </summary>
        public void AddProcess(int processID, int burstTime, int priority)
        {
            ProcessNode newNode = new ProcessNode(processID, burstTime, priority);

            if (head == null)
            {
                head = newNode;
                head.Next = head;
            }
            else
            {
                ProcessNode last = head;
                while (last.Next != head)
                {
                    last = last.Next;
                }
                newNode.Next = head;
                last.Next = newNode;
            }
            Console.WriteLine($"Process {processID} added with burst time {burstTime}");
        }

        /// <summary>
        /// Remove a process by Process ID after its execution
        /// </summary>
        public void RemoveProcess(int processID)
        {
            if (head == null)
            {
                Console.WriteLine("No processes to remove");
                return;
            }

            if (head.ProcessID == processID)
            {
                if (head.Next == head)
                {
                    head = null;
                }
                else
                {
                    ProcessNode last = head;
                    while (last.Next != head)
                    {
                        last = last.Next;
                    }
                    last.Next = head.Next;
                    head = head.Next;
                }
                return;
            }

            ProcessNode node = head;
            do
            {
                if (node.Next.ProcessID == processID)
                {
                    node.Next = node.Next.Next;
                    return;
                }
                node = node.Next;
            } while (node != head);
        }

        /// <summary>
        /// Simulate the round-robin scheduling
        /// </summary>
        public void SimulateRoundRobinScheduling()
        {
            if (head == null)
            {
                Console.WriteLine("No processes to schedule");
                return;
            }

            int totalTime = 0;
            ProcessNode current = head;
            List<string> executionLog = new List<string>();

            Console.WriteLine("\n=== Round Robin Scheduling Started ===");
            Console.WriteLine($"Time Quantum: {timeQuantum}\n");

            while (current != null)
            {
                Console.WriteLine($"--- Round ---");
                Console.WriteLine(DisplayCurrentQueue());

                if (current.RemainingTime > 0)
                {
                    int executionTime = Math.Min(timeQuantum, current.RemainingTime);
                    current.RemainingTime -= executionTime;
                    totalTime += executionTime;

                    string log = $"Process {current.ProcessID} executed for {executionTime} units (Total Time: {totalTime})";
                    executionLog.Add(log);
                    Console.WriteLine(log);

                    if (current.RemainingTime == 0)
                    {
                        completionTimes.Add(totalTime);
                        turnaroundTimes.Add(totalTime); // Assuming arrival time is 0
                        waitingTimes.Add(totalTime - current.BurstTime);
                        Console.WriteLine($"Process {current.ProcessID} completed at time {totalTime}");
                        ProcessNode nextNode = current.Next == current ? null : current.Next;
                        RemoveProcess(current.ProcessID);
                        current = nextNode;
                    }
                    else
                    {
                        current = current.Next;
                    }
                }
                else
                {
                    current = current.Next;
                }

                if (current == null && head != null && head.RemainingTime > 0)
                {
                    current = head;
                }

                Console.WriteLine();

                // Safety check to prevent infinite loops
                if (head != null && current == head && head.RemainingTime == 0)
                {
                    break;
                }
            }

            Console.WriteLine("=== Scheduling Completed ===\n");
        }

        /// <summary>
        /// Display the current queue of processes
        /// </summary>
        public string DisplayCurrentQueue()
        {
            if (head == null)
                return "Queue is empty";

            string queue = "Current Queue: ";
            ProcessNode node = head;

            do
            {
                queue += $"{node.ProcessID}(R:{node.RemainingTime}) -> ";
                node = node.Next;
            } while (node != head);

            queue += "...";
            return queue;
        }

        /// <summary>
        /// Calculate and display the average waiting time and turn-around time
        /// </summary>
        public void DisplaySchedulingMetrics()
        {
            if (completionTimes.Count == 0)
            {
                Console.WriteLine("No completed processes");
                return;
            }

            double avgWaitingTime = 0;
            double avgTurnaroundTime = 0;

            foreach (int wt in waitingTimes)
            {
                avgWaitingTime += wt;
            }
            avgWaitingTime /= waitingTimes.Count;

            foreach (int tt in turnaroundTimes)
            {
                avgTurnaroundTime += tt;
            }
            avgTurnaroundTime /= turnaroundTimes.Count;

            Console.WriteLine("\n=== Scheduling Metrics ===");
            Console.WriteLine($"Average Waiting Time: {avgWaitingTime:F2}");
            Console.WriteLine($"Average Turnaround Time: {avgTurnaroundTime:F2}");
            Console.WriteLine();
        }

        /// <summary>
        /// Get the number of processes in the queue
        /// </summary>
        public int GetProcessCount()
        {
            if (head == null)
                return 0;

            int count = 1;
            ProcessNode node = head.Next;

            while (node != head)
            {
                count++;
                node = node.Next;
            }

            return count;
        }
    }

    // Example Usage
    public class RoundRobinSchedulerExample
    {
        public static void Main()
        {
            RoundRobinScheduler scheduler = new RoundRobinScheduler(4); // Time quantum = 4 units

            // Add processes
            scheduler.AddProcess(1, 8, 1);
            scheduler.AddProcess(2, 4, 2);
            scheduler.AddProcess(3, 2, 3);
            scheduler.AddProcess(4, 3, 1);
            scheduler.AddProcess(5, 5, 2);

            Console.WriteLine($"Total processes: {scheduler.GetProcessCount()}\n");

            // Simulate scheduling
            scheduler.SimulateRoundRobinScheduling();

            // Display metrics
            scheduler.DisplaySchedulingMetrics();
        }
    }
}
