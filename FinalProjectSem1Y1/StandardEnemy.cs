using System.Security.Cryptography.X509Certificates;

public class StandardEnemy
{

    public Point Position { get; set; }
    public int MaxHP;
    public int CurHP;
    public Weapon Weapon;
    private Player Player { get { return GameManager.Player; } }
    public bool IsDead()
    {
        if (CurHP <= 0)
            return true;
        return false;
    }

    public StandardEnemy(Point point)
    {
        Position = point;
        MaxHP = 3;
        CurHP = MaxHP;
        //Weapon = new Weapon
    }
    public void EnemyAttack()
    {
        //make a pattern for this system, either enemies only attack from range 1 or they check if the player is within reach for complex weapons
        if (IsDead()) return;
        if (CurrentLevel.CurrentMapState[Position.Y - 1, Position.X] == TileENUM.Player)
        {
            Player.CurHP -= 1;
            GameManager.GameLog.LogEvent("Standard Enemy has hit Player for 1 damage");
            return;
        }
        else if (CurrentLevel.CurrentMapState[Position.Y + 1, Position.X] == TileENUM.Player)
        {
            Player.CurHP -= 1;
            GameManager.GameLog.LogEvent("Standard Enemy has hit Player for 1 damage");
            return;
        }
        else if (CurrentLevel.CurrentMapState[Position.Y, Position.X - 1] == TileENUM.Player)
        {
            Player.CurHP -= 1;
            GameManager.GameLog.LogEvent("Standard Enemy has hit Player for 1 damage");
            return;
        }
        else if (CurrentLevel.CurrentMapState[Position.Y, Position.X + 1] == TileENUM.Player)
        {
            Player.CurHP -= 1;
            GameManager.GameLog.LogEvent("Standard Enemy has hit Player for 1 damage");
            return;
        }
        else return;

    }
    public void EnemyMove()
    {
        if (IsDead()) {  return; }
        string moveDirection = DetermineMovementDirection();
        if (moveDirection == "NoMove") return;
        if (moveDirection == "up") { Position.Y--; return; }
        if (moveDirection == "down") { Position.Y++; return; }
        if (moveDirection == "left") { Position.X--; return; }
        if (moveDirection == "right") { Position.X++; return; }
    }

    public bool IsPlayerAdjacent()
    {
        if (CurrentLevel.CurrentMapState[Position.Y - 1, Position.X] == TileENUM.Player)
        {
            return true;
        }
        else if (CurrentLevel.CurrentMapState[Position.Y + 1, Position.X] == TileENUM.Player)
        {
            return true;
        }
        else if (CurrentLevel.CurrentMapState[Position.Y, Position.X - 1] == TileENUM.Player)
        {
            return true;
        }
        else if (CurrentLevel.CurrentMapState[Position.Y, Position.X + 1] == TileENUM.Player)
        {
            return true;
        }
        else return false;
    }

    public string DetermineMovementDirection()
    {
        int priorityUp = 0;
        int priorityDown = 0;
        int priorityLeft = 0;
        int priorityRight = 0;

        if (CurrentLevel.CurrentMapState[Position.Y - 1, Position.X] == TileENUM.Wall)
        {
            priorityUp = -100;
        }
        if (CurrentLevel.CurrentMapState[Position.Y + 1, Position.X] == TileENUM.Wall)
        {
            priorityDown = -100;
        }
        if (CurrentLevel.CurrentMapState[Position.Y, Position.X - 1] == TileENUM.Wall)
        {
            priorityLeft = -100;
        }
        if (CurrentLevel.CurrentMapState[Position.Y, Position.X + 1] == TileENUM.Wall)
        {
            priorityRight = -100;
        }

        if (CurrentLevel.CurrentMapState[Position.Y - 1, Position.X] == TileENUM.Player)
        {
            return "NoMove";
        }
        if (CurrentLevel.CurrentMapState[Position.Y + 1, Position.X] == TileENUM.Player)
        {
            return "NoMove";
        }
        if (CurrentLevel.CurrentMapState[Position.Y, Position.X - 1] == TileENUM.Player)
        {
            return "NoMove";
        }
        if (CurrentLevel.CurrentMapState[Position.Y, Position.X + 1] == TileENUM.Player)
        {
            return "NoMove";
        }
        if (CurrentLevel.CurrentMapState[Position.Y - 1, Position.X] == TileENUM.StandardEnemy)
        {
            priorityUp -= 50;
        }
        if (CurrentLevel.CurrentMapState[Position.Y + 1, Position.X] == TileENUM.StandardEnemy)
        {
            priorityDown -= 50;
        }
        if (CurrentLevel.CurrentMapState[Position.Y, Position.X - 1] == TileENUM.StandardEnemy)
        {
            priorityLeft -= 50;
        }
        if (CurrentLevel.CurrentMapState[Position.Y, Position.X + 1] == TileENUM.StandardEnemy)
        {
            priorityRight -= 50;
        }

        if (CurrentLevel.Player.Position.Y > Position.Y)
        {
            priorityDown += 1;
        }
        if (CurrentLevel.Player.Position.Y < Position.Y)
        {
            priorityUp += 1;
        }
        if (CurrentLevel.Player.Position.X > Position.X)
        {
            priorityRight += 1;
        }
        if (CurrentLevel.Player.Position.X < Position.X)
        {
            priorityLeft += 1;
        }

        List<int> possibleDirections = new List<int>();
        //Up - 1
        //Left - 2
        //Down - 3
        //Right - 4
        if (priorityUp > 0) possibleDirections.Add(1);
        if (priorityDown > 0) possibleDirections.Add(3);
        if (priorityLeft > 0) possibleDirections.Add(2);
        if (priorityRight > 0) possibleDirections.Add(4);

        if (possibleDirections.Count == 0)
        {
            return "NoMove";
        }

        int AIChoice = Random.Shared.Next(0, possibleDirections.Count);
        if (possibleDirections[AIChoice] == 1) return "up";
        if (possibleDirections[AIChoice] == 2) return "left";
        if (possibleDirections[AIChoice] == 3) return "down";
        if (possibleDirections[AIChoice] == 4) return "right";

        throw new Exception("enemy movement failed");
    }
}