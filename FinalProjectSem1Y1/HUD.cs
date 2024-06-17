public class HUD
{
    public int PlayerHP { get { return CurrentLevel.Player.CurHP; } }
    public int HealingPotionsStock { get { return GameManager.Inventory.HealingPotions; } }

    public void PrintMonitors()
    {
        int line = 15;
        Console.SetCursorPosition(0, line);
        Console.Write("Player Health: " + PlayerHP + "             ");
        line++;
        Console.SetCursorPosition(0, line);
        Console.Write("Level: " + levelNum + "             ");
        line++;
        Console.SetCursorPosition(0, line);
        Console.Write("Healing Potions: " + HealingPotionsStock + "                  ");
        line++;
        Console.SetCursorPosition(0, line);
        Console.Write("Weapons in inventory: ");
        foreach (Weapon weapon in GameManager.Inventory.ObtainedWeapons)
        {
            Console.Write(weapon.WeaponName + ", ");
        }
    }
}