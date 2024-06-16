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
        return true;
    }

    public Exit(Point position)
    {
        Position = position;
    }
}