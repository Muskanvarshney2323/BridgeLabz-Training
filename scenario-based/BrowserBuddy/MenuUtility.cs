using System;

public class MenuUtility
{
    private BrowserManager browser = new BrowserManager();

    public void ShowMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n--- BrowserBuddy Menu ---");
            Console.WriteLine("1. Open New Tab");
            Console.WriteLine("2. Visit Page");
            Console.WriteLine("3. Back");
            Console.WriteLine("4. Forward");
            Console.WriteLine("5. Close Current Tab");
            Console.WriteLine("6. Restore Closed Tab");
            Console.WriteLine("7. Show Current Page");
            Console.WriteLine("8. Show All Tabs");
            Console.WriteLine("9. Exit");

            Console.Write("Enter choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    browser.OpenNewTab();
                    break;
                case 2:
                    Console.Write("Enter URL: ");
                    string url = Console.ReadLine();
                    browser.GetCurrentTab()?.VisitPage(url);
                    break;
                case 3:
                    browser.GetCurrentTab()?.Back();
                    break;
                case 4:
                    browser.GetCurrentTab()?.Forward();
                    break;
                case 5:
                    browser.CloseCurrentTab();
                    break;
                case 6:
                    browser.RestoreClosedTab();
                    break;
                case 7:
                    browser.ShowCurrentPage();
                    break;
                case 8:
                    browser.ShowAllTabs();
                    break;
                case 9:
                    exit = true;
                    break;
            }
        }
    }
}
