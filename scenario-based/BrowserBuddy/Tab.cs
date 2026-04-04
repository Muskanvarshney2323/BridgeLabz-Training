using System;

public class Tab : ITab
{
    private DoublyLinkedList history;

    public Tab()
    {
        history = new DoublyLinkedList();
    }

    public void VisitPage(string url)
    {
        history.VisitPage(url);
    }

    public void Back()
    {
        history.GoBack();
    }

    public void Forward()
    {
        history.GoForward();
    }

    public void GetCurrentPage()
    {
        history.GetCurrentPage();
    }

    public void OpenNewTab()
    {
        throw new NotImplementedException();
    }

    public void CloseCurrentTab()
    {
        throw new NotImplementedException();
    }

    public void RestoreClosedTab()
    {
        throw new NotImplementedException();
    }

    public void ShowCurrentPage()
    {
        GetCurrentPage();
    }
}
