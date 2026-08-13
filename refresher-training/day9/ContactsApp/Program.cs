using ContactsApp.Data;
using ContactsApp.Models;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer(
        "Server=localhost,1433;Database=ContactsDb;User Id=sa;Password=Password@123;TrustServerCertificate=True;"
    )
    .Options;

using var db = new AppDbContext(options);

Console.WriteLine("===== CONTACT APP =====");

while (true)
{
    Console.WriteLine("\n1. Add Contact");
    Console.WriteLine("2. View Contacts");
    Console.WriteLine("3. Update Contact");
    Console.WriteLine("4. Delete Contact");
    Console.WriteLine("5. Exit");

    Console.Write("Enter choice: ");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddContact();
            break;

        case "2":
            ViewContacts();
            break;

        case "3":
            UpdateContact();
            break;

        case "4":
            DeleteContact();
            break;

        case "5":
            return;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

void AddContact()
{
    Console.Write("Enter name: ");
    string name = Console.ReadLine() ?? "";

    Console.Write("Enter phone: ");
    string phone = Console.ReadLine() ?? "";

    Console.Write("Enter email: ");
    string email = Console.ReadLine() ?? "";

    Contact contact = new Contact
    {
        Name = name,
        Phone = phone,
        Email = email
    };

    db.Contacts.Add(contact);
    db.SaveChanges();

    Console.WriteLine("Contact added successfully.");
}

void ViewContacts()
{
    var contacts = db.Contacts.ToList();

    foreach (var contact in contacts)
    {
        Console.WriteLine(
            $"Id: {contact.Id}, Name: {contact.Name}, Phone: {contact.Phone}, Email: {contact.Email}"
        );
    }
}

void UpdateContact()
{
    Console.Write("Enter contact ID: ");
    int id = Convert.ToInt32(Console.ReadLine());

    var contact = db.Contacts.Find(id);

    if (contact == null)
    {
        Console.WriteLine("Contact not found.");
        return;
    }

    Console.Write("Enter new name: ");
    contact.Name = Console.ReadLine() ?? "";

    Console.Write("Enter new phone: ");
    contact.Phone = Console.ReadLine() ?? "";

    Console.Write("Enter new email: ");
    contact.Email = Console.ReadLine() ?? "";

    db.SaveChanges();

    Console.WriteLine("Contact updated successfully.");
}

void DeleteContact()
{
    Console.Write("Enter contact ID: ");
    int id = Convert.ToInt32(Console.ReadLine());

    var contact = db.Contacts.Find(id);

    if (contact == null)
    {
        Console.WriteLine("Contact not found.");
        return;
    }

    db.Contacts.Remove(contact);
    db.SaveChanges();

    Console.WriteLine("Contact deleted successfully.");
}