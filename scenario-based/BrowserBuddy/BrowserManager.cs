using System;
using System.Collections.Generic;

public class BrowserManager : ITab
{
    private List<Tab> openTabs;
    private Tab currentTab;
    private ClosedTabStack closedTabs;

    public BrowserManager()
    {
        openTabs = new List<Tab>();
        closedTabs = new ClosedTabStack();
    }

    public void OpenNewTab()
    {
        Tab tab = new Tab();
        openTabs.Add(tab);
        currentTab = tab;
        Console.WriteLine("New tab opened.");
    }

    public void CloseCurrentTab()
    {
        if (currentTab == null)
        {
            Console.WriteLine("No tab to close.");
            return;
        }

        openTabs.Remove(currentTab);
        closedTabs.Push(currentTab);
        currentTab = openTabs.Count > 0 ? openTabs[openTabs.Count - 1] : null;

        Console.WriteLine("Current tab closed.");
    }

    public void RestoreClosedTab()
    {
        Tab tab = closedTabs.Pop();
        if (tab == null)
        {
            Console.WriteLine("No closed tab to restore.");
            return;
        }

        openTabs.Add(tab);
        currentTab = tab;
        Console.WriteLine("Closed tab restored.");
    }

    public void ShowCurrentPage()
    {
        if (currentTab == null)
        {
            Console.WriteLine("No active tab.");
            return;
        }

        currentTab.GetCurrentPage();
    }

    public Tab GetCurrentTab()
    {
        return currentTab;
    }

    public void ShowAllTabs()
    {
        Console.WriteLine("Open Tabs Count: " + openTabs.Count);
    }
}
