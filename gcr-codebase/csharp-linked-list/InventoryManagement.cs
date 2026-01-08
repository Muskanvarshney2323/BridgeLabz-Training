using System;

class Item
{
    public int ItemId;
    public string Name;
    public int Quantity;
    public double Price;
    public Item Next;

    public Item(int id, string name, int qty, double price)
    {
        ItemId = id;
        Name = name;
        Quantity = qty;
        Price = price;
        Next = null;
    }
}

class InventoryList
{
    private Item head;

    // Add at Beginning
    public void AddAtBeginning(int id, string name, int qty, double price)
    {
        Item newNode = new Item(id, name, qty, price);
        newNode.Next = head;
        head = newNode;
    }

    // Add at End
    public void AddAtEnd(int id, string name, int qty, double price)
    {
        Item newNode = new Item(id, name, qty, price);

        if (head == null)
        {
            head = newNode;
            return;
        }

        Item temp = head;
        while (temp.Next != null)
            temp = temp.Next;

        temp.Next = newNode;
    }

    // Add at Specific Position
    public void AddAtPosition(int pos, int id, string name, int qty, double price)
    {
        if (pos == 1)
        {
            AddAtBeginning(id, name, qty, price);
            return;
        }

        Item temp = head;
        for (int i = 1; i < pos - 1 && temp != null; i++)
            temp = temp.Next;

        if (temp == null)
        {
            Console.WriteLine("Invalid position");
            return;
        }

        Item newNode = new Item(id, name, qty, price);
        newNode.Next = temp.Next;
        temp.Next = newNode;
    }

    // Remove by Item ID
    public void RemoveById(int id)
    {
        if (head == null)
        {
            Console.WriteLine("Inventory empty");
            return;
        }

        if (head.ItemId == id)
        {
            head = head.Next;
            Console.WriteLine("Item removed");
            return;
        }

        Item temp = head;
        while (temp.Next != null && temp.Next.ItemId != id)
            temp = temp.Next;

        if (temp.Next == null)
        {
            Console.WriteLine("Item not found");
            return;
        }

        temp.Next = temp.Next.Next;
        Console.WriteLine("Item removed");
    }

    // Update Quantity
    public void UpdateQuantity(int id, int newQty)
    {
        Item temp = head;

        while (temp != null)
        {
            if (temp.ItemId == id)
            {
                temp.Quantity = newQty;
                Console.WriteLine("Quantity updated");
                return;
            }
            temp = temp.Next;
        }
        Console.WriteLine("Item not found");
    }

    // Search by ID
    public void SearchById(int id)
    {
        Item temp = head;

        while (temp != null)
        {
            if (temp.ItemId == id)
            {
                DisplayItem(temp);
                return;
            }
            temp = temp.Next;
        }
        Console.WriteLine("Item not found");
    }

    // Search by Name
    public void SearchByName(string name)
    {
        Item temp = head;
        bool found = false;

        while (temp != null)
        {
            if (temp.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                DisplayItem(temp);
                found = true;
            }
            temp = temp.Next;
        }

        if (!found)
            Console.WriteLine("Item not found");
    }

    // Calculate Total Inventory Value
    public void TotalInventoryValue()
    {
        double total = 0;
        Item temp = head;

        while (temp != null)
        {
            total += temp.Price * temp.Quantity;
            temp = temp.Next;
        }

        Console.WriteLine($"Total Inventory Value: ₹{total}");
    }

    // Sort Inventory
    public void SortByName(bool ascending)
    {
        for (Item i = head; i != null; i = i.Next)
        {
            for (Item j = i.Next; j != null; j = j.Next)
            {
                if ((ascending && string.Compare(i.Name, j.Name) > 0) ||
                    (!ascending && string.Compare(i.Name, j.Name) < 0))
                {
                    SwapData(i, j);
                }
            }
        }
        Console.WriteLine("Sorted by Name");
    }

    public void SortByPrice(bool ascending)
    {
        for (Item i = head; i != null; i = i.Next)
        {
            for (Item j = i.Next; j != null; j = j.Next)
            {
                if ((ascending && i.Price > j.Price) ||
                    (!ascending && i.Price < j.Price))
                {
                    SwapData(i, j);
                }
            }
        }
        Console.WriteLine("Sorted by Price");
    }

    private void SwapData(Item a, Item b)
    {
        (a.ItemId, b.ItemId) = (b.ItemId, a.ItemId);
        (a.Name, b.Name) = (b.Name, a.Name);
        (a.Quantity, b.Quantity) = (b.Quantity, a.Quantity);
        (a.Price, b.Price) = (b.Price, a.Price);
    }

    // Display All Items
    public void DisplayAll()
    {
        if (head == null)
        {
            Console.WriteLine("Inventory empty");
            return;
        }

        Item temp = head;
        while (temp != null)
        {
            DisplayItem(temp);
            temp = temp.Next;
        }
    }

    private void DisplayItem(Item i)
    {
        Console.WriteLine($"ID: {i.ItemId}, Name: {i.Name}, Qty: {i.Quantity}, Price: ₹{i.Price}");
    }
}

class Program
{
    static void Main()
    {
        InventoryList inventory = new InventoryList();

        inventory.AddAtEnd(101, "Laptop", 5, 60000);
        inventory.AddAtEnd(102, "Mouse", 20, 500);
        inventory.AddAtBeginning(103, "Keyboard", 10, 1500);

        Console.WriteLine("Inventory List:");
        inventory.DisplayAll();

        inventory.UpdateQuantity(102, 25);
        inventory.SearchByName("Laptop");

        inventory.SortByPrice(true);
        inventory.DisplayAll();

        inventory.TotalInventoryValue();
    }
}
