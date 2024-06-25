public class VictoryScreen
{
    public void DisplayVictoryScreen()
    {
        Console.Clear();
        Console.SetCursorPosition(15, 15);
        Console.Write("         You Win, Congratulations");
        Console.SetCursorPosition(15, 16);
        Console.Write("Press any key to go back to the start screen");
        Console.ReadKey();
        Console.Clear();
    }
}