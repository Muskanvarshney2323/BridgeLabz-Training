public interface IHistory
{
    void VisitPage(string url);
    void GoBack();
    void GoForward();
    void GetCurrentPage();
}