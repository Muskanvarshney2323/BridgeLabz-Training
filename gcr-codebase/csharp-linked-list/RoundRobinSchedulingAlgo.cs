using System;
using System.Collections.Generic;

class ProcessNode
{
    public int ProcessId;
    public int BurstTime;
    public int RemainingTime;
    public int Priority;
    public int WaitingTime;
    public int TurnAroundTime;
    public ProcessNode Next;

    public ProcessNode(int id, int burst, int priority)
    {
        ProcessId = id;
        BurstTime = burst;
        RemainingTime = burst;
        Priority = priority;
        Next = null;
    }
}

class RoundRobinScheduler
{
    private ProcessNode head;
    private ProcessNode tail;
    private int timeQuantum;
    private int currentTime = 0;

    private List<ProcessNode> completed = new List<ProcessNode>();

    public RoundRobinScheduler(int quantum)
    {
        timeQuantum = quantum;
    }

    // Add process at end
    public void AddProcess(int id, int burst, int priority)
    {
        ProcessNode newNode = new ProcessNode(id, burst, priority);

        if (head == null)
        {
            head = tail = newNode;
            newNode.Next = head;
        }
        else
        {
            tail.Next = newNode;
            newNode.Next = head;
            tail = newNode;
        }
    }

    // Remove process by ID
    private void RemoveProcess(ProcessNode prev, ProcessNode current)
    {
        if (current == head && current == tail)
        {
            head = tail = null;
        }
        else
        {
            prev.Next = current.Next;
            if (current == head)
                head = current.Next;
            if (current == tail)
                tail = prev;
        }
    }

    // Simulate Round Robin
    public void Simulate()
    {
        if (head == null)
        {
            Console.WriteLine("No processes to schedule");
            return;
        }

        Console.WriteLine("\n--- Round Robin Scheduling Start ---");

        ProcessNode current = head;
        ProcessNode prev = tail;

        while (head != null)
        {
            Console.WriteLine($"\nExecuting Process P{current.ProcessId}");

            if (current.RemainingTime > timeQuantum)
            {
                current.RemainingTime -= timeQuantum;
                currentTime += timeQuantum;
            }
            else
            {
                currentTime += current.RemainingTime;
                current.RemainingTime = 0;

                current.TurnAroundTime = currentTime;
                current.WaitingTime = current.TurnAroundTime - current.BurstTime;

                completed.Add(current);

                Console.WriteLine($"Process P{current.ProcessId} completed");

                RemoveProcess(prev, current);
                current = prev.Next;
                DisplayQueue();
                continue;
            }

            prev = current;
            current = current.Next;
            DisplayQueue();
        }

        CalculateAverageTimes();
    }

    // Display Circular Queue
    public void DisplayQueue()
    {
        if (head == null)
        {
            Console.WriteLine("Queue Empty");
            return;
        }

        ProcessNode temp = head;
        Console.Write("Queue: ");
        do
        {
            Console.Write($"P{temp.ProcessId}(RT={temp.RemainingTime}) -> ");
            temp = temp.Next;
        } while (temp != head);
        Console.WriteLine("HEAD");
    }

    // Calculate Averages
    private void CalculateAverageTimes()
    {
        double totalWT = 0, totalTAT = 0;

        foreach (var p in completed)
        {
            totalWT += p.WaitingTime;
            totalTAT += p.TurnAroundTime;
        }

        Console.WriteLine("\n--- Final Result ---");
        Console.WriteLine("PID\tWT\tTAT");
        foreach (var p in completed)
        {
            Console.WriteLine($"P{p.ProcessId}\t{p.WaitingTime}\t{p.TurnAroundTime}");
        }

        Console.WriteLine($"\nAverage Waiting Time = {totalWT / completed.Count}");
        Console.WriteLine($"Average Turnaround Time = {totalTAT / completed.Count}");
    }
}

class Program
{
    static void Main()
    {
        RoundRobinScheduler rr = new RoundRobinScheduler(2); // Time Quantum = 2

        rr.AddProcess(1, 6, 1);
        rr.AddProcess(2, 4, 2);
        rr.AddProcess(3, 8, 1);

        rr.DisplayQueue();
        rr.Simulate();
    }
}
