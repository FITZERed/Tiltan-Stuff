public class StandardEnemy
{

    public Point Position { get; set; }
    public int MaxHP;
    public int CurHP;
    public Weapon Weapon;

    public void EnemyActions()
    {
        EnemyMove();

    }

    public void EnemyAttack()
    {
        //make a pattern for this system, either enemies only attack from range 1 or they check if the player is within reach for complex weapons
    }
    public void EnemyMove()
    {
        string moveDirection = DetermineMovementDirection();
        if (moveDirection == "NoMove") return;
        if (moveDirection == "up") { Position.Y--; return; }
        if (moveDirection == "down") { Position.Y++; return; }
        if (moveDirection == "left") { Position.X--; return; }
        if (moveDirection == "right") { Position.X++; return; }
    }

    public string DetermineMovementDirection()
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
            return "NoMove";
        }
        if (GameManager.CurrentLevel.CurrentMapState[Position.Y - 1, Position.X] == TileENUM.Player)
        {
            return "NoMove";
        }
        if (GameManager.CurrentLevel.CurrentMapState[Position.Y, Position.X - 1] == TileENUM.Player)
        {
            return "NoMove";
        }
        if (GameManager.CurrentLevel.CurrentMapState[Position.Y, Position.X + 1] == TileENUM.Player)
        {
            return "NoMove";
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
        List<int> possibleDirections = new List<int>(4);
        //Up - 1
        //Left - 2
        //Down - 3
        //Right - 4
        if (priorityUp > 0)  possibleDirections[0] = 1; 
        if (priorityDown > 0)  possibleDirections[0] = 3;
        if (priorityLeft > 0) possibleDirections[1] = 2;
        if (priorityRight > 0) possibleDirections[1] = 4;
        int AIChoice;
        AIChoice = Random.Shared.Next(0, possibleDirections.Count);
        if (possibleDirections[AIChoice] == 1) return "up";
        if (possibleDirections[AIChoice] == 2) return "left";
        if (possibleDirections[AIChoice] == 3) return "down";
        if (possibleDirections[AIChoice] == 4) return "right";
        throw new Exception("enemy movement failed");
    }
}