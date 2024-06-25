public class Exit
{
    public Point Position { get; set; }
    public bool IsAvailable()
    {
        foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)//adding all enemy lists required
        {
            if (!standardEnemy.IsDead())
            {
                return false;
            }
        }
        foreach (RangedEnemy rangedEnemy in CurrentLevel.InteractablesLists.RangedEnemiesPresent)
        {
            if (!rangedEnemy.IsDead())
            {
                return false;
            }
        }
        foreach (RangedMiniBoss miniBoss in CurrentLevel.InteractablesLists.RangedMiniBossPresent)
        {
            if (!miniBoss.IsDead())
            {
                return false;
            }
        }
        foreach (HeavyEnemy heavy in CurrentLevel.InteractablesLists.HeavyEnemiesPresent)
        {
            if (!heavy.IsDead())
            {
                return false;
            }
        }
        foreach (TiltanBoss boss in CurrentLevel.InteractablesLists.BossList)
        {
            if (!boss.IsDead())
            {
                return false;
            }
        }
        return true;
    }

    public Exit(Point position)
    {
        Position = position;
    }
}