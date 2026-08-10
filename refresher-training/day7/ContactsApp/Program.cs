using ContactsApp.Models;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

List<Contact> contacts = new List<Contact>
{
    new Contact
    {
        Id = 1,
        Name = "Muskan",
        Phone = "9876543210",
        Email = "muskan@gmail.com"
    },
    new Contact
    {
        Id = 2,
        Name = "Rahul",
        Phone = "9876501234",
        Email = "rahul@gmail.com"
    }
};

// GET - Get all contacts
app.MapGet("/contacts", () =>
{
    return Results.Ok(contacts);
});

// GET - Get contact by ID
app.MapGet("/contacts/{id}", (int id) =>
{
    var contact = contacts.FirstOrDefault(c => c.Id == id);

    if (contact == null)
        return Results.NotFound("Contact not found");

    return Results.Ok(contact);
});

// POST - Add contact
app.MapPost("/contacts", (Contact contact) =>
{
    contact.Id = contacts.Count == 0 ? 1 : contacts.Max(c => c.Id) + 1;

    contacts.Add(contact);

    return Results.Created($"/contacts/{contact.Id}", contact);
});

// PUT - Update contact
app.MapPut("/contacts/{id}", (int id, Contact updatedContact) =>
{
    var contact = contacts.FirstOrDefault(c => c.Id == id);

    if (contact == null)
        return Results.NotFound("Contact not found");

    contact.Name = updatedContact.Name;
    contact.Phone = updatedContact.Phone;
    contact.Email = updatedContact.Email;

    return Results.Ok(contact);
});

// DELETE - Delete contact
app.MapDelete("/contacts/{id}", (int id) =>
{
    var contact = contacts.FirstOrDefault(c => c.Id == id);

    if (contact == null)
        return Results.NotFound("Contact not found");

    contacts.Remove(contact);

    return Results.Ok("Contact deleted successfully");
});

app.MapGet("/", () =>
{
    return "Welcome to Contacts App";
});
app.Run();