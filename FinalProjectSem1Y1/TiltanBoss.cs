public class TiltanBoss
{
    public Point Position;
    public int MaxHP;
    public int CurHP;
    public int Damage;
    public int BossCounterToMove;
    public int BossGracePeriodTimer;
    public BossAttackCycleState State;
    public FaceDirection AttackDirection;
    private Player Player { get { return GameManager.Player; } }


    public bool IsDead()
    {
        if (CurHP <= 0)
            return true;
        return false;
    }
    public TiltanBoss(Point point)
    {
        Position = point;
        MaxHP = 25;
        CurHP = MaxHP;
        Damage = 6;
        BossCounterToMove = 0;
        BossGracePeriodTimer = 0;
        State = BossAttackCycleState.Recharging;
    }
    public void BossAttack()
    {
        string attackdirection;
        if (IsDead()) { return; }
        if (BossGracePeriodTimer < 5) { BossGracePeriodTimer++; return; }
        if (State == BossAttackCycleState.Recharging)
        {
            attackdirection = PlayerRelativity();
            State = BossAttackCycleState.PreparingAttack;
            GameManager.GameLog.LogEvent($"Tiltan is preparing it's attack {attackdirection}");
            if (attackdirection == "left") { AttackDirection = FaceDirection.Left; }
            else if (attackdirection == "right") {  AttackDirection = FaceDirection.Right; }
            else if (attackdirection == "up") {  AttackDirection = FaceDirection.Up; }
            else if (attackdirection == "down") { AttackDirection = FaceDirection.Down; }
        }
        else if (State == BossAttackCycleState.PreparingAttack)
        {
            if (AttackDirection == FaceDirection.Left) 
            { 
                GameManager.GameLog.LogEvent("Tiltan is attacking left"); 
                if (Player.Position.X < Position.X - 1)
                {
                    Player.CurHP -= Damage;
                }
            }
            else if (AttackDirection == FaceDirection.Right) 
            { 
                GameManager.GameLog.LogEvent("Tiltan is attacking right"); 
                if (Player.Position.X > Position.X + 1)
                {
                    Player.CurHP -= Damage;
                }
            }
            else if (AttackDirection == FaceDirection.Up) 
            { 
                GameManager.GameLog.LogEvent("Tiltan is attacking up");
                if (Player.Position.Y < Position.Y - 1)
                {
                    Player.CurHP -= Damage;
                }
            }
            else if (AttackDirection == FaceDirection.Down) 
            { 
                GameManager.GameLog.LogEvent("Tiltan is attacking down");
                if (Player.Position.Y > Position.Y + 1)
                {
                    Player.CurHP -= Damage;
                }
            }
            else return;
            State = BossAttackCycleState.Attacking;
            
        }
        else
        {
            State = BossAttackCycleState.Recharging;
        }
    }
    public string PlayerRelativity()
    {
        int priorityUp = 0;
        int priorityDown = 0;
        int priorityLeft = 0;
        int priorityRight = 0;
        if (Player.Position.Y > Position.Y)
        {
            priorityDown += 1;
        }
        if (Player.Position.Y < Position.Y)
        {
            priorityUp += 1;
        }
        if (Player.Position.X > Position.X)
        {
            priorityRight += 1;
        }
        if (Player.Position.X < Position.X)
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

        int AIChoice = Random.Shared.Next(0, possibleDirections.Count);
        if (possibleDirections[AIChoice] == 1) return "up";
        if (possibleDirections[AIChoice] == 2) return "left";
        if (possibleDirections[AIChoice] == 3) return "down";
        if (possibleDirections[AIChoice] == 4) return "right";

        throw new Exception("enemy movement failed");
    }
    public void AdvanceBossMoveCounter()
    {
        if (CurHP > 15) { return; }
        if (IsPlayerAdjacent()) { return; }
        if (BossCounterToMove == 0)
        {
            BossCounterToMove++;
        }
        else if (BossCounterToMove == 1)
        {
            BossCounterToMove++;
        }
        else if (BossCounterToMove == 2)
        {
            BossMove();
            BossCounterToMove++;
        }
        else if (BossCounterToMove == 3)
        {
            BossCounterToMove = 0;
        }
    }
    public void BossMove()
    {
            if (IsDead()) { return; }
            string moveDirection = DetermineMovementDirection();
            if (moveDirection == "NoMove") return;
            if (moveDirection == "up") { Position.Y--; return; }
            if (moveDirection == "down") { Position.Y++; return; }
            if (moveDirection == "left") { Position.X--; return; }
            if (moveDirection == "right") { Position.X++; return; }
    }
    public bool IsPlayerAdjacent()
    {
        if (CurrentLevel.CurrentMapState[Position.Y + 2, Position.X + 1] == TileENUM.Player
         || CurrentLevel.CurrentMapState[Position.Y + 2, Position.X - 1] == TileENUM.Player    // Checking adjacent Down
         || CurrentLevel.CurrentMapState[Position.Y - 2, Position.X + 1] == TileENUM.Player
         || CurrentLevel.CurrentMapState[Position.Y - 2, Position.X - 1] == TileENUM.Player    // Checking Adjacent Up
         || CurrentLevel.CurrentMapState[Position.Y + 1, Position.X - 2] == TileENUM.Player
         || CurrentLevel.CurrentMapState[Position.Y - 1, Position.X - 2] == TileENUM.Player    // Checking Adjacent Left
         || CurrentLevel.CurrentMapState[Position.Y + 1, Position.X + 2] == TileENUM.Player
         || CurrentLevel.CurrentMapState[Position.Y - 1, Position.X + 2] == TileENUM.Player)   // Checking Adjacent Right
        {
            return true;
        }
        return false;
    }
    public string DetermineMovementDirection()
    {
        int priorityUp = 0;
        int priorityDown = 0;
        int priorityLeft = 0;
        int priorityRight = 0;
        if (CurrentLevel.CurrentMapState[Position.Y - 2, Position.X] != TileENUM.Empty)
        {
            priorityUp -= 100;
        }
        if (CurrentLevel.CurrentMapState[Position.Y + 2, Position.X] != TileENUM.Empty)
        {
            priorityDown -= 100;
        }
        if (CurrentLevel.CurrentMapState[Position.Y, Position.X - 2] != TileENUM.Empty)
        {
            priorityLeft -= 100;
        }
        if (CurrentLevel.CurrentMapState[Position.Y, Position.X + 2] != TileENUM.Empty)
        {
            priorityRight -= 100;
        }

        if (Player.Position.Y > Position.Y)
        {
            priorityDown += 1;
        }
        if (Player.Position.Y < Position.Y)
        {
            priorityUp += 1;
        }
        if (Player.Position.X > Position.X)
        {
            priorityRight += 1;
        }
        if (Player.Position.X < Position.X)
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
public enum BossAttackCycleState
{
    Recharging,
    PreparingAttack,
    Attacking
}