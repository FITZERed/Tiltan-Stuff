public class StartScreen
{
    public void DisplayStartScreen()
    {
        Console.SetCursorPosition(15, 10);
        Console.Write("                     This is the Start Screen");
        Console.SetCursorPosition(15, 11);
        Console.Write("        Here I'll explain the controls, and send you on your way.");
        Console.SetCursorPosition(15, 13);
        Console.Write("           WASD are used for movement, probably as you expect...");
        Console.SetCursorPosition(15, 14);
        Console.Write("            Spacebar is used to attack, when pressing it the game");
        Console.SetCursorPosition(15, 15);
        Console.Write(" will ask you to input a direction for the attack, I'm sure you'll do fine.");
        Console.SetCursorPosition(15, 17);
        Console.Write("   E is used to open chests, and will ask a directional input as well.");
        Console.SetCursorPosition(15, 19);
        Console.Write("          X is used to switch weapons, once you obtain new ones.");
        Console.SetCursorPosition(15, 20);
        Console.Write("        H is to use a healing potion, when you have one of course.");
        Console.SetCursorPosition(15, 22);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("                      Press any key to start");
        Console.ForegroundColor = ConsoleColor.White;
        Console.ReadKey();
        Console.Clear();
    }
}