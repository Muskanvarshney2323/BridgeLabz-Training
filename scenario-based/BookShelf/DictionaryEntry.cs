using System;

public class DictionaryEntry
{
    public string Key { get; set; }
    public BookLinkedList Value { get; set; }

    public DictionaryEntry(string key, BookLinkedList value)
    {
        this.Key = key;
        this.Value = value;
    }
}
