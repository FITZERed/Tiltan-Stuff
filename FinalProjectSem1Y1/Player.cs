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


    public Player()
    {
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
                if (CurrentLevel.CurrentMapState[Position.Y, Position.X] == TileENUM.Exit)
                {
                    TryExit();
                }
                else if (CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Empty && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Entrance) Position.Y++;
                //need to check if new tile is valid, not sure how to access CurrentMapState from player...
                break;
            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                Position.Y++;
                if (CurrentLevel.CurrentMapState[Position.Y, Position.X] == TileENUM.Exit)
                {
                    TryExit();
                }
                else if (CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Empty && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Entrance) Position.Y--;
                break;
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                Position.X++;
                if (CurrentLevel.CurrentMapState[Position.Y, Position.X] == TileENUM.Exit)
                {
                    TryExit();
                }
                else if (CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Empty && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Entrance) Position.X--;
                break;
            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                Position.X--;
                if (CurrentLevel.CurrentMapState[Position.Y, Position.X] == TileENUM.Exit)
                {
                    TryExit();
                }
                else if (CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Empty && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Entrance) Position.X++;
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

    public void TryExit()
    {
        if (CurrentLevel.Exit.IsAvailable())
        {
            levelNum++;
            AdvanceLevel();
        }
    }

    public void PerformAttack(WeaponType playerWeaponAttack)
    {
        var attackDirection = Console.ReadKey();
        switch (attackDirection.Key)
        {
            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                DetermineTargetsAndAttackLeft(playerWeaponAttack);
                break;
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                DetermineTargetsAndAttackRight(playerWeaponAttack);
                break;
            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                DetermineTargetsAndAttackDown(playerWeaponAttack);
                break;
            case ConsoleKey.W:
            case ConsoleKey.UpArrow:
                DetermineTargetsAndAttackUp(playerWeaponAttack);
                break;
            default: break;

        }
    }
    
    public void DetermineTargetsAndAttackLeft(WeaponType playerWeapon)
    {
        switch (playerWeapon)
        {
            case WeaponType.Spear:
                {
                    //point x- x-2
                    foreach (StandardEnemy standardEnemy in CurrentLevel.EnemyLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.Y == Position.Y && (standardEnemy.Position.X == Position.X - 1 || standardEnemy.Position.X == Position.X - 2))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= 2;
                                GameManager.GameLog.LogEvent("Player has hit Standard Enemy for 2 damage");
                                if (standardEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Standard Enemy defeated");
                                }
                            }
                            //if (standardEnemy.CurHP <= 0) { GameManager.GameLog.LogEvent("Standard Enemy defeated"); }
                        }
                    }
                }
                break;
        }
    }
    public void DetermineTargetsAndAttackRight(WeaponType playerWeapon)
    {
        switch (playerWeapon)
        {
            case WeaponType.Spear:
                {
                    //point x+ x+2
                    foreach (StandardEnemy standardEnemy in CurrentLevel.EnemyLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.Y == Position.Y && (standardEnemy.Position.X == Position.X + 1 || standardEnemy.Position.X == Position.X + 2))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= 2;
                                GameManager.GameLog.LogEvent("Player has hit Standard Enemy for 2 damage");
                                if (standardEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Standard Enemy defeated");
                                }
                            }
                        }
                    }
                }
                break;
        }
    }
    public void DetermineTargetsAndAttackUp(WeaponType playerWeapon)
    {
        switch (playerWeapon)
        {
            case WeaponType.Spear:
                {
                    //point y- y-2
                    foreach (StandardEnemy standardEnemy in CurrentLevel.EnemyLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.X == Position.X && (standardEnemy.Position.Y == Position.Y - 1 || standardEnemy.Position.Y == Position.Y - 2))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= 2;
                                GameManager.GameLog.LogEvent("Player has hit Standard Enemy for 2 damage");
                                if (standardEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Standard Enemy defeated");
                                }
                            }
                        }
                    }
                }
                break;
        }
    }
    public void DetermineTargetsAndAttackDown(WeaponType playerWeapon)
    {
        switch (playerWeapon)
        {
            case WeaponType.Spear:
                {
                    //point y- y-2
                    foreach (StandardEnemy standardEnemy in CurrentLevel.EnemyLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.X == Position.X && (standardEnemy.Position.Y == Position.Y + 1 || standardEnemy.Position.Y == Position.Y + 2))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= 2;
                                GameManager.GameLog.LogEvent("Player has hit Standard Enemy for 2 damage");
                                if (standardEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Standard Enemy defeated");
                                }
                            }
                        }
                    }
                }
                break;
        }
    }
}