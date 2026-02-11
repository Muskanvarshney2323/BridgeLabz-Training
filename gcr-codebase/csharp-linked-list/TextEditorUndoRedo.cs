using System;
using System.Collections.Generic;

namespace LinkedListProblems
{
 
    public class EditorState
    {
        public string TextContent { get; set; }
        public string Action { get; set; }
        public EditorState Next { get; set; }
        public EditorState Previous { get; set; }

        public EditorState(string textContent, string action)
        {
            TextContent = textContent;
            Action = action;
            Next = null;
            Previous = null;
        }

        public override string ToString()
        {
            return $"[Content: '{TextContent}', Action: '{Action}']";
        }
    }

    public class TextEditorWithUndoRedo
    {
        private EditorState head;
        private EditorState current;
        private int maxHistory;
        private int historyCount;

        public TextEditorWithUndoRedo(int maxHistorySize = 10)
        {
            head = null;
            current = null;
            maxHistory = maxHistorySize;
            historyCount = 0;
        }

        /// <summary>
        /// Add a new text state at the end of the list
        /// </summary>
        public void AddState(string textContent, string action)
        {
            EditorState newState = new EditorState(textContent, action);

            if (head == null)
            {
                head = newState;
                current = head;
            }
            else
            {
                // Remove any redo states if they exist
                if (current.Next != null)
                {
                    current.Next = null;
                }

                newState.Previous = current;
                current.Next = newState;
                current = newState;
            }

            historyCount++;

            // Check if we've exceeded max history and remove oldest
            if (historyCount > maxHistory)
            {
                head = head.Next;
                if (head != null)
                {
                    head.Previous = null;
                }
                historyCount--;
            }

            Console.WriteLine($"State added: {action} -> '{textContent}'");
        }

        /// <summary>
        /// Implement the undo functionality
        /// </summary>
        public string Undo()
        {
            if (current == null || current.Previous == null)
            {
                Console.WriteLine("Cannot undo: at the beginning of history");
                return "";
            }

            current = current.Previous;
            Console.WriteLine($"Undo performed: {current.Action} -> '{current.TextContent}'");
            return current.TextContent;
        }

        /// <summary>
        /// Implement the redo functionality
        /// </summary>
        public string Redo()
        {
            if (current == null || current.Next == null)
            {
                Console.WriteLine("Cannot redo: at the end of history");
                return "";
            }

            current = current.Next;
            Console.WriteLine($"Redo performed: {current.Action} -> '{current.TextContent}'");
            return current.TextContent;
        }

        /// <summary>
        /// Display the current state of the text
        /// </summary>
        public string GetCurrentText()
        {
            return current != null ? current.TextContent : "";
        }

        /// <summary>
        /// Get current action
        /// </summary>
        public string GetCurrentAction()
        {
            return current != null ? current.Action : "";
        }

        /// <summary>
        /// Display the entire edit history
        /// </summary>
        public void DisplayEditHistory()
        {
            if (head == null)
            {
                Console.WriteLine("No edit history");
                return;
            }

            Console.WriteLine("\n--- Edit History ---");
            EditorState state = head;
            int count = 1;

            while (state != null)
            {
                string marker = (state == current) ? " <- CURRENT" : "";
                Console.WriteLine($"{count}. {state}{marker}");
                state = state.Next;
                count++;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Get undo/redo availability
        /// </summary>
        public (bool canUndo, bool canRedo) GetUndoRedoState()
        {
            bool canUndo = current != null && current.Previous != null;
            bool canRedo = current != null && current.Next != null;
            return (canUndo, canRedo);
        }

        /// <summary>
        /// Check if undo is possible
        /// </summary>
        public bool CanUndo => current != null && current.Previous != null;

        /// <summary>
        /// Check if redo is possible
        /// </summary>
        public bool CanRedo => current != null && current.Next != null;

        /// <summary>
        /// Get history size
        /// </summary>
        public int GetHistorySize()
        {
            int count = 0;
            EditorState state = head;

            while (state != null)
            {
                count++;
                state = state.Next;
            }

            return count;
        }
    }

    // Example Usage
    public class TextEditorExample
    {
        public static void Main()
        {
            TextEditorWithUndoRedo editor = new TextEditorWithUndoRedo(10);

            // Simulate typing and editing
            editor.AddState("H", "Type 'H'");
            editor.AddState("He", "Type 'e'");
            editor.AddState("Hel", "Type 'l'");
            editor.AddState("Hell", "Type 'l'");
            editor.AddState("Hello", "Type 'o'");
            editor.AddState("Hello ", "Type ' '");
            editor.AddState("Hello W", "Type 'W'");
            editor.AddState("Hello Wo", "Type 'o'");
            editor.AddState("Hello Wor", "Type 'r'");
            editor.AddState("Hello World", "Type 'd'");

            editor.DisplayEditHistory();

            Console.WriteLine($"Current Text: '{editor.GetCurrentText()}'");
            Console.WriteLine($"Current Action: {editor.GetCurrentAction()}");
            Console.WriteLine();

            // Undo operations
            Console.WriteLine("Performing undo operations...");
            editor.Undo(); // Hello Wor
            editor.Undo(); // Hello Wo
            editor.Undo(); // Hello W

            Console.WriteLine($"\nCurrent Text: '{editor.GetCurrentText()}'\n");

            // Redo operations
            Console.WriteLine("Performing redo operations...");
            editor.Redo(); // Hello Wo
            editor.Redo(); // Hello Wor

            Console.WriteLine($"\nCurrent Text: '{editor.GetCurrentText()}'\n");

            // Edit after undo
            editor.AddState("Hello World", "Type 'ld'");

            editor.DisplayEditHistory();

            // Check undo/redo state
            var (canUndo, canRedo) = editor.GetUndoRedoState();
            Console.WriteLine($"Can Undo: {canUndo}, Can Redo: {canRedo}");

            Console.WriteLine($"History Size: {editor.GetHistorySize()}");
        }
    }
}
