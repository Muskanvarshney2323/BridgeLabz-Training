using System;
using System.Collections.Generic;

class User
{
    public int UserId;
    public string Name;
    public int Age;
    public List<int> Friends;
    public User Next;

    public User(int id, string name, int age)
    {
        UserId = id;
        Name = name;
        Age = age;
        Friends = new List<int>();
        Next = null;
    }
}

class SocialMediaList
{
    private User head;

    // Add User
    public void AddUser(int id, string name, int age)
    {
        User newUser = new User(id, name, age);
        newUser.Next = head;
        head = newUser;
    }

    // Find User by ID
    private User FindUserById(int id)
    {
        User temp = head;
        while (temp != null)
        {
            if (temp.UserId == id)
                return temp;
            temp = temp.Next;
        }
        return null;
    }

    // Add Friend Connection
    public void AddFriend(int id1, int id2)
    {
        User u1 = FindUserById(id1);
        User u2 = FindUserById(id2);

        if (u1 == null || u2 == null)
        {
            Console.WriteLine("User not found");
            return;
        }

        if (!u1.Friends.Contains(id2))
            u1.Friends.Add(id2);

        if (!u2.Friends.Contains(id1))
            u2.Friends.Add(id1);

        Console.WriteLine("Friend connection added");
    }

    // Remove Friend Connection
    public void RemoveFriend(int id1, int id2)
    {
        User u1 = FindUserById(id1);
        User u2 = FindUserById(id2);

        if (u1 == null || u2 == null)
        {
            Console.WriteLine("User not found");
            return;
        }

        u1.Friends.Remove(id2);
        u2.Friends.Remove(id1);

        Console.WriteLine("Friend connection removed");
    }

    // Find Mutual Friends
    public void MutualFriends(int id1, int id2)
    {
        User u1 = FindUserById(id1);
        User u2 = FindUserById(id2);

        if (u1 == null || u2 == null)
        {
            Console.WriteLine("User not found");
            return;
        }

        Console.WriteLine("Mutual Friends:");
        bool found = false;

        foreach (int f in u1.Friends)
        {
            if (u2.Friends.Contains(f))
            {
                Console.WriteLine($"User ID: {f}");
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("No mutual friends");
    }

    // Display Friends of a User
    public void DisplayFriends(int id)
    {
        User user = FindUserById(id);

        if (user == null)
        {
            Console.WriteLine("User not found");
            return;
        }

        Console.WriteLine($"Friends of {user.Name}:");
        if (user.Friends.Count == 0)
        {
            Console.WriteLine("No friends");
            return;
        }

        foreach (int f in user.Friends)
            Console.WriteLine($"Friend ID: {f}");
    }

    // Search by User ID
    public void SearchById(int id)
    {
        User user = FindUserById(id);

        if (user == null)
        {
            Console.WriteLine("User not found");
            return;
        }

        DisplayUser(user);
    }

    // Search by Name
    public void SearchByName(string name)
    {
        User temp = head;
        bool found = false;

        while (temp != null)
        {
            if (temp.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                DisplayUser(temp);
                found = true;
            }
            temp = temp.Next;
        }

        if (!found)
            Console.WriteLine("User not found");
    }

    // Count Friends for Each User
    public void CountFriends()
    {
        User temp = head;

        while (temp != null)
        {
            Console.WriteLine($"{temp.Name} has {temp.Friends.Count} friends");
            temp = temp.Next;
        }
    }

    private void DisplayUser(User u)
    {
        Console.WriteLine($"ID: {u.UserId}, Name: {u.Name}, Age: {u.Age}");
    }
}

class Program
{
    static void Main()
    {
        SocialMediaList sm = new SocialMediaList();

        sm.AddUser(1, "Aman", 22);
        sm.AddUser(2, "Neha", 21);
        sm.AddUser(3, "Ravi", 23);
        sm.AddUser(4, "Pooja", 20);

        sm.AddFriend(1, 2);
        sm.AddFriend(1, 3);
        sm.AddFriend(2, 3);
        sm.AddFriend(3, 4);

        sm.DisplayFriends(1);

        sm.MutualFriends(1, 2);

        sm.RemoveFriend(1, 2);

        sm.DisplayFriends(1);

        sm.SearchByName("Ravi");

        sm.CountFriends();
    }
}
