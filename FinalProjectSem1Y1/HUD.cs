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
        Console.Write("Equipped Weapon: " + GameManager.Player.CurrentWeapon.WeaponName + "                          ");
        line++;
        Console.SetCursorPosition(0, line);
        Console.Write("Weapons in inventory: ");
        line++;
        Console.SetCursorPosition(0, line);
        for (int i = 0; i < GameManager.Inventory.ObtainedWeapons.Count; i++)
        {
            Console.Write(i+1 + ". " + GameManager.Inventory.ObtainedWeapons[i].WeaponName);
            line++;
            Console.SetCursorPosition(0, line);
        }
    }
}