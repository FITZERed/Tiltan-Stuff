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
                    PlayerPerformAttack();
                }
                break;
            case ConsoleKey.E:
                {
                    OpenChest();
                }
                break;
            case ConsoleKey.H:
                {
                    UseHealPotion();
                }
                break;
            case ConsoleKey.X:
                {
                    SwitchWeapon();
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
        else { GameManager.GameLog.LogEvent("Exit Unavailable, there are still enemies present."); }
    }
    public void UseHealPotion()
    {
        if(GameManager.Inventory.HealingPotions <= 0)
        {
            GameManager.GameLog.LogEvent("You have no Healing Potions.");
            return;
        }
        CurHP += 3;
        if (CurHP > MaxHP) { CurHP = MaxHP; }
        GameManager.Inventory.SubtractPotion();
        GameManager.GameLog.LogEvent("Player has used a Healing Potion.");
    }
    public void SwitchWeapon()
    {
        GameManager.GameLog.LogEvent("Choose the desired weapon's slot in inventory");
        var playerWeaponChoice = Console.ReadKey();
        int temp = 0;
        switch(playerWeaponChoice.Key)
        {
            case ConsoleKey.D1:
                temp = 0;
                break;
            case ConsoleKey.D2:
                temp = 1;
                break;
            case ConsoleKey.D3:
                temp = 2;
                break;
            case ConsoleKey.D4:
                temp = 3;
                break;
            default: temp = 0; break;
        }
        if(temp >= GameManager.Inventory.ObtainedWeapons.Count)
        {
            GameManager.GameLog.LogEvent("There is no weapon in this slot... Weapon unchanged");
            return;
        }
        else
        {
            _currentWeaponIndex = temp;
        }
    }
    public void OpenChest()
    {
        var directionToChestToOpen = Console.ReadKey();
        switch (directionToChestToOpen.Key)
        {
            case ConsoleKey.W:
            case ConsoleKey.UpArrow:
                if (CurrentLevel.CurrentMapState[Position.Y - 1, Position.X] == TileENUM.Chest)
                {
                    foreach (Chest chest in CurrentLevel.InteractablesLists.ChestsPresent)
                        if (chest.Position.X == Position.X && chest.Position.Y == Position.Y - 1)
                        {
                            if (!chest.IsLooted)
                            {
                                GainChestContents(chest);
                                GameManager.GameLog.LogEvent("Chest looted.");
                            }
                            else { GameManager.GameLog.LogEvent("This Chest is already looted."); }
                        }
                }
                break;
            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                if (CurrentLevel.CurrentMapState[Position.Y + 1, Position.X] == TileENUM.Chest)
                {
                    foreach (Chest chest in CurrentLevel.InteractablesLists.ChestsPresent)
                        if (chest.Position.X == Position.X && chest.Position.Y == Position.Y + 1)
                        {
                            if (!chest.IsLooted)
                            {
                                GainChestContents(chest);
                                GameManager.GameLog.LogEvent("Chest looted.");
                            }
                            else { GameManager.GameLog.LogEvent("This Chest is already looted."); }
                        }
                }
                break;
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                if (CurrentLevel.CurrentMapState[Position.Y, Position.X + 1] == TileENUM.Chest)
                {
                    foreach (Chest chest in CurrentLevel.InteractablesLists.ChestsPresent)
                        if (chest.Position.X == Position.X + 1 && chest.Position.Y == Position.Y)
                        {
                            if (!chest.IsLooted)
                            {
                                GainChestContents(chest);
                                GameManager.GameLog.LogEvent("Chest looted.");
                            }
                            else { GameManager.GameLog.LogEvent("This Chest is already looted."); }
                        }
                }
                break;
            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                if (CurrentLevel.CurrentMapState[Position.Y, Position.X - 1] == TileENUM.Chest)
                {
                    foreach (Chest chest in CurrentLevel.InteractablesLists.ChestsPresent)
                        if (chest.Position.X == Position.X - 1 && chest.Position.Y == Position.Y)
                        {
                            if (!chest.IsLooted)
                            {
                                GainChestContents(chest);
                                GameManager.GameLog.LogEvent("Chest looted.");
                            }
                            else { GameManager.GameLog.LogEvent("This Chest is already looted."); }
                        }
                }
                break;
            default: break;
        }
    }
    public void GainChestContents(Chest chest)
    {
        switch(chest.Content) 
        {
            case ChestContent.HealingPotion:
                GameManager.Inventory.AddPotion();
                chest.LootChest();
                break;
            case ChestContent.Axe:
                GameManager.Inventory.GainWeapon(WeaponType.Axe);
                chest.LootChest();
                break;
                //Add cases for different chest contents
            default: break;
        }
    }

    public void PlayerPerformAttack()
    {
        GameManager.GameLog.LogEvent("Choose a direction to attack in");
        var attackDirection = Console.ReadKey();
        switch (attackDirection.Key)
        {
            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                DetermineTargetsAndAttackLeft(CurrentWeapon.AttackAOE);
                break;
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                DetermineTargetsAndAttackRight(CurrentWeapon.AttackAOE);
                break;
            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                DetermineTargetsAndAttackDown(CurrentWeapon.AttackAOE);
                break;
            case ConsoleKey.W:
            case ConsoleKey.UpArrow:
                DetermineTargetsAndAttackUp(CurrentWeapon.AttackAOE);
                break;
            default: break;

        }
    }

    
    public void DetermineTargetsAndAttackLeft(AttackAOE weaponAOE)
    {
        switch (weaponAOE)
        {
            case AttackAOE.TwoForward:
                {
                    //point x- x-2
                    foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.Y == Position.Y && (standardEnemy.Position.X == Position.X - 1 || standardEnemy.Position.X == Position.X - 2))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= CurrentWeapon.Power;
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
            case AttackAOE.ThreeInFront:
                {
                    //point x-, x-y-, x-y+
                    foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.X == Position.X - 1 && (standardEnemy.Position.Y == Position.Y || standardEnemy.Position.Y == Position.Y - 1 || standardEnemy.Position.Y == Position.Y + 1))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Standard Enemy for 3 damage");
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
    public void DetermineTargetsAndAttackRight(AttackAOE weaponAOE)
    {
        switch (weaponAOE)
        {
            case AttackAOE.TwoForward:
                {
                    //point x+ x+2
                    foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.Y == Position.Y && (standardEnemy.Position.X == Position.X + 1 || standardEnemy.Position.X == Position.X + 2))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= CurrentWeapon.Power;
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
            case AttackAOE.ThreeInFront:
                {
                    //point x+, x+y-, x+y+
                    foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.X == Position.X + 1 && (standardEnemy.Position.Y == Position.Y || standardEnemy.Position.Y == Position.Y - 1 || standardEnemy.Position.Y == Position.Y + 1))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Standard Enemy for 3 damage");
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
    public void DetermineTargetsAndAttackUp(AttackAOE weaponAOE)
    {
        switch (weaponAOE)
        {
            case AttackAOE.TwoForward:
                {
                    //point y- y-2
                    foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.X == Position.X && (standardEnemy.Position.Y == Position.Y - 1 || standardEnemy.Position.Y == Position.Y - 2))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= CurrentWeapon.Power;
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
            case AttackAOE.ThreeInFront:
                {
                    //point y-, x-y-, x+y-
                    foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.Y == Position.Y - 1 && (standardEnemy.Position.X == Position.X || standardEnemy.Position.X == Position.X - 1 || standardEnemy.Position.X == Position.X + 1))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Standard Enemy for 3 damage");
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
    public void DetermineTargetsAndAttackDown(AttackAOE weaponAOE)
    {
        switch (weaponAOE)
        {
            case AttackAOE.TwoForward:
                {
                    //point y- y-2
                    foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.X == Position.X && (standardEnemy.Position.Y == Position.Y + 1 || standardEnemy.Position.Y == Position.Y + 2))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= CurrentWeapon.Power;
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
            case AttackAOE.ThreeInFront:
                {
                    //point y+, x-y+, x+y+
                    foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)
                    {
                        if (standardEnemy.Position.Y == Position.Y + 1 && (standardEnemy.Position.X == Position.X || standardEnemy.Position.X == Position.X - 1 || standardEnemy.Position.X == Position.X + 1))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Standard Enemy for 3 damage");
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