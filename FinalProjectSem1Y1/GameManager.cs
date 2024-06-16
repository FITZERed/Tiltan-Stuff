global using static GameManager;
using System.Text;
public  static class GameManager
{
    public static Level CurrentLevel;
    public static Player Player;
    public static Inventory Inventory;
    public static GameLog GameLog;
    public static HUD Hud;
    public static int levelNum = 1;

    public static void StartGame()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Player = new Player();
        Inventory = new Inventory();
        //adding spear could be here
        CurrentLevel = new Level(levelNum);
        GameLog = new GameLog();
        Hud = new HUD();
        Hud.PrintMonitors();
        GameLog.InitEventLog();
        GameLog.PrintLog();
        while (!Player.IsDead())
        {
            Player.PlayerInput();
            CurrentLevel.RefreshMap();
            //here account for more things player input may cause
            //then put environmental changes
            EnemyActions();
            CurrentLevel.RefreshMap();
            Hud.PrintMonitors();
            CurrentLevel.PrintCurrentMapState();
        }
    }
    public static void StartNewLevel()
    {

    }
    public static void EnemyActions()
    {
        foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)
        {
            standardEnemy.EnemyMove();
            CurrentLevel.RefreshMap();
            Hud.PrintMonitors();
        }
        foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)
        {
            standardEnemy.EnemyAttack();
            CurrentLevel.RefreshMap();
            Hud.PrintMonitors();
        }
    }
    public static void AdvanceLevel()
    {
        CurrentLevel = new Level(levelNum);
        GameLog.LogEvent("Advanced to Level " + levelNum);
    }
}