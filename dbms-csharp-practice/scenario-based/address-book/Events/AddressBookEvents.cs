using AddressBookSystem.Models;
using System.Threading;

namespace AddressBookSystem.Events
{
    /// <summary>
    /// Custom event arguments for address book operations
    /// </summary>
    public class ContactEventArgs : EventArgs
    {
        public string OperationType { get; set; }
        public Person? Contact { get; set; }
        public DateTime Timestamp { get; set; }

        public ContactEventArgs()
        {
            OperationType = "";
            Contact = null;
            Timestamp = DateTime.Now;
        }

        public ContactEventArgs(string operation, Person? contact)
        {
            OperationType = operation;
            Contact = contact;
            Timestamp = DateTime.Now;
        }
    }

    /// <summary>
    /// Event handler delegate for address book operations
    /// </summary>
    public delegate void ContactEventHandler(object sender, ContactEventArgs e);

    /// <summary>
    /// Address book event publisher demonstrating events and multithreading
    /// </summary>
    public class AddressBookEventPublisher
    {
        // Events
        public event ContactEventHandler? OnContactAdded;
        public event ContactEventHandler? OnContactDeleted;
        public event ContactEventHandler? OnContactModified;

        private object lockObject = new object();

        public void PublishContactAdded(Person contact)
        {
            lock (lockObject)
            {
                Thread.Sleep(50); // Simulate processing
                OnContactAdded?.Invoke(this, new ContactEventArgs("ContactAdded", contact));
            }
        }

        public void PublishContactDeleted(Person contact)
        {
            lock (lockObject)
            {
                Thread.Sleep(50); // Simulate processing
                OnContactDeleted?.Invoke(this, new ContactEventArgs("ContactDeleted", contact));
            }
        }

        public void PublishContactModified(Person contact)
        {
            lock (lockObject)
            {
                Thread.Sleep(50); // Simulate processing
                OnContactModified?.Invoke(this, new ContactEventArgs("ContactModified", contact));
            }
        }

        /// <summary>
        /// Async operation that publishes events on different threads
        /// </summary>
        public async Task ProcessContactsAsync(List<Person> contacts)
        {
            await Task.Run(() =>
            {
                foreach (var contact in contacts)
                {
                    PublishContactAdded(contact);
                }
            });
        }
    }

    /// <summary>
    /// Event listener that handles address book events
    /// </summary>
    public class AddressBookEventListener
    {
        private Queue<ContactEventArgs> eventQueue;

        public AddressBookEventListener()
        {
            eventQueue = new Queue<ContactEventArgs>();
        }

        public void OnContactAdded(object sender, ContactEventArgs e)
        {
            lock (eventQueue)
            {
                eventQueue.Enqueue(e);
                Console.WriteLine($"[EVENT] Contact Added: {e.Contact?.FirstName} {e.Contact?.LastName} at {e.Timestamp:yyyy-MM-dd HH:mm:ss}");
            }
        }

        public void OnContactDeleted(object sender, ContactEventArgs e)
        {
            lock (eventQueue)
            {
                eventQueue.Enqueue(e);
                Console.WriteLine($"[EVENT] Contact Deleted: {e.Contact?.FirstName} {e.Contact?.LastName} at {e.Timestamp:yyyy-MM-dd HH:mm:ss}");
            }
        }

        public void OnContactModified(object sender, ContactEventArgs e)
        {
            lock (eventQueue)
            {
                eventQueue.Enqueue(e);
                Console.WriteLine($"[EVENT] Contact Modified: {e.Contact?.FirstName} {e.Contact?.LastName} at {e.Timestamp:yyyy-MM-dd HH:mm:ss}");
            }
        }

        public List<ContactEventArgs> GetEventLog()
        {
            lock (eventQueue)
            {
                return new List<ContactEventArgs>(eventQueue);
            }
        }

        public void ClearEventLog()
        {
            lock (eventQueue)
            {
                eventQueue.Clear();
            }
        }
    }
}
