public class GameManager
{
    public static Level CurrentLevel;
    public static Inventory Inventory;
    public static void StartGame()
    {
        int levelNum = 1;
        Inventory = new Inventory();
        Inventory.ObtainedWeapons.Add(new Weapon(WeaponType.Spear));
        CurrentLevel = new Level(levelNum);
        while (!CurrentLevel.Player.IsDead())
        {
            CurrentLevel.Player.PlayerInput();
            //here account for more things player input may cause
            //then put environmental changes
            CurrentLevel.RefreshMap();
            CurrentLevel.PrintCurrentMapState();
        }
    }
    public static void StartNewLevel()
    {

    }
}