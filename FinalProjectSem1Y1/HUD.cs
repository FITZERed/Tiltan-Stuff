public class HUD
{
    public int PlayerHP { get { return CurrentLevel.Player.CurHP; } }
    public int HealingPotionsStock { get { return GameManager.Inventory.HealingPotions; } }

    public void PrintMonitors()
    {
        Console.SetCursorPosition(0, 15);
        Console.Write("Player Health: " + PlayerHP + "             ");
        Console.SetCursorPosition(0, 16);
        Console.Write("Level: " + levelNum + "             ");
        Console.SetCursorPosition(0, 17);
        Console.Write("Healing Potions: " + HealingPotionsStock + "                  ");
    }
}