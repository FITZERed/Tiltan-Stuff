public  static class GameManager
{
    public static Level CurrentLevel;
    public static Inventory Inventory;
    public static GameLog GameLog;

    public static void StartGame()
    {
        int levelNum = 1;
        Inventory = new Inventory();
        //adding spear could be here
        CurrentLevel = new Level(levelNum);
        GameLog = new GameLog();
        GameLog.InitEventLog();
        GameLog.PrintLog();
        while (!CurrentLevel.Player.IsDead())
        {
            CurrentLevel.Player.PlayerInput();
            CurrentLevel.RefreshMap();
            //here account for more things player input may cause
            //then put environmental changes
            EnemyActions();
            CurrentLevel.RefreshMap();
            CurrentLevel.PrintCurrentMapState();
        }
    }
    public static void StartNewLevel()
    {

    }
    public static void EnemyActions()
    {
        foreach (StandardEnemy standardEnemy in GameManager.CurrentLevel.EnemyLists.StandardEnemiesPresent)
        {
            standardEnemy.EnemyMove();
            CurrentLevel.RefreshMap();
        }

    }
}