using System;
using System.Collections.Generic;

namespace OopsPractice.EncapsulationAbstractionPolymorphism
{
    interface IReservable
    {
        bool ReserveItem(string userId);
        bool CheckAvailability();
    }

    abstract class LibraryItem
    {
        private string _itemId;
        private string _title;
        private string _author;

        protected LibraryItem(string id, string title, string author)
        {
            _itemId = id; _title = title; _author = author;
        }

        public string ItemId => _itemId;
        public string Title => _title;
        public string Author => _author;

        public abstract int GetLoanDuration();

        public void GetItemDetails() => Console.WriteLine($"{Title} by {Author} (Id:{ItemId})");
    }

    class Book : LibraryItem, IReservable
    {
        private bool _reserved;
        public Book(string id, string t, string a) : base(id,t,a) { }
        public override int GetLoanDuration() => 21;
        public bool ReserveItem(string userId) { _reserved = true; return true; }
        public bool CheckAvailability() => !_reserved;
    }

    class Magazine : LibraryItem
    {
        public Magazine(string id, string t, string a) : base(id,t,a) { }
        public override int GetLoanDuration() => 7;
    }

    class DVD : LibraryItem
    {
        public DVD(string id, string t, string a) : base(id,t,a) { }
        public override int GetLoanDuration() => 14;
    }

    class Program
    {
        static void Main()
        {
            var items = new List<LibraryItem>
            {
                new Book("B001","C# in Depth","Jon"), new Magazine("M001","Time","Several"), new DVD("D001","Inception","Nolan")
            };
            foreach(var it in items)
            {
                it.GetItemDetails();
                Console.WriteLine($" Loan days: {it.GetLoanDuration()}");
            }
        }
    }
}