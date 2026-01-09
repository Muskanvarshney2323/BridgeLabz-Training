using System;
using System.Collections.Generic;

namespace LinkedListProblems
{
    /// <summary>
    /// Problem 7: Singly Linked List - Social Media Friend Connections
    /// 
    /// Create a system to manage social media friend connections using a singly linked list. 
    /// Each node represents a user with User ID, Name, Age, and List of Friend IDs.
    /// </summary>
    public class UserNode
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<int> FriendIDs { get; set; }
        public UserNode Next { get; set; }

        public UserNode(int userID, string name, int age)
        {
            UserID = userID;
            Name = name;
            Age = age;
            FriendIDs = new List<int>();
            Next = null;
        }

        public override string ToString()
        {
            return $"[ID: {UserID}, Name: {Name}, Age: {Age}, Friends: {FriendIDs.Count}]";
        }
    }

    public class SocialMediaNetwork
    {
        private UserNode head;

        public SocialMediaNetwork()
        {
            head = null;
        }

        /// <summary>
        /// Add a new user
        /// </summary>
        public void AddUser(int userID, string name, int age)
        {
            UserNode newNode = new UserNode(userID, name, age);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                UserNode current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            Console.WriteLine($"User '{name}' (ID: {userID}) added to the network");
        }

        /// <summary>
        /// Search for a user by User ID
        /// </summary>
        public UserNode SearchByUserID(int userID)
        {
            UserNode current = head;
            while (current != null)
            {
                if (current.UserID == userID)
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }

        /// <summary>
        /// Search for a user by Name
        /// </summary>
        public List<UserNode> SearchByName(string name)
        {
            List<UserNode> results = new List<UserNode>();
            UserNode current = head;

            while (current != null)
            {
                if (current.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(current);
                }
                current = current.Next;
            }

            return results;
        }

        /// <summary>
        /// Add a friend connection between two users
        /// </summary>
        public void AddFriendConnection(int userID1, int userID2)
        {
            UserNode user1 = SearchByUserID(userID1);
            UserNode user2 = SearchByUserID(userID2);

            if (user1 == null || user2 == null)
            {
                Console.WriteLine("One or both users not found");
                return;
            }

            if (!user1.FriendIDs.Contains(userID2))
            {
                user1.FriendIDs.Add(userID2);
            }

            if (!user2.FriendIDs.Contains(userID1))
            {
                user2.FriendIDs.Add(userID1);
            }

            Console.WriteLine($"{user1.Name} and {user2.Name} are now friends");
        }

        /// <summary>
        /// Remove a friend connection
        /// </summary>
        public void RemoveFriendConnection(int userID1, int userID2)
        {
            UserNode user1 = SearchByUserID(userID1);
            UserNode user2 = SearchByUserID(userID2);

            if (user1 == null || user2 == null)
            {
                Console.WriteLine("One or both users not found");
                return;
            }

            user1.FriendIDs.Remove(userID2);
            user2.FriendIDs.Remove(userID1);

            Console.WriteLine($"Friendship between {user1.Name} and {user2.Name} removed");
        }

        /// <summary>
        /// Find mutual friends between two users
        /// </summary>
        public List<int> FindMutualFriends(int userID1, int userID2)
        {
            UserNode user1 = SearchByUserID(userID1);
            UserNode user2 = SearchByUserID(userID2);

            List<int> mutualFriends = new List<int>();

            if (user1 == null || user2 == null)
            {
                Console.WriteLine("One or both users not found");
                return mutualFriends;
            }

            foreach (int friendID in user1.FriendIDs)
            {
                if (user2.FriendIDs.Contains(friendID))
                {
                    mutualFriends.Add(friendID);
                }
            }

            return mutualFriends;
        }

        /// <summary>
        /// Display all friends of a specific user
        /// </summary>
        public void DisplayFriendsOfUser(int userID)
        {
            UserNode user = SearchByUserID(userID);

            if (user == null)
            {
                Console.WriteLine("User not found");
                return;
            }

            Console.WriteLine($"\nFriends of {user.Name}:");
            if (user.FriendIDs.Count == 0)
            {
                Console.WriteLine("  No friends");
            }
            else
            {
                foreach (int friendID in user.FriendIDs)
                {
                    UserNode friend = SearchByUserID(friendID);
                    if (friend != null)
                    {
                        Console.WriteLine($"  - {friend.Name} (ID: {friendID})");
                    }
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Count the number of friends for a user
        /// </summary>
        public int CountFriendsOfUser(int userID)
        {
            UserNode user = SearchByUserID(userID);
            return user != null ? user.FriendIDs.Count : 0;
        }

        /// <summary>
        /// Display all users and their friend counts
        /// </summary>
        public void DisplayAllUsers()
        {
            if (head == null)
            {
                Console.WriteLine("No users in the network");
                return;
            }

            Console.WriteLine("\n--- Social Media Network ---");
            UserNode current = head;
            int count = 1;

            while (current != null)
            {
                Console.WriteLine($"{count}. {current}");
                current = current.Next;
                count++;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Display network statistics
        /// </summary>
        public void DisplayNetworkStatistics()
        {
            if (head == null)
            {
                Console.WriteLine("Network is empty");
                return;
            }

            int totalUsers = 0;
            int totalConnections = 0;
            UserNode current = head;

            while (current != null)
            {
                totalUsers++;
                totalConnections += current.FriendIDs.Count;
                current = current.Next;
            }

            Console.WriteLine("\n=== Network Statistics ===");
            Console.WriteLine($"Total Users: {totalUsers}");
            Console.WriteLine($"Total Friend Connections: {totalConnections / 2}"); // Divide by 2 because each connection is counted twice
            Console.WriteLine();
        }
    }

    // Example Usage
    public class SocialMediaNetworkExample
    {
        public static void Main()
        {
            SocialMediaNetwork network = new SocialMediaNetwork();

            // Add users
            network.AddUser(1, "Alice", 25);
            network.AddUser(2, "Bob", 26);
            network.AddUser(3, "Charlie", 24);
            network.AddUser(4, "Diana", 27);
            network.AddUser(5, "Eve", 25);

            // Add friend connections
            network.AddFriendConnection(1, 2);
            network.AddFriendConnection(1, 3);
            network.AddFriendConnection(2, 3);
            network.AddFriendConnection(2, 4);
            network.AddFriendConnection(3, 5);
            network.AddFriendConnection(4, 5);

            network.DisplayAllUsers();
            network.DisplayNetworkStatistics();

            // Display friends
            network.DisplayFriendsOfUser(1);
            network.DisplayFriendsOfUser(2);

            // Find mutual friends
            Console.WriteLine("Mutual friends between Alice (1) and Bob (2):");
            var mutualFriends = network.FindMutualFriends(1, 2);
            foreach (int friendID in mutualFriends)
            {
                UserNode friend = network.SearchByUserID(friendID);
                Console.WriteLine($"  - {friend.Name}");
            }

            // Remove a friend connection
            network.RemoveFriendConnection(1, 2);

            network.DisplayFriendsOfUser(1);
        }
    }
}
