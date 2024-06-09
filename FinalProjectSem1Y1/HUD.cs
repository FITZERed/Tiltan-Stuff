public class HUD
{
    public int PlayerHP { get { return GameManager.CurrentLevel.Player.CurHP; } }

    public void PrintMonitors()
    {
        Console.SetCursorPosition(0, 15);
        Console.Write("Player Health: " + PlayerHP + "             ");
    }
}