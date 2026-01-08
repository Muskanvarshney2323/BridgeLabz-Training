using System;

class TaskNode
{
    public int TaskId;
    public string TaskName;
    public int Priority;
    public DateTime DueDate;
    public TaskNode Next;

    public TaskNode(int id, string name, int priority, DateTime dueDate)
    {
        TaskId = id;
        TaskName = name;
        Priority = priority;
        DueDate = dueDate;
        Next = null;
    }
}

class CircularTaskList
{
    private TaskNode head;
    private TaskNode current;

    // Add at Beginning
    public void AddAtBeginning(int id, string name, int priority, DateTime dueDate)
    {
        TaskNode newNode = new TaskNode(id, name, priority, dueDate);

        if (head == null)
        {
            head = newNode;
            newNode.Next = head;
            current = head;
            return;
        }

        TaskNode temp = head;
        while (temp.Next != head)
            temp = temp.Next;

        newNode.Next = head;
        temp.Next = newNode;
        head = newNode;
    }

    // Add at End
    public void AddAtEnd(int id, string name, int priority, DateTime dueDate)
    {
        TaskNode newNode = new TaskNode(id, name, priority, dueDate);

        if (head == null)
        {
            head = newNode;
            newNode.Next = head;
            current = head;
            return;
        }

        TaskNode temp = head;
        while (temp.Next != head)
            temp = temp.Next;

        temp.Next = newNode;
        newNode.Next = head;
    }

    // Add at Specific Position
    public void AddAtPosition(int pos, int id, string name, int priority, DateTime dueDate)
    {
        if (pos == 1)
        {
            AddAtBeginning(id, name, priority, dueDate);
            return;
        }

        TaskNode temp = head;
        for (int i = 1; i < pos - 1 && temp.Next != head; i++)
            temp = temp.Next;

        TaskNode newNode = new TaskNode(id, name, priority, dueDate);
        newNode.Next = temp.Next;
        temp.Next = newNode;
    }

    // Remove by Task ID
    public void RemoveById(int id)
    {
        if (head == null)
        {
            Console.WriteLine("List is empty");
            return;
        }

        TaskNode temp = head;
        TaskNode prev = null;

        do
        {
            if (temp.TaskId == id)
            {
                if (prev == null)
                {
                    TaskNode last = head;
                    while (last.Next != head)
                        last = last.Next;

                    if (head == last)
                    {
                        head = current = null;
                        return;
                    }

                    head = head.Next;
                    last.Next = head;
                }
                else
                {
                    prev.Next = temp.Next;
                }

                Console.WriteLine("Task Removed");
                return;
            }

            prev = temp;
            temp = temp.Next;

        } while (temp != head);

        Console.WriteLine("Task not found");
    }

    // View Current Task & Move to Next
    public void ViewCurrentTask()
    {
        if (current == null)
        {
            Console.WriteLine("No tasks available");
            return;
        }

        DisplayTask(current);
        current = current.Next;
    }

    // Display All Tasks
    public void DisplayAll()
    {
        if (head == null)
        {
            Console.WriteLine("No tasks to display");
            return;
        }

        TaskNode temp = head;
        do
        {
            DisplayTask(temp);
            temp = temp.Next;
        } while (temp != head);
    }

    // Search by Priority
    public void SearchByPriority(int priority)
    {
        if (head == null)
        {
            Console.WriteLine("List is empty");
            return;
        }

        TaskNode temp = head;
        bool found = false;

        do
        {
            if (temp.Priority == priority)
            {
                DisplayTask(temp);
                found = true;
            }
            temp = temp.Next;
        } while (temp != head);

        if (!found)
            Console.WriteLine("No tasks found with this priority");
    }

    private void DisplayTask(TaskNode t)
    {
        Console.WriteLine($"ID: {t.TaskId}, Name: {t.TaskName}, Priority: {t.Priority}, Due: {t.DueDate.ToShortDateString()}");
    }
}

class Program
{
    static void Main()
    {
        CircularTaskList scheduler = new CircularTaskList();

        scheduler.AddAtEnd(1, "Design UI", 1, DateTime.Now.AddDays(2));
        scheduler.AddAtEnd(2, "Write Code", 2, DateTime.Now.AddDays(5));
        scheduler.AddAtBeginning(3, "Fix Bugs", 1, DateTime.Now.AddDays(1));

        Console.WriteLine("All Tasks:");
        scheduler.DisplayAll();

        Console.WriteLine("\nCurrent Task Rotation:");
        scheduler.ViewCurrentTask();
        scheduler.ViewCurrentTask();
        scheduler.ViewCurrentTask();

        Console.WriteLine("\nSearch Priority 1:");
        scheduler.SearchByPriority(1);

        Console.WriteLine("\nRemove Task:");
        scheduler.RemoveById(2);

        Console.WriteLine("\nAfter Removal:");
        scheduler.DisplayAll();
    }
}
