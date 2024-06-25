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
    public static StartScreen StartScreen = new StartScreen();
    public static DeathScreen DeathScreen = new DeathScreen();
    public static VictoryScreen VictoryScreen = new VictoryScreen();

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
            RefreshEnemyLists();
            EnemyActions();
            CurrentLevel.RefreshMap();
            Hud.PrintMonitors();
            CurrentLevel.PrintCurrentMapState();
            if (levelNum == 11)
            {
                Player.CurHP = 0;
            }
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
        foreach (RangedEnemy rangeEnemy in CurrentLevel.InteractablesLists.RangedEnemiesPresent)
        {
                rangeEnemy.CycleShooting();
                CurrentLevel.RefreshMap();
                Hud.PrintMonitors();
        }
        if (CurrentLevel.InteractablesLists.RangedEnemiesPresent.Count > 0)
        {
            CurrentLevel.CycleRangedEnemiesStateTracker();
        }
        foreach (RangedMiniBoss rangedMiniBoss in CurrentLevel.InteractablesLists.RangedMiniBossPresent)
        {
            rangedMiniBoss.SpotAndShoot();
            CurrentLevel.RefreshMap();
            Hud.PrintMonitors();
        }
        foreach (HeavyEnemy heavy in CurrentLevel.InteractablesLists.HeavyEnemiesPresent)
        {
            heavy.HeavyEnemyMove();
            CurrentLevel.RefreshMap();
            Hud.PrintMonitors();
        }
        foreach (HeavyEnemy heavy in CurrentLevel.InteractablesLists.HeavyEnemiesPresent)
        {
            heavy.HeavyEnemyAttack();
            CurrentLevel.RefreshMap();
            Hud.PrintMonitors();
        }
        foreach (TiltanBoss tiltanBoss in CurrentLevel.InteractablesLists.BossList)
        {
            tiltanBoss.AdvanceBossMoveCounter();
            CurrentLevel.RefreshMap();
            Hud.PrintMonitors();
            tiltanBoss.BossAttack();
            CurrentLevel.RefreshMap();
            Hud.PrintMonitors();
        }
    }
    public static void RefreshEnemyLists()
    {
        List<StandardEnemy> SEList = new List<StandardEnemy>();
        List<RangedEnemy> REList = new List<RangedEnemy>();
        List<RangedMiniBoss> RMBList = new List<RangedMiniBoss>();
        List<HeavyEnemy> HEList = new List<HeavyEnemy>();
        List<TiltanBoss> TBList = new List<TiltanBoss>();
        foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)
        {
            if (!standardEnemy.IsDead())
            {
                SEList.Add(standardEnemy);
            }
        }
        foreach (RangedEnemy rangeEnemy in CurrentLevel.InteractablesLists.RangedEnemiesPresent)
        {
            if (!rangeEnemy.IsDead())
            {
                REList.Add(rangeEnemy);
            }
        }
        foreach (RangedMiniBoss rangedMiniBoss in CurrentLevel.InteractablesLists.RangedMiniBossPresent)
        {
            if (!rangedMiniBoss.IsDead())
            {
                RMBList.Add(rangedMiniBoss);
            }
        }
        foreach (HeavyEnemy heavy in CurrentLevel.InteractablesLists.HeavyEnemiesPresent)
        {
            if (!heavy.IsDead())
            {
                HEList.Add(heavy);
            }
        }
        foreach (TiltanBoss tiltanBoss in CurrentLevel.InteractablesLists.BossList)
        {
            if (!tiltanBoss.IsDead())
            {
                TBList.Add(tiltanBoss);
            }
        }
        CurrentLevel.InteractablesLists.StandardEnemiesPresent = SEList;
        CurrentLevel.InteractablesLists.RangedEnemiesPresent = REList;
        CurrentLevel.InteractablesLists.RangedMiniBossPresent = RMBList;
        CurrentLevel.InteractablesLists.HeavyEnemiesPresent = HEList;
        CurrentLevel.InteractablesLists.BossList = TBList;
    }
    public static void AdvanceLevel()
    {
        CurrentLevel = new Level(levelNum);
        GameLog.LogEvent("Advanced to Level " + levelNum);
    }
}