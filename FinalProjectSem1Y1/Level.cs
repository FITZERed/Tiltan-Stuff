using System.Security.Cryptography.X509Certificates;

public class Level
{
    public TileENUM[,] map = new TileENUM[12, 21];
    public Point PlayerStartingPosition;
    public TileENUM[,] CurrentMapState;
    public Player Player;
    public InteractablesLists InteractablesLists = new();
    public Exit Exit;
    public Entrance Entrance;
    public RangedEnemyState RangedEnemiesStateTracker;

    public Level(int levelNum)
    {
        Player = GameManager.Player;
        map = BuildInitialMapState(MapBuilder.ReadTextFile("Level " + levelNum + ".txt"));
        CurrentMapState = map;
        if (InteractablesLists.RangedEnemiesPresent.Count > 0)
        {
            RangedEnemiesStateTracker = RangedEnemyState.PrepingShot;
        }
        PrintCurrentMapState();
        RefreshMap();
        PrintCurrentMapState();
    }

    public TileENUM[,] BuildInitialMapState(char[,] charMatrix)
    {
        TileENUM[,] map = new TileENUM[charMatrix.GetLength(0), charMatrix.GetLength(1)];
        for (int i = 0; i < charMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < charMatrix.GetLength(1); j++)
            {
                switch (charMatrix[i, j])
                {
                    case ' ':
                        map[i, j] = TileENUM.Empty; break;
                    case 'P':
                        map[i, j] = TileENUM.Player;
                        PlayerStartingPosition = new Point(j, i);
                        Player.Position = new Point(PlayerStartingPosition);
                        Entrance = new Entrance(new Point(PlayerStartingPosition));
                            break;
                    case 'E':
                        map[i, j] = TileENUM.StandardEnemy;
                        InteractablesLists.StandardEnemiesPresent.Add(new StandardEnemy(new Point(j, i)));
                            break;
                    case '█':
                        map[i, j] = TileENUM.Wall; break;
                    case 'X':
                        map[i, j] = TileENUM.Exit;
                        Exit = new Exit(new Point(j, i));
                        break;
                    case 'H':
                        map[i, j] = TileENUM.Chest;
                        InteractablesLists.ChestsPresent.Add(new Chest(new Point(j, i), ChestContent.HealingPotion));
                        break;
                    case 'A':
                        map[i, j] = TileENUM.Chest;
                        InteractablesLists.ChestsPresent.Add(new Chest(new Point(j, i), ChestContent.Axe));
                        break;
                    case 'b':
                        map[i, j] = TileENUM.Chest;
                        InteractablesLists.ChestsPresent.Add(new Chest(new Point(j, i), ChestContent.ShortBow));
                        break;
                    case 'i':
                        map[i, j] = TileENUM.RangedEnemyUp;
                        InteractablesLists.RangedEnemiesPresent.Add(new RangedEnemy(new Point(j, i), FaceDirection.Up));
                        break;
                    case 'k':
                        map[i, j] = TileENUM.RangedEnemyDown;
                        InteractablesLists.RangedEnemiesPresent.Add(new RangedEnemy(new Point(j, i), FaceDirection.Down));
                        break;
                    case 'j':
                        map[i, j] = TileENUM.RangedEnemyLeft;
                        InteractablesLists.RangedEnemiesPresent.Add(new RangedEnemy(new Point(j, i), FaceDirection.Left));
                        break;
                    case 'l':
                        map[i, j] = TileENUM.RangedEnemyRight;
                        InteractablesLists.RangedEnemiesPresent.Add(new RangedEnemy(new Point(j, i), FaceDirection.Right));
                        break;
                    case 'R':
                        map[i, j] = TileENUM.RangedMiniBoss;
                        InteractablesLists.RangedMiniBossPresent.Add(new RangedMiniBoss(new Point(j, i)));
                        break;
                    case 'T':
                        map[i, j] = TileENUM.TiltanBoss;
                        InteractablesLists.BossList.Add(new TiltanBoss(new Point(j, i)));
                        break;
                    default: throw new ArgumentOutOfRangeException(charMatrix[i, j].ToString());


                }
            }
        }

