public class DeathScreen
{
    public void DisplayDeathScreen()
    {
        Console.Clear();
        Console.SetCursorPosition(15, 15);
        Console.WriteLine("You died, press any key to try again.");
        Console.ReadKey();
        Console.Clear();
    }
}