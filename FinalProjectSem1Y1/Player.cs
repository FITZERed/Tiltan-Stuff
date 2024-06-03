public class Player
{
    public Point Position { get; set; }
    public int MaxHP;
    public int CurHP;
    private int _currentWeaponIndex;
    public Weapon CurrentWeapon { get { return GameManager.Inventory.ObtainedWeapons[_currentWeaponIndex]; } }
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
        _currentWeaponIndex = 0;
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
            default:
                break;
        }
    }


    public void PerformAttack(WeaponType playerWeaponAttack)
    {
        var attackDirection = Console.ReadKey();
        switch (attackDirection.Key)
        {
            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                DetermineAttackTargetsLeft(playerWeaponAttack);
                break;
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                DetermineAttackTargetsRight(playerWeaponAttack);
                break;
            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                DetermineAttackTargetsDown(playerWeaponAttack);
                break;
            case ConsoleKey.W:
            case ConsoleKey.UpArrow:
                DetermineAttackTargetsUp(playerWeaponAttack);
                break;
            default: break;

        }
    }
    
    public void DetermineAttackTargetsLeft(WeaponType playerWeapon)
    {
        switch (playerWeapon)
        {
            case WeaponType.Spear:
                {
                    //point x- x-2
                    foreach (StandardEnemy standardEnemy in GameManager.CurrentLevel.EnemyLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.Y == Position.Y && (standardEnemy.Position.X == Position.X - 1 || standardEnemy.Position.X == Position.X - 2))
                        {
                            standardEnemy.CurHP -= 2;
                        }
                    }
                }
                break;
        }
    }
    public void DetermineAttackTargetsRight(WeaponType playerWeapon)
    {
        switch (playerWeapon)
        {
            case WeaponType.Spear:
                {
                    //point x+ x+2
                    foreach (StandardEnemy standardEnemy in GameManager.CurrentLevel.EnemyLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.Y == Position.Y && (standardEnemy.Position.X == Position.X + 1 || standardEnemy.Position.X == Position.X + 2))
                        {
                            standardEnemy.CurHP -= 2;
                        }
                    }
                }
                break;
        }
    }
    public void DetermineAttackTargetsUp(WeaponType playerWeapon)
    {
        switch (playerWeapon)
        {
            case WeaponType.Spear:
                {
                    //point y- y-2
                    foreach (StandardEnemy standardEnemy in GameManager.CurrentLevel.EnemyLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.X == Position.X && (standardEnemy.Position.Y == Position.Y - 1 || standardEnemy.Position.Y == Position.Y - 2))
                        {
                            standardEnemy.CurHP -= 2;
                        }
                    }
                }
                break;
        }
    }
    public void DetermineAttackTargetsDown(WeaponType playerWeapon)
    {
        switch (playerWeapon)
        {
            case WeaponType.Spear:
                {
                    //point y- y-2
                    foreach (StandardEnemy standardEnemy in GameManager.CurrentLevel.EnemyLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.X == Position.X && (standardEnemy.Position.Y == Position.Y + 1 || standardEnemy.Position.Y == Position.Y + 2))
                        {
                            standardEnemy.CurHP -= 2;
                        }
                    }
                }
                break;
        }
    }
}