        return map;
    }
    public static char ConvertTileENUMtoChar(TileENUM tile)
    {
        switch (tile)
        {
            case TileENUM.Empty:
                Console.ForegroundColor = ConsoleColor.Black;
                return ' ';
            case TileENUM.Wall:
                Console.ForegroundColor = ConsoleColor.White;
                return '█';
            case TileENUM.Player:
                Console.ForegroundColor = ConsoleColor.Red;
                return '♦';
            case TileENUM.StandardEnemy:
                Console.ForegroundColor = ConsoleColor.Green;
                return '♠';
            case TileENUM.Exit:
                Console.ForegroundColor = ConsoleColor.Blue;
                return 'X';
            case TileENUM.Entrance:
                Console.ForegroundColor = ConsoleColor.White;
                return 'E';
            case TileENUM.Chest:
                Console.ForegroundColor = ConsoleColor.Yellow;
                return '◘';
            case TileENUM.RangedEnemyUp:
                Console.ForegroundColor = ConsoleColor.Green;
                return '↑';
            case TileENUM.RangedEnemyDown:
                Console.ForegroundColor = ConsoleColor.Green;
                return '↓';
            case TileENUM.RangedEnemyLeft:
                Console.ForegroundColor = ConsoleColor.Green;
                return '←';
            case TileENUM.RangedEnemyRight:
                Console.ForegroundColor = ConsoleColor.Green;
                return '→';
            case TileENUM.RangedMiniBoss:
                Console.ForegroundColor = ConsoleColor.Green;
                return '╬';
            case TileENUM.TiltanBoss:
                Console.ForegroundColor = ConsoleColor.Green;
                return '♣';
            case TileENUM.TiltanBossSide:
                Console.ForegroundColor = ConsoleColor.Green;
                return '♣';
            default:
                throw new ArgumentOutOfRangeException(nameof(tile));
        }
    }
    //♦
    //♠
    //♣
    //◘
    //↑
    //↓
    //←
    //→
    //╬

    public void PrintCurrentMapState()
    {
        for (int i = 0; i < CurrentMapState.GetLength(0); i++)
        {
            Console.SetCursorPosition(0, i);
            for (int j = 0; j < CurrentMapState.GetLength(1); j++)
            {
                char tileToPrint = ConvertTileENUMtoChar(CurrentMapState[i, j]);
                Console.Write(tileToPrint);
            }
        }
    }
    public void RefreshMap()
    {
        
        for (int i = 0; i < CurrentMapState.GetLength(0); ++i)
        {
            for (int j = 0; j < CurrentMapState.GetLength(1); ++j)
            {
                if (CurrentMapState[i, j] == TileENUM.Wall)
                    CurrentMapState[i, j] = TileENUM.Wall;
                else if (CurrentMapState[i, j] == TileENUM.Exit)
                    CurrentMapState[i, j] = TileENUM.Exit;
                else if (i == PlayerStartingPosition.Y && j == PlayerStartingPosition.X)
                    CurrentMapState[i, j] = TileENUM.Entrance;
                else if (CurrentMapState[i, j] == TileENUM.Chest)
                    CurrentMapState[i, j] |= TileENUM.Chest;
                else CurrentMapState[i, j] = TileENUM.Empty;

            }
        }
        foreach (StandardEnemy standardEnemy in InteractablesLists.StandardEnemiesPresent)
        {
            if (standardEnemy.IsDead()) continue;
            CurrentMapState[standardEnemy.Position.Y, standardEnemy.Position.X] = TileENUM.StandardEnemy;
        }
        foreach (RangedEnemy rangedEnemy in InteractablesLists.RangedEnemiesPresent)
        {
            if (rangedEnemy.IsDead()) continue;
            if (rangedEnemy.Direction == FaceDirection.Up)
            {
                CurrentMapState[rangedEnemy.Position.Y, rangedEnemy.Position.X] = TileENUM.RangedEnemyUp;
            }
            else if (rangedEnemy.Direction == FaceDirection.Down)
            {
                CurrentMapState[rangedEnemy.Position.Y, rangedEnemy.Position.X] = TileENUM.RangedEnemyDown;
            }
            else if (rangedEnemy.Direction == FaceDirection.Left)
            {
                CurrentMapState[rangedEnemy.Position.Y, rangedEnemy.Position.X] = TileENUM.RangedEnemyLeft;
            }
            else if (rangedEnemy.Direction == FaceDirection.Right)
            {
                CurrentMapState[rangedEnemy.Position.Y, rangedEnemy.Position.X] = TileENUM.RangedEnemyRight;
            }
        }
        foreach (RangedMiniBoss rangedMiniBoss in InteractablesLists.RangedMiniBossPresent)
        {
            if (rangedMiniBoss.IsDead()) continue;
            CurrentMapState[rangedMiniBoss.Position.Y, rangedMiniBoss.Position.X] = TileENUM.RangedMiniBoss;
        }
        foreach (TiltanBoss tiltanBoss in InteractablesLists.BossList)
        {
            if (tiltanBoss.IsDead()) continue;
            CurrentMapState[tiltanBoss.Position.Y, tiltanBoss.Position.X] = TileENUM.TiltanBoss;
            CurrentMapState[tiltanBoss.Position.Y + 1, tiltanBoss.Position.X + 1] = TileENUM.TiltanBossSide;
            CurrentMapState[tiltanBoss.Position.Y + 1, tiltanBoss.Position.X]     = TileENUM.TiltanBossSide;
            CurrentMapState[tiltanBoss.Position.Y + 1, tiltanBoss.Position.X - 1] = TileENUM.TiltanBossSide;
            CurrentMapState[tiltanBoss.Position.Y, tiltanBoss.Position.X + 1]     = TileENUM.TiltanBossSide;
            CurrentMapState[tiltanBoss.Position.Y, tiltanBoss.Position.X - 1]     = TileENUM.TiltanBossSide;
            CurrentMapState[tiltanBoss.Position.Y - 1, tiltanBoss.Position.X + 1] = TileENUM.TiltanBossSide;
            CurrentMapState[tiltanBoss.Position.Y - 1, tiltanBoss.Position.X]     = TileENUM.TiltanBossSide;
            CurrentMapState[tiltanBoss.Position.Y - 1, tiltanBoss.Position.X - 1] = TileENUM.TiltanBossSide;
        }
        CurrentMapState[Player.Position.Y, Player.Position.X] = TileENUM.Player;
        //need to get the player position and things, maybe this shoud be in GameManager
    }

    public void CycleRangedEnemiesStateTracker()
    {
        if (RangedEnemiesStateTracker == RangedEnemyState.PrepingShot)
        {
            RangedEnemiesStateTracker = RangedEnemyState.DrawingShot;
            GameManager.GameLog.LogEvent("Ranged Enemies drawing arrows");
        }
        else if (RangedEnemiesStateTracker == RangedEnemyState.DrawingShot)
        {
            RangedEnemiesStateTracker = RangedEnemyState.JustShot;
            GameManager.GameLog.LogEvent("Ranged Enemies shooting");
        }
        else if (RangedEnemiesStateTracker == RangedEnemyState.JustShot)
        {
            RangedEnemiesStateTracker = RangedEnemyState.PrepingShot;
            GameManager.GameLog.LogEvent("Ranged Enemies preparing new arrows");
        }
    }

}
public enum TileENUM
{
    Empty,
    Wall,
    Player,
    StandardEnemy,
    RangedEnemyUp,
    RangedEnemyDown,
    RangedEnemyLeft,
    RangedEnemyRight,
    RangedMiniBoss,
    Exit,
    Entrance,
    Chest,
    TiltanBoss,
    TiltanBossSide
}