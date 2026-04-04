using AddressBookSystem.Menu;

namespace AddressBookSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            AddressBookMenu menu = new AddressBookMenu();
            menu.ShowMenu();
        }
    }
}
