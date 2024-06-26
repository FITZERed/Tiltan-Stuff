﻿using System.Diagnostics;

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
                    else if (CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Empty && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Entrance && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.HiddenTrap && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.VisibleTrap) 
                        Position.Y++;
                    if (CurrentLevel.CurrentMapState[Position.Y, Position.X] == TileENUM.VisibleTrap)
                    {
                        foreach (Trap trap in CurrentLevel.InteractablesLists.TrapsPresent)
                        {
                            if (trap.Position.X == Position.X && trap.Position.Y == Position.Y)
                            {
                                trap.TriggerTrap();
                            }
                        }
                    }
                    DetectTraps();
                    break;
            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                Position.Y++;
                if (CurrentLevel.CurrentMapState[Position.Y, Position.X] == TileENUM.Exit)
                {
                    TryExit();
                }
                else if (CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Empty && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Entrance && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.HiddenTrap && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.VisibleTrap) 
                    Position.Y--;
                if (CurrentLevel.CurrentMapState[Position.Y, Position.X] == TileENUM.VisibleTrap)
                {
                    foreach (Trap trap in CurrentLevel.InteractablesLists.TrapsPresent)
                    {
                        if (trap.Position.X == Position.X && trap.Position.Y == Position.Y)
                        {
                            trap.TriggerTrap();
                        }
                    }
                }
                DetectTraps();
                break;
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                Position.X++;
                if (CurrentLevel.CurrentMapState[Position.Y, Position.X] == TileENUM.Exit)
                {
                    TryExit();
                }
                else if (CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Empty && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Entrance && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.HiddenTrap && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.VisibleTrap) 
                    Position.X--;
                if (CurrentLevel.CurrentMapState[Position.Y, Position.X] == TileENUM.VisibleTrap)
                {
                    foreach (Trap trap in CurrentLevel.InteractablesLists.TrapsPresent)
                    {
                        if (trap.Position.X == Position.X && trap.Position.Y == Position.Y)
                        {
                            trap.TriggerTrap();
                        }
                    }
                }
                DetectTraps();
                break;
            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                Position.X--;
                if (CurrentLevel.CurrentMapState[Position.Y, Position.X] == TileENUM.Exit)
                {
                    TryExit();
                }
                else if (CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Empty && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.Entrance && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.HiddenTrap && CurrentLevel.CurrentMapState[Position.Y, Position.X] != TileENUM.VisibleTrap) 
                    Position.X++;
                if (CurrentLevel.CurrentMapState[Position.Y, Position.X] == TileENUM.VisibleTrap)
                {
                    foreach (Trap trap in CurrentLevel.InteractablesLists.TrapsPresent)
                    {
                        if (trap.Position.X == Position.X && trap.Position.Y == Position.Y)
                        {
                            trap.TriggerTrap();
                        }
                    }
                }
                DetectTraps();
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
            if (levelNum == 11)
            {
                return;
            }
            AdvanceLevel();
        }
        else { GameManager.GameLog.LogEvent("Exit Unavailable, there are still enemies present."); }
    }
    public void DetectTraps()
    {
        foreach (Trap trap in CurrentLevel.InteractablesLists.TrapsPresent)
        {
            if (trap.Position.X >= Position.X - 1 && trap.Position.X <= Position.X + 1 && trap.Position.Y >= Position.Y - 1 && trap.Position.Y <= Position.Y + 1)
            {
                trap.DetectTrap();
            }
        }
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
            case ConsoleKey.D5:
                temp = 4;
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
            case ChestContent.ShortBow:
                GameManager.Inventory.GainWeapon(WeaponType.ShortBow);
                chest.LootChest();
                break;
            case ChestContent.LongBow:
                GameManager.Inventory.GainWeapon(WeaponType.LongBow);
                chest.LootChest();
                break;
            case ChestContent.LegendSword:
                GameManager.Inventory.GainWeapon(WeaponType.LegendarySword);
                GameManager.GameLog.LogEvent($"Player has obtained \"FireLight\", the legendary sword");
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
                    if (CurrentLevel.CurrentMapState[Position.Y, Position.X - 1] == TileENUM.Wall) break;
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
                    foreach (RangedEnemy rangedEnemy in CurrentLevel.InteractablesLists.RangedEnemiesPresent)
                    {
                        if (rangedEnemy.Position.Y == Position.Y && (rangedEnemy.Position.X == Position.X - 1 || rangedEnemy.Position.X == Position.X - 2))
                        {
                            if (!rangedEnemy.IsDead())
                            {
                                rangedEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Ranged Enemy for 2 damage");
                                if (rangedEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedMiniBoss rangedMiniBoss in CurrentLevel.InteractablesLists.RangedMiniBossPresent)
                    {
                        if (rangedMiniBoss.Position.Y == Position.Y && (rangedMiniBoss.Position.X == Position.X - 1 || rangedMiniBoss.Position.X == Position.X - 2))
                        {
                            if (!rangedMiniBoss.IsDead())
                            {
                                rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Mini-Boss for 2 damage");
                                if (rangedMiniBoss.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Mini-Boss defeated");
                                }
                            }
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y), out HeavyEnemy heavyEnemy))
                    {
                        heavyEnemy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavyEnemy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 2, Position.Y), out HeavyEnemy heavyEnemy1))
                    {
                        heavyEnemy1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavyEnemy1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 2, Position.Y), out TiltanBoss tiltanBoss1))
                    {
                        tiltanBoss1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
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
                    foreach (RangedEnemy rangedEnemy in CurrentLevel.InteractablesLists.RangedEnemiesPresent)
                    {
                        if (rangedEnemy.Position.X == Position.X - 1 && (rangedEnemy.Position.Y == Position.Y || rangedEnemy.Position.Y == Position.Y - 1 || rangedEnemy.Position.Y == Position.Y + 1))
                        {
                            if (!rangedEnemy.IsDead())
                            {
                                rangedEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Ranged Enemy for 3 damage");
                                if (rangedEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedMiniBoss rangedMiniBoss in CurrentLevel.InteractablesLists.RangedMiniBossPresent)
                    {
                        if (rangedMiniBoss.Position.X == Position.X - 1 && (rangedMiniBoss.Position.Y == Position.Y || rangedMiniBoss.Position.Y == Position.Y - 1 || rangedMiniBoss.Position.Y == Position.Y + 1))
                        {
                            if (!rangedMiniBoss.IsDead())
                            {
                                rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Mini-Boss for 3 damage");
                                if (rangedMiniBoss.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Mini-Boss defeated");
                                }
                            }
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y), out HeavyEnemy heavy))
                    {
                        heavy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y - 1), out HeavyEnemy heavy1))
                    {
                        heavy1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y + 1), out HeavyEnemy heavy2))
                    {
                        heavy2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y - 1), out TiltanBoss tiltanBoss1))
                    {
                        tiltanBoss1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y + 1), out TiltanBoss tiltanBoss2))
                    {
                        tiltanBoss2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                }
                break;
            case AttackAOE.Ranged:
                for (int i = Position.X - 1; i > 0; i--)
                {
                    if (CurrentLevel.CurrentMapState[Position.Y, i] == TileENUM.Empty) continue;
                    else if (CurrentLevel.CurrentMapState[Position.Y, i] == TileENUM.Wall) break;
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(i, Position.Y), out StandardEnemy standardEnemy))
                    {
                        standardEnemy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Standard Enemy for {CurrentWeapon.Power} damage");
                        if (standardEnemy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Standard Enemy defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(i, Position.Y), out RangedEnemy rangedEnemy))
                    {
                        rangedEnemy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Ranged Enemy for {CurrentWeapon.Power} damage");
                        if (rangedEnemy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(i, Position.Y), out RangedMiniBoss rangedMiniBoss))
                    {
                        rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Mini-Boss for {CurrentWeapon.Power} damage");
                        if (rangedMiniBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Mini-Boss defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(i, Position.Y), out HeavyEnemy heavy))
                    {
                        heavy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(i, Position.Y), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                        break;
                    }
                    else { break; }
                }
                break;
            case AttackAOE.PlusShape:
                {
                    foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)
                    {
                        if ((standardEnemy.Position.X == Position.X - 1 && (standardEnemy.Position.Y == Position.Y || standardEnemy.Position.Y == Position.Y - 1 || standardEnemy.Position.Y == Position.Y + 1)) || (standardEnemy.Position.X == Position.X - 2 && standardEnemy.Position.Y == Position.Y))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent($"Player has hit Standard Enemy for {CurrentWeapon.Power} damage");
                                if (standardEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Standard Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedEnemy rangedEnemy in CurrentLevel.InteractablesLists.RangedEnemiesPresent)
                    {
                        if ((rangedEnemy.Position.X == Position.X - 1 && (rangedEnemy.Position.Y == Position.Y || rangedEnemy.Position.Y == Position.Y - 1 || rangedEnemy.Position.Y == Position.Y + 1)) || (rangedEnemy.Position.X == Position.X - 2 && rangedEnemy.Position.Y == Position.Y))
                        {
                            if (!rangedEnemy.IsDead())
                            {
                                rangedEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent($"Player has hit Ranged Enemy for {CurrentWeapon.Power} damage");
                                if (rangedEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedMiniBoss rangedMiniBoss in CurrentLevel.InteractablesLists.RangedMiniBossPresent)
                    {
                        if ((rangedMiniBoss.Position.X == Position.X - 1 && (rangedMiniBoss.Position.Y == Position.Y || rangedMiniBoss.Position.Y == Position.Y - 1 || rangedMiniBoss.Position.Y == Position.Y + 1)) || (rangedMiniBoss.Position.X == Position.X - 2 && rangedMiniBoss.Position.Y == Position.Y))
                        {
                            if (!rangedMiniBoss.IsDead())
                            {
                                rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent($"Player has hit Mini-Boss for {CurrentWeapon.Power} damage");
                                if (rangedMiniBoss.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Mini-Boss defeated");
                                }
                            }
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y), out HeavyEnemy heavy))
                    {
                        heavy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y - 1), out HeavyEnemy heavy1))
                    {
                        heavy1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y + 1), out HeavyEnemy heavy2))
                    {
                        heavy2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 2, Position.Y), out HeavyEnemy heavy3))
                    {
                        heavy3.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy3.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y - 1), out TiltanBoss tiltanBoss1))
                    {
                        tiltanBoss1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y + 1), out TiltanBoss tiltanBoss2))
                    {
                        tiltanBoss2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 2, Position.Y), out TiltanBoss tiltanBoss3))
                    {
                        tiltanBoss3.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss3.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    break;
                }
        }
    }
    public void DetermineTargetsAndAttackRight(AttackAOE weaponAOE)
    {
        switch (weaponAOE)
        {
            case AttackAOE.TwoForward:
                {
                    if (CurrentLevel.CurrentMapState[Position.Y, Position.X + 1] == TileENUM.Wall) break;
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
                    foreach (RangedEnemy rangedEnemy in CurrentLevel.InteractablesLists.RangedEnemiesPresent)
                    {
                        if (rangedEnemy.Position.Y == Position.Y && (rangedEnemy.Position.X == Position.X + 1 || rangedEnemy.Position.X == Position.X + 2))
                        {
                            if (!rangedEnemy.IsDead())
                            {
                                rangedEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Ranged Enemy for 2 damage");
                                if (rangedEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedMiniBoss rangedMiniBoss in CurrentLevel.InteractablesLists.RangedMiniBossPresent)
                    {
                        if (rangedMiniBoss.Position.Y == Position.Y && (rangedMiniBoss.Position.X == Position.X + 1 || rangedMiniBoss.Position.X == Position.X + 2))
                        {
                            if (!rangedMiniBoss.IsDead())
                            {
                                rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Mini-Boss for 2 damage");
                                if (rangedMiniBoss.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Mini-Boss defeated");
                                }
                            }
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y), out HeavyEnemy heavy))
                    {
                        heavy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 2, Position.Y), out HeavyEnemy heavy1))
                    {
                        heavy1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 2, Position.Y), out TiltanBoss tiltanBoss1))
                    {
                        tiltanBoss1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
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
                    foreach (RangedEnemy rangedEnemy in CurrentLevel.InteractablesLists.RangedEnemiesPresent)
                    {
                        if (rangedEnemy.Position.X == Position.X + 1 && (rangedEnemy.Position.Y == Position.Y || rangedEnemy.Position.Y == Position.Y - 1 || rangedEnemy.Position.Y == Position.Y + 1))
                        {
                            if (!rangedEnemy.IsDead())
                            {
                                rangedEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Ranged Enemy for 3 damage");
                                if (rangedEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedMiniBoss rangedMiniBoss in CurrentLevel.InteractablesLists.RangedMiniBossPresent)
                    {
                        if (rangedMiniBoss.Position.X == Position.X + 1 && (rangedMiniBoss.Position.Y == Position.Y || rangedMiniBoss.Position.Y == Position.Y - 1 || rangedMiniBoss.Position.Y == Position.Y + 1))
                        {
                            if (!rangedMiniBoss.IsDead())
                            {
                                rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Mini-Boss for 3 damage");
                                if (rangedMiniBoss.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Mini-Boss defeated");
                                }
                            }
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y), out HeavyEnemy heavy))
                    {
                        heavy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y - 1), out HeavyEnemy heavy1))
                    {
                        heavy1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y + 1), out HeavyEnemy heavy2))
                    {
                        heavy2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y - 1), out TiltanBoss tiltanBoss1))
                    {
                        tiltanBoss1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y + 1), out TiltanBoss tiltanBoss2))
                    {
                        tiltanBoss2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                }
                break;
            case AttackAOE.Ranged:
                for (int i = Position.X + 1; i < 24; i++)
                {
                    if (CurrentLevel.CurrentMapState[Position.Y, i] == TileENUM.Empty) continue;
                    else if (CurrentLevel.CurrentMapState[Position.Y, i] == TileENUM.Wall) break;
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(i, Position.Y), out StandardEnemy standardEnemy))
                    {
                        standardEnemy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Standard Enemy for {CurrentWeapon.Power} damage");
                        if (standardEnemy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Standard Enemy defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(i, Position.Y), out RangedEnemy rangedEnemy))
                    {
                        rangedEnemy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Ranged Enemy for {CurrentWeapon.Power} damage");
                        if (rangedEnemy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(i, Position.Y), out RangedMiniBoss rangedMiniBoss))
                    {
                        rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Mini-Boss for {CurrentWeapon.Power} damage");
                        if (rangedMiniBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Mini-Boss defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(i, Position.Y), out HeavyEnemy heavy))
                    {
                        heavy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(i, Position.Y), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                        break;
                    }
                    else { break; }
                }
                break;
            case AttackAOE.PlusShape:
                {
                    foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)
                    {
                        if ((standardEnemy.Position.X == Position.X + 1 && (standardEnemy.Position.Y == Position.Y || standardEnemy.Position.Y == Position.Y - 1 || standardEnemy.Position.Y == Position.Y + 1)) || (standardEnemy.Position.X == Position.X + 2 && standardEnemy.Position.Y == Position.Y))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent($"Player has hit Standard Enemy for {CurrentWeapon.Power} damage");
                                if (standardEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Standard Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedEnemy rangedEnemy in CurrentLevel.InteractablesLists.RangedEnemiesPresent)
                    {
                        if ((rangedEnemy.Position.X == Position.X + 1 && (rangedEnemy.Position.Y == Position.Y || rangedEnemy.Position.Y == Position.Y - 1 || rangedEnemy.Position.Y == Position.Y + 1)) || (rangedEnemy.Position.X == Position.X + 2 && rangedEnemy.Position.Y == Position.Y))
                        {
                            if (!rangedEnemy.IsDead())
                            {
                                rangedEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent($"Player has hit Ranged Enemy for {CurrentWeapon.Power} damage");
                                if (rangedEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedMiniBoss rangedMiniBoss in CurrentLevel.InteractablesLists.RangedMiniBossPresent)
                    {
                        if ((rangedMiniBoss.Position.X == Position.X + 1 && (rangedMiniBoss.Position.Y == Position.Y || rangedMiniBoss.Position.Y == Position.Y - 1 || rangedMiniBoss.Position.Y == Position.Y + 1)) || (rangedMiniBoss.Position.X == Position.X + 2 && rangedMiniBoss.Position.Y == Position.Y))
                        {
                            if (!rangedMiniBoss.IsDead())
                            {
                                rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent($"Player has hit Mini-Boss for {CurrentWeapon.Power} damage");
                                if (rangedMiniBoss.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Mini-Boss defeated");
                                }
                            }
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y), out HeavyEnemy heavy))
                    {
                        heavy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y - 1), out HeavyEnemy heavy1))
                    {
                        heavy1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y + 1), out HeavyEnemy heavy2))
                    {
                        heavy2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 2, Position.Y), out HeavyEnemy heavy3))
                    {
                        heavy3.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy3.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y - 1), out TiltanBoss tiltanBoss1))
                    {
                        tiltanBoss1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y + 1), out TiltanBoss tiltanBoss2))
                    {
                        tiltanBoss2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 2, Position.Y), out TiltanBoss tiltanBoss3))
                    {
                        tiltanBoss3.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss3.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    break;
                }
        }
    }
    public void DetermineTargetsAndAttackUp(AttackAOE weaponAOE)
    {
        switch (weaponAOE)
        {
            case AttackAOE.TwoForward:
                {
                    if (CurrentLevel.CurrentMapState[Position.Y - 1, Position.X] == TileENUM.Wall) break;
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
                    foreach (RangedEnemy rangedEnemy in CurrentLevel.InteractablesLists.RangedEnemiesPresent)
                    {
                        if (rangedEnemy.Position.X == Position.X && (rangedEnemy.Position.Y == Position.Y - 1 || rangedEnemy.Position.Y == Position.Y - 2))
                        {
                            if (!rangedEnemy.IsDead())
                            {
                                rangedEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Ranged Enemy for 2 damage");
                                if (rangedEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedMiniBoss rangedMiniBoss in CurrentLevel.InteractablesLists.RangedMiniBossPresent)
                    {
                        if (rangedMiniBoss.Position.X == Position.X && (rangedMiniBoss.Position.Y == Position.Y - 1 || rangedMiniBoss.Position.Y == Position.Y - 2))
                        {
                            if (!rangedMiniBoss.IsDead())
                            {
                                rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Mini-Boss for 2 damage");
                                if (rangedMiniBoss.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Mini-Boss defeated");
                                }
                            }
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y - 1), out HeavyEnemy heavy))
                    {
                        heavy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y - 2), out HeavyEnemy heavy1))
                    {
                        heavy1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y - 1), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y - 2), out TiltanBoss tiltanBoss1))
                    {
                        tiltanBoss1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
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
                    foreach (RangedEnemy rangedEnemy in CurrentLevel.InteractablesLists.RangedEnemiesPresent)
                    {
                        if (rangedEnemy.Position.Y == Position.Y - 1 && (rangedEnemy.Position.X == Position.X || rangedEnemy.Position.X == Position.X - 1 || rangedEnemy.Position.X == Position.X + 1))
                        {
                            if (!rangedEnemy.IsDead())
                            {
                                rangedEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Ranged Enemy for 3 damage");
                                if (rangedEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedMiniBoss rangedMiniBoss in CurrentLevel.InteractablesLists.RangedMiniBossPresent)
                    {
                        if (rangedMiniBoss.Position.Y == Position.Y - 1 && (rangedMiniBoss.Position.X == Position.X || rangedMiniBoss.Position.X == Position.X - 1 || rangedMiniBoss.Position.X == Position.X + 1))
                        {
                            if (!rangedMiniBoss.IsDead())
                            {
                                rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Mini-Boss for 3 damage");
                                if (rangedMiniBoss.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Mini-Boss defeated");
                                }
                            }
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y - 1), out HeavyEnemy heavy))
                    {
                        heavy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y - 1), out HeavyEnemy heavy1))
                    {
                        heavy1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y - 1), out HeavyEnemy heavy2))
                    {
                        heavy2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y - 1), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y - 1), out TiltanBoss tiltanBoss1))
                    {
                        tiltanBoss1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y - 1), out TiltanBoss tiltanBoss2))
                    {
                        tiltanBoss2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                }
                break;
            case AttackAOE.Ranged:
                for (int i = Position.Y - 1; i > 0; i--)
                {
                    if (CurrentLevel.CurrentMapState[i, Position.X] == TileENUM.Empty) continue;
                    else if (CurrentLevel.CurrentMapState[i, Position.X] == TileENUM.Wall) break;
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, i), out StandardEnemy standardEnemy))
                    {
                        standardEnemy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Standard Enemy for {CurrentWeapon.Power} damage");
                        if (standardEnemy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Standard Enemy defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, i), out RangedEnemy rangedEnemy))
                    {
                        rangedEnemy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Ranged Enemy for {CurrentWeapon.Power} damage");
                        if (rangedEnemy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, i), out RangedMiniBoss rangedMiniBoss))
                    {
                        rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Mini-Boss for {CurrentWeapon.Power} damage");
                        if (rangedMiniBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Mini-Boss defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, i), out HeavyEnemy heavy))
                    {
                        heavy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, i), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                        break;
                    }
                    else { break; }
                }
                break;
            case AttackAOE.PlusShape:
                {
                    foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)
                    {
                        if ((standardEnemy.Position.Y == Position.Y - 1 && (standardEnemy.Position.X == Position.X || standardEnemy.Position.X == Position.X - 1 || standardEnemy.Position.X == Position.X + 1)) || (standardEnemy.Position.Y == Position.Y - 2 && standardEnemy.Position.X == Position.X))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent($"Player has hit Standard Enemy for {CurrentWeapon.Power} damage");
                                if (standardEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Standard Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedEnemy rangedEnemy in CurrentLevel.InteractablesLists.RangedEnemiesPresent)
                    {
                        if ((rangedEnemy.Position.Y == Position.Y - 1 && (rangedEnemy.Position.X == Position.X || rangedEnemy.Position.X == Position.X - 1 || rangedEnemy.Position.X == Position.X + 1)) || (rangedEnemy.Position.Y == Position.Y - 2 && rangedEnemy.Position.X == Position.X))
                        {
                            if (!rangedEnemy.IsDead())
                            {
                                rangedEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent($"Player has hit Ranged Enemy for {CurrentWeapon.Power} damage");
                                if (rangedEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedMiniBoss rangedMiniBoss in CurrentLevel.InteractablesLists.RangedMiniBossPresent)
                    {
                        if ((rangedMiniBoss.Position.Y == Position.Y - 1 && (rangedMiniBoss.Position.X == Position.X || rangedMiniBoss.Position.X == Position.X - 1 || rangedMiniBoss.Position.X == Position.X + 1)) || (rangedMiniBoss.Position.Y == Position.Y - 2 && rangedMiniBoss.Position.X == Position.X))
                        {
                            if (!rangedMiniBoss.IsDead())
                            {
                                rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent($"Player has hit Mini-Boss for {CurrentWeapon.Power} damage");
                                if (rangedMiniBoss.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Mini-Boss defeated");
                                }
                            }
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y - 1), out HeavyEnemy heavy))
                    {
                        heavy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y - 1), out HeavyEnemy heavy1))
                    {
                        heavy1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y - 1), out HeavyEnemy heavy2))
                    {
                        heavy2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y - 2), out HeavyEnemy heavy3))
                    {
                        heavy3.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy3.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y - 1), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y - 1), out TiltanBoss tiltanBoss1))
                    {
                        tiltanBoss1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y - 1), out TiltanBoss tiltanBoss2))
                    {
                        tiltanBoss2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y - 2), out TiltanBoss tiltanBoss3))
                    {
                        tiltanBoss3.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss3.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    break;
                }
        }
    }
    public void DetermineTargetsAndAttackDown(AttackAOE weaponAOE)
    {
        switch (weaponAOE)
        {
            case AttackAOE.TwoForward:
                {
                    if (CurrentLevel.CurrentMapState[Position.Y + 1, Position.X] == TileENUM.Wall) break;
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
                    foreach (RangedEnemy rangedEnemy in CurrentLevel.InteractablesLists.RangedEnemiesPresent)
                    {
                        if (rangedEnemy.Position.X == Position.X && (rangedEnemy.Position.Y == Position.Y + 1 || rangedEnemy.Position.Y == Position.Y + 2))
                        {
                            if (!rangedEnemy.IsDead())
                            {
                                rangedEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Ranged Enemy for 2 damage");
                                if (rangedEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedMiniBoss rangedMiniBoss in CurrentLevel.InteractablesLists.RangedMiniBossPresent)
                    {
                        if (rangedMiniBoss.Position.X == Position.X && (rangedMiniBoss.Position.Y == Position.Y + 1 || rangedMiniBoss.Position.Y == Position.Y + 2))
                        {
                            if (!rangedMiniBoss.IsDead())
                            {
                                rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Mini-Boss for 2 damage");
                                if (rangedMiniBoss.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Mini-Boss defeated");
                                }
                            }
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y + 1), out HeavyEnemy heavy))
                    {
                        heavy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y + 2), out HeavyEnemy heavy1))
                    {
                        heavy1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y + 1), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y + 2), out TiltanBoss tiltanBoss1))
                    {
                        tiltanBoss1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
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
                    foreach (RangedEnemy rangedEnemy in CurrentLevel.InteractablesLists.RangedEnemiesPresent)
                    {
                        if (rangedEnemy.Position.Y == Position.Y + 1 && (rangedEnemy.Position.X == Position.X || rangedEnemy.Position.X == Position.X - 1 || rangedEnemy.Position.X == Position.X + 1))
                        {
                            if (!rangedEnemy.IsDead())
                            {
                                rangedEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Ranged Enemy for 3 damage");
                                if (rangedEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedMiniBoss rangedMiniBoss in CurrentLevel.InteractablesLists.RangedMiniBossPresent)
                    {
                        if (rangedMiniBoss.Position.Y == Position.Y + 1 && (rangedMiniBoss.Position.X == Position.X || rangedMiniBoss.Position.X == Position.X - 1 || rangedMiniBoss.Position.X == Position.X + 1))
                        {
                            if (!rangedMiniBoss.IsDead())
                            {
                                rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent("Player has hit Mini-Boss for 3 damage");
                                if (rangedMiniBoss.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Mini-Boss defeated");
                                }
                            }
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y + 1), out HeavyEnemy heavy))
                    {
                        heavy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y + 1), out HeavyEnemy heavy1))
                    {
                        heavy1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y + 1), out HeavyEnemy heavy2))
                    {
                        heavy2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y + 1), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y + 1), out TiltanBoss tiltanBoss1))
                    {
                        tiltanBoss1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y + 1), out TiltanBoss tiltanBoss2))
                    {
                        tiltanBoss2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                }
                break;
            case AttackAOE.Ranged:
                for (int i = Position.Y + 1; i < 13; i++)
                {
                    if (CurrentLevel.CurrentMapState[i, Position.X] == TileENUM.Empty) continue;
                    else if (CurrentLevel.CurrentMapState[i, Position.X] == TileENUM.Wall) break;
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, i), out StandardEnemy standardEnemy))
                    {
                        standardEnemy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Standard Enemy for {CurrentWeapon.Power} damage");
                        if (standardEnemy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Standard Enemy defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, i), out RangedEnemy rangedEnemy))
                    {
                        rangedEnemy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Ranged Enemy for {CurrentWeapon.Power} damage");
                        if (rangedEnemy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, i), out RangedMiniBoss rangedMiniBoss))
                    {
                        rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Mini-Boss for {CurrentWeapon.Power} damage");
                        if (rangedMiniBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Mini-Boss defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, i), out HeavyEnemy heavy))
                    {
                        heavy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                        break;
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, i), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                        break;
                    }
                    else { break; }
                }
                break;
            case AttackAOE.PlusShape:
                {
                    foreach (StandardEnemy standardEnemy in CurrentLevel.InteractablesLists.StandardEnemiesPresent)
                    {
                        if ((standardEnemy.Position.Y == Position.Y + 1 && (standardEnemy.Position.X == Position.X || standardEnemy.Position.X == Position.X - 1 || standardEnemy.Position.X == Position.X + 1)) || (standardEnemy.Position.Y == Position.Y + 2 && standardEnemy.Position.X == Position.X))
                        {
                            if (!standardEnemy.IsDead())
                            {
                                standardEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent($"Player has hit Standard Enemy for {CurrentWeapon.Power} damage");
                                if (standardEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Standard Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedEnemy rangedEnemy in CurrentLevel.InteractablesLists.RangedEnemiesPresent)
                    {
                        if ((rangedEnemy.Position.Y == Position.Y + 1 && (rangedEnemy.Position.X == Position.X || rangedEnemy.Position.X == Position.X - 1 || rangedEnemy.Position.X == Position.X + 1)) || (rangedEnemy.Position.Y == Position.Y + 2 && rangedEnemy.Position.X == Position.X))
                        {
                            if (!rangedEnemy.IsDead())
                            {
                                rangedEnemy.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent($"Player has hit Ranged Enemy for {CurrentWeapon.Power} damage");
                                if (rangedEnemy.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Ranged Enemy defeated");
                                }
                            }
                        }
                    }
                    foreach (RangedMiniBoss rangedMiniBoss in CurrentLevel.InteractablesLists.RangedMiniBossPresent)
                    {
                        if ((rangedMiniBoss.Position.Y == Position.Y + 1 && (rangedMiniBoss.Position.X == Position.X || rangedMiniBoss.Position.X == Position.X - 1 || rangedMiniBoss.Position.X == Position.X + 1)) || (rangedMiniBoss.Position.Y == Position.Y + 2 && rangedMiniBoss.Position.X == Position.X))
                        {
                            if (!rangedMiniBoss.IsDead())
                            {
                                rangedMiniBoss.CurHP -= CurrentWeapon.Power;
                                GameManager.GameLog.LogEvent($"Player has hit Mini-Boss for {CurrentWeapon.Power} damage");
                                if (rangedMiniBoss.IsDead())
                                {
                                    GameManager.GameLog.LogEvent("Mini-Boss defeated");
                                }
                            }
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y + 1), out HeavyEnemy heavy))
                    {
                        heavy.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y + 1), out HeavyEnemy heavy1))
                    {
                        heavy1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y + 1), out HeavyEnemy heavy2))
                    {
                        heavy2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y + 2), out HeavyEnemy heavy3))
                    {
                        heavy3.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Heavy for {CurrentWeapon.Power} damage");
                        if (heavy3.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Heavy was defeated");
                        }
                    }
                    if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y + 1), out TiltanBoss tiltanBoss))
                    {
                        tiltanBoss.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X + 1, Position.Y + 1), out TiltanBoss tiltanBoss1))
                    {
                        tiltanBoss1.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss1.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X - 1, Position.Y + 1), out TiltanBoss tiltanBoss2))
                    {
                        tiltanBoss2.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss2.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    else if (CurrentLevel.InteractablesLists.FindEnemy(new Point(Position.X, Position.Y + 2), out TiltanBoss tiltanBoss3))
                    {
                        tiltanBoss3.CurHP -= CurrentWeapon.Power;
                        GameManager.GameLog.LogEvent($"Player has hit Tiltan for {CurrentWeapon.Power} damage");
                        if (tiltanBoss3.IsDead())
                        {
                            GameManager.GameLog.LogEvent("Tiltan was defeated");
                        }
                    }
                    break;
                }
        }
    }
}