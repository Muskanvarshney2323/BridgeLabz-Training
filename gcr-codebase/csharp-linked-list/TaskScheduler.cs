using System;
using System.Collections.Generic;

namespace LinkedListProblems
{
    /// <summary>
    /// Problem 3: Circular Linked List - Task Scheduler
    /// 
    /// Create a task scheduler using a circular linked list. Each node in the list represents a task 
    /// with Task ID, Task Name, Priority, and Due Date.
    /// </summary>
    public class TaskNode
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public int Priority { get; set; }  // 1-5, where 5 is highest
        public string DueDate { get; set; }
        public TaskNode Next { get; set; }

        public TaskNode(int taskID, string taskName, int priority, string dueDate)
        {
            TaskID = taskID;
            TaskName = taskName;
            Priority = priority;
            DueDate = dueDate;
            Next = null;
        }

        public override string ToString()
        {
            return $"[Task ID: {TaskID}, Name: {TaskName}, Priority: {Priority}, Due: {DueDate}]";
        }
    }

    public class TaskScheduler
    {
        private TaskNode head;
        private TaskNode current;

        public TaskScheduler()
        {
            head = null;
            current = null;
        }

        /// <summary>
        /// Add a task at the beginning of the circular list
        /// </summary>
        public void AddAtBeginning(int taskID, string taskName, int priority, string dueDate)
        {
            TaskNode newNode = new TaskNode(taskID, taskName, priority, dueDate);

            if (head == null)
            {
                head = newNode;
                head.Next = head; // Make it circular
                current = head;
            }
            else
            {
                // Find the last node
                TaskNode last = head;
                while (last.Next != head)
                {
                    last = last.Next;
                }

                newNode.Next = head;
                last.Next = newNode;
                head = newNode;
            }
            Console.WriteLine($"Task '{taskName}' added at the beginning");
        }

        /// <summary>
        /// Add a task at the end of the circular list
        /// </summary>
        public void AddAtEnd(int taskID, string taskName, int priority, string dueDate)
        {
            TaskNode newNode = new TaskNode(taskID, taskName, priority, dueDate);

            if (head == null)
            {
                head = newNode;
                head.Next = head; // Make it circular
                current = head;
            }
            else
            {
                TaskNode last = head;
                while (last.Next != head)
                {
                    last = last.Next;
                }

                newNode.Next = head;
                last.Next = newNode;
            }
            Console.WriteLine($"Task '{taskName}' added at the end");
        }

        /// <summary>
        /// Add a task at a specific position
        /// </summary>
        public void AddAtPosition(int taskID, string taskName, int priority, string dueDate, int position)
        {
            if (position == 0)
            {
                AddAtBeginning(taskID, taskName, priority, dueDate);
                return;
            }

            TaskNode newNode = new TaskNode(taskID, taskName, priority, dueDate);

            if (head == null)
            {
                head = newNode;
                head.Next = head;
                current = head;
                return;
            }

            TaskNode node = head;
            int count = 0;

            do
            {
                if (count == position - 1)
                {
                    newNode.Next = node.Next;
                    node.Next = newNode;
                    Console.WriteLine($"Task '{taskName}' added at position {position}");
                    return;
                }
                node = node.Next;
                count++;
            } while (node != head);

            Console.WriteLine("Position out of bounds");
        }

        /// <summary>
        /// Remove a task by Task ID
        /// </summary>
        public void RemoveByTaskID(int taskID)
        {
            if (head == null)
            {
                Console.WriteLine("List is empty");
                return;
            }

            if (head.TaskID == taskID)
            {
                if (head.Next == head)
                {
                    head = null;
                    current = null;
                }
                else
                {
                    TaskNode last = head;
                    while (last.Next != head)
                    {
                        last = last.Next;
                    }
                    last.Next = head.Next;
                    head = head.Next;
                    if (current == head.Previous)
                    {
                        current = head;
                    }
                }
                Console.WriteLine($"Task with ID {taskID} removed");
                return;
            }

            TaskNode node = head;
            do
            {
                if (node.Next.TaskID == taskID)
                {
                    node.Next = node.Next.Next;
                    Console.WriteLine($"Task with ID {taskID} removed");
                    return;
                }
                node = node.Next;
            } while (node != head);

            Console.WriteLine($"Task with ID {taskID} not found");
        }

        /// <summary>
        /// View the current task and move to the next task
        /// </summary>
        public TaskNode ViewCurrentAndMoveNext()
        {
            if (current == null)
            {
                Console.WriteLine("No tasks available");
                return null;
            }

            TaskNode result = current;
            current = current.Next;
            return result;
        }

        /// <summary>
        /// Display all tasks starting from the head node
        /// </summary>
        public void DisplayAll()
        {
            if (head == null)
            {
                Console.WriteLine("No tasks in the scheduler");
                return;
            }

            Console.WriteLine("\n--- All Tasks ---");
            TaskNode node = head;
            int count = 1;

            do
            {
                Console.WriteLine($"{count}. {node}");
                node = node.Next;
                count++;
            } while (node != head);

            Console.WriteLine();
        }

        /// <summary>
        /// Search for a task by Priority
        /// </summary>
        public List<TaskNode> SearchByPriority(int priority)
        {
            List<TaskNode> results = new List<TaskNode>();

            if (head == null)
                return results;

            TaskNode node = head;
            do
            {
                if (node.Priority == priority)
                {
                    results.Add(node);
                }
                node = node.Next;
            } while (node != head);

            return results;
        }

        /// <summary>
        /// Get total number of tasks
        /// </summary>
        public int GetTaskCount()
        {
            if (head == null)
                return 0;

            int count = 1;
            TaskNode node = head;

            while (node.Next != head)
            {
                count++;
                node = node.Next;
            }

            return count;
        }
    }

    // Example Usage
    public class TaskSchedulerExample
    {
        public static void Main()
        {
            TaskScheduler scheduler = new TaskScheduler();

            // Add tasks
            scheduler.AddAtEnd(1, "Complete Project", 5, "2024-01-15");
            scheduler.AddAtEnd(2, "Review Code", 4, "2024-01-16");
            scheduler.AddAtEnd(3, "Write Documentation", 3, "2024-01-17");
            scheduler.AddAtEnd(4, "Testing", 4, "2024-01-18");

            scheduler.AddAtBeginning(0, "Planning", 5, "2024-01-14");

            scheduler.DisplayAll();

            // View and move through tasks
            Console.WriteLine("Viewing tasks in circular order:");
            for (int i = 0; i < 7; i++)
            {
                TaskNode task = scheduler.ViewCurrentAndMoveNext();
                Console.WriteLine($"Current: {task}");
            }

            // Search by priority
            Console.WriteLine("\nHigh Priority Tasks (Priority >= 4):");
            var highPriorityTasks = scheduler.SearchByPriority(4);
            foreach (var task in highPriorityTasks)
            {
                Console.WriteLine(task);
            }

            Console.WriteLine($"\nTotal tasks: {scheduler.GetTaskCount()}");

            // Remove a task
            scheduler.RemoveByTaskID(3);

            scheduler.DisplayAll();
        }
    }
}
