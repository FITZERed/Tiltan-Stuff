public class HeavyEnemy
{
    public Point Position { get; set; }
    public int MaxHP;
    public int CurHP;
    public int Damage;
    private Player Player { get { return GameManager.Player; } }
    private HeavyEnemyAttackState attackState;
    public bool IsDead()
    {
        if (CurHP <= 0)
            return true;
        return false;
    }
    public HeavyEnemy(Point point)
    {
        Position = point;
        MaxHP = 12;
        CurHP = MaxHP;
        Damage = 6;
        attackState = HeavyEnemyAttackState.NotReady;
    }

    public void HeavyEnemyAttack()
    {
        if (IsDead()) return;
        if (attackState == HeavyEnemyAttackState.AttackReadyLeft) PerformAttackLeft();
        else if (attackState == HeavyEnemyAttackState.AttackReadyRight) PerformAttackRight();
        else if (attackState == HeavyEnemyAttackState.AttackReadyUp) PerformAttackUp();
        else if (attackState == HeavyEnemyAttackState.AttackReadyDown) PerformAttackDown();
        CheckIsPlayerInAttackRange();
    }
    public void CheckIsPlayerInAttackRange()
    {
        if (IsPlayerLeft()) 
        {
            attackState = HeavyEnemyAttackState.AttackReadyLeft;
            GameManager.GameLog.LogEvent("Heavy Enemy is charging a ← attack");
            return;
        }
        else if (IsPlayerRight())
        {
            attackState = HeavyEnemyAttackState.AttackReadyRight;
            GameManager.GameLog.LogEvent("Heavy Enemy is charging a → attack");
            return;
        }
        else if (IsPLayerUp())
        {
            attackState = HeavyEnemyAttackState.AttackReadyUp;
            GameManager.GameLog.LogEvent("Heavy Enemy is charging a ↑ attack");
            return;
        }
        else if (IsPlayerDown())
        {
            attackState = HeavyEnemyAttackState.AttackReadyDown;
            GameManager.GameLog.LogEvent("Heavy Enemy is charging a ↓ attack");
            return;
        }
        else attackState = HeavyEnemyAttackState.NotReady; return;
    }
    public void PerformAttackLeft()
    {
        if ((Player.Position.X == Position.X - 1 && Player.Position.Y == Position.Y) || Player.Position.X == Position.X - 2 && (Player.Position.Y <= Position.Y + 1 && Player.Position.Y >= Position.Y - 1))
        {
            Player.CurHP -= Damage;
        }
        attackState = HeavyEnemyAttackState.NotReady;
    }
    public void PerformAttackRight()
    {
        if ((Player.Position.X == Position.X + 1 && Player.Position.Y == Position.Y) || Player.Position.X == Position.X + 2 && (Player.Position.Y <= Position.Y + 1 && Player.Position.Y >= Position.Y - 1))
        {
            Player.CurHP -= Damage;
        }
        attackState = HeavyEnemyAttackState.NotReady;
    }
    public void PerformAttackUp()
    {
        if ((Player.Position.Y == Position.Y - 1 && Player.Position.X == Position.X) || Player.Position.Y == Position.Y - 2 && (Player.Position.X <= Position.X + 1 && Player.Position.X >= Position.X - 1))
        {
            Player.CurHP -= Damage;
        }
        attackState = HeavyEnemyAttackState.NotReady;
    }
    public void PerformAttackDown()
    {
        if ((Player.Position.Y == Position.Y + 1 && Player.Position.X == Position.X) || Player.Position.Y == Position.Y + 2 && (Player.Position.X <= Position.X + 1 && Player.Position.X >= Position.X - 1))
        {
            Player.CurHP -= Damage;
        }
        attackState = HeavyEnemyAttackState.NotReady;
    }
    public bool IsPlayerLeft()
    {
        if ((Player.Position.X == Position.X - 1 && Player.Position.Y == Position.Y) || Player.Position.X == Position.X - 2 && (Player.Position.Y <= Position.Y + 1 && Player.Position.Y >= Position.Y - 1))
        {
            return true;
        }
        return false;
    }
    public bool IsPlayerRight()
    {
        if ((Player.Position.X == Position.X + 1 && Player.Position.Y == Position.Y) || Player.Position.X == Position.X + 2 && (Player.Position.Y <= Position.Y + 1 && Player.Position.Y >= Position.Y - 1))
        {
            return true;
        }
        return false;
    }
    public bool IsPLayerUp()
    {
        if ((Player.Position.Y == Position.Y - 1 && Player.Position.X == Position.X) || Player.Position.Y == Position.Y - 2 && (Player.Position.X <= Position.X + 1 && Player.Position.X >= Position.X - 1))
        {
            return true;
        }
        return false;
    }
    public bool IsPlayerDown()
    {
        if ((Player.Position.Y == Position.Y + 1 && Player.Position.X == Position.X) || Player.Position.Y == Position.Y + 2 && (Player.Position.X <= Position.X + 1 && Player.Position.X >= Position.X - 1))
        {
            return true;
        }
        return false;
    }

    public void HeavyEnemyMove()
    {
        if (IsDead()) { return; }
        if (attackState != HeavyEnemyAttackState.NotReady) { return; }
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
        if (IsPlayerAdjacent())
        {
            return "NoMove";
        }
        int priorityUp = 0;
        int priorityDown = 0;
        int priorityLeft = 0;
        int priorityRight = 0;

        if (CurrentLevel.CurrentMapState[Position.Y - 1, Position.X] != TileENUM.Empty)
        {
            priorityUp -= 100;
        }
        if (CurrentLevel.CurrentMapState[Position.Y + 1, Position.X] != TileENUM.Empty)
        {
            priorityDown -= 100;
        }
        if (CurrentLevel.CurrentMapState[Position.Y, Position.X - 1] != TileENUM.Empty)
        {
            priorityLeft -= 100;
        }
        if (CurrentLevel.CurrentMapState[Position.Y, Position.X + 1] != TileENUM.Empty)
        {
            priorityRight -= 100;
        }
        if (CurrentLevel.CurrentMapState[Position.Y - 1, Position.X] == TileENUM.Entrance)
        {
            priorityUp = 0;
        }
        if (CurrentLevel.CurrentMapState[Position.Y + 1, Position.X] == TileENUM.Entrance)
        {
            priorityDown = 0;
        }
        if (CurrentLevel.CurrentMapState[Position.Y, Position.X - 1] == TileENUM.Entrance)
        {
            priorityLeft = 0;
        }
        if (CurrentLevel.CurrentMapState[Position.Y, Position.X + 1] == TileENUM.Entrance)
        {
            priorityRight = 0;
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

public enum HeavyEnemyAttackState
{
    NotReady,
    AttackReadyLeft,
    AttackReadyRight,
    AttackReadyUp,
    AttackReadyDown
}