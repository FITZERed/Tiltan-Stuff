public class StandardEnemy
{
    
    public Point Position { get; set; }
    public int MaxHP;
    public int CurHP;
    public Weapon Weapon;

    public void EnemyActions()
    {

    }

    public void DetermineMovementDirection()
    {
        int priorityUp = 0;
        int priorityDown = 0;
        int priorityLeft = 0;
        int priorityRight = 0;
        if (GameManager.CurrentLevel.CurrentMapState[Position.Y + 1, Position.X] == TileENUM.Wall)
        {
            priorityUp = -100;
        }
        if (GameManager.CurrentLevel.CurrentMapState[Position.Y - 1, Position.X] == TileENUM.Wall)
        {
            priorityDown = -100;
        }
        if (GameManager.CurrentLevel.CurrentMapState[Position.Y, Position.X - 1] == TileENUM.Wall)
        {
            priorityLeft = -100;
        }
        if (GameManager.CurrentLevel.CurrentMapState[Position.Y, Position.X + 1] == TileENUM.Wall)
        {
            priorityRight = -100;
        }
        if (GameManager.CurrentLevel.CurrentMapState[Position.Y + 1, Position.X] == TileENUM.Player)
        {
            return;
        }
        if (GameManager.CurrentLevel.CurrentMapState[Position.Y - 1, Position.X] == TileENUM.Player)
        {
            return;
        }
        if (GameManager.CurrentLevel.CurrentMapState[Position.Y, Position.X - 1] == TileENUM.Player)
        {
            return;
        }
        if (GameManager.CurrentLevel.CurrentMapState[Position.Y, Position.X + 1] == TileENUM.Player)
        {
            return;
        }
        if (GameManager.CurrentLevel.Player.Position.Y > Position.Y)
        {
            priorityDown += 1;
        }
        if (GameManager.CurrentLevel.Player.Position.Y < Position.Y)
        {
            priorityUp += 1;
        }
        if (GameManager.CurrentLevel.Player.Position.X > Position.X)
        {
            priorityRight += 1;
        }
        if (GameManager.CurrentLevel.Player.Position.X < Position.X)
        {
            priorityLeft += 1;
        }
        int[] possibleDirections = new int[2];
        //Up - 1
        //Left - 2
        //Down - 3
        //Right - 4
        if (priorityUp > 0)  possibleDirections[0] = 1; 
        if (priorityDown > 0)  possibleDirections[0] = 3;
        if (priorityLeft > 0) possibleDirections[1] = 2;
        if (priorityRight > 0) possibleDirections[1] = 4;
    }
}