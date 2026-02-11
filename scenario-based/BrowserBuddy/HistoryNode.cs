public class HistoryNode
{
    public string url; // url of webpage
    public HistoryNode prev; //pointer to previous node
    public HistoryNode next; //pointer to next node
    public HistoryNode(string url) //Constructor
    {
        this.url = url;
        this.prev = null;
        this.next = null;
    }
}