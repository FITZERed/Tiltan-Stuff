public class Exit
{
    public Point Position { get; set; }
    public bool IsAvailable()
    {
        foreach (StandardEnemy standardEnemy in GameManager.CurrentLevel.EnemyLists.StandardEnemiesPresent)//adding all enemy lists required
        {
            if (!standardEnemy.IsDead())
            {
                return false;
            }
        }
        return true;
    }
}