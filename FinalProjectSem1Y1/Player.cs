public class Player
{
    public Point Position { get; set; }
    public int MaxHP;
    public int CurHP;
    public Weapon CurrentWeapon;

    public bool IsDead()
    {
        if (CurHP <= 0) return true;
        else return false;
    }


    public Player(Point position)
    {
        Position = position;
        MaxHP = 15;
        CurHP = MaxHP;
        CurrentWeapon = GameManager.Inventory.ObtainedWeapons[0];
    }

    public void PlayerInput()
    {
        var input = Console.ReadKey();
        switch (input.Key)
        {
            case ConsoleKey.W:
            case ConsoleKey.UpArrow:
                Position.Y--;
                if (GameManager.CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Empty) Position.Y++;
                //need to check if new tile is valid, not sure how to access CurrentMapState from player...
                break;
            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                Position.Y++;
                if (GameManager.CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Empty) Position.Y--;
                break;
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                Position.X++;
                if (GameManager.CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Empty) Position.X--;
                break;
            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                Position.X--;
                if (GameManager.CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Empty) Position.X++;
                break;
            case ConsoleKey.Spacebar:
                {
                    PerformAttack(CurrentWeapon.WeaponType);
                }
                break;
                //Create Attack Logic
            default:
                break;
        }
    }


    public void PerformAttack(WeaponType playerWeapon)
    {
        var attackDirection = Console.ReadKey();
        switch (attackDirection.Key)
        {
            case ConsoleKey.A: break;
            case ConsoleKey.D: break;
            case ConsoleKey.S: break;
            case ConsoleKey.W: break;
            default: break;

        }
    }
    
    public void DetermineAttackTargets(AttackAOE attackAOE)
    {
        
    }
}