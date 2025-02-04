using System;

class Program
{
    static void Main(string[] args)
    {
        StartMenu startMenu = new StartMenu();

        while (true)
        {
            startMenu.DisplayMenu();
        }
    }
}
