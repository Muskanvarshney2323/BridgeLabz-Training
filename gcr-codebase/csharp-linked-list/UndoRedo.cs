using System;

class TextState
{
    public string Content;
    public TextState Prev;
    public TextState Next;

    public TextState(string content)
    {
        Content = content;
        Prev = null;
        Next = null;
    }
}

class UndoRedoEditor
{
    private TextState head;
    private TextState tail;
    private TextState current;
    private int size;
    private const int MAX_HISTORY = 10;

    // Add new text state
    public void AddState(string content)
    {
        TextState newNode = new TextState(content);

        // If undo was used, remove all redo states
        if (current != null && current.Next != null)
        {
            current.Next.Prev = null;
            current.Next = null;
            tail = current;
        }

        if (head == null)
        {
            head = tail = current = newNode;
            size = 1;
            return;
        }

        tail.Next = newNode;
        newNode.Prev = tail;
        tail = newNode;
        current = newNode;
        size++;

        // Limit history
        if (size > MAX_HISTORY)
        {
            head = head.Next;
            head.Prev = null;
            size--;
        }
    }

    // Undo operation
    public void Undo()
    {
        if (current == null || current.Prev == null)
        {
            Console.WriteLine("Nothing to undo");
            return;
        }

        current = current.Prev;
        DisplayCurrent();
    }

    // Redo operation
    public void Redo()
    {
        if (current == null || current.Next == null)
        {
            Console.WriteLine("Nothing to redo");
            return;
        }

        current = current.Next;
        DisplayCurrent();
    }

    // Display current state
    public void DisplayCurrent()
    {
        if (current == null)
        {
            Console.WriteLine("Editor is empty");
            return;
        }

        Console.WriteLine("Current Text:");
        Console.WriteLine(current.Content);
    }
}

class Program
{
    static void Main()
    {
        UndoRedoEditor editor = new UndoRedoEditor();

        editor.AddState("Hello");
        editor.AddState("Hello World");
        editor.AddState("Hello World!");
        editor.AddState("Hello World! Welcome");

        editor.DisplayCurrent();

        Console.WriteLine("\nUndo:");
        editor.Undo();

        Console.WriteLine("\nUndo:");
        editor.Undo();

        Console.WriteLine("\nRedo:");
        editor.Redo();

        Console.WriteLine("\nAdd new text after undo:");
        editor.AddState("Hello ChatGPT");

        editor.DisplayCurrent();
    }
}
