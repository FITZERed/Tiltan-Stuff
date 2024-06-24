using System.Diagnostics;

public class InteractablesLists
{
    public List<StandardEnemy> StandardEnemiesPresent = new List<StandardEnemy>();
    public List<RangedEnemy> RangedEnemiesPresent = new List<RangedEnemy>();
    public List<RangedMiniBoss> RangedMiniBossPresent = new List<RangedMiniBoss>();
    public List<HeavyEnemy> HeavyEnemiesPresent = new List<HeavyEnemy>();
    public List<TiltanBoss> BossList = new List<TiltanBoss>();

    public bool FindEnemy(Point point, out StandardEnemy standardEnemy)
    {
        foreach (var enemy in StandardEnemiesPresent)
        {
            if (enemy.Position.X == point.X && enemy.Position.Y == point.Y) 
            { 
            standardEnemy = enemy;
            return true;
            }
        }
        standardEnemy = null;
        return false;

    }
    public bool FindEnemy(Point point, out RangedEnemy rangedEnemy)
    {
        foreach (var enemy in RangedEnemiesPresent)
        {
            if (enemy.Position.X == point.X && enemy.Position.Y == point.Y)
            {
                rangedEnemy = enemy;
                return true;
            }
        }
        rangedEnemy = null;
        return false;
    }
    public bool FindEnemy(Point point, out RangedMiniBoss rangedMiniBoss)
    {
        foreach (var enemy in RangedMiniBossPresent)
        {
            if (enemy.Position.X == point.X && enemy.Position.Y == point.Y)
            {
                rangedMiniBoss = enemy;
                return true;
            }
        }
        rangedMiniBoss = null;
        return false;
    }
    public bool FindEnemy(Point point, out HeavyEnemy heavyEnemy)
    {
        foreach (var enemy in HeavyEnemiesPresent)
        {
            if (enemy.Position.X == point.X && enemy.Position.Y == point.Y)
            {
                heavyEnemy = enemy;
                return true;
            }
        }
        heavyEnemy = null;
        return false;

    }
    public bool FindEnemy(Point point, out TiltanBoss tiltanBoss)
    {
        foreach (var enemy in BossList)
        {
            if ((enemy.Position.X - 1 <= point.X && enemy.Position.X + 1 >= point.X) && (enemy.Position.Y - 1 <= point.Y && enemy.Position.Y + 1 >= point.Y))
            {
                tiltanBoss = enemy;
                return true;
            }
        }
        tiltanBoss = null;
        return false;

    }

    public List<Chest> ChestsPresent = new List<Chest>();
}