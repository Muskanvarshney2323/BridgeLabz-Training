using System;
public class FlashDealzApp
{
    public void Start()
    {
        Console.WriteLine("=======Welcome to FlashDealz!========");
        FlashDealzMenu menu = new FlashDealzMenu();
        menu.ShowMenu();
    }
}