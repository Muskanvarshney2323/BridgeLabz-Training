# Address Book System

## Overview
This is a comprehensive Address Book Management System built in C# that implements all 18 use cases with professional coding practices and advanced C# concepts.

## Project Structure

```
address-book/
├── Models/
│   ├── Person.cs                 # Contact person model
│   └── AddressBook.cs            # Address book container
├── Interfaces/
│   └── IAddressBook.cs           # Interface definitions
├── Services/
│   ├── AddressBookRepository.cs  # Core CRUD operations (Repository Pattern)
│   └── AddressBookUtilityImpl.cs  # Main system implementation
├── Utilities/
│   ├── InputValidator.cs         # Input validation with regex
│   ├── FileOperationHelper.cs    # CSV/JSON/JSONServer operations
│   ├── DatabaseHelper.cs         # Async database operations with reflection
│   └── SortingUtility.cs         # QuickSort implementation with delegates
├── Exceptions/
│   └── AddressBookException.cs   # Custom exception classes
├── Menu/
│   └── AddressBookMenu.cs        # Menu system
├── Program.cs                     # Entry point
└── AddressBookSystem.csproj       # Project file
```

## 18 Use Cases Implemented

### 1. Create Contact with Details
- First Name, Last Name, Address, City, State, Zip, Phone Number, Email

### 2. Add New Contact
- Single contact addition with duplicate checking

### 3. Edit Existing Contact
- Edit contact using first and last name

### 4. Delete Contact
- Delete contact using first and last name

### 5. Add Multiple Contacts
- Batch addition of multiple contacts

### 6. Multiple Address Books
- Support for multiple address books with unique names
- Switch between different address books

### 7. No Duplicate Entries
- Using Equals() and GetHashCode() override
- Prevents duplicate person entries in a single address book

### 8. Search by City/State Across Multiple Address Books
- Search for persons across all address books by city or state

### 9. View Persons by City/State
- Display all persons filtered by city or state

### 10. Count by City/State
- Get count of persons in a specific city or state

### 11. Sort by Name
- QuickSort algorithm for alphabetical sorting by first and last name

### 12. Sort by City/State/Zip
- QuickSort algorithm for sorting by city, state, and zip code

### 13. CSV File I/O
- Read from and write to CSV files with proper async operations

### 14. JSON File I/O
- Read from and write to JSON files using System.Text.Json

### 15. JSON Server I/O
- Read from and write to JSON Server using HttpClient

### 16. Database I/O
- Async database operations with reflection support

### 17. Non-Blocking I/O
- All file operations use async/await to prevent blocking main thread
- Uses Task.Wait() for synchronous context integration

### 18. Database & Extension Support
- Open/Closed Principle adherence with pluggable data sources
- Abstract IDataSource interface for future data sources
- Reflection used for property access without hard-coding

## Core C# Concepts Used

### OOP (Object-Oriented Programming)
- Encapsulation (private fields, public properties)
- Inheritance (base classes and virtual methods)
- Polymorphism (interface implementation)
- Abstraction (interfaces and abstract classes)

### Collections & Generics
- List<T> for dynamic collections
- Dictionary<string, T> for address book storage
- Generic type constraints in interfaces

### Exception Handling
- Custom exceptions for different error scenarios
- Try-catch blocks with specific exception handling
- Throwing and catching custom exceptions

### Delegates & Events
- SortingUtility uses delegates (PersonComparator) for flexible sorting
- Delegate pattern for comparison logic

### Reflection
- DatabaseHelper uses reflection to get/set property values
- PropertyInfo for dynamic property access
- BindingFlags for reflection configuration

### Multithreading & Async
- async/await for non-blocking I/O operations
- Task<T> for asynchronous operations
- Task.Wait() for synchronous integration

### Data Structures & Algorithms
- QuickSort implementation for efficient sorting
- Binary search capabilities for lookups
- Custom data structure patterns

### Attributes
- Custom attributes can be added for validation

### Regular Expressions
- Email validation using regex patterns
- Phone number format validation

## How to Run

1. **Build the project:**
   ```
   dotnet build
   ```

2. **Run the application:**
   ```
   dotnet run
   ```

3. **Main Menu Options:**
   - Create Address Book
   - Switch Address Book
   - Manage contacts (Add, Edit, Delete)
   - Search and view operations
   - Sort contacts
   - File I/O operations

## File Operations

### CSV Operations
- Comma-separated values format
- Header row with column names
- Async read/write operations

### JSON Operations
- JSON format using System.Text.Json
- Pretty-printed output
- Property name case-insensitive

### JSON Server
- Requires running JSON Server instance
- Default URL: `http://localhost:3000/contacts`
- HTTP GET/POST operations

### Database
- Async operations with reflection
- Extension point for real database implementation

## Advanced Features

1. **Input Validation**
   - Email format validation
   - Phone number validation (10+ digits)
   - Zip code validation (5+ digits)

2. **Error Handling**
   - Duplicate contact prevention
   - Contact not found handling
   - Invalid input handling
   - File operation error handling

3. **Data Integrity**
   - Using Equals() and GetHashCode() for duplicate detection
   - Proper exception propagation
   - Transaction-like behavior for batch operations

4. **Performance**
   - Efficient QuickSort implementation
   - Lazy loading of data
   - Async I/O to prevent blocking

## Design Patterns Used

1. **Repository Pattern** - AddressBookRepository
2. **Facade Pattern** - AddressBookUtilityImpl
3. **Delegate Pattern** - SortingUtility
4. **Strategy Pattern** - Different data sources (CSV, JSON, Database)
5. **Factory Pattern** - IDataSource implementations

## Example Usage

```csharp
// Create address book
system.AddAddressBook();

// Switch to address book
system.SwitchAddressBook();

// Add contact
contacts.AddContact();

// Search by city
contacts.SearchPersonByCityOrState();

// Sort contacts
contacts.SortContactsByName();

// Save to CSV
contacts.WriteAddressBookToCSV();
```

## Notes

- No LINQ is used as per requirements
- All operations follow proper C# conventions
- Thread-safe where applicable
- Extensible architecture for future enhancements
