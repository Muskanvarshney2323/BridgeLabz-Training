using System.Collections.Generic;

public class ClosedTabStack
{
    private Stack<Tab> closedTabs = new Stack<Tab>();

    public void Push(Tab tab)
    {
        closedTabs.Push(tab);
    }

    public Tab Pop()
    {
        return closedTabs.Count > 0 ? closedTabs.Pop() : null;
    }
}
